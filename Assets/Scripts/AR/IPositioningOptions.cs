using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ARQ.AR.Positioning
{
    public interface IPositioningOptions
    {
        GameObject[] ObjectsToActivateFirst{ get; set; }
    }
}

