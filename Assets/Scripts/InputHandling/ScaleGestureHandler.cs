using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
namespace ARQ.InputHandling
{
    public class ScaleGestureHandler : GestureHandlerBase
    {
        protected override void SetUpRecognizer()
        {
            gestureRecognizer = new ScaleGestureRecognizer();
        }

    }

}
