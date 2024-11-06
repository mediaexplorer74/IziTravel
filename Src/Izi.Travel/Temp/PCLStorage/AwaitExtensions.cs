// Decompiled with JetBrains decompiler
// Type: PCLStorage.AwaitExtensions
// Assembly: PCLStorage, Version=1.0.2.0, Culture=neutral, PublicKeyToken=286fe515a2c35b64
// MVID: C962FBF1-A378-45AB-97C6-C265F8F0F86C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\PCLStorage.xml

using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

#nullable disable
namespace PCLStorage
{
  /// <summary>
  /// Extensions for use internally by PCLStorage for awaiting.
  /// </summary>
  /// <summary>
  /// Extensions for use internally by PCLStorage for awaiting.
  /// </summary>
  internal static class AwaitExtensions
  {
    internal static Task<Task<T>> AsTaskNoThrow<T>(
      this IAsyncOperation<T> operation,
      CancellationToken cancellationToken)
    {
      TaskCompletionSource<Task<T>> state1 = new TaskCompletionSource<Task<T>>();
      operation.AsTask<T>(cancellationToken).ContinueWith((Action<Task<T>, object>) ((t, state) => ((TaskCompletionSource<Task<T>>) state).SetResult(t)), (object) state1);
      return state1.Task;
    }

    /// <summary>
    /// Causes the caller who awaits this method to
    /// switch off the Main thread. It has no effect if
    /// the caller is already off the main thread.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>An awaitable that does the thread switching magic.</returns>
    internal static AwaitExtensions.TaskSchedulerAwaiter SwitchOffMainThreadAsync(
      CancellationToken cancellationToken)
    {
      cancellationToken.ThrowIfCancellationRequested();
      return new AwaitExtensions.TaskSchedulerAwaiter(SynchronizationContext.Current != null ? TaskScheduler.Default : (TaskScheduler) null, cancellationToken);
    }

    internal struct TaskSchedulerAwaiter : INotifyCompletion
    {
      private TaskScheduler taskScheduler;
      private CancellationToken cancellationToken;

      internal TaskSchedulerAwaiter(
        TaskScheduler taskScheduler,
        CancellationToken cancellationToken)
      {
        this.taskScheduler = taskScheduler;
        this.cancellationToken = cancellationToken;
      }

      internal AwaitExtensions.TaskSchedulerAwaiter GetAwaiter() => this;

      public bool IsCompleted => this.taskScheduler == null;

      public void OnCompleted(Action continuation)
      {
        if (this.taskScheduler == null)
          throw new InvalidOperationException("IsCompleted is true, so this is unexpected.");
        Task.Factory.StartNew(continuation, CancellationToken.None, TaskCreationOptions.None, this.taskScheduler);
      }

      public void GetResult() => this.cancellationToken.ThrowIfCancellationRequested();
    }
  }
}
