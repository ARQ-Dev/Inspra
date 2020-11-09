using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

namespace ARQ.AR.StateReporter
{
    [DisallowMultipleComponent]
    public class ARSessionLivecycleReporter : MonoBehaviour
    {
        [SerializeField]
        private GameObject stateReporter;

        private IARSessionStateReporter _stateReporter;

        private ARPlaneManager _planeManager;

        private bool _planeDetected = false;

        #region MonoBehaviour
        private void OnValidate()
        {
            if (stateReporter == null) return;
            _stateReporter = stateReporter.GetComponent<IARSessionStateReporter>();
            if (_stateReporter == null) stateReporter = null;
        }

        private void OnEnable()
        {
            _stateReporter = stateReporter.GetComponent<IARSessionStateReporter>();
            _planeManager = GetComponent<ARPlaneManager>();
            ARSession.stateChanged += StateChangedEvent;
            if (_planeManager != null) _planeManager.planesChanged += PlaneChangedEvent;
        }

        private void OnDisable()
        {
            ARSession.stateChanged -= StateChangedEvent;
            if (_planeManager != null) _planeManager.planesChanged -= PlaneChangedEvent;
        }
        #endregion

        private void StateChangedEvent(ARSessionStateChangedEventArgs args)
        {

            switch (args.state)
            {

                case ARSessionState.None:
                    _stateReporter.None();
                    break;
                case ARSessionState.Unsupported:
                    _stateReporter.Unsupported();
                    break;
                case ARSessionState.CheckingAvailability:
                    _stateReporter.CheckingAvalibility();
                    break;
                case ARSessionState.NeedsInstall:
                    _stateReporter.NeedsInstall();
                    break;
                case ARSessionState.Installing:
                    _stateReporter.Installing();
                    break;
                case ARSessionState.Ready:
                    _stateReporter.Ready();
                    break;
                case ARSessionState.SessionInitializing:
                    _stateReporter.SessionInitialized();
                    break;
                case ARSessionState.SessionTracking:
                    _stateReporter.SessionTracking();
                    break;
            }
        }

        private void PlaneChangedEvent(ARPlanesChangedEventArgs args)
        {
            if (!_planeDetected)
            {
                _stateReporter.OnPlaneDetected(true);
                _planeDetected = true;
            }
            if (_planeManager.trackables.count < 1)
            {
                _stateReporter.OnPlaneDetected(false);
                _planeDetected = false;
            }

        }
    }
}