using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstantiator : MonoBehaviour
{
    [SerializeField]
    private Transform _transformToInstantiate;

    [SerializeField]
    private GameObject _goToInstantiate;

    private GameObject _instance;

    public void InstantiateGO()
    {
        _instance = Instantiate(_goToInstantiate, _transformToInstantiate.position, _transformToInstantiate.rotation);
    }
}
