// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ResultCompletionEventArgs
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// The event args for the Completed event of an <see cref="T:Caliburn.Micro.IResult" />.
  /// </summary>
  public class ResultCompletionEventArgs : EventArgs
  {
    /// <summary>Gets or sets the error if one occurred.</summary>
    /// <value>The error.</value>
    public Exception Error;
    /// <summary>
    /// Gets or sets a value indicating whether the result was cancelled.
    /// </summary>
    /// <value><c>true</c> if cancelled; otherwise, <c>false</c>.</value>
    public bool WasCancelled;
  }
}
