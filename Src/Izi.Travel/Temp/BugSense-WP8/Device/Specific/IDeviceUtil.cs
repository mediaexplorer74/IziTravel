// Decompiled with JetBrains decompiler
// Type: BugSense.Device.Specific.IDeviceUtil
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;

#nullable disable
namespace BugSense.Device.Specific
{
  internal interface IDeviceUtil
  {
    void AppendBugSenseInfo();

    void GetDeviceConnectionInfo();

    void GetScreenInfo();

    AppEnvironment GetAppEnvironment();

    BugSensePerformance GetBugSensePerformance();

    bool IsLowMemDevice { get; }
  }
}
