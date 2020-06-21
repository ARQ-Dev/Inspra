using UnityEngine;
using System.IO;
using System.Collections.Generic;
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


    #endregion

    const string REPORT_CONTAINER_FILENAME = "reports-storage";

    private TrackingData _currentSessionData;

    public double AcceptableBackgroundTime { get; set; } = 30;

    private bool _isSessionInProgress = false;

    private string UserName => NetworkManager.Instance.UserDataStorage.login;

    private ReportsStorage _reportsStorage;

    #region MonoBehaviour

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

    #region Methods

    public void GrabReports()
    {
        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
        if (File.Exists(path))
        {
            _reportsStorage = BsonDataManager.ReadData<ReportsStorage>(path);
            if (!_reportsStorage.reports.ContainsKey(UserName))
            {
                _reportsStorage.reports.Add(UserName, new ReportContainer());
            }
        }
        else
        {
            _reportsStorage = new ReportsStorage();
            _reportsStorage.reports.Add(UserName, new ReportContainer());
        }

        TrySendReport();

    }

    public void SessionEnded()
    {
        if (_isSessionInProgress)
        {
            Timer.TimerReport report = _timer.StopTimer();
            _currentSessionData.Duration = (int)report.WorkingTime;
            _currentSessionData.PauseIgnoreDuration = (int)report.WorkingTimeWithoutPause;
            _currentSessionData.SesseionStartTime = report.StartTime;

            ReportContainer container;

            if (_reportsStorage.reports.TryGetValue(UserName, out container))
            {
                container.data.Add(_currentSessionData);
            }
            else
            {
                container = new ReportContainer();
                container.data.Add(_currentSessionData);
                _reportsStorage.reports.Add(UserName, container);
            }

        }

        TrySendReport();

        _isSessionInProgress = false;
    }

    public void TrySendReport()
    {
        if (_reportsStorage == null) return;
        if (_reportsStorage.reports.TryGetValue(UserName, out ReportContainer container))
        {
            if (container.data.Count < 1) return;

            string report = JsonConvert.SerializeObject(container);

            var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);

            NetworkManager.Instance.SendReport(report,
                (e, n) =>
                {
                    BsonDataManager.WriteData(path, _reportsStorage);
                },
                () =>
                {
                    _reportsStorage.reports.Remove(UserName);
                    BsonDataManager.WriteData(path, _reportsStorage);
                });

        }
    }

    #endregion


    private void SessionStarted(int number)
    {
        _currentSessionData = new TrackingData();
        _currentSessionData.VisualizationNumber = number;

        _timer.StartTimer();
        _isSessionInProgress = true;
    }

    private void OnVisualizationInstantiated(GameObject go)
    {
        var visualization = go.GetComponent<Visualization>();

        if (visualization != null)
            SessionStarted(visualization.Number);

    }
}
