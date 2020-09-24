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
    private PlaneDetectionStateReporter _planeStateReporter;

    [SerializeField]
    private ARPlaneManager _planeManager;

    [SerializeField]
    private ARSession _session;

    [SerializeField]
    private Timer _timer;

    [SerializeField]
    AudioControllerBeforePlaced audioControllerBeforePlaced;


    #region MonoBehaviour

    private void OnEnable()
    {
        _view.CloseTapped += OnBackTapped;
        _view.PauseTapped += OnPauseTapped;
        _view.UnPauseTapped += OnUnPauseTapped;

        _istantiator.Instantiaded += OnInstantiated;

        _planeStateReporter.PlaneDetected += OnPlaneDetected;

        var director = FindObjectOfType<PlayableDirector>();

        _session.Reset();

#if !UNITY_EDITOR
        _view.ActivateUI(false);
#endif
        _planeManager.enabled = true;
        _timelineController.Director = director;

        _view.ActivateUI(false);

    }

    private void OnDisable()
    {
        _view.CloseTapped -= OnBackTapped;
        _view.PauseTapped -= OnPauseTapped;
        _view.UnPauseTapped -= OnUnPauseTapped;

        _istantiator.Instantiaded -= OnInstantiated;

        _planeStateReporter.PlaneDetected -= OnPlaneDetected;

        if (_planeManager != null)
            _planeManager.enabled = false;


    }

    #endregion

    private void OnBackTapped()
    {
        _view.ActivateUI(false);
        _usageTrackingManager.SessionEnded();
        _istantiator.DeleteInstantiatedPrefab();
        Present(_nextViewController);
        _planeManager.enabled = false;
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

    private void OnPlaneDetected(bool isPlaneDetected)
    {
        if (!App.Instance.IsARAvailable)
            return;
#if !UNITY_EDITOR
        _view.ActivateHint(!isPlaneDetected);
        if (isPlaneDetected)
            _view.PresentPopup();
#endif


    }

    private void OnInstantiated(GameObject go)
    {
        _view.HideActivePopup();
        _view.ActivateUI(true);
        audioControllerBeforePlaced.StopAfterPlaced();
        go.GetComponent<Visualization>().StartCloseAnim += GetComponent<CloseAnimController>().StartAnim;
    }
}
