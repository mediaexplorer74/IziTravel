// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Parameter
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Represents a parameter of an <see cref="T:Caliburn.Micro.ActionMessage" />.
  /// </summary>
  public class Parameter : DependencyObject, IAttachedObject
  {
    private DependencyObject associatedObject;
    private WeakReference owner;
    /// <summary>
    /// A dependency property representing the parameter's value.
    /// </summary>
    public static readonly DependencyProperty ValueProperty = DependencyProperty.Register(nameof (Value), typeof (object), typeof (Parameter), new PropertyMetadata(new PropertyChangedCallback(Parameter.OnValueChanged)));

    /// <summary>Gets or sets the value of the parameter.</summary>
    /// <value>The value.</value>
    [Category("Common Properties")]
    public object Value
    {
      get => this.GetValue(Parameter.ValueProperty);
      set => this.SetValue(Parameter.ValueProperty, value);
    }

    DependencyObject IAttachedObject.AssociatedObject => this.associatedObject;

    /// <summary>Gets or sets the owner.</summary>
    protected ActionMessage Owner
    {
      get => this.owner != null ? this.owner.Target as ActionMessage : (ActionMessage) null;
      set => this.owner = new WeakReference((object) value);
    }

    void IAttachedObject.Attach(DependencyObject dependencyObject)
    {
      this.associatedObject = dependencyObject;
    }

    void IAttachedObject.Detach() => this.associatedObject = (DependencyObject) null;

    /// <summary>
    /// Makes the parameter aware of the <see cref="T:Caliburn.Micro.ActionMessage" /> that it's attached to.
    /// </summary>
    /// <param name="owner">The action message.</param>
    internal void MakeAwareOf(ActionMessage owner) => this.Owner = owner;

    private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((Parameter) d).Owner?.UpdateAvailability();
    }
  }
}
