using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    private EFE_Base _panelController;

    #region Methods

    public void Open()
    {
        _panelController.OpenPanel(gameObject);
    }

    protected void Awake()
    {
        _panelController = FindObjectOfType<EFE_Base>();
    }

    #endregion

    protected void Present(ViewController viewController)
    {
        _panelController.OpenPanel(viewController.gameObject);
    }


}
