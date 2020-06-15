using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.ARFoundation;

public class VisualizationViewController : ViewController
{
    [SerializeField]
    private VisualizationInstantiator _istantiator;

    [SerializeField]
    private TimelineController _timelineController;

    [SerializeField]
    private VisualizationView _view;

    [SerializeField]
    private ViewController _nextViewController;

    [SerializeField]
    private UsageTrackingManager _usageTrackingManager;

    [SerializeField]
    private Timer _timer;


    #region MonoBehaviour

    private void OnEnable()
    {
        _view.CloseTapped += OnBackTapped;
        _view.PauseTapped += OnPauseTapped;
        _view.UnPauseTapped += OnUnPauseTapped;

        var director = FindObjectOfType<PlayableDirector>();

        _timelineController.Director = director;

    }

    private void OnDisable()
    {
        _view.CloseTapped -= OnBackTapped;
        _view.PauseTapped -= OnPauseTapped;
        _view.UnPauseTapped -= OnUnPauseTapped;

    }

    #endregion

    private void OnBackTapped()
    {
        _istantiator.DeleteInstantiatedPrefab();
        Present(_nextViewController);
        _usageTrackingManager.SessionEnded();

    }



    private void OnPauseTapped()
    {
        _timelineController.Pause();
        _timer.PauseTimer();
    }

    private void OnUnPauseTapped()
    {
        _timelineController.UnPause();
        _timer.UnpauseTimer();
    }

    protected override void OnPresended()
    {

    }
}
