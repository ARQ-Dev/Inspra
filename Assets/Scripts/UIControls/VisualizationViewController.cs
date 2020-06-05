using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizationViewController : ViewController
{
    [SerializeField]
    private VisualizationView _view;

    [SerializeField]
    private ViewController _nextViewController;

    #region MonoBehaviour

    private void OnEnable()
    {
        _view.BackTapped += OnBackTapped;
    }

    private void OnDisable()
    {
        _view.BackTapped -= OnBackTapped;
    }

    #endregion

    private void OnBackTapped()
    {
        Present(_nextViewController, null);
    }

}
