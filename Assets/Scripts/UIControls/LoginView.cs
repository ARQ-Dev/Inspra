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

    [SerializeField]
    private SlidingText _slidingText;

    public event Action<string, string> LoginTapped;

    #region Methods

    public void Login()
    {
        var username = _usernameField.text;
        var password = _passwordField.text;
        LoginTapped?.Invoke(username, password);
        Clean();
    }

    #endregion

    public void SendResponse(bool isDataCorrect)
    {
        if (!isDataCorrect)
        {
            _slidingText.Show();
        }
    }

    private void Clean()
    {
        _usernameField.text = "";
        _passwordField.text = "";
    }

}
