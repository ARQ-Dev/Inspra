using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PauseController : MonoBehaviour
{
    enum directorCondition
    {
        Paused,
        Unpaused
    }

    PlayableDirector Director;
    AudioController _audioController;

    directorCondition _directorCondition;

    private void Start()
    {
        _directorCondition = directorCondition.Unpaused;
        _audioController = GetComponent<AudioController>();
        Director = GetComponent<PlayableDirector>();
    }

    //пауза останавливает речь -
    //пауза останавливает анимацию -

    void PauseAudio()
    {
        _audioController.PauseAudio();
    }

    void ResumeAudio()
    {
        _audioController.ResumeAudio();
    }
    public void Pause()
    {
        if (!Director) return;
        _directorCondition = directorCondition.Paused;
        PauseAudio();
        Director.playableGraph.GetRootPlayable(0).SetSpeed(0);
    }

    public void UnPause()
    {
        if (!Director) return;
        _directorCondition = directorCondition.Unpaused;
        if (!(stopTimeline._firstTimeLineWasPaused || StopTimeline2._secondTimeLineWasPaused))
        {
            ResumeAudio();
            Director.playableGraph.GetRootPlayable(0).SetSpeed(1);
        }
    }

    public bool TimelineWasPaused()
    {
        return _directorCondition == directorCondition.Paused;
    }
}
