using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialTextBackground : MonoBehaviour
{
    public void SetBackgroundSize(Vector3 size)
    {
        transform.localScale = size;
    }
}
