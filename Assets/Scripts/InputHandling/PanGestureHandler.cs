using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
namespace ARQ.InputHandling
{
    public class PanGestureHandler : GestureHandlerBase
    {
        protected override void SetUpRecognizer()
        {
            gestureRecognizer = new PanGestureRecognizer() { MaximumNumberOfTouchesToTrack = 1 };
        }
    }
}

