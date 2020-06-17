using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class App : MonoBehaviour

{
    #region SerializeFields

    [SerializeField]
    private LoginViewController _login;

    [SerializeField]
    private ChoiceViewController _choice;

    [SerializeField]
    private double _acceptableBackgroundTime = 30;

    [SerializeField]
    private TimeCounter _counter;

    [SerializeField]
    private VisualizationInstantiator _visualizationInstantiator;

    [SerializeField]
    private UsageTrackingManager _usageTrackingManager;

    #endregion

    private const string TOKENS_STORAGE_FILENAME = "tokens-storage";

    #region MonoBehaviour

    private void OnEnable()
    {
        _choice.LogOut += Logout;
        _login.Login += Login;

    }

    private void OnDisable()
    {
        _choice.LogOut -= Logout;
        _login.Login -= Login;
    }

    private void Start()
    {

        _usageTrackingManager.AcceptableBackgroundTime = _acceptableBackgroundTime;

        var tokensPath = Path.Combine(Application.persistentDataPath, TOKENS_STORAGE_FILENAME);
        TokenStorage tokensStorage = BsonDataManager.ReadData<TokenStorage>(tokensPath);

        if (tokensStorage == null)
        {
            _login.Open();
            return;
        }

        NetworkManager.Instance.TokenStorage = tokensStorage;
        NetworkManager.Instance.Authorization(
            (e, r) =>
            {
                print($"Error: {e}, Code: {r}");
                _login.Open();
            },
            () =>
            {
                _choice.Open();
            });
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (_counter.SinceLastBackgroundTime > _acceptableBackgroundTime)
            {
                _visualizationInstantiator.DeleteInstantiatedPrefab();
                _choice.Open();
            }
        }
    }

    private void OnApplicationQuit()
    {
   
    }

    private void Logout()
    {
        var tokensPath = Path.Combine(Application.persistentDataPath, TOKENS_STORAGE_FILENAME);
        NetworkManager.Instance.TokenStorage = null;
        BsonDataManager.DeleteData(tokensPath);
    }

    private void Login(string username, string password)
    {

        UserData userData = new UserData(username, password);
        NetworkManager.Instance.Login(userData,
            (e, n) =>
            {
                _login.FailedLogin();
            },
            () =>
            {
                _login.SeccesfuleLogin();
                var tokenStorage = NetworkManager.Instance.TokenStorage;
                var path = Path.Combine(Application.persistentDataPath, TOKENS_STORAGE_FILENAME);
                BsonDataManager.WriteData(path, tokenStorage);
            });
    }


    #endregion
}
