using UnityEngine;
using DigitalRubyShared;
using ARQ.InputHandling;

public class ScaleModel : PositionModifier
{

    private ScaleGestureRecognizer _scaleRecognizer;

    protected override void OnGestureBegan(GestureRecognizer gesture)
    {
        _scaleRecognizer = _gestureHandler.GestureRecognizer as ScaleGestureRecognizer;
    }

    protected override void OnGestureExecuting(GestureRecognizer gesture)
    {

        print($"Scale object: {Object.name}");

        if (Object == null) return;
        if (_scaleRecognizer == null) return;
        Object.transform.localScale *= _scaleRecognizer.ScaleMultiplier;
    }

}
