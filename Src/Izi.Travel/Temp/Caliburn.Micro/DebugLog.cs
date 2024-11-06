// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.DebugLog
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Diagnostics;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   A simple logger thats logs everything to the debugger.
  /// </summary>
  public class DebugLog : ILog
  {
    private readonly string typeName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.DebugLog" /> class.
    /// </summary>
    /// <param name="type">The type.</param>
    public DebugLog(Type type) => this.typeName = type.FullName;

    /// <summary>Logs the message as info.</summary>
    /// <param name="format">A formatted message.</param>
    /// <param name="args">Parameters to be injected into the formatted message.</param>
    public void Info(string format, params object[] args)
    {
      Debug.WriteLine("[{1}] INFO: {0}", (object) string.Format(format, args), (object) this.typeName);
    }

    /// <summary>Logs the message as a warning.</summary>
    /// <param name="format">A formatted message.</param>
    /// <param name="args">Parameters to be injected into the formatted message.</param>
    public void Warn(string format, params object[] args)
    {
      Debug.WriteLine("[{1}] WARN: {0}", (object) string.Format(format, args), (object) this.typeName);
    }

    /// <summary>Logs the exception.</summary>
    /// <param name="exception">The exception.</param>
    public void Error(Exception exception)
    {
      Debug.WriteLine("[{1}] ERROR: {0}", (object) exception, (object) this.typeName);
    }
  }
}
