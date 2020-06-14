using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Mover : MonoBehaviour
{
    private List<int> _timingList;

    [SerializeField]
    PlayableDirector _director;
    

    private void Start()
    {
        _timingList = new List<int> { 0, 7,27,40,46,48,52,69};
    }

    public void NextMove()
    {
        var dirTime = _director.time;
        if (dirTime < 69)
        {
            foreach (var timing in _timingList)
            {
                if(timing > (int)dirTime)
                {
                    _director.time = timing;
                    if(timing!=52)
                        _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                    else
                        _director.playableGraph.GetRootPlayable(0).SetSpeed(0);
                    break;
                }
            }
        }
    }

    public void PrevMove()
    {
        var dirTime = _director.time;
        List<int> reverseTimingList = new List<int>(_timingList);
        reverseTimingList.Reverse();
        foreach (var timing in reverseTimingList)
        {
            if(timing< (int)dirTime-2)
            {
                _director.time = timing;
                if (timing != 52)
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(1);
                else
                    _director.playableGraph.GetRootPlayable(0).SetSpeed(0);
                break;
            }
        }
    }
}
