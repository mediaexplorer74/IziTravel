// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskResult`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A couroutine that encapsulates an <see cref="T:System.Threading.Tasks.Task`1" />.
  /// </summary>
  /// <typeparam name="TResult">The type of the result.</typeparam>
  public class TaskResult<TResult> : TaskResult, IResult<TResult>, IResult
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.TaskResult`1" /> class.
    /// </summary>
    /// <param name="task">The task.</param>
    public TaskResult(Task<TResult> task)
      : base((Task) task)
    {
    }

    /// <summary>Gets the result of the asynchronous operation.</summary>
    public TResult Result { get; private set; }

    /// <summary>Called when the asynchronous task has completed.</summary>
    /// <param name="task">The completed task.</param>
    protected override void OnCompleted(Task task)
    {
      if (!task.IsFaulted && !task.IsCanceled)
        this.Result = ((Task<TResult>) task).Result;
      base.OnCompleted(task);
    }
  }
}
