using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.ARFoundation;

public class ChoiceViewController : ViewController
{
    [SerializeField]
    private ChoiceView _view;

    [SerializeField]
    private VisualizationInstantiator _visualizationInstantiator;

    [SerializeField]
    private VisualizationInstantiator _instantiator;

    [SerializeField]
    private ViewController _visualizacionVC;

    [SerializeField]
    private ViewController _loginVC;

    [SerializeField]
    private List<GameObject> _arRelatedGos;

    [SerializeField]
    private List<GameObject> _ordinaryGos;

    [SerializeField]
    private ARSession _arSession;

    public event Action LogOut;

    private bool _isAR = true;

    private Vector3 PosVis1 = new Vector3(0, 1.15f, -2.15f);
    private Vector3 PosVis2 = new Vector3(0, 1.2f, -2.35f);

    #region MonoBehaviour

    private void OnEnable()
    {
        _view.FirstSelected += OnFirstSelected;
        _view.SecondSelected += OnSecondSelected;
        _view.LogOutTapped += OnLogoutTapped;
        _view.RegimeTapped += OnRegimeTapped;
        
    }

    private void OnDisable()
    {
        _view.FirstSelected -= OnFirstSelected;
        _view.SecondSelected -= OnSecondSelected;
        _view.LogOutTapped -= OnLogoutTapped;
        _view.RegimeTapped -= OnRegimeTapped;
    }

    #endregion

    private void OnFirstSelected()
    {
        _instantiator.InstantiateFirst();
        Present(_visualizacionVC);
        _ordinaryGos[0].transform.position = PosVis1;
    }

    private void OnSecondSelected()
    {
        _instantiator.InstantiateSecond();
        Present(_visualizacionVC);
        _ordinaryGos[0].transform.position = PosVis2;

    }

    private void OnLogoutTapped()
    {
        LogOut?.Invoke();
        Present(_loginVC);
    }
    private void OnRegimeTapped()
    {
        _isAR = !_isAR;
        ChangeRegime(_isAR);
    }
    public void DisableChoiseRegimeButton()
    {
        _view.DisableRegime();
    }

    private void ChangeRegime(bool isAR)
    {
        foreach (GameObject go in _ordinaryGos)
            go.SetActive(!isAR);

        foreach (GameObject go in _arRelatedGos)
            go.SetActive(isAR);

        _arSession.enabled = isAR;
        _visualizationInstantiator.IsARAvailable = isAR;
        App.Instance.IsARAvailable = isAR;
    }

}
