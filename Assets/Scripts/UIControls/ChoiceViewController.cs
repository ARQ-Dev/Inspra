using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceViewController : ViewController
{
    [SerializeField]
    private ChoiceView _view;

    [SerializeField]
    private VisualizationInstantiator _instantiator;

    [SerializeField]
    private ViewController _nextViewController;

    #region MonoBehaviour

    private void OnEnable()
    {
        _view.FirstSelected += OnFirstSelected;
        _view.SecondSelected += OnSecondSelected;
    }

    private void OnDisable()
    {
        _view.FirstSelected -= OnFirstSelected;
        _view.SecondSelected -= OnSecondSelected;
    }

    #endregion

    private void OnFirstSelected()
    {
        _instantiator.InstantiateFirst();
        Present(_nextViewController, null);
    }

    private void OnSecondSelected()
    {
        _instantiator.InstantiateSecond();
        Present(_nextViewController, null);
    }

}
