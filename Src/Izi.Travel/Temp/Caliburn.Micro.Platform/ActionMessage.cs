// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ActionMessage
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Interactivity;
using System.Windows.Markup;
using System.Windows.Media;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Used to send a message from the UI to a presentation model class, indicating that a particular Action should be invoked.
  /// </summary>
  [DefaultTrigger(typeof (FrameworkElement), typeof (System.Windows.Interactivity.EventTrigger), "MouseLeftButtonDown")]
  [DefaultTrigger(typeof (ButtonBase), typeof (System.Windows.Interactivity.EventTrigger), "Click")]
  [ContentProperty("Parameters")]
  [TypeConstraint(typeof (FrameworkElement))]
  public class ActionMessage : TriggerAction<FrameworkElement>, IHaveParameters
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ActionMessage));
    private ActionExecutionContext context;
    internal IApplicationBarMenuItem applicationBarSource;
    internal static readonly DependencyProperty HandlerProperty = DependencyProperty.RegisterAttached("Handler", typeof (object), typeof (ActionMessage), new PropertyMetadata((object) null, new PropertyChangedCallback(ActionMessage.HandlerPropertyChanged)));
    /// <summary>
    ///  Causes the action invocation to "double check" if the action should be invoked by executing the guard immediately before hand.
    /// </summary>
    /// <remarks>This is disabled by default. If multiple actions are attached to the same element, you may want to enable this so that each individaul action checks its guard regardless of how the UI state appears.</remarks>
    public static bool EnforceGuardsDuringInvocation = false;
    /// <summary>
    ///  Causes the action to throw if it cannot locate the target or the method at invocation time.
    /// </summary>
    /// <remarks>True by default.</remarks>
    public static bool ThrowsExceptions = true;
    /// <summary>Represents the method name of an action message.</summary>
    public static readonly DependencyProperty MethodNameProperty = DependencyProperty.Register(nameof (MethodName), typeof (string), typeof (ActionMessage), (PropertyMetadata) null);
    /// <summary>Represents the parameters of an action message.</summary>
    public static readonly DependencyProperty ParametersProperty = DependencyProperty.Register(nameof (Parameters), typeof (AttachedCollection<Parameter>), typeof (ActionMessage), (PropertyMetadata) null);
    /// <summary>
    /// Invokes the action using the specified <see cref="T:Caliburn.Micro.ActionExecutionContext" />
    /// </summary>
    public static Action<ActionExecutionContext> InvokeAction = (Action<ActionExecutionContext>) (context =>
    {
      object[] parameters = MessageBinder.DetermineParameters(context, context.Method.GetParameters());
      object obj = context.Method.Invoke(context.Target, parameters);
      if (obj is Task task2)
        obj = (object) task2.AsResult();
      if (obj is IResult result2)
        obj = (object) new IResult[1]{ result2 };
      if (obj is IEnumerable<IResult> results2)
        obj = (object) results2.GetEnumerator();
      if (!(obj is IEnumerator<IResult> coroutine2))
        return;
      CoroutineExecutionContext context1 = new CoroutineExecutionContext()
      {
        Source = (object) context.Source,
        View = (object) context.View,
        Target = context.Target
      };
      Coroutine.BeginExecute(coroutine2, context1);
    });
    /// <summary>
    /// Applies an availability effect, such as IsEnabled, to an element.
    /// </summary>
    /// <remarks>Returns a value indicating whether or not the action is available.</remarks>
    public static Func<ActionExecutionContext, bool> ApplyAvailabilityEffect = (Func<ActionExecutionContext, bool>) (context =>
    {
      ActionMessage message = context.Message;
      if (message != null && message.applicationBarSource != null)
      {
        if (context.CanExecute != null)
          message.applicationBarSource.IsEnabled = context.CanExecute();
        return message.applicationBarSource.IsEnabled;
      }
      if (!(context.Source is Control source2))
        return true;
      if (!ConventionManager.HasBinding((FrameworkElement) source2, Control.IsEnabledProperty) && context.CanExecute != null)
        source2.IsEnabled = context.CanExecute();
      return source2.IsEnabled;
    });
    /// <summary>
    /// Finds the method on the target matching the specified message.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="message">The message.</param>
    /// <returns>The matching method, if available.</returns>
    public static Func<ActionMessage, object, MethodInfo> GetTargetMethod = (Func<ActionMessage, object, MethodInfo>) ((message, target) => ((IEnumerable<MethodInfo>) target.GetType().GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (method => method.Name == message.MethodName)).Select(method => new
    {
      method = method,
      methodParameters = method.GetParameters()
    }).Where(_param1 => message.Parameters.Count == _param1.methodParameters.Length).Select(_param0 => _param0.method).FirstOrDefault<MethodInfo>());
    /// <summary>
    /// Sets the target, method and view on the context. Uses a bubbling strategy by default.
    /// </summary>
    public static Action<ActionExecutionContext> SetMethodBinding = (Action<ActionExecutionContext>) (context =>
    {
      FrameworkElement source3 = context.Source;
      for (DependencyObject dependencyObject = (DependencyObject) source3; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
      {
        if (Action.HasTargetSet(dependencyObject))
        {
          object handler = Message.GetHandler(dependencyObject);
          if (handler != null)
          {
            MethodInfo methodInfo = ActionMessage.GetTargetMethod(context.Message, handler);
            if (methodInfo != null)
            {
              context.Method = methodInfo;
              context.Target = handler;
              context.View = dependencyObject;
              return;
            }
          }
          else
          {
            context.View = dependencyObject;
            return;
          }
        }
      }
      if (source3 == null || source3.DataContext == null)
        return;
      object dataContext = source3.DataContext;
      MethodInfo methodInfo1 = ActionMessage.GetTargetMethod(context.Message, dataContext);
      if (methodInfo1 == null)
        return;
      context.Target = dataContext;
      context.Method = methodInfo1;
      context.View = (DependencyObject) source3;
    });
    /// <summary>Prepares the action execution context for use.</summary>
    public static Action<ActionExecutionContext> PrepareContext = (Action<ActionExecutionContext>) (context =>
    {
      ActionMessage.SetMethodBinding(context);
      if (context.Target == null || context.Method == null)
        return;
      string guardName = "Can" + context.Method.Name;
      Type type = context.Target.GetType();
      MethodInfo guard = ActionMessage.TryFindGuardMethod(context);
      if (guard == null)
      {
        INotifyPropertyChanged inpc = context.Target as INotifyPropertyChanged;
        if (inpc == null)
          return;
        guard = type.GetMethod("get_" + guardName);
        if (guard == null)
          return;
        PropertyChangedEventHandler handler = (PropertyChangedEventHandler) null;
        handler = (PropertyChangedEventHandler) ((s, e) =>
        {
          if (!string.IsNullOrEmpty(e.PropertyName) && !(e.PropertyName == guardName))
            return;
          ((System.Action) (() =>
          {
            ActionMessage message = context.Message;
            if (message == null)
              inpc.PropertyChanged -= handler;
            else
              message.UpdateAvailability();
          })).OnUIThread();
        });
        inpc.PropertyChanged += handler;
        context.Disposing += (EventHandler) ((param0, param1) => inpc.PropertyChanged -= handler);
        context.Message.Detaching += (EventHandler) ((param0, param1) => inpc.PropertyChanged -= handler);
      }
      context.CanExecute = (Func<bool>) (() => (bool) guard.Invoke(context.Target, MessageBinder.DetermineParameters(context, guard.GetParameters())));
    });

    /// <summary>
    /// Creates an instance of <see cref="T:Caliburn.Micro.ActionMessage" />.
    /// </summary>
    public ActionMessage()
    {
      this.SetValue(ActionMessage.ParametersProperty, (object) new AttachedCollection<Parameter>());
    }

    /// <summary>
    /// Gets or sets the name of the method to be invoked on the presentation model class.
    /// </summary>
    /// <value>The name of the method.</value>
    [Category("Common Properties")]
    public string MethodName
    {
      get => (string) this.GetValue(ActionMessage.MethodNameProperty);
      set => this.SetValue(ActionMessage.MethodNameProperty, (object) value);
    }

    /// <summary>
    /// Gets the parameters to pass as part of the method invocation.
    /// </summary>
    /// <value>The parameters.</value>
    [Category("Common Properties")]
    public AttachedCollection<Parameter> Parameters
    {
      get => (AttachedCollection<Parameter>) this.GetValue(ActionMessage.ParametersProperty);
    }

    /// <summary>
    /// Occurs before the message detaches from the associated object.
    /// </summary>
    public event EventHandler Detaching = (param0, param1) => { };

    /// <summary>
    /// Called after the action is attached to an AssociatedObject.
    /// </summary>
    protected override void OnAttached()
    {
      if (!View.InDesignMode)
      {
        this.Parameters.Attach((DependencyObject) this.AssociatedObject);
        this.Parameters.Apply<Parameter>((Action<Parameter>) (x => x.MakeAwareOf(this)));
        if (View.ExecuteOnLoad(this.AssociatedObject, new RoutedEventHandler(this.ElementLoaded)) && Interaction.GetTriggers((DependencyObject) this.AssociatedObject).FirstOrDefault<System.Windows.Interactivity.TriggerBase>((Func<System.Windows.Interactivity.TriggerBase, bool>) (t => t.Actions.Contains((System.Windows.Interactivity.TriggerAction) this))) is System.Windows.Interactivity.EventTrigger eventTrigger && eventTrigger.EventName == "Loaded")
          this.Invoke((object) new RoutedEventArgs());
      }
      base.OnAttached();
    }

    private static void HandlerPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ((ActionMessage) d).UpdateContext();
    }

    /// <summary>
    /// Called when the action is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    protected override void OnDetaching()
    {
      if (!View.InDesignMode)
      {
        this.Detaching((object) this, EventArgs.Empty);
        this.AssociatedObject.Loaded -= new RoutedEventHandler(this.ElementLoaded);
        this.Parameters.Detach();
      }
      base.OnDetaching();
    }

    private void ElementLoaded(object sender, RoutedEventArgs e)
    {
      this.UpdateContext();
      DependencyObject dependencyObject;
      if (this.context.View == null)
      {
        dependencyObject = (DependencyObject) this.AssociatedObject;
        while (dependencyObject != null && !Action.HasTargetSet(dependencyObject))
          dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
      }
      else
        dependencyObject = this.context.View;
      Binding binding = (Binding) XamlReader.Load("<Binding xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation' xmlns:cal='clr-namespace:Caliburn.Micro;assembly=Caliburn.Micro.Platform' Path='(cal:Message.Handler)' />");
      binding.Source = (object) dependencyObject;
      BindingOperations.SetBinding((DependencyObject) this, ActionMessage.HandlerProperty, (BindingBase) binding);
    }

    private void UpdateContext()
    {
      if (this.context != null)
        this.context.Dispose();
      this.context = new ActionExecutionContext()
      {
        Message = this,
        Source = this.AssociatedObject
      };
      ActionMessage.PrepareContext(this.context);
      this.UpdateAvailabilityCore();
    }

    /// <summary>Invokes the action.</summary>
    /// <param name="eventArgs">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
    protected override void Invoke(object eventArgs)
    {
      ActionMessage.Log.Info("Invoking {0}.", (object) this);
      if (this.context == null)
        this.UpdateContext();
      if (this.context.Target == null || this.context.View == null)
      {
        ActionMessage.PrepareContext(this.context);
        if (this.context.Target == null)
        {
          Exception exception = new Exception(string.Format("No target found for method {0}.", (object) this.context.Message.MethodName));
          ActionMessage.Log.Error(exception);
          if (!ActionMessage.ThrowsExceptions)
            return;
          throw exception;
        }
        if (!this.UpdateAvailabilityCore())
          return;
      }
      if (this.context.Method == null)
      {
        Exception exception = new Exception(string.Format("Method {0} not found on target of type {1}.", (object) this.context.Message.MethodName, (object) this.context.Target.GetType()));
        ActionMessage.Log.Error(exception);
        if (ActionMessage.ThrowsExceptions)
          throw exception;
      }
      else
      {
        this.context.EventArgs = eventArgs;
        if (ActionMessage.EnforceGuardsDuringInvocation && this.context.CanExecute != null && !this.context.CanExecute())
          return;
        ActionMessage.InvokeAction(this.context);
        this.context.EventArgs = (object) null;
      }
    }

    /// <summary>
    /// Forces an update of the UI's Enabled/Disabled state based on the the preconditions associated with the method.
    /// </summary>
    public void UpdateAvailability()
    {
      if (this.context == null)
        return;
      if (this.context.Target == null || this.context.View == null)
        ActionMessage.PrepareContext(this.context);
      this.UpdateAvailabilityCore();
    }

    private bool UpdateAvailabilityCore()
    {
      ActionMessage.Log.Info("{0} availability update.", (object) this);
      return ActionMessage.ApplyAvailabilityEffect(this.context);
    }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString() => "Action: " + this.MethodName;

    /// <summary>
    /// Try to find a candidate for guard function, having:
    /// 	- a name in the form "CanXXX"
    /// 	- no generic parameters
    /// 	- a bool return type
    /// 	- no parameters or a set of parameters corresponding to the action method
    /// </summary>
    /// <param name="context">The execution context</param>
    /// <returns>A MethodInfo, if found; null otherwise</returns>
    private static MethodInfo TryFindGuardMethod(ActionExecutionContext context)
    {
      string name = "Can" + context.Method.Name;
      MethodInfo method = context.Target.GetType().GetMethod(name);
      if (method == null)
        return (MethodInfo) null;
      if (method.ContainsGenericParameters)
        return (MethodInfo) null;
      if (typeof (bool) != method.ReturnType)
        return (MethodInfo) null;
      ParameterInfo[] parameters1 = method.GetParameters();
      ParameterInfo[] parameters2 = context.Method.GetParameters();
      if (parameters1.Length == 0)
        return method;
      if (parameters1.Length != parameters2.Length)
        return (MethodInfo) null;
      return ((IEnumerable<ParameterInfo>) parameters1).Zip<ParameterInfo, ParameterInfo, bool>((IEnumerable<ParameterInfo>) context.Method.GetParameters(), (Func<ParameterInfo, ParameterInfo, bool>) ((x, y) => x.ParameterType == y.ParameterType)).Any<bool>((Func<bool, bool>) (x => !x)) ? (MethodInfo) null : method;
    }
  }
}
