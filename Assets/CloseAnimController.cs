using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseAnimController : MonoBehaviour
{
    [SerializeField]
    private Animator _closeAnimator;

    public void StartAnim()
    {
        _closeAnimator.SetTrigger("CloseTrigger");
    }

}
