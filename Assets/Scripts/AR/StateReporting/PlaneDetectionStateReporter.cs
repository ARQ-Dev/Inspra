using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ARQ.AR.StateReporter;
public class PlaneDetectionStateReporter : MonoBehaviour, IARSessionStateReporter
{

    public event Action<bool> PlaneDetected;

    public void CheckingAvalibility()
    {

    }

    public void Installing()
    {

    }

    public void NeedsInstall()
    {

    }

    public void None()
    {
      
    }

    public void OnPlaneDetected(bool isPlaneDetected)
    {
        PlaneDetected?.Invoke(isPlaneDetected);
    }

    public void Ready()
    {

    }

    public void SessionInitialized()
    {

    }

    public void SessionTracking()
    {

    }

    public void Unsupported()
    {

    }
}
