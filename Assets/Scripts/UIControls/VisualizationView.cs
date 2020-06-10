using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualizationView : MonoBehaviour
{

    public event Action CloseTapped;

    public event Action PauseTapped;

    public event Action UnPauseTapped;

    #region Methods

    public void Close()
    {
        CloseTapped?.Invoke();
    }

    public void Pause()
    {
        PauseTapped?.Invoke();
    }

    public void UnPause()
    {
        UnPauseTapped?.Invoke();
    }

    #endregion
}
