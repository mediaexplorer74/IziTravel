// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IDeactivate
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes an instance which requires deactivation.</summary>
  public interface IDeactivate
  {
    /// <summary>Raised before deactivation.</summary>
    event EventHandler<DeactivationEventArgs> AttemptingDeactivation;

    /// <summary>Deactivates this instance.</summary>
    /// <param name="close">Indicates whether or not this instance is being closed.</param>
    void Deactivate(bool close);

    /// <summary>Raised after deactivation.</summary>
    event EventHandler<DeactivationEventArgs> Deactivated;
  }
}
