using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Text;
using System;

public class NetworkManager : Singleton<NetworkManager>
{
    const string BASE_URL = "https://api.projects.arq.studio/cardio/";
    const string AUTH = "signin";
    const string STAT = "statistic";

    private TokenStorage _tokenStorage;
    public TokenStorage TokenStorage
    {
        get
        {
            if (_tokenStorage == null)
            {
                _tokenStorage = new TokenStorage();
                _tokenStorage.refreshToken = "";
                _tokenStorage.token = "";
            }
            return _tokenStorage;
        }

        set
        {
            _tokenStorage = value;
        }

    }

    #region MonoBehaviour

    #endregion


    #region Methods

    public void Login(UserData userData, Action<string, long> onLoginFailed = null, Action onLoginSecces = null)
    {
        var request = JsonConvert.SerializeObject(userData);
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };
        StartCoroutine(Request(BASE_URL + AUTH, "POST", request, headers,
            (webRequest) =>
            {

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onLoginFailed?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    LoginResponse resonse = JsonConvert.DeserializeObject<LoginResponse>(webRequest.downloadHandler.text);
                    TokenStorage.token = resonse.token;
                    TokenStorage.refreshToken = resonse.refreshToken;
                    onLoginSecces?.Invoke();
                }
            }));
    }

    public void Authentication(Action onTokenValid, Action<string, long> onTokenInvalid)
    {
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Authorization", $"Bearer {TokenStorage.token}" } };
        StartCoroutine(Request(BASE_URL + AUTH, "GET", "", headers,
            (webRequest) =>
            {

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onTokenInvalid?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    onTokenValid?.Invoke();
                }


            }));
    }

    public void UpdateToken(Action<string, long> onRefreshFailed = null, Action onRefreshSecces = null)
    {
        var request = "{\"refreshToken\": \"" + $"{TokenStorage.refreshToken}" + "\"}";
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };
        StartCoroutine(Request(BASE_URL + AUTH, "PUT", request, headers,
            (webRequest) =>
            {
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onRefreshFailed?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    LoginResponse resonse = JsonConvert.DeserializeObject<LoginResponse>(webRequest.downloadHandler.text);
                    TokenStorage.token = resonse.token;
                    TokenStorage.refreshToken = resonse.refreshToken;
                    onRefreshSecces?.Invoke();
                }
            }));
    }

    public void UpdateToken(string refreshToken, Action<string, long> onRefreshFailed = null, Action<TokenStorage> onRefreshSecces = null)
    {
        var request = "{\"refreshToken\": \"" + $"{refreshToken}" + "\"}";
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };
        StartCoroutine(Request(BASE_URL + AUTH, "PUT", request, headers,
            (webRequest) =>
            {
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onRefreshFailed?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    LoginResponse resonse = JsonConvert.DeserializeObject<LoginResponse>(webRequest.downloadHandler.text);

                    var tokenStorage = new TokenStorage
                    {
                        refreshToken = resonse.refreshToken,
                        token = resonse.token
                    };

                    onRefreshSecces?.Invoke(tokenStorage);
                }
            }));
    }

    public void Authorization(Action<string, long> onFailed, Action onSecces)
    {
        Authentication(onSecces,
            (e, n) =>
            {
                UpdateToken(onFailed, onSecces);

            });
    }

    public void SendReport(
        string report,
        string refreshToken,
        Action<string, long> onTokenUpdateFailed,
        Action<string, long> onSendingFailed,
        Action onSecces,
        Action onCompleat)
    {

        UpdateToken(refreshToken,
            (e, n) =>
            {
                onTokenUpdateFailed?.Invoke(e, n);
            },

            (storage) =>
            {
                SendReportInternal(report, storage, onSendingFailed, onSecces, onCompleat);
            });

    }

    private void SendReportInternal(
        string report,
        TokenStorage tokenStorage,
        Action<string, long> onFailed,
        Action onSecces,
        Action onCompleat)
    {
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Authorization", $"Bearer {tokenStorage.token}" }, { "Content-Type", "application/json" } };
        StartCoroutine(Request(BASE_URL + STAT, "POST", report, headers,
            (webRequest) =>
            {
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onFailed?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    onSecces?.Invoke();
                }
                onCompleat?.Invoke();
            }));
    }

    #endregion    

    private IEnumerator Request(
        string uri,
        string method,
        string request,
        Dictionary<string, string> headers,
        Action<UnityWebRequest> resultHandler = null)
    {
        var webRequest = new UnityWebRequest(uri, method);
        if (!string.IsNullOrWhiteSpace(request))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(request);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        }

        webRequest.downloadHandler = new DownloadHandlerBuffer();

        foreach (var item in headers)
        {
            webRequest.SetRequestHeader(item.Key, item.Value);
        }

        yield return webRequest.SendWebRequest();

        resultHandler?.Invoke(webRequest);
    }
}
