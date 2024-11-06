// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IActivate
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes an instance which requires activation.</summary>
  public interface IActivate
  {
    /// <summary>Indicates whether or not this instance is active.</summary>
    bool IsActive { get; }

    /// <summary>Activates this instance.</summary>
    void Activate();

    /// <summary>Raised after activation occurs.</summary>
    event EventHandler<ActivationEventArgs> Activated;
  }
}
