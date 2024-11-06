// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ActionExecutionContext
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// The context used during the execution of an Action or its guard.
  /// </summary>
  public class ActionExecutionContext : IDisposable
  {
    private WeakReference message;
    private WeakReference source;
    private WeakReference target;
    private WeakReference view;
    private Dictionary<string, object> values;
    /// <summary>Determines whether the action can execute.</summary>
    /// <remarks>Returns true if the action can execute, false otherwise.</remarks>
    public Func<bool> CanExecute;
    /// <summary>
    /// Any event arguments associated with the action's invocation.
    /// </summary>
    public object EventArgs;
    /// <summary>The actual method info to be invoked.</summary>
    public MethodInfo Method;

    /// <summary>The message being executed.</summary>
    public ActionMessage Message
    {
      get => this.message != null ? this.message.Target as ActionMessage : (ActionMessage) null;
      set => this.message = new WeakReference((object) value);
    }

    /// <summary>The source from which the message originates.</summary>
    public FrameworkElement Source
    {
      get => this.source != null ? this.source.Target as FrameworkElement : (FrameworkElement) null;
      set => this.source = new WeakReference((object) value);
    }

    /// <summary>The instance on which the action is invoked.</summary>
    public object Target
    {
      get => this.target != null ? this.target.Target : (object) null;
      set => this.target = new WeakReference(value);
    }

    /// <summary>The view associated with the target.</summary>
    public DependencyObject View
    {
      get => this.view != null ? this.view.Target as DependencyObject : (DependencyObject) null;
      set => this.view = new WeakReference((object) value);
    }

    /// <summary>
    /// Gets or sets additional data needed to invoke the action.
    /// </summary>
    /// <param name="key">The data key.</param>
    /// <returns>Custom data associated with the context.</returns>
    public object this[string key]
    {
      get
      {
        if (this.values == null)
          this.values = new Dictionary<string, object>();
        object obj;
        this.values.TryGetValue(key, out obj);
        return obj;
      }
      set
      {
        if (this.values == null)
          this.values = new Dictionary<string, object>();
        this.values[key] = value;
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.Disposing((object) this, System.EventArgs.Empty);

    /// <summary>Called when the execution context is disposed</summary>
    public event EventHandler Disposing = (param0, param1) => { };
  }
}
