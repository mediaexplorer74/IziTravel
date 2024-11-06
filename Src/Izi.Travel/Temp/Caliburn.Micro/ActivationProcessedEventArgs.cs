// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ActivationProcessedEventArgs
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Contains details about the success or failure of an item's activation through an <see cref="T:Caliburn.Micro.IConductor" />.
  /// </summary>
  public class ActivationProcessedEventArgs : EventArgs
  {
    /// <summary>The item whose activation was processed.</summary>
    public object Item;
    /// <summary>
    /// Gets or sets a value indicating whether the activation was a success.
    /// </summary>
    /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
    public bool Success;
  }
}
