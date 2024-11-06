// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PropertyChangedBase
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A base class that implements the infrastructure for property change notification and automatically performs UI thread marshalling.
  /// </summary>
  [DataContract]
  public class PropertyChangedBase : INotifyPropertyChangedEx, INotifyPropertyChanged
  {
    /// <summary>
    /// Creates an instance of <see cref="T:Caliburn.Micro.PropertyChangedBase" />.
    /// </summary>
    public PropertyChangedBase() => this.IsNotifying = true;

    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler PropertyChanged = (param0, param1) => { };

    /// <summary>Enables/Disables property change notification.</summary>
    public bool IsNotifying { get; set; }

    /// <summary>
    /// Raises a change notification indicating that all bindings should be refreshed.
    /// </summary>
    public void Refresh() => this.NotifyOfPropertyChange(string.Empty);

    /// <summary>Notifies subscribers of the property change.</summary>
    /// <param name="propertyName">Name of the property.</param>
    public virtual void NotifyOfPropertyChange([CallerMemberName] string propertyName = null)
    {
      if (!this.IsNotifying)
        return;
      ((Action) (() => this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName)))).OnUIThread();
    }

    /// <summary>Notifies subscribers of the property change.</summary>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <param name="property">The property expression.</param>
    public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> property)
    {
      this.NotifyOfPropertyChange(property.GetMemberInfo().Name);
    }

    /// <summary>
    /// Raises the <see cref="E:Caliburn.Micro.PropertyChangedBase.PropertyChanged" /> event directly.
    /// </summary>
    /// <param name="e">The <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> instance containing the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    protected void OnPropertyChanged(PropertyChangedEventArgs e)
    {
      PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
      if (propertyChanged == null)
        return;
      propertyChanged((object) this, e);
    }
  }
}
