using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstHeadersController : MonoBehaviour
{
    [SerializeField]
    ARQ.AR.Positioning.PlanePositioner _planePositioner;

    [SerializeField]
    List<GameObject> Headers;

    private void Start()
    {
        _planePositioner.ObjectPositioned += HideFirstHeaders;
    }

    public void HideFirstHeaders(EventArgs args)
    {
        foreach(var header in Headers)
        {
            header.SetActive(false);
        }
        GetComponent<Image>().enabled = false;
    }


}
