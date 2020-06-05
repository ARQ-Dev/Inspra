using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualizationView : MonoBehaviour
{

    public event Action BackTapped;

    #region Methods

    public void Back()
    {
        BackTapped?.Invoke();
    }

    #endregion
}
