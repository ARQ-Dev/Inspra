using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserName { get; set; }
    public string Password { get; set; }

    public UserData(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

}
