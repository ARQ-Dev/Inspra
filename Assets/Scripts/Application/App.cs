using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.XR.ARFoundation;
public class App : Singleton<App>
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

    [SerializeField]
    private ARSession _arSession;

    [SerializeField]
    private List<GameObject> _arRelatedGos;

    [SerializeField]
    private List<GameObject> _ordinaryGos;

    public bool IsARAvailable { get; private set; } = true;
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
        StartCoroutine(CheckAvailability());

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
                if (r == 0 || r >= 500)
                {
                    _choice.Open();
                    UsageTrackingManager.Instance.GrabReports();
                }
                else
                {
                    _login.Open();
                }

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
                print($"Error nober: {n}, message: {e}");
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

    private IEnumerator CheckAvailability()
    {
        if ((ARSession.state == ARSessionState.None) ||
          (ARSession.state == ARSessionState.CheckingAvailability))
        {
            yield return ARSession.CheckAvailability();
        }

        if (ARSession.state == ARSessionState.Unsupported)
        {
            foreach (GameObject go in _ordinaryGos)
                go.SetActive(true);

            foreach (GameObject go in _arRelatedGos)
                go.SetActive(false);

            _visualizationInstantiator.IsARAvailable = false;
            IsARAvailable = false;
        }
        else
        {
            _arSession.enabled = true;

            foreach (GameObject go in _ordinaryGos)
                go.SetActive(false);

            foreach (GameObject go in _arRelatedGos)
                go.SetActive(true);

            _visualizationInstantiator.IsARAvailable = true;
            IsARAvailable = true;
        }

    }

    #endregion
}
