// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.LogManager
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Used to manage logging.</summary>
  public static class LogManager
  {
    private static readonly ILog NullLogInstance = (ILog) new LogManager.NullLog();
    /// <summary>
    /// Creates an <see cref="T:Caliburn.Micro.ILog" /> for the provided type.
    /// </summary>
    public static Func<Type, ILog> GetLog = (Func<Type, ILog>) (type => LogManager.NullLogInstance);

    private class NullLog : ILog
    {
      public void Info(string format, params object[] args)
      {
      }

      public void Warn(string format, params object[] args)
      {
      }

      public void Error(Exception exception)
      {
      }
    }
  }
}
