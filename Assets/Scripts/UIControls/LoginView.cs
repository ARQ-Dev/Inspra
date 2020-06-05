using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : ViewController
{
    [SerializeField]
    private InputField _usernameField;

    [SerializeField]
    private InputField _passwordField;

    public event Action<string, string> LoginTapped;

    #region Methods

    public void Login()
    {
        var username = _usernameField.text;
        var password = _passwordField.text;
        LoginTapped?.Invoke(username, password);
    }

    #endregion

    public void SendResponse(bool isDataCorrect)
    {

    }

}
