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

    private GameObject _istantiatedPrefab;


    #region MonoEhaviour

    private void OnEnable()
    {
        _positioner.Instantiated += OnInstantiated;
    }

    private void OnDisable()
    {
        _positioner.Instantiated -= OnInstantiated;
    }

    #endregion

    #region Methods

    public void InstantiateFirst()
    {

#if UNITY_EDITOR
        _istantiatedPrefab = Instantiate(_first, Vector3.zero, Quaternion.identity);
#else
        _positioner.StartPositioning(_first);
#endif

    }

    public void InstantiateSecond()
    {
#if UNITY_EDITOR
        _istantiatedPrefab = Instantiate(_second, Vector3.zero, Quaternion.identity);
#else
        _positioner.StartPositioning(_second);
#endif
    }

    public void DeleteInstantiatedPrefab()
    {
        if (_istantiatedPrefab == null) return;
        Destroy(_istantiatedPrefab);
    }

#endregion

    private void OnInstantiated(GameObject go)
    {
        if (_istantiatedPrefab != null) Destroy(_istantiatedPrefab);
        _istantiatedPrefab = go;
    }

}
