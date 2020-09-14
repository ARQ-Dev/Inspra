using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using DigitalRubyShared;
using ARQ.InputHandling;

namespace ARQ.AR.Positioning
{
    public class PositioningWithPan : PositionModifier
    {
        [SerializeField]
        private Camera _camera;

        [SerializeField]
        private ARRaycastManager _raycastManager;

        private bool _objectSelected;
        private bool _positionIsValid = false;

        private Vector3 _offset = Vector3.zero;

        #region MonoBehaviour

        #endregion

        #region GestureHandlerClient

    

        protected override void OnGestureBegan(GestureRecognizer gesture)
        {
            if (Object == null) return;
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(new Vector3(gesture.StartFocusX, gesture.StartFocusY, 0));
            _objectSelected = Physics.Raycast(ray, out hit);
            _offset = hit.point - Object.transform.position;
        }

        protected override void OnGestureExecuting(GestureRecognizer gesture)
        {
            print($"Positioning object: {Object.name}");

            if (Object == null) return;
            if (!_objectSelected) return;

            var hits = new List<ARRaycastHit>();
            _raycastManager.Raycast(new Vector2(gesture.FocusX, gesture.FocusY), hits, TrackableType.PlaneEstimated);
            _positionIsValid = hits.Count > 0;
            if (!_positionIsValid) return;

            Object.transform.position = hits[0].pose.position - _offset;
        }

        protected override void OnGestureEnded(GestureRecognizer gesture)
        {
            _objectSelected = false;
            _offset = Vector3.zero;
        }

        protected override void OnGestureEndPanding(GestureRecognizer gesture)
        {
            _objectSelected = false;
            _offset = Vector3.zero;
        }

        protected override void OnGestureFailed(GestureRecognizer gesture)
        {
            _objectSelected = false;
            _offset = Vector3.zero;
        }
        #endregion
    }
}


