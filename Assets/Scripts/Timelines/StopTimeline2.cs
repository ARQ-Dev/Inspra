using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopTimeline2 : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Models;
    void OnEnable()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (Models.isActiveAndEnabled)
        {
            Models.Play("ModelsIdle");
        }
    }

    public void UnPause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
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
