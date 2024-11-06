// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.INotifyPropertyChangedEx
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.ComponentModel;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extends <see cref="T:System.ComponentModel.INotifyPropertyChanged" /> such that the change event can be raised by external parties.
  /// </summary>
  public interface INotifyPropertyChangedEx : INotifyPropertyChanged
  {
    /// <summary>Enables/Disables property change notification.</summary>
    bool IsNotifying { get; set; }

    /// <summary>Notifies subscribers of the property change.</summary>
    /// <param name="propertyName">Name of the property.</param>
    void NotifyOfPropertyChange(string propertyName);

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    void Refresh();
  }
}
