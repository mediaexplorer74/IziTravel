// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ElementConvention
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Reflection;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Represents the conventions for a particular element type.
  /// </summary>
  public class ElementConvention
  {
    /// <summary>The type of element to which the conventions apply.</summary>
    public Type ElementType;
    /// <summary>
    /// Gets the default property to be used in binding conventions.
    /// </summary>
    public Func<DependencyObject, DependencyProperty> GetBindableProperty;
    /// <summary>
    /// The default trigger to be used when wiring actions on this element.
    /// </summary>
    public Func<System.Windows.Interactivity.TriggerBase> CreateTrigger;
    /// <summary>
    /// The default property to be used for parameters of this type in actions.
    /// </summary>
    public string ParameterProperty;
    /// <summary>Applies custom conventions for elements of this type.</summary>
    /// <remarks>Pass the view model type, property path, property instance, framework element and its convention.</remarks>
    public Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool> ApplyBinding = (Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool>) ((viewModelType, path, property, element, convention) => ConventionManager.SetBindingWithoutBindingOverwrite(viewModelType, path, property, element, convention, convention.GetBindableProperty((DependencyObject) element)));
  }
}
