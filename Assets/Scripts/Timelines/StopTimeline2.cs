using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopTimeline2 : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Male;
    public Animator Female;
    public Animator Canv;
    public Animator Button;

    public bool isItSecond;

    void OnEnable()
    {
        if(isItSecond) Button.SetTrigger("Holding");
        Canv.SetTrigger("isPause");
        if (isItSecond)
        {
            Male.SetTrigger("FullHolding");
            Female.SetTrigger("FullHolding");
        }
        else
        {
            Male.SetTrigger("Holding");
            Female.SetTrigger("HoldingFemale");
        }
        director.Pause();
    }

    public void UnPause()
    {
        Debug.Log("here");
        if (isItSecond) Button.ResetTrigger("Holding");
        if (isItSecond) Button.SetTrigger("nothing");
        Canv.ResetTrigger("isPause");
        if (!isItSecond) Male.ResetTrigger("Holding");
        else Male.SetTrigger("FullHolding");
        Female.SetTrigger("HoldingFemale");
        director.Resume();
    }

}
