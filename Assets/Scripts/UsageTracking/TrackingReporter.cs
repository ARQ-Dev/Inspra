using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TrackingReporter : MonoBehaviour
{

    private const string REPORT_MARK_SUBSTRING = "-report";
    private List<TrackingData> _unsentReports = new List<TrackingData>();


    #region Methods

    public void GrabReports()
    {
        var contentPaths = Directory.GetFiles(Application.persistentDataPath);
        foreach (var path in contentPaths)
        {
            if (!path.Contains(REPORT_MARK_SUBSTRING))
                continue;
            var data = BsonDataManager.ReadData<TrackingData>(path);
            _unsentReports.Add(data);
        }
    }

    public void SendReports()
    {

    }

    #endregion

}
