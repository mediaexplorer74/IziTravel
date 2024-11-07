// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.RateHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public class RateHelper
  {
    private const string Prefix = "Rate.";
    private const double HoursToExpired = 2.0;

    public static void Clear()
    {
      IsolatedStorageSettings.ApplicationSettings.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (x => x.Key.StartsWith("Rate.") && RateHelper.IsExpired(x.Value))).Select<KeyValuePair<string, object>, string>((Func<KeyValuePair<string, object>, string>) (x => x.Key)).ToList<string>().ForEach((Action<string>) (x => IsolatedStorageSettings.ApplicationSettings.Remove(x)));
      IsolatedStorageSettings.ApplicationSettings.Save();
    }

    public static void Rate(string uid, string hash)
    {
      IsolatedStorageSettings.ApplicationSettings.Set<string, object>(RateHelper.GetKey(uid, hash), (object) DateTime.Now);
      IsolatedStorageSettings.ApplicationSettings.Save();
    }

    public static bool CanRate(string uid, string hash)
    {
      return RateHelper.IsExpired(IsolatedStorageSettings.ApplicationSettings.Get<string, object>(RateHelper.GetKey(uid, hash)));
    }

    private static string GetKey(string uid, string hash) => "Rate." + uid + "." + hash;

    private static bool IsExpired(object value)
    {
      return !(value is DateTime dateTime) || DateTime.Now - dateTime > TimeSpan.FromHours(2.0);
    }
  }
}
