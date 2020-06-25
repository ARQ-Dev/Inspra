using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ViewController : MonoBehaviour
{
    private EFE_Base _panelController;

    #region Methods

    public void Open()
    {

        if (_panelController == null)
            _panelController = FindObjectOfType<EFE_Base>();

        _panelController.OpenPanel(gameObject);
        OnOpened();
    }

    protected void Awake()
    {
        if (_panelController == null)
            _panelController = FindObjectOfType<EFE_Base>();
    }

    #endregion

    protected void Present(ViewController viewController)
    {
        _panelController.OpenPanel(viewController.gameObject);
        viewController.OnPresended();
    }

    protected virtual void OnPresended()
    {

    }

    protected virtual void OnOpened()
    {
    }

}
