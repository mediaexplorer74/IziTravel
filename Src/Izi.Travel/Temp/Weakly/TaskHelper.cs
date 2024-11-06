// Decompiled with JetBrains decompiler
// Type: Weakly.TaskHelper
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Weakly
{
  /// <summary>Helper to create completed, canceled and faulted tasks.</summary>
  public static class TaskHelper
  {
    private static readonly Task CompletedTask = (Task) Task.FromResult<object>((object) null);

    /// <summary>Gets an already completed task.</summary>
    /// <returns>The completed task.</returns>
    public static Task Completed() => TaskHelper.CompletedTask;

    /// <summary>Gets an already canceled task.</summary>
    /// <returns>The canceled task.</returns>
    public static Task Canceled() => (Task) TaskHelper.CanceledTask<object>.Task;

    /// <summary>Gets an already canceled task.</summary>
    /// <returns>The canceled task.</returns>
    public static Task<TResult> Canceled<TResult>() => TaskHelper.CanceledTask<TResult>.Task;

    /// <summary>
    /// Creates a task that is fauled with the specified exception.
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the task.</typeparam>
    /// <param name="ex">The exception.</param>
    /// <returns>The faulted task.</returns>
    public static Task<TResult> Faulted<TResult>(Exception ex)
    {
      TaskCompletionSource<TResult> completionSource = new TaskCompletionSource<TResult>();
      completionSource.TrySetException(ex);
      return completionSource.Task;
    }

    /// <summary>
    /// Creates a task that is fauled with the specified exception.
    /// </summary>
    /// <param name="ex">The exception.</param>
    /// <returns>The faulted task.</returns>
    public static Task Faulted(Exception ex) => (Task) TaskHelper.Faulted<object>(ex);

    /// <summary>
    /// Suppresses default exception handling of a Task that would otherwise reraise the exception on the finalizer thread.
    /// </summary>
    /// <param name="task">The Task to be monitored.</param>
    /// <returns>The original Task.</returns>
    public static Task IgnoreExceptions(this Task task)
    {
      AggregateException exception;
      task.ContinueWith((Action<Task>) (t => exception = t.Exception), CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted | TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
      return task;
    }

    /// <summary>
    /// Suppresses default exception handling of a Task that would otherwise reraise the exception on the finalizer thread.
    /// </summary>
    /// <param name="task">The Task to be monitored.</param>
    /// <returns>The original Task.</returns>
    public static Task<T> IgnoreExceptions<T>(this Task<T> task)
    {
      return (Task<T>) task.IgnoreExceptions();
    }

    private static class CanceledTask<TResult>
    {
      public static readonly Task<TResult> Task = TaskHelper.CanceledTask<TResult>.CreateCanceled();

      private static Task<TResult> CreateCanceled()
      {
        TaskCompletionSource<TResult> completionSource = new TaskCompletionSource<TResult>();
        completionSource.TrySetCanceled();
        return completionSource.Task;
      }
    }
  }
}
