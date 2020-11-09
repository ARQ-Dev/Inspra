using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
public class ReportContainer
{
    [JsonProperty("data")]
    public List<TrackingData> data = new List<TrackingData>();
}
