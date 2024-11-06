// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension methods to bring <see cref="T:System.Threading.Tasks.Task" /> and <see cref="T:Caliburn.Micro.IResult" /> together.
  /// </summary>
  public static class TaskExtensions
  {
    /// <summary>
    /// Executes an <see cref="T:Caliburn.Micro.IResult" /> asynchronous.
    /// </summary>
    /// <param name="result">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// <returns>A task that represents the asynchronous coroutine.</returns>
    public static Task ExecuteAsync(this IResult result, CoroutineExecutionContext context = null)
    {
      return (Task) TaskExtensions.InternalExecuteAsync<object>(result, context);
    }

    /// <summary>
    /// Executes an <see cref="T:Caliburn.Micro.IResult`1" /> asynchronous.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="result">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// <returns>A task that represents the asynchronous coroutine.</returns>
    public static Task<TResult> ExecuteAsync<TResult>(
      this IResult<TResult> result,
      CoroutineExecutionContext context = null)
    {
      return TaskExtensions.InternalExecuteAsync<TResult>((IResult) result, context);
    }

    private static Task<TResult> InternalExecuteAsync<TResult>(
      IResult result,
      CoroutineExecutionContext context)
    {
      TaskCompletionSource<TResult> taskSource = new TaskCompletionSource<TResult>();
      EventHandler<ResultCompletionEventArgs> completed = (EventHandler<ResultCompletionEventArgs>) null;
      completed = (EventHandler<ResultCompletionEventArgs>) ((s, e) =>
      {
        result.Completed -= completed;
        if (e.Error != null)
          taskSource.SetException(e.Error);
        else if (e.WasCancelled)
        {
          taskSource.SetCanceled();
        }
        else
        {
          IResult<TResult> result1 = result as IResult<TResult>;
          taskSource.SetResult(result1 != null ? result1.Result : default (TResult));
        }
      });
      try
      {
        IoC.BuildUp((object) result);
        result.Completed += completed;
        result.Execute(context ?? new CoroutineExecutionContext());
      }
      catch (Exception ex)
      {
        result.Completed -= completed;
        taskSource.SetException(ex);
      }
      return taskSource.Task;
    }

    /// <summary>Encapsulates a task inside a couroutine.</summary>
    /// <param name="task">The task.</param>
    /// <returns>The coroutine that encapsulates the task.</returns>
    public static TaskResult AsResult(this Task task) => new TaskResult(task);

    /// <summary>Encapsulates a task inside a couroutine.</summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="task">The task.</param>
    /// <returns>The coroutine that encapsulates the task.</returns>
    public static TaskResult<TResult> AsResult<TResult>(this Task<TResult> task)
    {
      return new TaskResult<TResult>(task);
    }
  }
}
