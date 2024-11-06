// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.XamlPlatformProvider
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A <see cref="T:Caliburn.Micro.IPlatformProvider" /> implementation for the XAML platfrom.
  /// </summary>
  public class XamlPlatformProvider : IPlatformProvider
  {
    private Dispatcher dispatcher;
    private static readonly DependencyProperty PreviouslyAttachedProperty = DependencyProperty.RegisterAttached("PreviouslyAttached", typeof (bool), typeof (XamlPlatformProvider), (PropertyMetadata) null);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.XamlPlatformProvider" /> class.
    /// </summary>
    public XamlPlatformProvider() => this.dispatcher = Deployment.Current.Dispatcher;

    /// <summary>
    /// Indicates whether or not the framework is in design-time mode.
    /// </summary>
    public bool InDesignMode => View.InDesignMode;

    private void ValidateDispatcher()
    {
      if (this.dispatcher == null)
        throw new InvalidOperationException("Not initialized with dispatcher.");
    }

    private bool CheckAccess() => this.dispatcher == null || this.dispatcher.CheckAccess();

    /// <summary>Executes the action on the UI thread asynchronously.</summary>
    /// <param name="action">The action to execute.</param>
    public void BeginOnUIThread(System.Action action)
    {
      this.ValidateDispatcher();
      this.dispatcher.BeginInvoke(action);
    }

    /// <summary>Executes the action on the UI thread asynchronously.</summary>
    /// <param name="action">The action to execute.</param>
    /// <returns></returns>
    public Task OnUIThreadAsync(System.Action action)
    {
      this.ValidateDispatcher();
      TaskCompletionSource<object> taskSource = new TaskCompletionSource<object>();
      this.dispatcher.BeginInvoke((System.Action) (() =>
      {
        try
        {
          action();
          taskSource.SetResult((object) null);
        }
        catch (Exception ex)
        {
          taskSource.SetException(ex);
        }
      }));
      return (Task) taskSource.Task;
    }

    /// <summary>Executes the action on the UI thread.</summary>
    /// <param name="action">The action to execute.</param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    public void OnUIThread(System.Action action)
    {
      if (this.CheckAccess())
      {
        action();
      }
      else
      {
        ManualResetEvent waitHandle = new ManualResetEvent(false);
        Exception exception = (Exception) null;
        this.dispatcher.BeginInvoke((System.Action) (() =>
        {
          try
          {
            action();
          }
          catch (Exception ex)
          {
            exception = ex;
          }
          waitHandle.Set();
        }));
        waitHandle.WaitOne();
        if (exception != null)
          throw new TargetInvocationException("An error occurred while dispatching a call to the UI Thread", exception);
      }
    }

    /// <summary>Used to retrieve the root, non-framework-created view.</summary>
    /// <param name="view">The view to search.</param>
    /// <returns>The root element that was not created by the framework.</returns>
    /// <remarks>
    /// In certain instances the services create UI elements.
    /// For example, if you ask the window manager to show a UserControl as a dialog, it creates a window to host the UserControl in.
    /// The WindowManager marks that element as a framework-created element so that it can determine what it created vs. what was intended by the developer.
    /// Calling GetFirstNonGeneratedView allows the framework to discover what the original element was.
    /// </remarks>
    public object GetFirstNonGeneratedView(object view) => View.GetFirstNonGeneratedView(view);

    /// <summary>Executes the handler the fist time the view is loaded.</summary>
    /// <param name="view">The view.</param>
    /// <param name="handler">The handler.</param>
    public void ExecuteOnFirstLoad(object view, Action<object> handler)
    {
      if (!(view is FrameworkElement element) || (bool) element.GetValue(XamlPlatformProvider.PreviouslyAttachedProperty))
        return;
      element.SetValue(XamlPlatformProvider.PreviouslyAttachedProperty, (object) true);
      View.ExecuteOnLoad(element, (RoutedEventHandler) ((s, e) => handler(s)));
    }

    /// <summary>
    /// Executes the handler the next time the view's LayoutUpdated event fires.
    /// </summary>
    /// <param name="view">The view.</param>
    /// <param name="handler">The handler.</param>
    public void ExecuteOnLayoutUpdated(object view, Action<object> handler)
    {
      if (!(view is FrameworkElement element))
        return;
      EventHandler handler1 = (EventHandler) ((s, e) => handler(s));
      View.ExecuteOnLayoutUpdated(element, handler1);
    }

    /// <summary>Get the close action for the specified view model.</summary>
    /// <param name="viewModel">The view model to close.</param>
    /// <param name="views">The associated views.</param>
    /// <param name="dialogResult">The dialog result.</param>
    /// <returns>
    /// An <see cref="T:Caliburn.Micro.Action" /> to close the view model.
    /// </returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    public System.Action GetViewCloseAction(
      object viewModel,
      ICollection<object> views,
      bool? dialogResult)
    {
      if (viewModel is IChild child)
      {
        IConductor conductor = child.Parent as IConductor;
        if (conductor != null)
          return (System.Action) (() => conductor.CloseItem(viewModel));
      }
      foreach (object view in (IEnumerable<object>) views)
      {
        object contextualView = view;
        Type type = contextualView.GetType();
        MethodInfo closeMethod = type.GetMethod("Close");
        if (closeMethod != null)
          return (System.Action) (() => closeMethod.Invoke(contextualView, (object[]) null));
        PropertyInfo isOpenProperty = type.GetProperty("IsOpen");
        if (isOpenProperty != null)
          return (System.Action) (() => isOpenProperty.SetValue(contextualView, (object) false, (object[]) null));
      }
      return (System.Action) (() => LogManager.GetLog(typeof (Screen)).Info("TryClose requires a parent IConductor or a view with a Close method or IsOpen property."));
    }
  }
}
