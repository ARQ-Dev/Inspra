using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;
public class TrackingData
{
    [JsonProperty("date")]
    public DateTime SesseionStartTime { get; set; }
    [JsonProperty("visualNumber")]
    public int VisualizationNumber { get; set; }
    [JsonProperty("time")]
    public int Duration { get; set; }
    [JsonProperty("timeWithoutPause")]
    public int PauseIgnoreDuration { get; set; }
}
