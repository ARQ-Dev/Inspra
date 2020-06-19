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
    public AudioClip _speechAfterButton;
    private int _speechCounter = 0;
    
    void OnEnable()
    {

        _firstTimeLineWasPaused = true;
        director.playableGraph.GetRootPlayable(0).SetSpeed(0);
        Male.Play("MaleFlash");
        Level.Play("IndicatorFlash");
        Solve.Play("SolveProblemAppearing");
        PlaySpeech();
        InvokeRepeating("PlaySpeech", 12.5f, 12.5f);
    }

    void PlaySpeech()
    {
        _mainAudioSource.clip = _speechBeforeButton;
        _mainAudioSource.Play();
        _speechCounter++;
        if (_speechCounter == 4)
        {
            CancelInvoke("PlaySpeech");
            _speechCounter = 0;
        }
    }

    public void UnPause()
    {
        if (!director.GetComponent<PauseController>().TimelineWasPaused())
        {
            CancelInvoke("PlaySpeech");
            _speechCounter = 0;
            _mainAudioSource.clip = _speechAfterButton;
            _mainAudioSource.Play();
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
