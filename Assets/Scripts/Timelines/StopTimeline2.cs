using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopTimeline2 : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Models;
    public static bool _secondTimeLineWasPaused = false;
    void OnEnable()
    {
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (Models.isActiveAndEnabled)
        {
            Models.Play("ModelsIdle");
        }
    }

    public void UnPause()
    {
        if (!director.GetComponent<PauseController>().TimelineWasPaused())
        {
            _secondTimeLineWasPaused = false;
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public void Pause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void DropRisks()
    {
        director.time = 95;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ReturnInspra()
    {
        director.time = 82;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);

    }

    public void Reasons()
    {
        director.time = 61;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
