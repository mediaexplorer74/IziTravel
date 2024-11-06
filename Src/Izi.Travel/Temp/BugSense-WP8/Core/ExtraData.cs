// Decompiled with JetBrains decompiler
// Type: BugSense.Core.ExtraData
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;
using System.Collections.Generic;

#nullable disable
namespace BugSense.Core
{
  internal static class ExtraData
  {
    public static LimitedCrashExtraDataList CrashExtraData { get; set; }

    public static List<BugSense.Core.Model.CrashExtraMap> CrashExtraMap { get; set; }

    public static LimitedBreadCrumbList BreadCrumbs { get; set; }

    static ExtraData()
    {
      ExtraData.CrashExtraData = new LimitedCrashExtraDataList();
      ExtraData.CrashExtraMap = new List<BugSense.Core.Model.CrashExtraMap>();
      ExtraData.BreadCrumbs = new LimitedBreadCrumbList();
    }
  }
}
