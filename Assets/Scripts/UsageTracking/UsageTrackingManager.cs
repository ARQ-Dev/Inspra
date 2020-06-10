using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;


public class UsageTrackingManager : Singleton<UsageTrackingManager>
{
    const string TRACKING_DATA_FILENAME = "usagetrackingdata";

    private TrackingData _trackingData;

    [SerializeField]
    private TimeCounter _counter;

    #region MonoBehaviour

    #endregion

    #region Methods

    public void SendTrackingData()
    {

    }

    public void AddOpeningsCount()
    {
        _trackingData.OpeningsCount++;
    }

    public void AddFirstVisCount()
    {
        _trackingData.FirstVisOpeningsCount++;
    }

    public void AddSecondVisCount()
    {
        _trackingData.SecondVisOpeningsCount++;
    }

    public void AddFirstVisDepth(int index)
    {
        if (index > _trackingData.FirstVisDepth.Count)
            return;
        _trackingData.FirstVisDepth[index]++;
    }

    public void AddSecondVisDepth(int index)
    {
        if (index > _trackingData.SecondVisDepth.Count)
            return;
        _trackingData.SecondVisDepth[index]++;
    }

    public void InitializeTrackingManager()
    {
        var isReaded = ReadUserData();
        if (!isReaded)
            _trackingData = new TrackingData();
    }

    #endregion

    private bool ReadUserData()
    {
        var path = Path.Combine(Application.persistentDataPath, TRACKING_DATA_FILENAME);

        if (!File.Exists(path)) return false;

        var bytes = File.ReadAllBytes(path);

        if (bytes == null) return false;

        MemoryStream ms = new MemoryStream(bytes);
        using (BsonReader reader = new BsonReader(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            _trackingData = serializer.Deserialize<TrackingData>(reader);
        }

        return _trackingData != null;
    }

    private void DeleteTrackingData()
    {
        _trackingData = new TrackingData();
        var path = Path.Combine(Application.persistentDataPath, TRACKING_DATA_FILENAME);
        File.Delete(path);
    }        


}
