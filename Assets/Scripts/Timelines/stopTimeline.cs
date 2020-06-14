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
    public AudioSource audioSource;
    void OnEnable()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Male.Play("MaleFlash");
        Level.Play("IndicatorFlash");
        Solve.Play("SolveProblemAppearing");
    }

    public void UnPause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        Solve.Play("OneBigAnim");
    }

    public void Pause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);

    }
    
}
