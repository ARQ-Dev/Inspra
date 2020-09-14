using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQ.AR.Positioning;
using System;
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

    public event Action<GameObject> Instantiaded;

    [SerializeField]
    private RotateModel _rotateModel;

    public bool IsARAvailable { get; set; }

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

        //#if UNITY_EDITOR
        //        _istantiatedPrefab = Instantiate(_first, Vector3.zero, Quaternion.identity);
        //        //Instantiaded?.Invoke(_istantiatedPrefab);
        //        StartCoroutine(InstantiatedCor());
        //#else

        if (IsARAvailable)
        {
            _positioner.StartPositioning(_first);
        }
        else
        {
            _istantiatedPrefab = Instantiate(_first, Vector3.zero, Quaternion.identity);
            StartCoroutine(InstantiatedCor());
        }


        //#endif

    }

    public void InstantiateSecond()
    {
        //#if UNITY_EDITOR
        //        _istantiatedPrefab = Instantiate(_second, Vector3.zero, Quaternion.identity);
        //        //Instantiaded?.Invoke(_istantiatedPrefab);

        //        StartCoroutine(InstantiatedCor());

        //#else

        //            _positioner.StartPositioning(_second);

        //#endif

        if (IsARAvailable)
        {
            _positioner.StartPositioning(_second);
        }
        else
        {
            _istantiatedPrefab = Instantiate(_second, Vector3.zero, Quaternion.identity);
            StartCoroutine(InstantiatedCor());
        }

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
        Instantiaded?.Invoke(_istantiatedPrefab);
        _rotateModel.Object = _istantiatedPrefab;
    }

    private IEnumerator InstantiatedCor()
    {
        yield return null;
        Instantiaded?.Invoke(_istantiatedPrefab);
        _rotateModel.Object = _istantiatedPrefab;
    }
}
