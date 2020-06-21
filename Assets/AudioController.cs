using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip[] _audioClips;


    public void PauseAudio()
    {
        _audioSource.Pause();
    }

    public void ResumeAudio()
    {
        _audioSource.UnPause();
    }

    public void PlayClip(int i)
    {
        _audioSource.Stop();
        _audioSource.clip = _audioClips[i];
        _audioSource.Play();
    }
}
