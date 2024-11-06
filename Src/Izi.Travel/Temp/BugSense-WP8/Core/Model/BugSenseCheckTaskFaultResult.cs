// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSenseCheckTaskFaultResult
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace BugSense.Core.Model
{
  public sealed class BugSenseCheckTaskFaultResult
  {
    public string Description { get; set; }

    public Exception TaskException { get; set; }

    public string Id { get; set; }

    public Task TaskToCheck { get; set; }

    public LimitedCrashExtraDataList ExtraData { get; set; }

    internal int SecondsToWait { get; set; }

    internal int Retries { get; set; }

    internal BugSenseCheckTaskFaultResult()
    {
    }
  }
}
