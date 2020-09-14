using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
namespace ARQ.InputHandling
{
    public class RotationGestureHandler : GestureHandlerBase
    {
        protected override void SetUpRecognizer()
        {
            gestureRecognizer = new RotateGestureRecognizer();
        }

    }
}

