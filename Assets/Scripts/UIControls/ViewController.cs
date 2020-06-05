using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    #region Methods

    public void Open()
    {
        gameObject.SetActive(true);
    }

    #endregion

    protected void Present(ViewController viewController, object message)
    {
        viewController.gameObject.SetActive(true);
        viewController.HandlePresentationMessage(message);
        gameObject.SetActive(false);

    }

    protected virtual void HandlePresentationMessage(object message)
    {

    }

}
