using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

public class UserManager : Singleton<UserManager>
{
    public UserData UserData { get; private set; } = null;

    const string USER_DATA_FILENAME = "userdata";

    #region Methods

    public bool ReadUserData()
    {
        var path = Path.Combine(Application.persistentDataPath, USER_DATA_FILENAME);

        if (!File.Exists(path)) return false;

        var bytes = File.ReadAllBytes(path);

        if (bytes == null) return false;

        MemoryStream ms = new MemoryStream(bytes);
        using (BsonReader reader = new BsonReader(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            UserData = serializer.Deserialize<UserData>(reader);
        }

        return UserData != null;
    }

    public void WriteUserData(string userName, string password)
    {
        UserData = new UserData(userName, password);

        MemoryStream ms = new MemoryStream();
        using (BsonWriter writer = new BsonWriter(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, UserData);
        }

        var path = Path.Combine(Application.persistentDataPath, USER_DATA_FILENAME);

        var bytes = ms.ToArray();
        File.WriteAllBytes(path, bytes);
    }

    public void DeleteUserData()
    {
        UserData = null;
        var path = Path.Combine(Application.persistentDataPath, USER_DATA_FILENAME);
        File.Delete(path);
    }

    #endregion

}
