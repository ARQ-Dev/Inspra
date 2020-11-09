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

    private UserDataStorage _userDataStorage;
    public UserDataStorage UserDataStorage
    {
        get
        {
            if (_userDataStorage == null)
            {
                _userDataStorage = new UserDataStorage();
                _userDataStorage.login = "";
                _userDataStorage.refreshToken = "";
                _userDataStorage.token = "";
            }
            return _userDataStorage;
        }

        set
        {
            _userDataStorage = value;
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
                    UserDataStorage.login = userData.login;
                    UserDataStorage.token = resonse.token;
                    UserDataStorage.refreshToken = resonse.refreshToken;
                    onLoginSecces?.Invoke();
                }
            }));
    }

    public void Authentication(Action onTokenValid, Action<string, long> onTokenInvalid)
    {
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Authorization", $"Bearer {UserDataStorage.token}" } };
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
        var request = "{\"refreshToken\": \"" + $"{UserDataStorage.refreshToken}" + "\"}";
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
                    UserDataStorage.token = resonse.token;
                    UserDataStorage.refreshToken = resonse.refreshToken;
                    onRefreshSecces?.Invoke();
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
    public void SendReport(string report, Action<string, long> onFailed, Action onSecces)
    {
        Authorization(onFailed,
            () =>
            {
                SendReportInternal(report, onFailed, onSecces);
            });
    }

    private void SendReportInternal(string report, Action<string, long> onFailed, Action onSecces)
    {
        Dictionary<string, string> headers = new Dictionary<string, string> { { "Authorization", $"Bearer {UserDataStorage.token}" }, { "Content-Type", "application/json" } };
        StartCoroutine(Request(BASE_URL + STAT, "POST", report, headers,
            (webRequest) =>
            {
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onFailed?.Invoke(webRequest.error, webRequest.responseCode);
                }
                else
                {
                    print(report);
                    onSecces?.Invoke();
                }
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
