using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

public static class BsonDataManager
{
    public static T ReadData<T>(string path) where T: class
    {
        //if (!File.Exists(path)) return null;

        //var json = File.ReadAllText(path);
        //return JsonConvert.DeserializeObject<T>(json);

        var bytes = File.ReadAllBytes(path);

        if (bytes == null) return null;

        MemoryStream ms = new MemoryStream(bytes);
        using (BsonReader reader = new BsonReader(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize<T>(reader);
        }
    }

    public static void WriteData<T>(string path, T data) where T : class
    {
        //var json = JsonConvert.SerializeObject(data);
        //File.WriteAllText(path, json);

        MemoryStream ms = new MemoryStream();
        using (BsonWriter writer = new BsonWriter(ms))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(writer, data);
        }
        var bytes = ms.ToArray();
        File.WriteAllBytes(path, bytes);
    }

    public static void DeleteData(string path)
    {
        File.Delete(path);
    }
}
