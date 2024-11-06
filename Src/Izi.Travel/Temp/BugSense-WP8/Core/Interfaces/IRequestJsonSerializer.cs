// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Interfaces.IRequestJsonSerializer
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;
using System;

#nullable disable
namespace BugSense.Core.Interfaces
{
  public interface IRequestJsonSerializer
  {
    SerializeResult SerializeEventToJson(BugSenseEventTag eventTag, AppEnvironment appEnvironment);

    SerializeResult SerializeEventToJson(string eventTag, AppEnvironment appEnvironment);

    SerializeResult SerializeCrashToJson(
      Exception exception,
      AppEnvironment appEnvironment,
      BugSensePerformance bugSensePerformance,
      bool handled,
      LimitedCrashExtraDataList extraData);

    string DecodeEncodedCrashJson(string encodedJson);

    string GetErrorHash(string jsonRequest);

    SerializeResult SerializeTransaction<T>(T transaction);
  }
}
