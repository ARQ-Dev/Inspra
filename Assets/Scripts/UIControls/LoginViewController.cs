using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class LoginViewController : ViewController
{
    [SerializeField]
    private LoginView _view;

    [SerializeField]
    private ViewController _nextViewController;

    public event Action<string, string> Login;

    #region MonoBehaviour

    private void OnEnable()
    {
        _view.LoginTapped += OnLogin;
    }

    private void OnDisable()
    {
        _view.LoginTapped -= OnLogin;
    }

    #endregion

    #region Methods



    #endregion

    private void OnLogin(string username, string password)
    {
        Login?.Invoke(username, password);
    }

    public void SeccesfuleLogin()
    {
        _view.SendResponse(true);
        Present(_nextViewController);
    }

    public void FailedLogin()
    {
        _view.SendResponse(false);
    }

}
