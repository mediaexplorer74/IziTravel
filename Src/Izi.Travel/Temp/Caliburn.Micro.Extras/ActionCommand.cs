// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.ActionCommand
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using Weakly;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>
  /// Wraps a ViewModel method (with guard) in an <see cref="T:System.Windows.Input.ICommand" />.
  /// </summary>
  public class ActionCommand : ICommand
  {
    private readonly WeakReference targetReference;
    private readonly MethodInfo method;
    private readonly WeakFunc<bool> canExecute;
    private readonly WeakEventSource canExecuteChangedSource = new WeakEventSource();
    private readonly string guardName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.Extras.ActionCommand" /> class.
    /// </summary>
    /// <param name="target">The target.</param>
    /// <param name="methodName">Name of the method.</param>
    public ActionCommand(object target, string methodName)
    {
      this.targetReference = target != null ? new WeakReference(target) : throw new ArgumentNullException(nameof (target));
      this.method = target.GetType().GetMethod(methodName);
      if (this.method == null)
        throw new ArgumentException("Specified method cannot be found.", nameof (methodName));
      this.guardName = "Can" + this.method.Name;
      MethodInfo method = target.GetType().GetMethod("get_" + this.guardName);
      if (!(target is INotifyPropertyChanged notifyPropertyChanged) || method == null)
        return;
      WeakEventHandler.Register<PropertyChangedEventArgs>((object) notifyPropertyChanged, "PropertyChanged", new Action<object, PropertyChangedEventArgs>(this.OnPropertyChanged));
      this.canExecute = new WeakFunc<bool>((object) notifyPropertyChanged, method);
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!string.IsNullOrEmpty(e.PropertyName) && !(e.PropertyName == this.guardName))
        return;
      ((System.Action) (() => this.canExecuteChangedSource.Raise((object) this, EventArgs.Empty))).OnUIThread();
    }

    /// <summary>
    /// Defines the method to be called when the command is invoked.
    /// </summary>
    /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
    public void Execute(object parameter)
    {
      object target = this.targetReference.Target;
      if (target == null)
        return;
      object obj = DynamicDelegate.From(this.method)(target, new object[0]);
      if (obj is Task task)
        obj = (object) task.AsResult();
      if (obj is IResult result)
        obj = (object) new IResult[1]{ result };
      if (obj is IEnumerable<IResult> results)
        obj = (object) results.GetEnumerator();
      if (!(obj is IEnumerator<IResult> coroutine))
        return;
      CoroutineExecutionContext context = new CoroutineExecutionContext()
      {
        Target = target
      };
      Coroutine.BeginExecute(coroutine, context);
    }

    /// <summary>
    /// Defines the method that determines whether the command can execute in its current state.
    /// </summary>
    /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
    /// <returns>true if this command can be executed; otherwise, false.</returns>
    public bool CanExecute(object parameter) => this.canExecute == null || this.canExecute.Invoke();

    /// <summary>
    /// Occurs when changes occur that affect whether the command should execute.
    /// </summary>
    public event EventHandler CanExecuteChanged
    {
      add => this.canExecuteChangedSource.Add(value);
      remove => this.canExecuteChangedSource.Remove(value);
    }
  }
}
