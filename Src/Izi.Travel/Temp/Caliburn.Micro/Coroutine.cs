// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Coroutine
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Manages coroutine execution.</summary>
  public static class Coroutine
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (Coroutine));
    /// <summary>Creates the parent enumerator.</summary>
    public static Func<IEnumerator<IResult>, IResult> CreateParentEnumerator = (Func<IEnumerator<IResult>, IResult>) (inner => (IResult) new SequentialResult(inner));

    /// <summary>Executes a coroutine.</summary>
    /// <param name="coroutine">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// 
    ///             /// <param name="callback">The completion callback for the coroutine.</param>
    public static void BeginExecute(
      IEnumerator<IResult> coroutine,
      CoroutineExecutionContext context = null,
      EventHandler<ResultCompletionEventArgs> callback = null)
    {
      Coroutine.Log.Info("Executing coroutine.");
      IResult result = Coroutine.CreateParentEnumerator(coroutine);
      IoC.BuildUp((object) result);
      if (callback != null)
        Coroutine.ExecuteOnCompleted(result, callback);
      Coroutine.ExecuteOnCompleted(result, Coroutine.Completed);
      result.Execute(context ?? new CoroutineExecutionContext());
    }

    /// <summary>Executes a coroutine asynchronous.</summary>
    /// <param name="coroutine">The coroutine to execute.</param>
    /// <param name="context">The context to execute the coroutine within.</param>
    /// <returns>A task that represents the asynchronous coroutine.</returns>
    public static Task ExecuteAsync(
      IEnumerator<IResult> coroutine,
      CoroutineExecutionContext context = null)
    {
      TaskCompletionSource<object> taskSource = new TaskCompletionSource<object>();
      Coroutine.BeginExecute(coroutine, context, (EventHandler<ResultCompletionEventArgs>) ((s, e) =>
      {
        if (e.Error != null)
          taskSource.SetException(e.Error);
        else if (e.WasCancelled)
          taskSource.SetCanceled();
        else
          taskSource.SetResult((object) null);
      }));
      return (Task) taskSource.Task;
    }

    private static void ExecuteOnCompleted(
      IResult result,
      EventHandler<ResultCompletionEventArgs> handler)
    {
      EventHandler<ResultCompletionEventArgs> onCompledted = (EventHandler<ResultCompletionEventArgs>) null;
      onCompledted = (EventHandler<ResultCompletionEventArgs>) ((s, e) =>
      {
        result.Completed -= onCompledted;
        handler(s, e);
      });
      result.Completed += onCompledted;
    }

    /// <summary>Called upon completion of a coroutine.</summary>
    public static event EventHandler<ResultCompletionEventArgs> Completed = (s, e) =>
    {
      if (e.Error != null)
        Coroutine.Log.Error(e.Error);
      else if (e.WasCancelled)
        Coroutine.Log.Info("Coroutine execution cancelled.");
      else
        Coroutine.Log.Info("Coroutine execution completed.");
    };
  }
}
