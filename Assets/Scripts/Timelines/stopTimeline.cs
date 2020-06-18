using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stopTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Male;
    public Animator Level;
    public Animator Solve;
    public static bool _firstTimeLineWasPaused = false;
    
    void OnEnable()
    {
        _firstTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Male.Play("MaleFlash");
        Level.Play("IndicatorFlash");
        Solve.Play("SolveProblemAppearing");
    }

    public void UnPause()
    {
        if (!director.GetComponent<PauseController>().TimelineWasPaused())
        {
            _firstTimeLineWasPaused = false;
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            Solve.Play("SwitchSolveButton");
        }
    }

    public void Pause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
}
