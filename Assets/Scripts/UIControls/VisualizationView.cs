using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQ.NeuroSym.UIKit;
using System;
public class VisualizationView : MonoBehaviour
{

    [SerializeField]
    private GameObject _hintView;

    [SerializeField]
    private Popup _popup;

    [SerializeField]
    private List<GameObject> _uiElements;

    public event Action CloseTapped;

    public event Action PauseTapped;

    public event Action UnPauseTapped;

    #region MonoBehaviour

    private void OnEnable()
    {

//#if UNITY_EDITOR
        if(!App.Instance.IsARAvailable)
            _hintView.SetActive(false);
//#endif

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

    public void ActivateHint(bool isActive)
    {
        _hintView.SetActive(isActive);
    }

    public void PresentPopup()
    {
        _popup.ShowPopupByIndex(0);
    }

    public void HideActivePopup()
    {
        _popup.HideActivePopup();
    }

    public void ActivateUI(bool isActive)
    {
        foreach (GameObject go in _uiElements)
        {
            go.SetActive(isActive);
        }
    }

    #endregion
}
