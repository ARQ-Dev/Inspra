using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginViewController : ViewController
{
    [SerializeField]
    private LoginView _view;

    [SerializeField]
    private ViewController _nextViewController;

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
        if (username == "admin" &&
            password == "admin")
        {
           UserManager.Instance.WriteUserData(username, password);
            _view.SendResponse(true);
            Present(_nextViewController, null);
        }
        else
        {
            _view.SendResponse(false);
        }

    }

}
