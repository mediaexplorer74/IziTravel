﻿// Decompiled with JetBrains decompiler
// Type: BugSense.Core.BugSenseEventFactory
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Globalization;

#nullable disable
namespace BugSense.Core
{
  internal static class BugSenseEventFactory
  {
    public static string CreateBugSenseEvent(BugSenseEventTag eventTag)
    {
      return BugSenseProperties.BugSenseVersion + ":" + (eventTag.Equals((object) BugSenseEventTag.Ping) ? "_ping" : "_gnip") + ":" + BugSenseProperties.PhoneModel + ":" + BugSenseProperties.PhoneBrand + ":" + BugSenseProperties.OSVersion + ":" + BugSenseProperties.AppVersion + ":" + CultureInfo.CurrentCulture.TwoLetterISOLanguageName + ":" + (object) (DateTime.UtcNow.Ticks / 10000000L);
    }

    public static string CreateBugSenseEvent(string tag)
    {
      if (!string.IsNullOrWhiteSpace(tag))
      {
        tag = tag.Trim();
        if (tag[0] == '_')
        {
          char[] charArray = tag.ToCharArray();
          charArray[0] = '-';
          tag = new string(charArray);
        }
        tag = tag.Replace("|", "-");
      }
      else
        tag = "Default";
      string bugSenseVersion = BugSenseProperties.BugSenseVersion;
      string phoneModel = BugSenseProperties.PhoneModel;
      string phoneBrand = BugSenseProperties.PhoneBrand;
      string osVersion = BugSenseProperties.OSVersion;
      string appVersion = BugSenseProperties.AppVersion;
      string letterIsoLanguageName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
      string str = ((long) (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds).ToString();
      int length = (int) byte.MaxValue - string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}", (object) bugSenseVersion, (object) string.Empty, (object) phoneModel, (object) phoneBrand, (object) osVersion, (object) appVersion, (object) letterIsoLanguageName, (object) str).Length;
      if (tag.Length > length)
        tag = tag.Substring(0, length);
      return string.Format("{0}:{1}:{2}:{3}:{4}:{5}:{6}:{7}", (object) bugSenseVersion, (object) tag, (object) phoneModel, (object) phoneBrand, (object) osVersion, (object) appVersion, (object) letterIsoLanguageName, (object) str);
    }
  }
}
