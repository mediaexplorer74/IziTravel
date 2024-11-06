// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ViewAttachedEventArgs
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// The event args for the <see cref="E:Caliburn.Micro.IViewAware.ViewAttached" /> event.
  /// </summary>
  public class ViewAttachedEventArgs : EventArgs
  {
    /// <summary>The view.</summary>
    public object View;
    /// <summary>The context.</summary>
    public object Context;
  }
}
