using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;

namespace ARQ.InputHandling
{
    public abstract class GestureHandlerBase : MonoBehaviour
    {
        protected GestureRecognizer gestureRecognizer;

        public GestureRecognizer GestureRecognizer => gestureRecognizer;

        public event Action<GestureRecognizer> GestureBegan;
        public event Action<GestureRecognizer> GestureExecuting;
        public event Action<GestureRecognizer> GestureEnded;
        public event Action<GestureRecognizer> GestureEndPanding;
        public event Action<GestureRecognizer> GestureFailed;
        public event Action<GestureRecognizer> GesturePossible;

        #region MonoBehaviour

        private void Start()
        {
            SetUpRecognizer();
            FingersScript.Instance.AddGesture(gestureRecognizer);
            gestureRecognizer.StateUpdated += OnGestureStateChanged;
        }


        private void OnDisable()
        {
            if (gestureRecognizer != null)
                gestureRecognizer.StateUpdated -= OnGestureStateChanged;
        }

        #endregion


        /// <summary>
        /// Override this method to instantiate necessary inheritor of GestureRecognizer.
        /// </summary>
        protected abstract void SetUpRecognizer();

        private void OnGestureStateChanged(GestureRecognizer gesture)
        {
            switch (gesture.State)
            {
                case GestureRecognizerState.Began:
                    GestureBegan?.Invoke(gesture);
                    break;
                case GestureRecognizerState.Executing:
                    GestureExecuting?.Invoke(gesture);
                    break;
                case GestureRecognizerState.Ended:
                    GestureEnded?.Invoke(gesture);
                    break;
                case GestureRecognizerState.EndPending:
                    GestureEndPanding?.Invoke(gesture);
                    break;
                case GestureRecognizerState.Failed:
                    GestureFailed?.Invoke(gesture);
                    break;
                case GestureRecognizerState.Possible:
                    GesturePossible?.Invoke(gesture);
                    break;
            }
        } 
    }
}


