using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoiceViewController : ViewController
{
    [SerializeField]
    private ChoiceView _view;

    [SerializeField]
    private VisualizationInstantiator _instantiator;

    [SerializeField]
    private ViewController _visualizacionVC;

    [SerializeField]
    private ViewController _loginVC;

    public event Action LogOut;

    #region MonoBehaviour

    private void OnEnable()
    {
        _view.FirstSelected += OnFirstSelected;
        _view.SecondSelected += OnSecondSelected;
        _view.LogOutTapped += OnLogoutTapped;

    }

    private void OnDisable()
    {
        _view.FirstSelected -= OnFirstSelected;
        _view.SecondSelected -= OnSecondSelected;
        _view.LogOutTapped -= OnLogoutTapped;
    }

    #endregion

    private void OnFirstSelected()
    {
        _instantiator.InstantiateFirst();
        Present(_visualizacionVC);
    }

    private void OnSecondSelected()
    {
        _instantiator.InstantiateSecond();
        Present(_visualizacionVC);
    }

    private void OnLogoutTapped()
    {
        LogOut?.Invoke();
        Present(_loginVC);
    }



}
