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
    public AudioClip _speechAfterButton2;
    [SerializeField]
    private AudioSource _mainAudioSource;
    private bool _wasPassed1 = false;
    public Animator ButtonAnimator;

    PauseController _pauseController;
    private int _speechCounter=0;
    void OnEnable()
    {
        _mainAudioSource = director.GetComponent<AudioSource>();
        _pauseController = director.GetComponent<PauseController>();
    }

    public void Pause1()
    {
        ButtonAnimator.ResetTrigger("nothing");
        InvokeRepeating("PlaySpeechBeforeButton", 0, 12.5f);
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        ButtonAnimator.Play("ReasonsAndFeaturesFlashing");
    }

    public void Pause2()
    {
        ButtonAnimator.ResetTrigger("nothing");
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Models.Play("ModelsIdle");
        ButtonAnimator.Play("ReasonsAndFeaturesFlashing");
    }

    public void Pause3()
    {
        
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        
    }
   
    public void UnPause3()
    {
        _secondTimeLineWasPaused = false;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    void PlaySpeechBeforeButton()
    {
        if (!_pauseController.TimelineWasPaused())
        {
            _mainAudioSource.clip = _speechBeforeButton;
            _mainAudioSource.Play();
            _speechCounter++;
            if (_speechCounter == 4)
            {
                CancelInvoke();
            }
        }
    }

    

    public void UnPause()
    {
        if (!_pauseController.TimelineWasPaused())
        {
            CancelInvoke();
            _mainAudioSource.Stop();
            _secondTimeLineWasPaused = false;
            if (director.time < 110)
            {
                _speechCounter = 0;
            }
            if (director.time > 115 && director.time<125)
            {
                _mainAudioSource.clip = _speechAfterButton2;
                _mainAudioSource.Play();
            }
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            ButtonAnimator.SetTrigger("nothing");

        }
    }


    public void IfWasPassed()
    {
        _mainAudioSource.Stop();
    }

    public void DropRisks()
    {
        _mainAudioSource.Stop();
        director.time = 171.8f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ReturnInspra()
    {
        _mainAudioSource.Stop();
        director.time = 144.5f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void Reasons()
    {
        _mainAudioSource.Stop();
        director.time = 99;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
