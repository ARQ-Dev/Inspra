using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class MoverSecond : MonoBehaviour
{
    private List<float> _timingList;

    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private StopTimeline2 _ControllerTimeLine;

    [SerializeField]
    private GameObject Button1;
    [SerializeField]
    private GameObject Button2;

    public static bool _ItIsPeremotka = false;

    private void Start()
    {
        _timingList = new List<float> { 0.0f, 42.86f, 105.2f, 128.18f };
    }
    public void NextMove()
    {

        var dirTime = _director.time;
        if (dirTime < 128)
        {
            foreach (var timing in _timingList)
            {
                if (timing > dirTime)
                {
                    if( timing == 105.2f  && timing > dirTime+5 || timing == 128.18f)
                        _ItIsPeremotka = true;
                    else
                        _ItIsPeremotka = false;
                    if (_director.GetComponent<PauseController>().TimelineWasPaused())
                    {
                        Button1.SetActive(true);
                        Button2.SetActive(false);
                        _director.GetComponent<PauseController>().UnPause();
                    }
                    _director.time = timing;
                    //stopTimeline._firstTimeLineWasPaused = false;
                    _director.GetComponent<AudioSource>().Stop();
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                    _ControllerTimeLine.CancelInvoke();
                    _director.gameObject.GetComponent<Visualization>().ResumeBackMusic();
                    break;
                }
            }
        }
    }

    public void PrevMove()
    {
        var dirTime = _director.time;
        List<float> reverseTimingList = new List<float>(_timingList);
        reverseTimingList.Reverse();
        foreach (var timing in reverseTimingList)
        {
            if (timing < dirTime - 2)
            {
                _director.time = timing;
                _ItIsPeremotka = false;
                if (_director.GetComponent<PauseController>().TimelineWasPaused())
                {
                    Button1.SetActive(true);
                    Button2.SetActive(false);
                }
                //stopTimeline._firstTimeLineWasPaused = false;
                _director.GetComponent<AudioSource>().Stop();
                _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                _ControllerTimeLine.CancelInvoke();
                _director.gameObject.GetComponent<Visualization>().ResumeBackMusic();
                break;
            }
        }
    }
}
