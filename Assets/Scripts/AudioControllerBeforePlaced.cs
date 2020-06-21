using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerBeforePlaced : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSourceBeforePlaced;

    [SerializeField]
    private AudioClip[] _audioClips;

    private AudioSource[] allAudioSources;

    [SerializeField]
    private Animator _headersBeforePlaced;

    public void OffAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.isPlaying)
                audioS.volume = 0;
        }
    }
    public void OnAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            if (audioS.isPlaying)
            {
                if(audioS.clip.name == "Cardio_music")
                    audioS.volume = 0.1f;
                else
                    audioS.volume = 1;
            }
                
        }
    }
    public void PlayClipsFirstScene()
    {
        StartCoroutine(ForFirstScene());
    }

    IEnumerator ForFirstScene()
    {
        _audioSourceBeforePlaced.Stop();
        _audioSourceBeforePlaced.clip = _audioClips[0];
        _audioSourceBeforePlaced.Play();
        _headersBeforePlaced.Play("PlayFirstHeader");
        yield return new WaitForSeconds(_audioSourceBeforePlaced.clip.length);
        _audioSourceBeforePlaced.clip = _audioClips[1];
        _audioSourceBeforePlaced.Play();
    }

    public void StopAllStartedSpeech()
    {
        StopAllCoroutines();
        _audioSourceBeforePlaced.Stop();
        foreach (Transform child in _headersBeforePlaced.gameObject.transform)
            child.gameObject.SetActive(false);
    }
    
    public void PlayClipsSecondScene()
    {
        StartCoroutine(ForSecondScene());
    }
    IEnumerator ForSecondScene()
    {
        _headersBeforePlaced.Play("PlaySecondHeader");
        _audioSourceBeforePlaced.Stop();
        yield return new WaitForSeconds(2);
          _audioSourceBeforePlaced.clip = _audioClips[3];
        _audioSourceBeforePlaced.Play();
    }
}
