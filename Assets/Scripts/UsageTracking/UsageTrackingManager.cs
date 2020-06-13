using UnityEngine;
using System.IO;
using Newtonsoft.Json;



public class UsageTrackingManager : Singleton<UsageTrackingManager>
{
    #region SerializedFields

    [SerializeField]
    private VisualizationInstantiator _instantiator;

    [SerializeField]
    private TimeCounter _counter;

    [SerializeField]
    private Timer _timer;

    private ReportsContainer _reportContainer;

    #endregion

    const string REPORT_CONTAINER_FILENAME = "report-cantainer";

    private TrackingData _currentSessionData;

    public double AcceptableBackgroundTime { get; set; } = 30;

    private bool _isSessionInProgress = false;

    #region MonoBehaviour

    private void Start()
    {
        GrabReports();

        if (_reportContainer.data.Count > 0)
        {
            var reportJson = JsonConvert.SerializeObject(_reportContainer);
            NetworkManager.Instance.SendReport(reportJson,
                (e, n) =>
                {
                },
                () =>
                {
                    DropContainer();
                });
        }
    }

    private void OnEnable()
    {
        _instantiator.Instantiaded += OnVisualizationInstantiated;
    }

    private void OnDisable()
    {
        _instantiator.Instantiaded -= OnVisualizationInstantiated;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            if (_counter.SinceLastBackgroundTime > AcceptableBackgroundTime)
            {
                SessionEnded();
            }
        }
    }

    private void OnApplicationQuit()
    {
        SessionEnded();
    }

    #endregion

    private void OnVisualizationInstantiated(GameObject go)
    {
        var visualization = go.GetComponent<Visualization>();

        if (visualization != null)
            SessionStarted(visualization.Number);

    }

    private void SessionStarted(int number)
    {
        _currentSessionData = new TrackingData();
        _currentSessionData.VisualizationNumber = number;
        
        _timer.StartTimer();
        _isSessionInProgress = true; 
    }

    public void SessionEnded()
    {
        if (!_isSessionInProgress) return;
        Timer.TimerReport report = _timer.StopTimer();
        _currentSessionData.Duration = (int)report.WorkingTime;
        _currentSessionData.PauseIgnoreDuration = (int)report.WorkingTimeWithoutPause;
        _currentSessionData.SesseionStartTime = report.StartTime;

        _reportContainer.data.Add(_currentSessionData);
        var reportJson = JsonConvert.SerializeObject(_reportContainer);
        NetworkManager.Instance.SendReport(reportJson,
            (e, n) =>
            {
                print($"Error number: {n} " +
                    $"Error msg: {e}");
                var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
                BsonDataManager.WriteData(path, _reportContainer);
            },
            () =>
            {
                print(reportJson);
                DropContainer();
            });

        _isSessionInProgress = false;
    }
    private void GrabReports()
    {
        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);

        if (File.Exists(path))
        {
            var container = BsonDataManager.ReadData<ReportsContainer>(path);
            _reportContainer = container == null ? new ReportsContainer() : container;
        }
        else
        {
            _reportContainer = new ReportsContainer();
        }
    }

    private void DropContainer()
    {
        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
        BsonDataManager.DeleteData(path);

        _reportContainer = new ReportsContainer();
    }
}
