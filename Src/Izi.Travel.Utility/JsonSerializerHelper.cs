// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.JsonSerializerHelper
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

#nullable disable
namespace Izi.Travel.Utility
{
  public static class JsonSerializerHelper
  {
    public static string Serialize<T>(T obj) => JsonConvert.SerializeObject((object) obj);

    public static T Deserialize<T>(string data) => JsonConvert.DeserializeObject<T>(data);

    public static object Deserialize(string data) => JsonConvert.DeserializeObject(data);

    public static T Deserialize<T>(Stream stream)
    {
      using (StreamReader reader1 = new StreamReader(stream))
      {
        using (JsonReader reader2 = (JsonReader) new JsonTextReader((TextReader) reader1))
          return new JsonSerializer().Deserialize<T>(reader2);
      }
    }

    public static object Deserialize(Stream stream, Type type)
    {
      using (StreamReader reader1 = new StreamReader(stream))
      {
        using (JsonReader reader2 = (JsonReader) new JsonTextReader((TextReader) reader1))
          return new JsonSerializer().Deserialize(reader2, type);
      }
    }

    public static byte[] SerializeToByteArray<T>(
      T obj,
      JsonSerializerSettings jsonSerializerSettings = null)
    {
      return Encoding.UTF8.GetBytes(jsonSerializerSettings != null ? JsonConvert.SerializeObject((object) obj, jsonSerializerSettings) : JsonConvert.SerializeObject((object) obj));
    }

    public static T DeserializeFromByteArray<T>(byte[] data)
    {
      return data != null ? JsonSerializerHelper.Deserialize<T>(Encoding.UTF8.GetString(data, 0, data.Length)) : default (T);
    }
  }
}
