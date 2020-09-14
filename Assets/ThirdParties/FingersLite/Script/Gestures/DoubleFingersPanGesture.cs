using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DigitalRubyShared;

public class DoubleFingersPanGesture : GestureRecognizer
{

    private void ProcessTouches(bool resetFocus)
    {
        bool firstFocus = CalculateFocus(CurrentTrackedTouches, resetFocus);

        if ((State == GestureRecognizerState.Began || State == GestureRecognizerState.Executing) && TrackedTouchCountIsWithinRange)
        {
            SetState(GestureRecognizerState.Executing);
        }
        else if (firstFocus)
        {
            SetState(GestureRecognizerState.Possible);
        }
        else if (State == GestureRecognizerState.Possible && TrackedTouchCountIsWithinRange)
        {
            float distance = Distance(DistanceX, DistanceY);
            if (distance >= ThresholdUnits && TrackedTouchCountIsWithinRange)
            {
                SetState(GestureRecognizerState.Began);
            }
            else
            {
                SetState(GestureRecognizerState.Possible);
            }
        }
    }

    protected override void TouchesBegan(IEnumerable<GestureTouch> touches)
    {
        ProcessTouches(true);
    }

    protected override void TouchesMoved()
    {
        ProcessTouches(false);
    }

    protected override void TouchesEnded()
    {
        if (State == GestureRecognizerState.Possible)
        {
            SetState(GestureRecognizerState.Failed);
        }
        else
        {
            ProcessTouches(false);
            SetState(GestureRecognizerState.Ended);
        }
    }

    public DoubleFingersPanGesture()
    {
        MinimumNumberOfTouchesToTrack = 2;
        MaximumNumberOfTouchesToTrack = 2;
        ThresholdUnits = 0.2f;
    }

    public float ThresholdUnits { get; set; }


}


