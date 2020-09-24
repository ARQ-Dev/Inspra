using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControllerBeforePlaced : MonoBehaviour
{
    private AudioSource[] allAudioSources;
    [SerializeField]
    private AudioClip clipBeforePlaced;
    [SerializeField]
    private AudioSource _mainAS;

    public void OffAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            
                audioS.volume = 0;
        }
    }
    public void OnAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            
            
                if(audioS.clip.name == "Cardio_music")
                    audioS.volume = 0.1f;
                else
                    audioS.volume = 1;
            
                
        }
    }

    public void PlayBeforePlaced()
    {
        if (App.Instance.IsARAvailable)
        {
            _mainAS.Stop();
            _mainAS.clip = clipBeforePlaced;
            _mainAS.Play();
        }
    }

    public void StopAfterPlaced()
    {
        _mainAS.Stop();
    }

    private void OnDisable()
    {
        _mainAS.clip = null;
    }
    private void OnEnable()
    {
        _mainAS.clip = null;
    }
}
