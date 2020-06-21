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
    public AudioSource _mainAudioSource;
    public AudioClip _speechBeforeButton;
    private int _speechCounter = 0;
    private PauseController _pauseController;
    void OnEnable()
    {
        _pauseController = director.GetComponent<PauseController>();
    }

    void PlaySpeech()
    {
        if (!_pauseController.TimelineWasPaused())
        {
            _mainAudioSource.Stop();
            _mainAudioSource.clip = _speechBeforeButton;
            _mainAudioSource.Play();
            _speechCounter++;
            if (_speechCounter == 4)
            {
                CancelInvoke();
                _speechCounter = 0;
            }
        }
    }

    public void UnPause()
    {
        if (!_pauseController.TimelineWasPaused())
        {
            CancelInvoke();
            _speechCounter = 0;
            _firstTimeLineWasPaused = false;
            _mainAudioSource.Stop();
            director.playableGraph.GetRootPlayable(0).SetSpeed(1);
            Solve.Play("SwitchSolveButton");
        }
    }


    public void Pause()
    {
        _mainAudioSource.Stop();
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
            InvokeRepeating("PlaySpeech", 2, 12.5f);
            Level.Play("IndicatorFlash");
            Male.Play("MaleFlash");
            _firstTimeLineWasPaused = true;
            Solve.Play("SolveProblemAppearing");
        
    }
}
