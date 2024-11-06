// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.RemoteSettingsData
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System.Runtime.Serialization;

#nullable disable
namespace BugSense.Core.Model
{
  [DataContract]
  public class RemoteSettingsData
  {
    [DataMember(Name = "logLevel")]
    public string LogLevel { get; set; }

    [DataMember(Name = "version")]
    public string Version { get; set; }

    [DataMember(Name = "refreshEveryXseconds")]
    public int RefreshInterval { get; set; }

    [DataMember(Name = "netMonitoring")]
    public bool NetMonitoring { get; set; }

    [DataMember(Name = "hashCode")]
    public string HashCode { get; set; }

    public static RemoteSettingsData Instance => RemoteSettingsData.Nested.instance;

    public RemoteSettingsData()
    {
      this.LogLevel = "verbose";
      this.Version = "1.0";
      this.RefreshInterval = 60;
      this.HashCode = "none";
    }

    private class Nested
    {
      internal static readonly RemoteSettingsData instance = new RemoteSettingsData();
    }
  }
}
