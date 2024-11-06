// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.DeactivationEventArgs
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>EventArgs sent during deactivation.</summary>
  public class DeactivationEventArgs : EventArgs
  {
    /// <summary>
    /// Indicates whether the sender was closed in addition to being deactivated.
    /// </summary>
    public bool WasClosed;
  }
}
