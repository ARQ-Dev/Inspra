using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ARQ.InputHandling;

public class DoubleFingersPanGestureHandler : GestureHandlerBase
{
    protected override void SetUpRecognizer()
    {
        gestureRecognizer = new DoubleFingersPanGesture();
    }
}
