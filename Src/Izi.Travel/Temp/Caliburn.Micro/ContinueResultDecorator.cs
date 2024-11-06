// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ContinueResultDecorator
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A result decorator which executes a coroutine when the wrapped result was cancelled.
  /// </summary>
  public class ContinueResultDecorator : ResultDecoratorBase
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (ContinueResultDecorator));
    private readonly Func<IResult> coroutine;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.ContinueResultDecorator" /> class.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="coroutine">The coroutine to execute when <paramref name="result" /> was canceled.</param>
    public ContinueResultDecorator(IResult result, Func<IResult> coroutine)
      : base(result)
    {
      this.coroutine = coroutine != null ? coroutine : throw new ArgumentNullException(nameof (coroutine));
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
      if (args.Error != null || !args.WasCancelled)
      {
        this.OnCompleted(new ResultCompletionEventArgs()
        {
          Error = args.Error
        });
      }
      else
      {
        ContinueResultDecorator.Log.Info(string.Format("Executing coroutine because {0} was cancelled.", (object) innerResult.GetType().Name));
        this.Continue(context);
      }
    }

    private void Continue(CoroutineExecutionContext context)
    {
      IResult sender;
      try
      {
        sender = this.coroutine();
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
        sender.Completed += new EventHandler<ResultCompletionEventArgs>(this.ContinueCompleted);
        IoC.BuildUp((object) sender);
        sender.Execute(context);
      }
      catch (Exception ex)
      {
        this.ContinueCompleted((object) sender, new ResultCompletionEventArgs()
        {
          Error = ex
        });
      }
    }

    private void ContinueCompleted(object sender, ResultCompletionEventArgs args)
    {
      ((IResult) sender).Completed -= new EventHandler<ResultCompletionEventArgs>(this.ContinueCompleted);
      this.OnCompleted(new ResultCompletionEventArgs()
      {
        Error = args.Error,
        WasCancelled = args.Error == null
      });
    }
  }
}
