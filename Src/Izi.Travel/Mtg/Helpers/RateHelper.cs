// ********************************************************************
// Type: Izi.Travel.Mtg.Helpers.RateHelper
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

#nullable disable
namespace Izi.Travel.Mtg.Helpers
{
  public class RateHelper
  {
    private const string Prefix = "Rate.";
    private const double HoursToExpired = 2.0;

    public static void Clear()
    {
      var settings = ApplicationData.Current.LocalSettings;
      var keysToRemove = settings.Values.Where(kvp => kvp.Key.StartsWith(Prefix) && IsExpired(kvp.Value)).Select(kvp => kvp.Key).ToList();
      foreach (var key in keysToRemove)
      {
        settings.Values.Remove(key);
      }
    }

    public static void Rate(string uid, string hash)
    {
      var settings = ApplicationData.Current.LocalSettings;
      settings.Values[GetKey(uid, hash)] = DateTime.Now.ToString("o");
    }

    public static bool CanRate(string uid, string hash)
    {
      var settings = ApplicationData.Current.LocalSettings;
      object value;
      settings.Values.TryGetValue(GetKey(uid, hash), out value);
      return IsExpired(value);
    }

    private static string GetKey(string uid, string hash) => "Rate." + uid + "." + hash;

    private static bool IsExpired(object value)
    {
      if (value is DateTime dt)
        return DateTime.Now - dt > TimeSpan.FromHours(HoursToExpired);
      if (value is string s && DateTime.TryParse(s, out var parsed))
        return DateTime.Now - parsed > TimeSpan.FromHours(HoursToExpired);
      return true;
    }
  }
}
