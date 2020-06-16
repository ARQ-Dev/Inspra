using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    enum directorCondition
    {
        Paused,
        Unpaused
    }

    directorCondition _directorCondition;

    private void Start()
    {
        _directorCondition = directorCondition.Unpaused;
    }
    public void Pause()
    {
        _directorCondition = directorCondition.Paused;
    }

    public void UnPause()
    {
        _directorCondition = directorCondition.Unpaused;
    }

    public bool TimelineWasPaused()
    {
        return _directorCondition == directorCondition.Paused;
    }
}
