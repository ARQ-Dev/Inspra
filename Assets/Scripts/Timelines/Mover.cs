using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mover : MonoBehaviour
{
    private List<float> _timingList;

    [SerializeField]
    private PlayableDirector _director;

    [SerializeField]
    private GameObject Button1;
    [SerializeField]
    private GameObject Button2;

    private void Start()
    {
        _timingList = new List<float> { 0, 23.5f, 54.52f, 93.589410430839f, 112.55253968254f, 122.489410430839f, 133.96537414966f, 155.439410430839f };
    }
    
    public void NextMove()
    {
        
        var dirTime = _director.time;
        if (dirTime < 156)
        {
            foreach (var timing in _timingList)
            {
                if(timing > dirTime)
                {
                    if (_director.GetComponent<PauseController>().TimelineWasPaused())
                    {
                        Button1.SetActive(true);
                        Button2.SetActive(false);
                    }
                    _director.time = timing;
                        stopTimeline._firstTimeLineWasPaused = false;
                        _director.GetComponent<AudioSource>().Stop();
                        _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                    
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
            if(timing< dirTime-2)
            {
                _director.time = timing;
                if (_director.GetComponent<PauseController>().TimelineWasPaused())
                {
                    Button1.SetActive(true);
                    Button2.SetActive(false);
                }
                stopTimeline._firstTimeLineWasPaused = false;
                    _director.GetComponent<AudioSource>().Stop();
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                
                break;
            }
        }
    }
}
