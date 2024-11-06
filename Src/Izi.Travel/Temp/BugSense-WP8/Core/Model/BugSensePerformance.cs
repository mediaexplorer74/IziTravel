// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSensePerformance
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System.Runtime.Serialization;

#nullable disable
namespace BugSense.Core.Model
{
  [DataContract]
  public class BugSensePerformance
  {
    [DataMember(Name = "appMemAvail")]
    public double AppMemAvail { get; set; }

    [DataMember(Name = "appMemMax")]
    public double AppMemMax { get; set; }

    [DataMember(Name = "appMemTotal")]
    public double AppMemTotal { get; set; }

    [DataMember(Name = "sysMemAvail")]
    public double SysMemAvail { get; set; }

    [DataMember(Name = "sysMemLow")]
    public string SysMemLow { get; set; }

    [DataMember(Name = "sysMemThreshold")]
    public double SysMemThreshold { get; set; }

    public BugSensePerformance()
    {
      this.AppMemAvail = 0.0;
      this.AppMemMax = 0.0;
      this.AppMemTotal = 0.0;
      this.SysMemAvail = 0.0;
      this.SysMemLow = "False";
      this.SysMemThreshold = 0.0;
    }
  }
}
