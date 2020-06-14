using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : MonoBehaviour
{

    public PlayableDirector Director { get; set; }

    public void Pause()
    {
        if (!Director) return;
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void UnPause()
    {
        if (!Director) return;
        Debug.Log(stopTimeline._firstTimeLineWasPaused);
        if(!stopTimeline._firstTimeLineWasPaused) Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

}
