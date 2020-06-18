using System.Collections.Generic;
using Newtonsoft.Json;
public class ReportsStorage
{
    [JsonProperty("reports")]
    public Dictionary<string, ReportContainer> reports = new Dictionary<string, ReportContainer>();
}
