using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mover : MonoBehaviour
{
    private List<float> _timingList;

    [SerializeField]
    PlayableDirector _director;

    private void Start()
    {
        _timingList = new List<float> { 0, 19.3f,50.3f,87.4f,101.5f,108.5f,120,140.6f};
    }
    
    public void NextMove()
    {
        var dirTime = _director.time;
        if (dirTime < 141)
        {
            foreach (var timing in _timingList)
            {
                if(timing >= dirTime)
                {
                    _director.time = timing;
                    if (timing != 120)
                    {
                        _director.GetComponent<AudioSource>().Stop();
                        _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                    }
                    else
                    {
                        _director.playableGraph.GetRootPlayable(0).SetSpeed(0);
                    }
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
                if (timing != 120)
                {
                    _director.GetComponent<AudioSource>().Stop();
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                }
                else
                {
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(0);
                }
                break;
            }
        }
    }
}
