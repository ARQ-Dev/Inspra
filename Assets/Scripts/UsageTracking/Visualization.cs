using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Visualization : MonoBehaviour
{
    [SerializeField]
    private int _number;

    [SerializeField]
    private AudioSource _backgroundMusicSource;

    public event Action StartCloseAnim;

    public void OnStartClose()
    {
        StartCloseAnim?.Invoke();
        _backgroundMusicSource.Stop();
    }

    public void ResumeBackMusic()
    {
        if(!_backgroundMusicSource.isPlaying)
            _backgroundMusicSource.Play();
    }

    public int Number { get { return _number; } }
}
