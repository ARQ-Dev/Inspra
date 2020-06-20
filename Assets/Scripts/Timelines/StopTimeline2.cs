using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StopTimeline2 : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Models;
    public static bool _secondTimeLineWasPaused = false;
    public AudioClip _speechBeforeButton;
    public AudioClip _speechAfterButton;
    public AudioClip _speechAfterButton2;
    private int _speechCounter=0;
    void OnEnable()
    {
        if (director.time < 95)
        {
            InvokeRepeating("PlaySpeechBeforeButton", 0,12.5f);
        }
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        if (Models.isActiveAndEnabled)
        {
            Models.Play("ModelsIdle");
        }
    }

    void PlaySpeechBeforeButton()
    {
        director.GetComponent<AudioSource>().clip = _speechBeforeButton;
        director.GetComponent<AudioSource>().Play();
        _speechCounter++;
        if(_speechCounter == 4)
        {
            CancelInvoke("PlaySpeechBeforeButton");
        }
    }

    public void UnPause()
    {
        if (!director.GetComponent<PauseController>().TimelineWasPaused())
        {
            _secondTimeLineWasPaused = false;
            if (director.time < 95)
            {
                CancelInvoke("PlaySpeechBeforeButton");
                director.GetComponent<AudioSource>().clip = _speechAfterButton;
                director.GetComponent<AudioSource>().Play();
            }
            if (director.time > 95 && director.time<110)
            {
                director.GetComponent<AudioSource>().clip = _speechAfterButton2;
                director.GetComponent<AudioSource>().Play();
            }
            _speechCounter = 0;
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public void Pause()
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void DropRisks()
    {
        director.time = 157.15f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ReturnInspra()
    {
        director.time = 132.2f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void Reasons()
    {
        director.time = 96;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
