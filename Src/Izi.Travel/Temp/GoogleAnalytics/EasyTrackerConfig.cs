// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.EasyTrackerConfig
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using System;
using System.Xml;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class EasyTrackerConfig
  {
    public EasyTrackerConfig()
    {
      this.SessionTimeout = new TimeSpan?(TimeSpan.FromSeconds(30.0));
      this.DispatchPeriod = TimeSpan.Zero;
      this.SampleFrequency = 100f;
      this.AutoAppLifetimeMonitoring = true;
      this.AutoTrackNetworkConnectivity = true;
    }

    internal static EasyTrackerConfig Load(XmlReader reader)
    {
      while (reader.NodeType != XmlNodeType.Element && !reader.EOF)
        reader.Read();
      return !reader.EOF && reader.Name == "analytics" ? EasyTrackerConfig.LoadConfigXml(reader) : new EasyTrackerConfig();
    }

    private static EasyTrackerConfig LoadConfigXml(XmlReader reader)
    {
      EasyTrackerConfig easyTrackerConfig = new EasyTrackerConfig();
      reader.ReadStartElement("analytics");
      while (reader.IsStartElement())
      {
        switch (reader.Name)
        {
          case "anonymizeIp":
            easyTrackerConfig.AnonymizeIp = reader.ReadElementContentAsBoolean();
            continue;
          case "appId":
            easyTrackerConfig.AppId = reader.ReadElementContentAsString();
            continue;
          case "appInstallerId":
            easyTrackerConfig.AppInstallerId = reader.ReadElementContentAsString();
            continue;
          case "appName":
            easyTrackerConfig.AppName = reader.ReadElementContentAsString();
            continue;
          case "appVersion":
            easyTrackerConfig.AppVersion = reader.ReadElementContentAsString();
            continue;
          case "autoAppLifetimeMonitoring":
            easyTrackerConfig.AutoAppLifetimeMonitoring = reader.ReadElementContentAsBoolean();
            continue;
          case "autoAppLifetimeTracking":
            easyTrackerConfig.AutoAppLifetimeTracking = reader.ReadElementContentAsBoolean();
            continue;
          case "autoTrackNetworkConnectivity":
            easyTrackerConfig.AutoTrackNetworkConnectivity = reader.ReadElementContentAsBoolean();
            continue;
          case "debug":
            easyTrackerConfig.Debug = reader.ReadElementContentAsBoolean();
            continue;
          case "dispatchPeriod":
            int num1 = reader.ReadElementContentAsInt();
            easyTrackerConfig.DispatchPeriod = TimeSpan.FromSeconds((double) num1);
            continue;
          case "reportUncaughtExceptions":
            easyTrackerConfig.ReportUncaughtExceptions = reader.ReadElementContentAsBoolean();
            continue;
          case "sampleFrequency":
            easyTrackerConfig.SampleFrequency = reader.ReadElementContentAsFloat();
            continue;
          case "sessionTimeout":
            int num2 = reader.ReadElementContentAsInt();
            easyTrackerConfig.SessionTimeout = num2 >= 0 ? new TimeSpan?(TimeSpan.FromSeconds((double) num2)) : new TimeSpan?();
            continue;
          case "trackingId":
            easyTrackerConfig.TrackingId = reader.ReadElementContentAsString();
            continue;
          case "useSecure":
            easyTrackerConfig.UseSecure = reader.ReadElementContentAsBoolean();
            continue;
          default:
            reader.Skip();
            continue;
        }
      }
      reader.ReadEndElement();
      return easyTrackerConfig;
    }

    internal void Validate()
    {
      if (this.AutoAppLifetimeTracking && !this.AutoAppLifetimeMonitoring)
        throw new ArgumentOutOfRangeException("AutoAppLifetimeTracking cannot be true if AutoAppLifetimeMonitoring is false.");
    }

    public string TrackingId { get; set; }

    public string AppName { get; set; }

    public string AppVersion { get; set; }

    public string AppId { get; set; }

    public string AppInstallerId { get; set; }

    public string UserId { get; set; }

    public bool Debug { get; set; }

    public TimeSpan DispatchPeriod { get; set; }

    public float SampleFrequency { get; set; }

    public bool AnonymizeIp { get; set; }

    public bool ReportUncaughtExceptions { get; set; }

    public TimeSpan? SessionTimeout { get; set; }

    public bool AutoAppLifetimeTracking { get; set; }

    public bool AutoAppLifetimeMonitoring { get; set; }

    public bool AutoTrackNetworkConnectivity { get; set; }

    public bool UseSecure { get; set; }
  }
}
