using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualizationView : MonoBehaviour
{

    [SerializeField]
    private GameObject _hintView;

    public event Action CloseTapped;

    public event Action PauseTapped;

    public event Action UnPauseTapped;

    #region MonoBehaviour

    private void OnEnable()
    {

#if UNITY_EDITOR
        _hintView.SetActive(false);
#endif

    }

    private void OnDisable()
    {
        
    }

    #endregion

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

    public void PlaneDetected(bool isPlaneDetected)
    {
        _hintView.SetActive(!isPlaneDetected);
    }

    #endregion
}
