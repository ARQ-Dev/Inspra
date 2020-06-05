using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQ.AR.Positioning;

public class VisualizationInstantiator : MonoBehaviour
{
    [SerializeField]
    private GameObject _first;

    [SerializeField]
    private GameObject _second;

    [SerializeField]
    private PlanePositioner _positioner;

    #region Methods

    public void InstantiateFirst()
    {
        _positioner.StartPositioning(_first);
    }

    public void InstantiateSecond()
    {
        _positioner.StartPositioning(_second);
    }

    #endregion

}
