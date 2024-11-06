// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.RescueResultDecorator`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A result decorator which rescues errors from the decorated result by executing a rescue coroutine.
  /// </summary>
  /// <typeparam name="TException">The type of the exception we want to perform the rescue on</typeparam>
  public class RescueResultDecorator<TException> : ResultDecoratorBase where TException : Exception
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (RescueResultDecorator<>));
    private readonly bool cancelResult;
    private readonly Func<TException, IResult> coroutine;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.RescueResultDecorator`1" /> class.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="coroutine">The rescue coroutine.</param>
    /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
    public RescueResultDecorator(
      IResult result,
      Func<TException, IResult> coroutine,
      bool cancelResult = true)
      : base(result)
    {
      this.coroutine = coroutine != null ? coroutine : throw new ArgumentNullException(nameof (coroutine));
      this.cancelResult = cancelResult;
    }

    /// <summary>
    /// Called when the execution of the decorated result has completed.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="innerResult">The decorated result.</param>
    /// <param name="args">The <see cref="T:Caliburn.Micro.ResultCompletionEventArgs" /> instance containing the event data.</param>
    protected override void OnInnerResultCompleted(
      CoroutineExecutionContext context,
      IResult innerResult,
      ResultCompletionEventArgs args)
    {
      if (!(args.Error is TException error))
      {
        this.OnCompleted(args);
      }
      else
      {
        RescueResultDecorator<TException>.Log.Error((Exception) error);
        RescueResultDecorator<TException>.Log.Info(string.Format("Executing coroutine because {0} threw an exception.", (object) innerResult.GetType().Name));
        this.Rescue(context, error);
      }
    }

    private void Rescue(CoroutineExecutionContext context, TException exception)
    {
      IResult sender;
      try
      {
        sender = this.coroutine(exception);
      }
      catch (Exception ex)
      {
        this.OnCompleted(new ResultCompletionEventArgs()
        {
          Error = ex
        });
        return;
      }
      try
      {
        sender.Completed += new EventHandler<ResultCompletionEventArgs>(this.RescueCompleted);
        IoC.BuildUp((object) sender);
        sender.Execute(context);
      }
      catch (Exception ex)
      {
        this.RescueCompleted((object) sender, new ResultCompletionEventArgs()
        {
          Error = ex
        });
      }
    }

    private void RescueCompleted(object sender, ResultCompletionEventArgs args)
    {
      ((IResult) sender).Completed -= new EventHandler<ResultCompletionEventArgs>(this.RescueCompleted);
      this.OnCompleted(new ResultCompletionEventArgs()
      {
        Error = args.Error,
        WasCancelled = args.Error == null && (args.WasCancelled || this.cancelResult)
      });
    }
  }
}
