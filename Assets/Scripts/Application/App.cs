using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    [SerializeField]
    private LoginViewController _login;

    [SerializeField]
    private ChoiceViewController _choice;

    #region MonoBehaviour

    private void Start()
    {
        var isReadinSuccess = UserManager.Instance.ReadUserData();

        if (isReadinSuccess)
        {
            _choice.Open();
        }
        else
        {
            _login.Open();
        }
    }

    #endregion
}
