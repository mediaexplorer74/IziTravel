// Decompiled with JetBrains decompiler
// Type: Splunk.Mi.Utilities.CommonHelper
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core;
using System;

#nullable disable
namespace Splunk.Mi.Utilities
{
  internal static class CommonHelper
  {
    public static string NewFileNamePath(FileNameType fileType)
    {
      string str = string.Empty;
      switch (fileType)
      {
        case FileNameType.UnhandledException:
          str = string.Format("{0}\\CCC_{1}_BugSense_Ex_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.LoggedException:
          str = string.Format("{0}\\LLC_{1}_BugSense_Ex_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.Ping:
          str = string.Format("{0}\\PCC_{1}_BugSense_Ev_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.Gnip:
          str = string.Format("{0}\\GCC_{1}_BugSense_Ev_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.Event:
          str = string.Format("{0}\\ECC_{1}_BugSense_Ev_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.TransactionStart:
          str = string.Format("{0}\\TRSTART_{1}_BugSense_Tr_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.TransactionStop:
          str = string.Format("{0}\\TRSTOP_{1}_BugSense_Tr_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
        case FileNameType.NetworkAction:
          str = string.Format("{0}\\NETWORK_{1}_BugSense_Net_{2}.dat", (object) BugSenseProperties.ExceptionsFolderName, (object) DateTime.UtcNow.ToString("yyyyMMddHHmmss"), (object) Guid.NewGuid());
          break;
      }
      return str;
    }
  }
}
