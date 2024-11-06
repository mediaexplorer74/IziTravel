// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ActivationEventArgs
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>EventArgs sent during activation.</summary>
  public class ActivationEventArgs : EventArgs
  {
    /// <summary>
    /// Indicates whether the sender was initialized in addition to being activated.
    /// </summary>
    public bool WasInitialized;
  }
}
