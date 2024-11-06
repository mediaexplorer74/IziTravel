// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Bind
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Windows;
using System.Windows.Data;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Hosts dependency properties for binding.</summary>
  public static class Bind
  {
    /// <summary>
    ///   Allows binding on an existing view. Use this on root UserControls, Pages and Windows; not in a DataTemplate.
    /// </summary>
    public static DependencyProperty ModelProperty = DependencyProperty.RegisterAttached("Model", typeof (object), typeof (Bind), new PropertyMetadata((object) null, new PropertyChangedCallback(Bind.ModelChanged)));
    /// <summary>
    ///   Allows binding on an existing view without setting the data context. Use this from within a DataTemplate.
    /// </summary>
    public static DependencyProperty ModelWithoutContextProperty = DependencyProperty.RegisterAttached("ModelWithoutContext", typeof (object), typeof (Bind), new PropertyMetadata((object) null, new PropertyChangedCallback(Bind.ModelWithoutContextChanged)));
    internal static DependencyProperty NoContextProperty = DependencyProperty.RegisterAttached("NoContext", typeof (bool), typeof (Bind), new PropertyMetadata((object) false));
    /// <summary>Allows application of conventions at design-time.</summary>
    public static DependencyProperty AtDesignTimeProperty = DependencyProperty.RegisterAttached("AtDesignTime", typeof (bool), typeof (Bind), new PropertyMetadata((object) false, new PropertyChangedCallback(Bind.AtDesignTimeChanged)));
    private static readonly DependencyProperty DataContextProperty = DependencyProperty.RegisterAttached("DataContext", typeof (object), typeof (Bind), new PropertyMetadata((object) null, new PropertyChangedCallback(Bind.DataContextChanged)));

    /// <summary>Gets the model to bind to.</summary>
    /// <param name="dependencyObject">The dependency object to bind to.</param>
    /// <returns>The model.</returns>
    public static object GetModelWithoutContext(DependencyObject dependencyObject)
    {
      return dependencyObject.GetValue(Bind.ModelWithoutContextProperty);
    }

    /// <summary>Sets the model to bind to.</summary>
    /// <param name="dependencyObject">The dependency object to bind to.</param>
    /// <param name="value">The model.</param>
    public static void SetModelWithoutContext(DependencyObject dependencyObject, object value)
    {
      dependencyObject.SetValue(Bind.ModelWithoutContextProperty, value);
    }

    /// <summary>Gets the model to bind to.</summary>
    /// <param name="dependencyObject">The dependency object to bind to.</param>
    /// <returns>The model.</returns>
    public static object GetModel(DependencyObject dependencyObject)
    {
      return dependencyObject.GetValue(Bind.ModelProperty);
    }

    /// <summary>Sets the model to bind to.</summary>
    /// <param name="dependencyObject">The dependency object to bind to.</param>
    /// <param name="value">The model.</param>
    public static void SetModel(DependencyObject dependencyObject, object value)
    {
      dependencyObject.SetValue(Bind.ModelProperty, value);
    }

    private static void ModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue)
        return;
      FrameworkElement fe = d as FrameworkElement;
      if (fe == null)
        return;
      View.ExecuteOnLoad(fe, (RoutedEventHandler) ((param0, param1) =>
      {
        object obj = e.NewValue;
        if (e.NewValue is string newValue2)
          obj = IoC.GetInstance((Type) null, newValue2);
        d.SetValue(View.IsScopeRootProperty, (object) true);
        string str = string.IsNullOrEmpty(fe.Name) ? fe.GetHashCode().ToString() : fe.Name;
        ViewModelBinder.Bind(obj, d, (object) str);
      }));
    }

    private static void ModelWithoutContextChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (View.InDesignMode || e.NewValue == null || e.NewValue == e.OldValue)
        return;
      FrameworkElement fe = d as FrameworkElement;
      if (fe == null)
        return;
      View.ExecuteOnLoad(fe, (RoutedEventHandler) ((param0, param1) =>
      {
        object obj = e.NewValue;
        if (e.NewValue is string newValue2)
          obj = IoC.GetInstance((Type) null, newValue2);
        d.SetValue(View.IsScopeRootProperty, (object) true);
        string str = string.IsNullOrEmpty(fe.Name) ? fe.GetHashCode().ToString() : fe.Name;
        d.SetValue(Bind.NoContextProperty, (object) true);
        ViewModelBinder.Bind(obj, d, (object) str);
      }));
    }

    /// <summary>
    /// Gets whether or not conventions are being applied at design-time.
    /// </summary>
    /// <param name="dependencyObject">The ui to apply conventions to.</param>
    /// <returns>Whether or not conventions are applied.</returns>
    public static bool GetAtDesignTime(DependencyObject dependencyObject)
    {
      return (bool) dependencyObject.GetValue(Bind.AtDesignTimeProperty);
    }

    /// <summary>Sets whether or not do bind conventions at design-time.</summary>
    /// <param name="dependencyObject">The ui to apply conventions to.</param>
    /// <param name="value">Whether or not to apply conventions.</param>
    public static void SetAtDesignTime(DependencyObject dependencyObject, bool value)
    {
      dependencyObject.SetValue(Bind.AtDesignTimeProperty, (object) value);
    }

    private static void AtDesignTimeChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!View.InDesignMode || !(bool) e.NewValue)
        return;
      BindingOperations.SetBinding(d, Bind.DataContextProperty, (BindingBase) new Binding());
    }

    private static void DataContextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!View.InDesignMode)
        return;
      object obj = d.GetValue(Bind.AtDesignTimeProperty);
      if (obj == null || !(bool) obj || e.NewValue == null || !(d is FrameworkElement frameworkElement))
        return;
      ViewModelBinder.Bind(e.NewValue, d, string.IsNullOrEmpty(frameworkElement.Name) ? (object) frameworkElement.GetHashCode().ToString() : (object) frameworkElement.Name);
    }
  }
}
