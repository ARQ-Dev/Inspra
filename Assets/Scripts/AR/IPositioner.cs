using System;
using UnityEngine;
namespace ARQ.AR.Positioning
{
    public interface IPositioner
    {
        void StartPositioning(GameObject prafabToInstantiate, IPositioningOptions options = null);
        void CancelPositioning();
        event Action<EventArgs> ObjectPositioned;
    }

}
