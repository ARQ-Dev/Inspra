using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class TimerView : MonoBehaviour
{
    [SerializeField]
    private TimeCounter _counter;

    [SerializeField]
    private Text _startTime;

    [SerializeField]
    private Text _forgroundTime;

    [SerializeField]
    private Text _bacgroundTime;

    [SerializeField]
    private Text _sinceLastBG;

    [SerializeField]
    private Text _lastBGDeparture;


    #region MonoBehaviour

    private void Start()
    {
        _counter = GetComponent<TimeCounter>();
    }

    private void Update()
    {
        _startTime.text = $"Start time {(_counter.StartTime ?? DateTime.Now).TimeOfDay}";
        _forgroundTime.text = $"Forground time: {_counter.ForgroundTime}";
        _bacgroundTime.text = $"Background time: {_counter.BackgroundTime}";
        _sinceLastBG.text = $"Since last bg: {_counter.SinceLastBackgroundTime}";
        _lastBGDeparture.text = $"Last bg departure: {_counter.LastBackgroundDepartureTime}";


    }

    #endregion
}
