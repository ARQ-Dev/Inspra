using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeCounter : MonoBehaviour
{
    public DateTime? StartTime { get; private set; } = null;

    public double ForgroundTime
    {
        get
        {
            return (DateTime.Now - (StartTime ?? DateTime.Now)).TotalSeconds - BackgroundTime;
        }
    }

    public double BackgroundTime { get; private set; } = 0;

    public double? SinceLastBackgroundTime
    {
        get
        {
            if (!LastBackgroundDepartureTime.HasValue) return null;
            return (DateTime.Now - LastBackgroundDepartureTime.Value).TotalSeconds;
        }
    }

    public DateTime? LastBackgroundDepartureTime { get; private set; } = null;

    #region MonoBehaviour

    private void Start()
    {
        StartTime = DateTime.Now;
    }

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

}
