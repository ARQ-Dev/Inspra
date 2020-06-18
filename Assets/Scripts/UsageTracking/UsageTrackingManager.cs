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

    private string RefreshToken => NetworkManager.Instance.TokenStorage.refreshToken;

    private ReportsStorage _reportsStorage;

    private ReportsStorage _failedReportsStorage;

    private int _sendedReportsCount = 0;

    #region MonoBehaviour

    private void Start()
    {
        GrabReports();

        //if (_reportContainer.data.Count > 0)
        //{
        //    var reportJson = JsonConvert.SerializeObject(_reportContainer);
        //    NetworkManager.Instance.SendReport(reportJson,
        //        (e, n) =>
        //        {
        //        },
        //        () =>
        //        {
        //            DropContainer();
        //        });
        //}
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
        print($"Session Started");
    }

    public void SessionEnded()
    {
        if (!_isSessionInProgress) return;
        //Timer.TimerReport report = _timer.StopTimer();
        //_currentSessionData.Duration = (int)report.WorkingTime;
        //_currentSessionData.PauseIgnoreDuration = (int)report.WorkingTimeWithoutPause;
        //_currentSessionData.SesseionStartTime = report.StartTime;

        //_reportContainer.data.Add(_currentSessionData);
        //var reportJson = JsonConvert.SerializeObject(_reportContainer);
        //NetworkManager.Instance.SendReport(reportJson,
        //    (e, n) =>
        //    {
        //        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
        //        BsonDataManager.WriteData(path, _reportsStorage);
        //    },
        //    () =>
        //    {
        //        DropContainers();
        //    });

        Timer.TimerReport report = _timer.StopTimer();
        _currentSessionData.Duration = (int)report.WorkingTime;
        _currentSessionData.PauseIgnoreDuration = (int)report.WorkingTimeWithoutPause;
        _currentSessionData.SesseionStartTime = report.StartTime;

        if (_reportsStorage.reports.TryGetValue(RefreshToken, out ReportContainer reportContainer))
        {
            reportContainer.data.Add(_currentSessionData);
        }
        else
        {
            var container = new ReportContainer();
            container.data.Add(_currentSessionData);
            _reportsStorage.reports.Add(RefreshToken, container);
        }

        _sendedReportsCount = 0;
        _failedReportsStorage = new ReportsStorage();
        foreach (var pair in _reportsStorage.reports)
        {
            var reportJson = JsonConvert.SerializeObject(pair.Value);
            NetworkManager.Instance.SendReport(reportJson, pair.Key,
                //On Token Update Failed
                (e, n) =>
                {

                },
                //On Sending Failed
                (e, n) =>
                {
                    _failedReportsStorage.reports.Add(pair.Key, pair.Value);
                },
                //On Sending Seccess
                () =>
                {

                },
                //On Sending Complete
                () =>
                {
                    OnReportSendingCompleat();
                });
        }

        _isSessionInProgress = false;
    }

    private void OnReportSendingCompleat()
    {
        _sendedReportsCount++;
        if (_sendedReportsCount == _reportsStorage.reports.Count)
        {

        }
    }

    private void GrabReports()
    {
        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
        if (File.Exists(path))
        {
            var storage = BsonDataManager.ReadData<ReportsStorage>(path);
            if (!storage.reports.ContainsKey(RefreshToken))
            {
                storage.reports.Add(RefreshToken, new ReportContainer());
            }
        }
        else
        {
            _reportsStorage = new ReportsStorage();
            _reportsStorage.reports.Add(RefreshToken, new ReportContainer());
        }
    }

    private void DropContainers()
    {
        var path = Path.Combine(Application.persistentDataPath, REPORT_CONTAINER_FILENAME);
        BsonDataManager.DeleteData(path);

        _reportsStorage = new ReportsStorage();
        _reportsStorage.reports.Add(RefreshToken, new ReportContainer());
    }
}
