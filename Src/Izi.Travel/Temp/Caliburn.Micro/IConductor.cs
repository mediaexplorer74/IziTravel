// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IConductor
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Denotes an instance which conducts other objects by managing an ActiveItem and maintaining a strict lifecycle.
  /// </summary>
  /// <remarks>Conducted instances can optin to the lifecycle by impelenting any of the follosing <see cref="T:Caliburn.Micro.IActivate" />, <see cref="T:Caliburn.Micro.IDeactivate" />, <see cref="T:Caliburn.Micro.IGuardClose" />.</remarks>
  public interface IConductor : IParent, INotifyPropertyChangedEx, INotifyPropertyChanged
  {
    /// <summary>Activates the specified item.</summary>
    /// <param name="item">The item to activate.</param>
    void ActivateItem(object item);

    /// <summary>Deactivates the specified item.</summary>
    /// <param name="item">The item to close.</param>
    /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
    void DeactivateItem(object item, bool close);

    /// <summary>Occurs when an activation request is processed.</summary>
    event EventHandler<ActivationProcessedEventArgs> ActivationProcessed;
  }
}
