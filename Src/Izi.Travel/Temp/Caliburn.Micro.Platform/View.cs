// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.View
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Hosts attached properties related to view models.</summary>
  public static class View
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (View));
    private static readonly ContentPropertyAttribute DefaultContentProperty = new ContentPropertyAttribute("Content");
    /// <summary>
    /// A dependency property which allows the framework to track whether a certain element has already been loaded in certain scenarios.
    /// </summary>
    public static readonly DependencyProperty IsLoadedProperty = DependencyProperty.RegisterAttached("IsLoaded", typeof (bool), typeof (View), new PropertyMetadata((object) false));
    /// <summary>
    /// A dependency property which marks an element as a name scope root.
    /// </summary>
    public static readonly DependencyProperty IsScopeRootProperty = DependencyProperty.RegisterAttached("IsScopeRoot", typeof (bool), typeof (View), new PropertyMetadata((object) false));
    /// <summary>
    /// A dependency property which allows the override of convention application behavior.
    /// </summary>
    public static readonly DependencyProperty ApplyConventionsProperty = DependencyProperty.RegisterAttached("ApplyConventions", typeof (bool?), typeof (View), (PropertyMetadata) null);
    /// <summary>
    /// A dependency property for assigning a context to a particular portion of the UI.
    /// </summary>
    public static readonly DependencyProperty ContextProperty = DependencyProperty.RegisterAttached("Context", typeof (object), typeof (View), new PropertyMetadata((object) null, new PropertyChangedCallback(View.OnContextChanged)));
    /// <summary>A dependency property for attaching a model to the UI.</summary>
    public static DependencyProperty ModelProperty = DependencyProperty.RegisterAttached("Model", typeof (object), typeof (View), new PropertyMetadata((object) null, new PropertyChangedCallback(View.OnModelChanged)));
    /// <summary>
    /// Used by the framework to indicate that this element was generated.
    /// </summary>
    public static readonly DependencyProperty IsGeneratedProperty = DependencyProperty.RegisterAttached("IsGenerated", typeof (bool), typeof (View), new PropertyMetadata((object) false));
    /// <summary>Used to retrieve the root, non-framework-created view.</summary>
    /// <param name="view">The view to search.</param>
    /// <returns>The root element that was not created by the framework.</returns>
    /// <remarks>In certain instances the services create UI elements.
    /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
    /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
    /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
    /// </remarks>
    public static Func<object, object> GetFirstNonGeneratedView = (Func<object, object>) (view =>
    {
      if (!(view is DependencyObject dependencyObject2))
        return view;
      if (!(bool) dependencyObject2.GetValue(View.IsGeneratedProperty))
        return (object) dependencyObject2;
      if (dependencyObject2 is ContentControl)
        return ((ContentControl) dependencyObject2).Content;
      Type type = dependencyObject2.GetType();
      ContentPropertyAttribute propertyAttribute = type.GetAttributes<ContentPropertyAttribute>(true).FirstOrDefault<ContentPropertyAttribute>() ?? View.DefaultContentProperty;
      return type.GetProperty(propertyAttribute.Name).GetValue((object) dependencyObject2, (object[]) null);
    });
    private static bool? inDesignMode;

    /// <summary>
    /// Executes the handler immediately if the element is loaded, otherwise wires it to the Loaded event.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="handler">The handler.</param>
    /// <returns>true if the handler was executed immediately; false otherwise</returns>
    public static bool ExecuteOnLoad(FrameworkElement element, RoutedEventHandler handler)
    {
      if ((bool) element.GetValue(View.IsLoadedProperty))
      {
        handler((object) element, new RoutedEventArgs());
        return true;
      }
      RoutedEventHandler loaded = (RoutedEventHandler) null;
      loaded = (RoutedEventHandler) ((s, e) =>
      {
        element.Loaded -= loaded;
        element.SetValue(View.IsLoadedProperty, (object) true);
        handler(s, e);
      });
      element.Loaded += loaded;
      return false;
    }

    /// <summary>Executes the handler when the element is unloaded.</summary>
    /// <param name="element">The element.</param>
    /// <param name="handler">The handler.</param>
    public static void ExecuteOnUnload(FrameworkElement element, RoutedEventHandler handler)
    {
      RoutedEventHandler unloaded = (RoutedEventHandler) null;
      unloaded = (RoutedEventHandler) ((s, e) =>
      {
        element.Unloaded -= unloaded;
        handler(s, e);
      });
      element.Unloaded += unloaded;
    }

    /// <summary>
    /// Executes the handler the next time the elements's LayoutUpdated event fires.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="handler">The handler.</param>
    public static void ExecuteOnLayoutUpdated(FrameworkElement element, EventHandler handler)
    {
      EventHandler onLayoutUpdate = (EventHandler) null;
      onLayoutUpdate = (EventHandler) ((s, e) =>
      {
        element.LayoutUpdated -= onLayoutUpdate;
        handler((object) element, e);
      });
      element.LayoutUpdated += onLayoutUpdate;
    }

    /// <summary>Gets the convention application behavior.</summary>
    /// <param name="d">The element the property is attached to.</param>
    /// <returns>Whether or not to apply conventions.</returns>
    public static bool? GetApplyConventions(DependencyObject d)
    {
      return (bool?) d.GetValue(View.ApplyConventionsProperty);
    }

    /// <summary>Sets the convention application behavior.</summary>
    /// <param name="d">The element to attach the property to.</param>
    /// <param name="value">Whether or not to apply conventions.</param>
    public static void SetApplyConventions(DependencyObject d, bool? value)
    {
      d.SetValue(View.ApplyConventionsProperty, (object) value);
    }

    /// <summary>Sets the model.</summary>
    /// <param name="d">The element to attach the model to.</param>
    /// <param name="value">The model.</param>
    public static void SetModel(DependencyObject d, object value)
    {
      d.SetValue(View.ModelProperty, value);
    }

    /// <summary>Gets the model.</summary>
    /// <param name="d">The element the model is attached to.</param>
    /// <returns>The model.</returns>
    public static object GetModel(DependencyObject d) => d.GetValue(View.ModelProperty);

    /// <summary>Gets the context.</summary>
    /// <param name="d">The element the context is attached to.</param>
    /// <returns>The context.</returns>
    public static object GetContext(DependencyObject d) => d.GetValue(View.ContextProperty);

    /// <summary>Sets the context.</summary>
    /// <param name="d">The element to attach the context to.</param>
    /// <param name="value">The context.</param>
    public static void SetContext(DependencyObject d, object value)
    {
      d.SetValue(View.ContextProperty, value);
    }

    private static void OnModelChanged(
      DependencyObject targetLocation,
      DependencyPropertyChangedEventArgs args)
    {
      if (args.OldValue == args.NewValue)
        return;
      if (args.NewValue != null)
      {
        object context = View.GetContext(targetLocation);
        UIElement view = ViewLocator.LocateForModel(args.NewValue, targetLocation, context);
        View.SetContentProperty((object) targetLocation, (object) view);
        ViewModelBinder.Bind(args.NewValue, (DependencyObject) view, context);
      }
      else
        View.SetContentProperty((object) targetLocation, args.NewValue);
    }

    private static void OnContextChanged(
      DependencyObject targetLocation,
      DependencyPropertyChangedEventArgs e)
    {
      if (e.OldValue == e.NewValue)
        return;
      object model = View.GetModel(targetLocation);
      if (model == null)
        return;
      UIElement view = ViewLocator.LocateForModel(model, targetLocation, e.NewValue);
      View.SetContentProperty((object) targetLocation, (object) view);
      ViewModelBinder.Bind(model, (DependencyObject) view, e.NewValue);
    }

    private static void SetContentProperty(object targetLocation, object view)
    {
      if (view is FrameworkElement frameworkElement && frameworkElement.Parent != null)
        View.SetContentPropertyCore((object) frameworkElement.Parent, (object) null);
      View.SetContentPropertyCore(targetLocation, view);
    }

    private static void SetContentPropertyCore(object targetLocation, object view)
    {
      try
      {
        Type type = targetLocation.GetType();
        ContentPropertyAttribute propertyAttribute = type.GetAttributes<ContentPropertyAttribute>(true).FirstOrDefault<ContentPropertyAttribute>() ?? View.DefaultContentProperty;
        type.GetProperty(propertyAttribute.Name).SetValue(targetLocation, view, (object[]) null);
      }
      catch (Exception ex)
      {
        View.Log.Error(ex);
      }
    }

    /// <summary>
    /// Gets a value that indicates whether the process is running in design mode.
    /// </summary>
    public static bool InDesignMode
    {
      get
      {
        if (!View.inDesignMode.HasValue)
          View.inDesignMode = new bool?(DesignerProperties.IsInDesignTool);
        return View.inDesignMode.GetValueOrDefault(false);
      }
    }
  }
}
