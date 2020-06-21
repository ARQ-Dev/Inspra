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

    private void Awake()
    {
        _usageTrackingManager.AcceptableBackgroundTime = _acceptableBackgroundTime;

        var tokensPath = Path.Combine(Application.persistentDataPath, TOKENS_STORAGE_FILENAME);
        UserDataStorage tokensStorage = BsonDataManager.ReadData<UserDataStorage>(tokensPath);

        if (tokensStorage == null)
        {
            _login.Open();
            return;
        }

        NetworkManager.Instance.UserDataStorage = tokensStorage;
        NetworkManager.Instance.Authorization(
            (e, r) =>
            {
                _login.Open();
            },
            () =>
            {
                _choice.Open();
                UsageTrackingManager.Instance.GrabReports();
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
        NetworkManager.Instance.UserDataStorage = null;
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
                var tokenStorage = NetworkManager.Instance.UserDataStorage;
                var path = Path.Combine(Application.persistentDataPath, TOKENS_STORAGE_FILENAME);
                BsonDataManager.WriteData(path, tokenStorage);

                UsageTrackingManager.Instance.GrabReports();
            });
    }

    #endregion
}
