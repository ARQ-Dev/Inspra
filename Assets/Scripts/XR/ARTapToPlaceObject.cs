﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARTapToPlaceObject : MonoBehaviour
{

    [SerializeField]
    private GameObject _goToInstantiate;

    private GameObject _spawnedObject;
    private ARRaycastManager _aRRaycastManager;
    private Vector2 touchPosition;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        _aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    void Update()
    {
        if (!TryGetTouchPosition(out Vector2 touchPosition))
            return;

        if(_aRRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            if(_spawnedObject == null)
            {
                _spawnedObject = Instantiate(_goToInstantiate, hitPose.position, hitPose.rotation);
            }
            else
            {
                _spawnedObject.transform.position = hitPose.position;
            }
        }
    }

    public void InstantiateGO()
    {
        _spawnedObject = Instantiate(_goToInstantiate, new Vector3(1, 12, 3), Quaternion.Euler(0, 90, 0));
    }
}
