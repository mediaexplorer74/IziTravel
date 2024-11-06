// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Helpers.Extensions
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using Newtonsoft.Json;
using Splunk.Mi.Utilities;
using System;

#nullable disable
namespace BugSense.Core.Helpers
{
  public static class Extensions
  {
    private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

    public static string SerializeToJson<T>(this T obj)
    {
      try
      {
        return JsonConvert.SerializeObject((object) obj);
      }
      catch (Exception ex)
      {
        ConsoleManager.LogToConsole(string.Format("Serialization FAILED: {0}", (object) ex));
      }
      return (string) null;
    }

    public static T DeserializeJson<T>(this string jsonData) where T : class
    {
      try
      {
        return JsonConvert.DeserializeObject<T>(jsonData);
      }
      catch (Exception ex)
      {
        ConsoleManager.LogToConsole(string.Format("Deserialization FAILED: {0}", (object) ex));
        return default (T);
      }
    }

    public static double DateTimeToUnixTimestamp(this DateTime dateTime)
    {
      return (dateTime - new DateTime(1970, 1, 1).ToLocalTime()).TotalSeconds;
    }

    public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
    {
      return new DateTime(1970, 1, 1, 0, 0, 0, 0).AddSeconds(unixTimeStamp).ToLocalTime();
    }

    public static long GetCurrentUnixTimestampMillis()
    {
      return (long) (DateTime.UtcNow - Extensions.UnixEpoch).TotalMilliseconds;
    }

    public static DateTime DateTimeFromUnixTimestampMillis(this long millis)
    {
      return Extensions.UnixEpoch.AddMilliseconds((double) millis);
    }

    public static long GetCurrentUnixTimestampSeconds()
    {
      return (long) (DateTime.UtcNow - Extensions.UnixEpoch).TotalSeconds;
    }

    public static DateTime DateTimeFromUnixTimestampSeconds(this long seconds)
    {
      return Extensions.UnixEpoch.AddSeconds((double) seconds);
    }
  }
}
