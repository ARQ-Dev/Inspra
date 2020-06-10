using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingData
{
    public int OpeningsCount { get; set; } = 0;

    public int FirstVisOpeningsCount { get; set; } = 0;

    public int SecondVisOpeningsCount { get; set; } = 0;

    public List<int> FirstVisDepth = new List<int>();

    public List<int> SecondVisDepth = new List<int>();

}
