using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
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

    #region MonoBehaviour

    private void Start()
    {

        UsageTrackingManager.Instance.InitializeTrackingManager();
        UsageTrackingManager.Instance.AddOpeningsCount();
        var isReadinSuccess = UserManager.Instance.ReadUserData();

        if (isReadinSuccess)
        {
            _choice.Open();
        }
        else
        {
            _login.Open();
        }
    }

    public void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (_counter.SinceLastBackgroundTime > _acceptableBackgroundTime)
            {
                _visualizationInstantiator.DeleteInstantiatedPrefab();
                _choice.Open();
                UsageTrackingManager.Instance.SendTrackingData();
            }
        }
    }

    private void OnApplicationQuit()
    {
        UsageTrackingManager.Instance.SendTrackingData();
    }

    #endregion
}
