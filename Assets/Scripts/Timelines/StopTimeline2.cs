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


    PauseController _pauseController;
    private int _speechCounter=0;
    void OnEnable()
    {
        _mainAudioSource = director.GetComponent<AudioSource>();
        _pauseController = director.GetComponent<PauseController>();
    }

    public void Pause1()
    {
        
            InvokeRepeating("PlaySpeechBeforeButton", 0, 12.5f);
            _secondTimeLineWasPaused = true;
            director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        
    }

    public void Pause2()
    {
        _secondTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Models.Play("ModelsIdle");
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
            _mainAudioSource.Stop();
            _secondTimeLineWasPaused = false;
            if (director.time < 110)
            {
                CancelInvoke();
                _speechCounter = 0;
            }
            if (director.time > 120 && director.time<130)
            {
                _mainAudioSource.clip = _speechAfterButton2;
                _mainAudioSource.Play();
            }
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }


    public void IfWasPassed()
    {
        _mainAudioSource.Stop();
    }

    public void DropRisks()
    {
        _mainAudioSource.Stop();
        director.time = 176.2f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void ReturnInspra()
    {
        _mainAudioSource.Stop();
        director.time = 149;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }

    public void Reasons()
    {
        _mainAudioSource.Stop();
        director.time = 104.1f;
        director.playableGraph.GetRootPlayable(0).SetSpeed(1);
    }
}
