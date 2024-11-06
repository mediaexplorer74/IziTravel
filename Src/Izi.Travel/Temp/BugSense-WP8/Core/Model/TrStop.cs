// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.TrStop
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Helpers;
using System;
using System.Runtime.Serialization;

#nullable disable
namespace BugSense.Core.Model
{
  [DataContract]
  public class TrStop : Transaction
  {
    [DataMember(Name = "duration")]
    public double Duration { get; set; }

    [DataMember(Name = "status", EmitDefaultValue = false)]
    public string CompletedStatus { get; set; }

    [DataMember(Name = "reason", EmitDefaultValue = false)]
    public string Reason { get; set; }

    [DataMember(Name = "slow")]
    public bool Slow { get; set; }

    public static TrStop GetInstance(
      string transactionId,
      AppEnvironment appEnvironment,
      double duration,
      string reason,
      string status)
    {
      string str = (string) null;
      try
      {
        str = appEnvironment.AppVersion.Substring(0, 3);
      }
      catch
      {
      }
      TrStop instance = new TrStop();
      instance.AppVersionCode = appEnvironment.AppVersion;
      instance.AppVersion = appEnvironment.AppVersion;
      instance.AppVersionName = appEnvironment.AppName;
      instance.BinaryName = appEnvironment.AppName;
      instance.Locale = appEnvironment.Locale;
      instance.OsVersion = appEnvironment.OsVersion;
      instance.PhoneModel = appEnvironment.PhoneModel;
      instance.SdkPlatform = BugSenseProperties.BugSenseName;
      instance.SdkVersion = BugSenseProperties.BugSenseVersion;
      instance.TypeRequest = JsonRequestType.TransactionStop;
      instance.Timestamp = DateTime.UtcNow.DateTimeToUnixTimestamp().ToString();
      instance.Uid = BugSenseProperties.UID;
      instance.UserIdentifier = BugSenseProperties.UserIdentifier;
      instance.Name = transactionId;
      instance.Duration = duration;
      instance.Reason = reason;
      instance.CompletedStatus = status;
      instance.Connection = Enum.GetName(typeof (ConnectionType), (object) BugSenseProperties.Connection);
      instance.AppVersionShort = str;
      return instance;
    }
  }
}
