// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskResult
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A couroutine that encapsulates an <see cref="T:System.Threading.Tasks.Task" />.
  /// </summary>
  public class TaskResult : IResult
  {
    private readonly Task innerTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.TaskResult" /> class.
    /// </summary>
    /// <param name="task">The task.</param>
    public TaskResult(Task task) => this.innerTask = task;

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      if (this.innerTask.IsCompleted)
        this.OnCompleted(this.innerTask);
      else
        this.innerTask.ContinueWith(new Action<Task>(this.OnCompleted), SynchronizationContext.Current != null ? TaskScheduler.FromCurrentSynchronizationContext() : TaskScheduler.Current);
    }

    /// <summary>Called when the asynchronous task has completed.</summary>
    /// <param name="task">The completed task.</param>
    protected virtual void OnCompleted(Task task)
    {
      this.Completed((object) this, new ResultCompletionEventArgs()
      {
        WasCancelled = task.IsCanceled,
        Error = (Exception) task.Exception
      });
    }

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };
  }
}
