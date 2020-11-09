using System.Collections;
using System.Collections.Generic;
using DigitalRubyShared;
using UnityEngine;

namespace ARQ.InputHandling
{

    public abstract class GestureHandlerClient : MonoBehaviour
    {
        [SerializeField]
        protected GestureHandlerBase _gestureHandler;



        #region MonoBehaviour

        private void OnEnable()
        {
            _gestureHandler.GestureBegan += OnGestureBegan;
            _gestureHandler.GestureExecuting += OnGestureExecuting;
            _gestureHandler.GestureEnded += OnGestureEnded;
            _gestureHandler.GestureEndPanding += OnGestureEndPanding;
            _gestureHandler.GestureFailed += OnGestureFailed;
            _gestureHandler.GesturePossible += OnGesturePossible;
        }

        private void OnDisable()
        {
            _gestureHandler.GestureBegan -= OnGestureBegan;
            _gestureHandler.GestureExecuting -= OnGestureExecuting;
            _gestureHandler.GestureEnded -= OnGestureEnded;
            _gestureHandler.GestureEndPanding -= OnGestureEndPanding;
            _gestureHandler.GestureFailed -= OnGestureFailed;
            _gestureHandler.GesturePossible -= OnGesturePossible;
        }

        #endregion

        protected virtual void OnGestureBegan(GestureRecognizer gesture) { }
        protected virtual void OnGestureExecuting(GestureRecognizer gesture) { }
        protected virtual void OnGestureEnded(GestureRecognizer gesture) { }
        protected virtual void OnGestureEndPanding(GestureRecognizer gesture) { }
        protected virtual void OnGestureFailed(GestureRecognizer gesture) { }
        protected virtual void OnGesturePossible(GestureRecognizer gesture) { }

    }


}

