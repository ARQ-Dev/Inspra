using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefreshTokenResponse
{
    public string message;
    public string token;

    public override string ToString()
    {
        var str = $"message : {message}," +
            $"token: {token}";
        return str;
    }
}
