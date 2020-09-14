using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
namespace ARQ.InputHandling
{

    public class TapGestureHandler : GestureHandlerBase
    {
        protected override void SetUpRecognizer()
        {
            gestureRecognizer = new TapGestureRecognizer();
        }
    }

}

