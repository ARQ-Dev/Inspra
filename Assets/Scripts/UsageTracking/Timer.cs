using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Timer : MonoBehaviour
{
    public class TimerReport
    {
        public double WorkingTime { get; set; }

        public double WorkingTimeWithoutPause { get; set; }

        public DateTime StartTime { get; set; }

        public override string ToString()
        {
            var str = $"Working time: {WorkingTime}," +
                $"WorkingTimeWithoutPause: {WorkingTimeWithoutPause}";
            return str;
        }

    }

    public DateTime? StartTime { get; private set; } = null;

    public double WorkingTime => (DateTime.Now - (StartTime ?? DateTime.Now)).TotalSeconds - BackgroundTime;

    public double WorkingTimeWithoutPause => WorkingTime - PauseAmountTime;

    public double PauseAmountTime { get; private set; } = 0;

    public double BackgroundTime { get; private set; } = 0;

    public DateTime? LastBackgroundDepartureTime { get; private set; } = null;

    public DateTime? LastPauseTime { get; private set; } = null;

    private bool isWorking = false;

    private bool isPaused = false;

    #region MonoBehaviour

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            LastBackgroundDepartureTime = DateTime.Now;
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (!LastBackgroundDepartureTime.HasValue) return;
            var deltaTime = (DateTime.Now - LastBackgroundDepartureTime.Value).TotalSeconds;
            BackgroundTime += deltaTime;
        }
    }

    #endregion

    #region Methods

    public void StartTimer()
    {
        if (isWorking) return;
        StartTime = DateTime.Now;
        LastBackgroundDepartureTime = null;
        LastPauseTime = null;
        BackgroundTime = 0;
        PauseAmountTime = 0;
        isWorking = true;
        isPaused = false;
    }

    public void PauseTimer()
    {
        if (!isWorking || isPaused) return;
        LastPauseTime = DateTime.Now;
        isPaused = true;
    }

    public void UnpauseTimer()
    {
        if (!isWorking || !isPaused) return;
        if (!LastPauseTime.HasValue) return;
        var deltaTime = (DateTime.Now - LastPauseTime.Value).TotalSeconds;
        PauseAmountTime += deltaTime;
        isPaused = false;
    }

    public TimerReport StopTimer()
    {
        UnpauseTimer();

        var report = new TimerReport
        {
            WorkingTime = WorkingTime,
            WorkingTimeWithoutPause = WorkingTimeWithoutPause,
            StartTime = StartTime ?? DateTime.Now
        };

        StartTime = null;
        LastBackgroundDepartureTime = null;
        LastPauseTime = null;
        BackgroundTime = 0;
        PauseAmountTime = 0;
        isWorking = false;
        isPaused = false;
        return report;
    }

    #endregion
}
