using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stopTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Aaldosterone_level_right;
    public Animator Male;

    void OnEnable()
    {
        Aaldosterone_level_right.SetTrigger("TriggerFlash");
        Male.SetTrigger("TriggerFlash");
        director.Pause();
    }

    public void UnPause()
    {
        Aaldosterone_level_right.ResetTrigger("TriggerFlash");
        Male.ResetTrigger("TriggerFlash");
        director.Resume();
    }
    
}
