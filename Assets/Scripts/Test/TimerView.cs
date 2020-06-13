using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerView : MonoBehaviour
{
    [SerializeField]
    private Text _startTime;

    [SerializeField]
    private Text _workingTime;

    [SerializeField]
    private Text _workingTimeWithoutPause;

    [SerializeField]
    private Text _pauseTime;

    [SerializeField]
    private Text _backgroundTime;

    [SerializeField]
    private Text _lastBCDep;

    [SerializeField]
    private Text _lastPause;

    [SerializeField]
    private Timer _timer;

    #region MonoBehaviour

    private void Update()
    {
        _startTime.text = $"Start time: {_timer.StartTime}";
        _workingTime.text = $"Working time: {_timer.WorkingTime}";
        _workingTimeWithoutPause.text = $"Work without pause: {_timer.WorkingTimeWithoutPause}";
        _pauseTime.text = $"Pause time: {_timer.PauseAmountTime}";
        _backgroundTime.text = $"Background time: {_timer.BackgroundTime}";
        _lastBCDep.text = $"Last Bg duration: {_timer.LastBackgroundDepartureTime}";
        _lastPause.text = $"Last pause: {_timer.LastPauseTime}";
    }

    #endregion

    public void StopTimer()
    {
        print(_timer.StopTimer());
    }


}
