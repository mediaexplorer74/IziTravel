// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IScreen
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Denotes an instance which implements <see cref="T:Caliburn.Micro.IHaveDisplayName" />, <see cref="T:Caliburn.Micro.IActivate" />,
  /// <see cref="T:Caliburn.Micro.IDeactivate" />, <see cref="T:Caliburn.Micro.IGuardClose" /> and <see cref="T:Caliburn.Micro.INotifyPropertyChangedEx" />
  /// </summary>
  public interface IScreen : 
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
  }
}
