using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class ChoiceView : MonoBehaviour
{
    [SerializeField]
    private Button _regimeApp;
    [SerializeField]
    private Sprite _3dRegimeSprite;
    [SerializeField]
    private Sprite _ARRegimeSprite;


    public event Action FirstSelected;
    public event Action SecondSelected;
    public event Action LogOutTapped;
    public event Action RegimeTapped;

    #region Methods

    public void First()
    {
        FirstSelected?.Invoke();
    }

    public void Second()
    {
        SecondSelected?.Invoke();
    }

    public void LogOut()
    {
        LogOutTapped?.Invoke();
    }
    public void Regime()
    {
        RegimeTapped?.Invoke();
        ChangeSpriteButtonRegime();
    }
    public void DisableRegime()
    {
        _regimeApp.gameObject.SetActive(false);
    }
    public void ChangeSpriteButtonRegime()
    {
        if(_regimeApp.image.sprite == _3dRegimeSprite)
        {
            _regimeApp.image.sprite = _ARRegimeSprite;
        }
        else
        {
            _regimeApp.image.sprite = _3dRegimeSprite;
        }
    }
    #endregion

}
