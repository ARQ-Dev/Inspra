using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class VisualizationView : MonoBehaviour
{

    public event Action CloseTapped;

    #region Methods

    public void Close()
    {
        CloseTapped?.Invoke();
    }

    #endregion
}
