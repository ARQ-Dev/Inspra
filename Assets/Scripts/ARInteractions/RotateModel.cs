using UnityEngine;
using DigitalRubyShared;
using ARQ.InputHandling;

namespace ARQ.AR.Positioning
{
    public class RotateModel : PositionModifier
    {
        [SerializeField]
        private float _rotationFactor = 0.01f;


        protected override void OnGestureExecuting(GestureRecognizer gesture)
        {

            if (Object == null) return;


            Object.transform.Rotate(0.0f, - gesture.DeltaX * Mathf.Rad2Deg * _rotationFactor, 0.0f);
        }

    }
}


