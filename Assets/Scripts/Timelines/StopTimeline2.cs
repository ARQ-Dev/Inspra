using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopTimeline2 : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Canv;
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

    public void AppearingMessage()
    {
        Canv.Play("AppearingMessage");
    }
    public void HidingMessage()
    {
        Canv.Play("HidingMessage");
    }

    public void AppearingPlashka(string NamePlashka)
    {
        Canv.Play(NamePlashka);
    }
    public void HidingPlashka(string NamePlashka)
    {
        Canv.Play(NamePlashka);
    }


    public void DropRisks()
    {
        director.time = 95;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ReturnInspra()
    {
        director.time = 90;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);

    }

    public void Reasons()
    {
        director.time = 66;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
