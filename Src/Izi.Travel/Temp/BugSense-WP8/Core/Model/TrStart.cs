// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.TrStart
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
  public class TrStart : Transaction
  {
    public static TrStart GetInstance(
      string transactionId,
      AppEnvironment appEnvironment,
      BugSensePerformance performance)
    {
      string str = (string) null;
      try
      {
        str = appEnvironment.AppVersion.Substring(0, 3);
      }
      catch
      {
      }
      TrStart instance = new TrStart();
      instance.AppVersion = appEnvironment.AppVersion;
      instance.AppVersionCode = appEnvironment.AppVersion;
      instance.AppVersionName = appEnvironment.AppName;
      instance.BinaryName = appEnvironment.AppName;
      instance.Locale = appEnvironment.Locale;
      instance.OsVersion = appEnvironment.OsVersion;
      instance.PhoneModel = appEnvironment.PhoneModel;
      instance.SdkPlatform = BugSenseProperties.BugSenseName;
      instance.SdkVersion = BugSenseProperties.BugSenseVersion;
      instance.TypeRequest = JsonRequestType.TransactionStart;
      instance.Timestamp = DateTime.UtcNow.DateTimeToUnixTimestamp().ToString();
      instance.Uid = BugSenseProperties.UID;
      instance.UserIdentifier = BugSenseProperties.UserIdentifier;
      instance.Name = transactionId;
      instance.Connection = Enum.GetName(typeof (ConnectionType), (object) BugSenseProperties.Connection);
      instance.AppVersionShort = str;
      return instance;
    }
  }
}
