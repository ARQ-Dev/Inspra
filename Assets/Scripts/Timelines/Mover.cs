using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mover : MonoBehaviour
{
    private List<float> _timingList;

    [SerializeField]
    private PlayableDirector _director;

    private void Start()
    {
        _timingList = new List<float> { 0, 30.5f, 61.52f, 100.589410430839f, 119.55253968254f, 129.489410430839f, 140.96537414966f, 162.439410430839f };
    }
    
    public void NextMove()
    {
        var dirTime = _director.time;
        if (dirTime < 164)
        {
            foreach (var timing in _timingList)
            {
                if(timing > dirTime)
                {
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
               
                stopTimeline._firstTimeLineWasPaused = false;
                    _director.GetComponent<AudioSource>().Stop();
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                
                break;
            }
        }
    }
}
