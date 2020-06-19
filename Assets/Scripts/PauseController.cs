using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseController : MonoBehaviour
{
    enum directorCondition
    {
        Paused,
        Unpaused
    }

    PlayableDirector Director { get; set; }

    directorCondition _directorCondition;

    private void Start()
    {
        _directorCondition = directorCondition.Unpaused;

        Director = GetComponent<PlayableDirector>();
    }
    public void Pause()
    {
        if (!Director) return;
        _directorCondition = directorCondition.Paused;
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void UnPause()
    {
        _directorCondition = directorCondition.Unpaused;
        if (!Director) return;
        if (!(stopTimeline._firstTimeLineWasPaused || StopTimeline2._secondTimeLineWasPaused))
        {
            Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public bool TimelineWasPaused()
    {
        return _directorCondition == directorCondition.Paused;
    }
}
