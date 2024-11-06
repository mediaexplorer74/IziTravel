// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ViewModelBinder
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Binds a view to a view model.</summary>
  public static class ViewModelBinder
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ViewModelBinder));
    /// <summary>
    /// Gets or sets a value indicating whether to apply conventions by default.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if conventions should be applied by default; otherwise, <c>false</c>.
    /// </value>
    public static bool ApplyConventionsByDefault = true;
    /// <summary>
    /// Indicates whether or not the conventions have already been applied to the view.
    /// </summary>
    public static readonly DependencyProperty ConventionsAppliedProperty = DependencyProperty.RegisterAttached("ConventionsApplied", typeof (bool), typeof (ViewModelBinder), (PropertyMetadata) null);
    /// <summary>
    /// Creates data bindings on the view's controls based on the provided properties.
    /// </summary>
    /// <remarks>Parameters include named Elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
    public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindProperties = (Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>>) ((namedElements, viewModelType) =>
    {
      List<FrameworkElement> frameworkElementList = new List<FrameworkElement>();
      foreach (FrameworkElement namedElement in namedElements)
      {
        string str = namedElement.Name.Trim('_');
        string[] strArray = str.Split(new char[1]{ '_' }, StringSplitOptions.RemoveEmptyEntries);
        PropertyInfo propertyCaseInsensitive = viewModelType.GetPropertyCaseInsensitive(strArray[0]);
        Type type = viewModelType;
        for (int index = 1; index < strArray.Length && propertyCaseInsensitive != null; ++index)
        {
          type = propertyCaseInsensitive.PropertyType;
          propertyCaseInsensitive = type.GetPropertyCaseInsensitive(strArray[index]);
        }
        if (propertyCaseInsensitive == null)
        {
          frameworkElementList.Add(namedElement);
          ViewModelBinder.Log.Info("Binding Convention Not Applied: Element {0} did not match a property.", (object) namedElement.Name);
        }
        else
        {
          ElementConvention elementConvention = ConventionManager.GetElementConvention(namedElement.GetType());
          if (elementConvention == null)
          {
            frameworkElementList.Add(namedElement);
            ViewModelBinder.Log.Warn("Binding Convention Not Applied: No conventions configured for {0}.", (object) namedElement.GetType());
          }
          else if (elementConvention.ApplyBinding(type, str.Replace('_', '.'), propertyCaseInsensitive, namedElement, elementConvention))
          {
            ViewModelBinder.Log.Info("Binding Convention Applied: Element {0}.", (object) namedElement.Name);
          }
          else
          {
            ViewModelBinder.Log.Info("Binding Convention Not Applied: Element {0} has existing binding.", (object) namedElement.Name);
            frameworkElementList.Add(namedElement);
          }
        }
      }
      return (IEnumerable<FrameworkElement>) frameworkElementList;
    });
    /// <summary>
    /// Attaches instances of <see cref="T:Caliburn.Micro.ActionMessage" /> to the view's controls based on the provided methods.
    /// </summary>
    /// <remarks>Parameters include the named elements to search through and the type of view model to determine conventions for. Returns unmatched elements.</remarks>
    public static Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>> BindActions = (Func<IEnumerable<FrameworkElement>, Type, IEnumerable<FrameworkElement>>) ((namedElements, viewModelType) =>
    {
      MethodInfo[] methods = viewModelType.GetMethods();
      List<FrameworkElement> list = namedElements.ToList<FrameworkElement>();
      foreach (MethodInfo methodInfo in methods)
      {
        FrameworkElement name = list.FindName(methodInfo.Name);
        if (name == null)
        {
          ViewModelBinder.Log.Info("Action Convention Not Applied: No actionable element for {0}.", (object) methodInfo.Name);
        }
        else
        {
          list.Remove(name);
          string attachText = methodInfo.Name;
          ParameterInfo[] parameters = methodInfo.GetParameters();
          if (parameters.Length > 0)
          {
            string str1 = attachText + "(";
            foreach (ParameterInfo parameterInfo in parameters)
            {
              string str2 = parameterInfo.Name;
              string key = "$" + str2.ToLower();
              if (MessageBinder.SpecialValues.ContainsKey(key))
                str2 = key;
              str1 = str1 + str2 + ",";
            }
            attachText = str1.Remove(str1.Length - 1, 1) + ")";
          }
          ViewModelBinder.Log.Info("Action Convention Applied: Action {0} on element {1}.", (object) methodInfo.Name, (object) attachText);
          Message.SetAttach((DependencyObject) name, attachText);
        }
      }
      return (IEnumerable<FrameworkElement>) list;
    });
    /// <summary>
    /// Allows the developer to add custom handling of named elements which were not matched by any default conventions.
    /// </summary>
    public static Action<IEnumerable<FrameworkElement>, Type> HandleUnmatchedElements = (Action<IEnumerable<FrameworkElement>, Type>) ((elements, viewModelType) => { });
    /// <summary>Binds the specified viewModel to the view.</summary>
    /// <remarks>Passes the the view model, view and creation context (or null for default) to use in applying binding.</remarks>
    public static Action<object, DependencyObject, object> Bind = (Action<object, DependencyObject, object>) ((viewModel, view, context) =>
    {
      if (View.InDesignMode)
      {
        Type type = viewModel.GetType();
        if (type.FullName == "Microsoft.Expression.DesignModel.InstanceBuilders.DesignInstanceExtension")
          viewModel = type.GetProperty("Instance", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(viewModel, (object[]) null);
      }
      ViewModelBinder.Log.Info("Binding {0} and {1}.", (object) view, viewModel);
      if ((bool) view.GetValue(Caliburn.Micro.Bind.NoContextProperty))
        Action.SetTargetWithoutContext(view, viewModel);
      else
        Action.SetTarget(view, viewModel);
      if (viewModel is IViewAware viewAware2)
      {
        ViewModelBinder.Log.Info("Attaching {0} to {1}.", (object) view, (object) viewAware2);
        viewAware2.AttachView((object) view, context);
      }
      if ((bool) view.GetValue(ViewModelBinder.ConventionsAppliedProperty))
        return;
      FrameworkElement element = View.GetFirstNonGeneratedView((object) view) as FrameworkElement;
      if (element == null)
        return;
      ViewModelBinder.BindAppBar(view);
      if (!ViewModelBinder.ShouldApplyConventions(element))
      {
        ViewModelBinder.Log.Info("Skipping conventions for {0} and {1}.", (object) element, viewModel);
        view.SetValue(ViewModelBinder.ConventionsAppliedProperty, (object) true);
      }
      else
      {
        Type type = viewModel.GetType();
        IEnumerable<FrameworkElement> enumerable = BindingScope.GetNamedElements((DependencyObject) element);
        enumerable.Apply<FrameworkElement>((Action<FrameworkElement>) (x => x.SetValue(View.IsLoadedProperty, element.GetValue(View.IsLoadedProperty))));
        IEnumerable<FrameworkElement> frameworkElements1 = ViewModelBinder.BindActions(enumerable, type);
        IEnumerable<FrameworkElement> frameworkElements2 = ViewModelBinder.BindProperties(frameworkElements1, type);
        ViewModelBinder.HandleUnmatchedElements(frameworkElements2, type);
        view.SetValue(ViewModelBinder.ConventionsAppliedProperty, (object) true);
      }
    });

    /// <summary>
    /// Determines whether a view should have conventions applied to it.
    /// </summary>
    /// <param name="view">The view to check.</param>
    /// <returns>Whether or not conventions should be applied to the view.</returns>
    public static bool ShouldApplyConventions(FrameworkElement view)
    {
      return View.GetApplyConventions((DependencyObject) view).GetValueOrDefault(ViewModelBinder.ApplyConventionsByDefault);
    }

    private static void BindAppBar(DependencyObject view)
    {
      if (!(view is PhoneApplicationPage phoneApplicationPage) || phoneApplicationPage.ApplicationBar == null)
        return;
      System.Windows.Interactivity.TriggerCollection triggers = Interaction.GetTriggers(view);
      foreach (object button1 in (IEnumerable) phoneApplicationPage.ApplicationBar.Buttons)
      {
        IAppBarActionMessage button = button1 as IAppBarActionMessage;
        if (button != null && !string.IsNullOrEmpty(button.Message))
        {
          System.Windows.Interactivity.TriggerBase parsedTrigger = Parser.Parse(view, button.Message).First<System.Windows.Interactivity.TriggerBase>();
          AppBarItemTrigger trigger = new AppBarItemTrigger((IApplicationBarMenuItem) button);
          parsedTrigger.Actions.OfType<ActionMessage>().ToList<ActionMessage>().Apply<ActionMessage>((Action<ActionMessage>) (x =>
          {
            x.applicationBarSource = (IApplicationBarMenuItem) button;
            parsedTrigger.Actions.Remove((System.Windows.Interactivity.TriggerAction) x);
            trigger.Actions.Add((System.Windows.Interactivity.TriggerAction) x);
          }));
          triggers.Add((System.Windows.Interactivity.TriggerBase) trigger);
        }
      }
      foreach (object menuItem1 in (IEnumerable) phoneApplicationPage.ApplicationBar.MenuItems)
      {
        IAppBarActionMessage menuItem = menuItem1 as IAppBarActionMessage;
        if (menuItem != null && !string.IsNullOrEmpty(menuItem.Message))
        {
          System.Windows.Interactivity.TriggerBase parsedTrigger = Parser.Parse(view, menuItem.Message).First<System.Windows.Interactivity.TriggerBase>();
          AppBarItemTrigger trigger = new AppBarItemTrigger((IApplicationBarMenuItem) menuItem);
          parsedTrigger.Actions.OfType<ActionMessage>().ToList<ActionMessage>().Apply<ActionMessage>((Action<ActionMessage>) (x =>
          {
            x.applicationBarSource = (IApplicationBarMenuItem) menuItem;
            parsedTrigger.Actions.Remove((System.Windows.Interactivity.TriggerAction) x);
            trigger.Actions.Add((System.Windows.Interactivity.TriggerAction) x);
          }));
          triggers.Add((System.Windows.Interactivity.TriggerBase) trigger);
        }
      }
    }
  }
}
