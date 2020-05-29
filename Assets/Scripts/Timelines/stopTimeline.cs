using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class stopTimeline : MonoBehaviour
{
    public PlayableDirector director;
    public Animator Aaldosterone_level_right;
    public Animator Male;
    public Animator Canv;

    void OnEnable()
    {
        Aaldosterone_level_right.SetTrigger("TriggerFlash");
        Male.SetTrigger("TriggerFlash");
        Canv.SetTrigger("TriggerFlash");
        director.Pause();
    }

    public void UnPause()
    {
        Aaldosterone_level_right.ResetTrigger("TriggerFlash");
        Aaldosterone_level_right.SetTrigger("Unflash");
        Male.ResetTrigger("TriggerFlash");
        Male.SetTrigger("Unflash");
        Canv.ResetTrigger("TriggerFlash");
        Canv.SetTrigger("Unpause");
        director.Resume();
    }
    
}
