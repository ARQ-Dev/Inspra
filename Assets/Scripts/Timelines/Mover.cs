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
        _timingList = new List<float> { 3, 19.3f,50.3f,71.7f,108.4f, 118.35f, 129.8f, 151.3f};
    }
    
    public void NextMove()
    {
        var dirTime = _director.time;
        if (dirTime < 152)
        {
            foreach (var timing in _timingList)
            {
                if(timing > dirTime)
                {
                    _director.time = timing;
                    if (timing == 129.8f)
                        stopTimeline.peremotka = true;
                    else
                        stopTimeline.peremotka = false;
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
                if (timing == 129.8f)
                    stopTimeline.peremotka = true;
                else
                    stopTimeline.peremotka = false;
                stopTimeline._firstTimeLineWasPaused = false;
                    _director.GetComponent<AudioSource>().Stop();
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                
                break;
            }
        }
    }
}
