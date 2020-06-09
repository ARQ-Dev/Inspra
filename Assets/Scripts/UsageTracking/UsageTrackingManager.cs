using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsageTrackingManager : Singleton<UsageTrackingManager>
{

    [SerializeField]
    private TimeCounter _counter;

    #region MonoBehaviour

    private void OnApplicationPause(bool pause)
    {

    }

    private void OnApplicationQuit()
    {

    }

    #endregion

    #region Methods


    #endregion

}
