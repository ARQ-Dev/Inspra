using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginResponse
{
    public string message;
    public string token;
    public string refreshToken;
    public string role;

    public override string ToString()
    {
        var str = $"message : {message}," +
            $"token: {token}" +
            $"refreshToken: {refreshToken}" +
            $"role: {role}";
        return str;
    }
}
