using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stopTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Male;
    public Animator Level;
    public AudioSource audioSource;
    void OnEnable()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Male.Play("MaleFlash");
        Level.Play("IndicatorFlash");


    }

    public void UnPause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void Pause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }
    
}
