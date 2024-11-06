// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IConductActiveItem
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// An <see cref="T:Caliburn.Micro.IConductor" /> that also implements <see cref="T:Caliburn.Micro.IHaveActiveItem" />.
  /// </summary>
  public interface IConductActiveItem : 
    IConductor,
    IParent,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    IHaveActiveItem
  {
  }
}
