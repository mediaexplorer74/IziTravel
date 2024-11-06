// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ConventionManager
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Used to configure the conventions used by the framework to apply bindings and create actions.
  /// </summary>
  public static class ConventionManager
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ConventionManager));
    /// <summary>
    /// Converters <see cref="T:System.Boolean" /> to/from <see cref="T:System.Windows.Visibility" />.
    /// </summary>
    public static IValueConverter BooleanToVisibilityConverter = (IValueConverter) new Caliburn.Micro.BooleanToVisibilityConverter();
    /// <summary>
    /// Indicates whether or not static properties should be included during convention name matching.
    /// </summary>
    /// <remarks>False by default.</remarks>
    public static bool IncludeStaticProperties = false;
    /// <summary>
    /// Indicates whether or not the Content of ContentControls should be overwritten by conventional bindings.
    /// </summary>
    /// <remarks>False by default.</remarks>
    public static bool OverwriteContent = false;
    /// <summary>
    /// The default DataTemplate used for ItemsControls when required.
    /// </summary>
    public static DataTemplate DefaultItemTemplate = (DataTemplate) XamlReader.Load("<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro.Platform'> <ContentControl cal:View.Model=\"{Binding}\" VerticalContentAlignment=\"Stretch\" HorizontalContentAlignment=\"Stretch\" IsTabStop=\"False\" /></DataTemplate>");
    /// <summary>
    /// The default DataTemplate used for Headered controls when required.
    /// </summary>
    public static DataTemplate DefaultHeaderTemplate = (DataTemplate) XamlReader.Load("<DataTemplate xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'><TextBlock Text=\"{Binding DisplayName, Mode=TwoWay}\" /></DataTemplate>");
    private static readonly Dictionary<Type, ElementConvention> ElementConventions = new Dictionary<Type, ElementConvention>();
    /// <summary>
    /// Changes the provided word from a plural form to a singular form.
    /// </summary>
    public static Func<string, string> Singularize = (Func<string, string>) (original => !original.EndsWith("ies") ? original.TrimEnd('s') : original.TrimEnd('s').TrimEnd('e').TrimEnd('i') + "y");
    /// <summary>Derives the SelectedItem property name.</summary>
    public static Func<string, IEnumerable<string>> DerivePotentialSelectionNames = (Func<string, IEnumerable<string>>) (name =>
    {
      string str = ConventionManager.Singularize(name);
      return (IEnumerable<string>) new string[3]
      {
        "Active" + str,
        "Selected" + str,
        "Current" + str
      };
    });
    /// <summary>
    /// Creates a binding and sets it on the element, applying the appropriate conventions.
    /// </summary>
    /// <param name="viewModelType"></param>
    /// <param name="path"></param>
    /// <param name="property"></param>
    /// <param name="element"></param>
    /// <param name="convention"></param>
    /// <param name="bindableProperty"></param>
    public static Action<Type, string, PropertyInfo, FrameworkElement, ElementConvention, DependencyProperty> SetBinding = (Action<Type, string, PropertyInfo, FrameworkElement, ElementConvention, DependencyProperty>) ((viewModelType, path, property, element, convention, bindableProperty) =>
    {
      Binding binding = new Binding(path);
      ConventionManager.ApplyBindingMode(binding, property);
      ConventionManager.ApplyValueConverter(binding, bindableProperty, property);
      ConventionManager.ApplyStringFormat(binding, convention, property);
      ConventionManager.ApplyValidation(binding, viewModelType, property);
      ConventionManager.ApplyUpdateSourceTrigger(bindableProperty, (DependencyObject) element, binding, property);
      BindingOperations.SetBinding((DependencyObject) element, bindableProperty, (BindingBase) binding);
    });
    /// <summary>Applies the appropriate binding mode to the binding.</summary>
    public static Action<Binding, PropertyInfo> ApplyBindingMode = (Action<Binding, PropertyInfo>) ((binding, property) =>
    {
      MethodInfo setMethod = property.GetSetMethod();
      binding.Mode = !property.CanWrite || setMethod == null || !setMethod.IsPublic ? BindingMode.OneWay : BindingMode.TwoWay;
    });
    /// <summary>
    /// Determines whether or not and what type of validation to enable on the binding.
    /// </summary>
    public static Action<Binding, Type, PropertyInfo> ApplyValidation = (Action<Binding, Type, PropertyInfo>) ((binding, viewModelType, property) =>
    {
      if (typeof (INotifyDataErrorInfo).IsAssignableFrom(viewModelType))
      {
        binding.ValidatesOnNotifyDataErrors = true;
        binding.ValidatesOnExceptions = true;
      }
      if (!typeof (IDataErrorInfo).IsAssignableFrom(viewModelType))
        return;
      binding.ValidatesOnDataErrors = true;
      binding.ValidatesOnExceptions = true;
    });
    /// <summary>
    /// Determines whether a value converter is is needed and applies one to the binding.
    /// </summary>
    public static Action<Binding, DependencyProperty, PropertyInfo> ApplyValueConverter = (Action<Binding, DependencyProperty, PropertyInfo>) ((binding, bindableProperty, property) =>
    {
      if (bindableProperty != UIElement.VisibilityProperty || !typeof (bool).IsAssignableFrom(property.PropertyType))
        return;
      binding.Converter = ConventionManager.BooleanToVisibilityConverter;
    });
    /// <summary>
    /// Determines whether a custom string format is needed and applies it to the binding.
    /// </summary>
    public static Action<Binding, ElementConvention, PropertyInfo> ApplyStringFormat = (Action<Binding, ElementConvention, PropertyInfo>) ((binding, convention, property) =>
    {
      if (!typeof (DateTime).IsAssignableFrom(property.PropertyType))
        return;
      binding.StringFormat = "{0:d}";
    });
    /// <summary>
    /// Determines whether a custom update source trigger should be applied to the binding.
    /// </summary>
    public static Action<DependencyProperty, DependencyObject, Binding, PropertyInfo> ApplyUpdateSourceTrigger = (Action<DependencyProperty, DependencyObject, Binding, PropertyInfo>) ((bindableProperty, element, binding, info) => ConventionManager.ApplySilverlightTriggers(element, bindableProperty, (Func<FrameworkElement, BindingExpression>) (x => x.GetBindingExpression(bindableProperty)), info, binding));
    /// <summary>Configures the selected item convention.</summary>
    /// <param name="selector">The element that has a SelectedItem property.</param>
    /// <param name="selectedItemProperty">The SelectedItem property.</param>
    /// <param name="viewModelType">The view model type.</param>
    /// <param name="path">The property path.</param>
    public static Action<FrameworkElement, DependencyProperty, Type, string> ConfigureSelectedItem = (Action<FrameworkElement, DependencyProperty, Type, string>) ((selector, selectedItemProperty, viewModelType, path) =>
    {
      if (ConventionManager.HasBinding(selector, selectedItemProperty))
        return;
      int num = path.LastIndexOf('.');
      int startIndex = num == -1 ? 0 : num + 1;
      string oldValue = path.Substring(startIndex);
      foreach (string str in ConventionManager.DerivePotentialSelectionNames(oldValue))
      {
        if (viewModelType.GetPropertyCaseInsensitive(str) != null)
        {
          string path1 = path.Replace(oldValue, str);
          Binding binding = new Binding(path1)
          {
            Mode = BindingMode.TwoWay
          };
          if (ConventionManager.ConfigureSelectedItemBinding(selector, selectedItemProperty, viewModelType, path1, binding))
          {
            BindingOperations.SetBinding((DependencyObject) selector, selectedItemProperty, (BindingBase) binding);
            ConventionManager.Log.Info("SelectedItem binding applied to {0}.", (object) selector.Name);
            break;
          }
          ConventionManager.Log.Info("SelectedItem binding not applied to {0} due to 'ConfigureSelectedItemBinding' customization.", (object) selector.Name);
        }
      }
    });
    /// <summary>
    /// Configures the SelectedItem binding for matched selection path.
    /// </summary>
    /// <param name="selector">The element that has a SelectedItem property.</param>
    /// <param name="selectedItemProperty">The SelectedItem property.</param>
    /// <param name="viewModelType">The view model type.</param>
    /// <param name="selectionPath">The property path.</param>
    /// <param name="binding">The binding to configure.</param>
    /// <returns>A bool indicating whether to apply binding</returns>
    public static Func<FrameworkElement, DependencyProperty, Type, string, Binding, bool> ConfigureSelectedItemBinding = (Func<FrameworkElement, DependencyProperty, Type, string, Binding, bool>) ((selector, selectedItemProperty, viewModelType, selectionPath, binding) => true);

    static ConventionManager()
    {
      ConventionManager.AddElementConvention<HyperlinkButton>(ContentControl.ContentProperty, "DataContext", "Click");
      ConventionManager.AddElementConvention<PasswordBox>(PasswordBox.PasswordProperty, "Password", "PasswordChanged");
      ConventionManager.AddElementConvention<UserControl>(UIElement.VisibilityProperty, "DataContext", "Loaded");
      ConventionManager.AddElementConvention<Image>(Image.SourceProperty, "Source", "Loaded");
      ConventionManager.AddElementConvention<ToggleButton>(ToggleButton.IsCheckedProperty, "IsChecked", "Click");
      ConventionManager.AddElementConvention<ButtonBase>(ContentControl.ContentProperty, "DataContext", "Click");
      ConventionManager.AddElementConvention<TextBox>(TextBox.TextProperty, "Text", "TextChanged");
      ConventionManager.AddElementConvention<TextBlock>(TextBlock.TextProperty, "Text", "DataContextChanged");
      ConventionManager.AddElementConvention<ProgressBar>(RangeBase.ValueProperty, "Value", "ValueChanged");
      ConventionManager.AddElementConvention<Selector>(ItemsControl.ItemsSourceProperty, "SelectedItem", "SelectionChanged").ApplyBinding = (Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool>) ((viewModelType, path, property, element, convention) =>
      {
        if (!ConventionManager.SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty))
          return false;
        ConventionManager.ConfigureSelectedItem(element, Selector.SelectedItemProperty, viewModelType, path);
        ConventionManager.ApplyItemTemplate((ItemsControl) element, property);
        return true;
      });
      ConventionManager.AddElementConvention<ItemsControl>(ItemsControl.ItemsSourceProperty, "DataContext", "Loaded").ApplyBinding = (Func<Type, string, PropertyInfo, FrameworkElement, ElementConvention, bool>) ((viewModelType, path, property, element, convention) =>
      {
        if (!ConventionManager.SetBindingWithoutBindingOrValueOverwrite(viewModelType, path, property, element, convention, ItemsControl.ItemsSourceProperty))
          return false;
        ConventionManager.ApplyItemTemplate((ItemsControl) element, property);
        return true;
      });
      ConventionManager.AddElementConvention<ContentControl>(ContentControl.ContentProperty, "DataContext", "Loaded").GetBindableProperty = (Func<DependencyObject, DependencyProperty>) (foundControl =>
      {
        ContentControl contentControl = (ContentControl) foundControl;
        if (contentControl.Content is DependencyObject && !ConventionManager.OverwriteContent)
          return (DependencyProperty) null;
        if (contentControl.ContentTemplate == null)
        {
          ConventionManager.Log.Info("ViewModel bound on {0}.", (object) contentControl.Name);
          return View.ModelProperty;
        }
        ConventionManager.Log.Info("Content bound on {0}. Template or content was present.", (object) contentControl.Name);
        return ContentControl.ContentProperty;
      });
      ConventionManager.AddElementConvention<Shape>(UIElement.VisibilityProperty, "DataContext", "MouseLeftButtonUp");
      ConventionManager.AddElementConvention<FrameworkElement>(UIElement.VisibilityProperty, "DataContext", "Loaded");
    }

    /// <summary>Adds an element convention.</summary>
    /// <typeparam name="T">The type of element.</typeparam>
    /// <param name="bindableProperty">The default property for binding conventions.</param>
    /// <param name="parameterProperty">The default property for action parameters.</param>
    /// <param name="eventName">The default event to trigger actions.</param>
    public static ElementConvention AddElementConvention<T>(
      DependencyProperty bindableProperty,
      string parameterProperty,
      string eventName)
    {
      return ConventionManager.AddElementConvention(new ElementConvention()
      {
        ElementType = typeof (T),
        GetBindableProperty = (Func<DependencyObject, DependencyProperty>) (element => bindableProperty),
        ParameterProperty = parameterProperty,
        CreateTrigger = (Func<System.Windows.Interactivity.TriggerBase>) (() => (System.Windows.Interactivity.TriggerBase) new System.Windows.Interactivity.EventTrigger()
        {
          EventName = eventName
        })
      });
    }

    /// <summary>Adds an element convention.</summary>
    /// <param name="convention"></param>
    public static ElementConvention AddElementConvention(ElementConvention convention)
    {
      return ConventionManager.ElementConventions[convention.ElementType] = convention;
    }

    /// <summary>
    /// Gets an element convention for the provided element type.
    /// </summary>
    /// <param name="elementType">The type of element to locate the convention for.</param>
    /// <returns>The convention if found, null otherwise.</returns>
    /// <remarks>Searches the class hierarchy for conventions.</remarks>
    public static ElementConvention GetElementConvention(Type elementType)
    {
      if (elementType == null)
        return (ElementConvention) null;
      ElementConvention elementConvention;
      ConventionManager.ElementConventions.TryGetValue(elementType, out elementConvention);
      return elementConvention ?? ConventionManager.GetElementConvention(elementType.BaseType);
    }

    /// <summary>
    /// Determines whether a particular dependency property already has a binding on the provided element.
    /// </summary>
    public static bool HasBinding(FrameworkElement element, DependencyProperty property)
    {
      return element.GetBindingExpression(property) != null;
    }

    /// <summary>
    /// Creates a binding and sets it on the element, guarding against pre-existing bindings.
    /// </summary>
    public static bool SetBindingWithoutBindingOverwrite(
      Type viewModelType,
      string path,
      PropertyInfo property,
      FrameworkElement element,
      ElementConvention convention,
      DependencyProperty bindableProperty)
    {
      if (bindableProperty == null || ConventionManager.HasBinding(element, bindableProperty))
        return false;
      ConventionManager.SetBinding(viewModelType, path, property, element, convention, bindableProperty);
      return true;
    }

    /// <summary>
    /// Creates a binding and set it on the element, guarding against pre-existing bindings and pre-existing values.
    /// </summary>
    /// <param name="viewModelType"></param>
    /// <param name="path"></param>
    /// <param name="property"></param>
    /// <param name="element"></param>
    /// <param name="convention"></param>
    /// <param name="bindableProperty"> </param>
    /// <returns></returns>
    public static bool SetBindingWithoutBindingOrValueOverwrite(
      Type viewModelType,
      string path,
      PropertyInfo property,
      FrameworkElement element,
      ElementConvention convention,
      DependencyProperty bindableProperty)
    {
      if (bindableProperty == null || ConventionManager.HasBinding(element, bindableProperty) || element.GetValue(bindableProperty) != null)
        return false;
      ConventionManager.SetBinding(viewModelType, path, property, element, convention, bindableProperty);
      return true;
    }

    /// <summary>
    /// Attempts to apply the default item template to the items control.
    /// </summary>
    /// <param name="itemsControl">The items control.</param>
    /// <param name="property">The collection property.</param>
    public static void ApplyItemTemplate(ItemsControl itemsControl, PropertyInfo property)
    {
      if (!string.IsNullOrEmpty(itemsControl.DisplayMemberPath) || ConventionManager.HasBinding((FrameworkElement) itemsControl, ItemsControl.DisplayMemberPathProperty) || itemsControl.ItemTemplate != null)
        return;
      if (property.PropertyType.IsGenericType)
      {
        Type c = ((IEnumerable<Type>) property.PropertyType.GetGenericArguments()).First<Type>();
        if (c.IsValueType || typeof (string).IsAssignableFrom(c))
          return;
      }
      itemsControl.ItemTemplate = ConventionManager.DefaultItemTemplate;
      ConventionManager.Log.Info("ItemTemplate applied to {0}.", (object) itemsControl.Name);
    }

    /// <summary>
    /// Applies a header template based on <see cref="T:Caliburn.Micro.IHaveDisplayName" />
    /// </summary>
    /// <param name="element"></param>
    /// <param name="headerTemplateProperty"></param>
    /// <param name="headerTemplateSelectorProperty"> </param>
    /// <param name="viewModelType"></param>
    public static void ApplyHeaderTemplate(
      FrameworkElement element,
      DependencyProperty headerTemplateProperty,
      DependencyProperty headerTemplateSelectorProperty,
      Type viewModelType)
    {
      object obj1 = element.GetValue(headerTemplateProperty);
      object obj2 = headerTemplateSelectorProperty != null ? element.GetValue(headerTemplateSelectorProperty) : (object) null;
      if (obj1 != null || obj2 != null || !typeof (IHaveDisplayName).IsAssignableFrom(viewModelType))
        return;
      element.SetValue(headerTemplateProperty, (object) ConventionManager.DefaultHeaderTemplate);
      ConventionManager.Log.Info("Header template applied to {0}.", (object) element.Name);
    }

    /// <summary>
    /// Gets a property by name, ignoring case and searching all interfaces.
    /// </summary>
    /// <param name="type">The type to inspect.</param>
    /// <param name="propertyName">The property to search for.</param>
    /// <returns>The property or null if not found.</returns>
    public static PropertyInfo GetPropertyCaseInsensitive(this Type type, string propertyName)
    {
      List<Type> source = new List<Type>() { type };
      if (type.IsInterface)
        source.AddRange((IEnumerable<Type>) type.GetInterfaces());
      BindingFlags flags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
      if (ConventionManager.IncludeStaticProperties)
        flags |= BindingFlags.Static;
      return source.Select<Type, PropertyInfo>((Func<Type, PropertyInfo>) (interfaceType => interfaceType.GetProperty(propertyName, flags))).FirstOrDefault<PropertyInfo>((Func<PropertyInfo, bool>) (property => property != null));
    }

    /// <summary>
    /// Accounts for the lack of UpdateSourceTrigger in silverlight.
    /// </summary>
    /// <param name="element">The element to wire for change events on.</param>
    /// <param name="dependencyProperty">The property that is being bound.</param>
    /// <param name="expressionSource">Gets the the binding expression that needs to be updated.</param>
    /// <param name="property">The property being bound to if available.</param>
    /// <param name="binding">The binding if available.</param>
    public static void ApplySilverlightTriggers(
      DependencyObject element,
      DependencyProperty dependencyProperty,
      Func<FrameworkElement, BindingExpression> expressionSource,
      PropertyInfo property,
      Binding binding)
    {
      TextBox textBox = element as TextBox;
      if (textBox != null && dependencyProperty == TextBox.TextProperty)
      {
        if (property != null)
        {
          switch (Type.GetTypeCode(property.PropertyType))
          {
            case TypeCode.Single:
            case TypeCode.Double:
            case TypeCode.Decimal:
              binding.UpdateSourceTrigger = UpdateSourceTrigger.Explicit;
              textBox.KeyUp += (KeyEventHandler) ((param0, param1) =>
              {
                int selectionStart = textBox.SelectionStart;
                string text = textBox.Text;
                expressionSource((FrameworkElement) textBox).UpdateSource();
                textBox.Text = text;
                textBox.SelectionStart = selectionStart;
              });
              return;
          }
        }
        textBox.TextChanged += (TextChangedEventHandler) ((param0, param1) => expressionSource((FrameworkElement) textBox).UpdateSource());
      }
      else
      {
        PasswordBox passwordBox = element as PasswordBox;
        if (passwordBox == null || dependencyProperty != PasswordBox.PasswordProperty)
          return;
        passwordBox.PasswordChanged += (RoutedEventHandler) ((param0, param1) => expressionSource((FrameworkElement) passwordBox).UpdateSource());
      }
    }
  }
}
