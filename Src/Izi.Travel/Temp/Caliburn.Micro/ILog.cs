// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ILog
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A logger.</summary>
  public interface ILog
  {
    /// <summary>Logs the message as info.</summary>
    /// <param name="format">A formatted message.</param>
    /// <param name="args">Parameters to be injected into the formatted message.</param>
    void Info(string format, params object[] args);

    /// <summary>Logs the message as a warning.</summary>
    /// <param name="format">A formatted message.</param>
    /// <param name="args">Parameters to be injected into the formatted message.</param>
    void Warn(string format, params object[] args);

    /// <summary>Logs the exception.</summary>
    /// <param name="exception">The exception.</param>
    void Error(Exception exception);
  }
}
