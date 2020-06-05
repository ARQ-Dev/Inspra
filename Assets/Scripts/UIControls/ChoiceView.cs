using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoiceView : MonoBehaviour
{
    public event Action FirstSelected;
    public event Action SecondSelected;

    #region Methods

    public void First()
    {
        FirstSelected?.Invoke();
    }

    public void Second()
    {
        SecondSelected?.Invoke();
    }

    #endregion

}
