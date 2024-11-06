// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Action
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A host for action related attached properties.</summary>
  public static class Action
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (Action));
    /// <summary>
    ///   A property definition representing the target of an <see cref="T:Caliburn.Micro.ActionMessage" /> . The DataContext of the element will be set to this instance.
    /// </summary>
    public static readonly DependencyProperty TargetProperty = DependencyProperty.RegisterAttached("Target", typeof (object), typeof (Action), new PropertyMetadata((object) null, new PropertyChangedCallback(Action.OnTargetChanged)));
    /// <summary>
    ///   A property definition representing the target of an <see cref="T:Caliburn.Micro.ActionMessage" /> . The DataContext of the element is not set to this instance.
    /// </summary>
    public static readonly DependencyProperty TargetWithoutContextProperty = DependencyProperty.RegisterAttached("TargetWithoutContext", typeof (object), typeof (Action), new PropertyMetadata((object) null, new PropertyChangedCallback(Action.OnTargetWithoutContextChanged)));

    /// <summary>
    ///   Sets the target of the <see cref="T:Caliburn.Micro.ActionMessage" /> .
    /// </summary>
    /// <param name="d"> The element to attach the target to. </param>
    /// <param name="target"> The target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> . </param>
    public static void SetTarget(DependencyObject d, object target)
    {
      d.SetValue(Action.TargetProperty, target);
    }

    /// <summary>
    ///   Gets the target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> .
    /// </summary>
    /// <param name="d"> The element to which the target is attached. </param>
    /// <returns> The target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> </returns>
    public static object GetTarget(DependencyObject d) => d.GetValue(Action.TargetProperty);

    /// <summary>
    ///   Sets the target of the <see cref="T:Caliburn.Micro.ActionMessage" /> .
    /// </summary>
    /// <param name="d"> The element to attach the target to. </param>
    /// <param name="target"> The target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> . </param>
    /// <remarks>The DataContext will not be set.</remarks>
    public static void SetTargetWithoutContext(DependencyObject d, object target)
    {
      d.SetValue(Action.TargetWithoutContextProperty, target);
    }

    /// <summary>
    ///   Gets the target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> .
    /// </summary>
    /// <param name="d"> The element to which the target is attached. </param>
    /// <returns> The target for instances of <see cref="T:Caliburn.Micro.ActionMessage" /> </returns>
    public static object GetTargetWithoutContext(DependencyObject d)
    {
      return d.GetValue(Action.TargetWithoutContextProperty);
    }

    /// <summary>
    ///   Checks if the <see cref="T:Caliburn.Micro.ActionMessage" /> -Target was set.
    /// </summary>
    /// <param name="element"> DependencyObject to check </param>
    /// <returns> True if Target or TargetWithoutContext was set on <paramref name="element" /> </returns>
    public static bool HasTargetSet(DependencyObject element)
    {
      if (Action.GetTarget(element) != null || Action.GetTargetWithoutContext(element) != null)
        return true;
      if (!(element is FrameworkElement element1))
        return false;
      return ConventionManager.HasBinding(element1, Action.TargetProperty) || ConventionManager.HasBinding(element1, Action.TargetWithoutContextProperty);
    }

    /// <summary>Uses the action pipeline to invoke the method.</summary>
    /// <param name="target"> The object instance to invoke the method on. </param>
    /// <param name="methodName"> The name of the method to invoke. </param>
    /// <param name="view"> The view. </param>
    /// <param name="source"> The source of the invocation. </param>
    /// <param name="eventArgs"> The event args. </param>
    /// <param name="parameters"> The method parameters. </param>
    public static void Invoke(
      object target,
      string methodName,
      DependencyObject view = null,
      FrameworkElement source = null,
      object eventArgs = null,
      object[] parameters = null)
    {
      ActionExecutionContext context = new ActionExecutionContext()
      {
        Target = target,
        Method = target.GetType().GetMethod(methodName),
        Message = new ActionMessage()
        {
          MethodName = methodName
        },
        View = view,
        Source = source,
        EventArgs = eventArgs
      };
      if (parameters != null)
        ((IEnumerable<object>) parameters).Apply<object>((Action<object>) (x =>
        {
          AttachedCollection<Parameter> parameters1 = context.Message.Parameters;
          if (!(x is Parameter parameter2))
            parameter2 = new Parameter() { Value = x };
          parameters1.Add(parameter2);
        }));
      ActionMessage.InvokeAction(context);
    }

    private static void OnTargetWithoutContextChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Action.SetTargetCore(e, d, false);
    }

    private static void OnTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Action.SetTargetCore(e, d, true);
    }

    private static void SetTargetCore(
      DependencyPropertyChangedEventArgs e,
      DependencyObject d,
      bool setContext)
    {
      if (e.NewValue == e.OldValue)
        return;
      object obj = e.NewValue;
      if (e.NewValue is string newValue)
        obj = IoC.GetInstance((Type) null, newValue);
      if (setContext && d is FrameworkElement)
      {
        Action.Log.Info("Setting DC of {0} to {1}.", (object) d, obj);
        ((FrameworkElement) d).DataContext = obj;
      }
      Action.Log.Info("Attaching message handler {0} to {1}.", obj, (object) d);
      Message.SetHandler(d, obj);
    }
  }
}
