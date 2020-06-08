using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{

    [SerializeField]
    private Camera _camera;

    #region MonoBehaviour

    void Update()
    {
        HandleClick();
    }

    #endregion


    private void HandleClick()
    {
        //if (Input.touchCount > 0)
        //{
        //    var touch = Input.GetTouch(0);
        //    if (touch.phase != TouchPhase.Began) return;
        //    Ray ray = _camera.ScreenPointToRay(touch.position);
        //    if (Physics.Raycast(ray, out RaycastHit hit))
        //    {
        //        print(hit.transform.gameObject.name);

        //        //hit.transform.gameObject.GetComponent

        //    }
        //}

        if (Input.GetMouseButtonDown(0))
        {

            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print(hit.transform.gameObject.name);

                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Clickable"))
                {
                    var timeLine = FindObjectOfType<stopTimeline>();
                    if (timeLine == null) return;
                    timeLine.UnPause();
                }

            }
        }

    }

}
