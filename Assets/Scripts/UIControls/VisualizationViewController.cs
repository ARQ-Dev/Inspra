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
        _view.CloseTapped += OnBackTapped;
    }

    private void OnDisable()
    {
        _view.CloseTapped -= OnBackTapped;
    }

    #endregion

    private void OnBackTapped()
    {
        Present(_nextViewController);
    }



}
