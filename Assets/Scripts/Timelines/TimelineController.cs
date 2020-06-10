using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimelineController : MonoBehaviour
{
    private stopTimeline _stopTimeline;
    private StopTimeline2 _stopTimeline2;

    public void FindTimelineController()
    {
        _stopTimeline = FindObjectOfType<stopTimeline>();
        _stopTimeline2 = FindObjectOfType<StopTimeline2>();
    }

    public void Pause()
    {
        if (_stopTimeline != null)
            _stopTimeline.Pause();

        if (_stopTimeline2 != null)
            _stopTimeline2.Pause();
    }

    public void UnPause()
    {
        if (_stopTimeline != null)
            _stopTimeline.UnPause();

        if (_stopTimeline2 != null)
            _stopTimeline2.UnPause();
    }

}
