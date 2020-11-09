using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRubyShared;
using ARQ.InputHandling;
public abstract class PositionModifier : GestureHandlerClient
{
    public GameObject Object { get; set; }
}
