// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.Extensions.TimeExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;

#nullable disable
namespace RestSharp.Authenticators.OAuth.Extensions
{
  internal static class TimeExtensions
  {
    public static DateTime FromNow(this TimeSpan value)
    {
      return new DateTime((DateTime.Now + value).Ticks);
    }

    public static DateTime FromUnixTime(this long seconds)
    {
      DateTime dateTime = new DateTime(1970, 1, 1);
      dateTime = dateTime.AddSeconds((double) seconds);
      return dateTime.ToLocalTime();
    }

    public static long ToUnixTime(this DateTime dateTime)
    {
      return (long) (dateTime - new DateTime(1970, 1, 1)).TotalSeconds;
    }
  }
}
