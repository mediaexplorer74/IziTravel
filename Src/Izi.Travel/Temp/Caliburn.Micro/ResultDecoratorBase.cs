// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ResultDecoratorBase
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Base class for all <see cref="T:Caliburn.Micro.IResult" /> decorators.
  /// </summary>
  public abstract class ResultDecoratorBase : IResult
  {
    private readonly IResult innerResult;
    private CoroutineExecutionContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.ResultDecoratorBase" /> class.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    protected ResultDecoratorBase(IResult result)
    {
      this.innerResult = result != null ? result : throw new ArgumentNullException(nameof (result));
    }

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      this.context = context;
      try
      {
        this.innerResult.Completed += new EventHandler<ResultCompletionEventArgs>(this.InnerResultCompleted);
        IoC.BuildUp((object) this.innerResult);
        this.innerResult.Execute(this.context);
      }
      catch (Exception ex)
      {
        this.InnerResultCompleted((object) this.innerResult, new ResultCompletionEventArgs()
        {
          Error = ex
        });
      }
    }

    private void InnerResultCompleted(object sender, ResultCompletionEventArgs args)
    {
      this.innerResult.Completed -= new EventHandler<ResultCompletionEventArgs>(this.InnerResultCompleted);
      this.OnInnerResultCompleted(this.context, this.innerResult, args);
      this.context = (CoroutineExecutionContext) null;
    }

    /// <summary>
    /// Called when the execution of the decorated result has completed.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="innerResult">The decorated result.</param>
    /// <param name="args">The <see cref="T:Caliburn.Micro.ResultCompletionEventArgs" /> instance containing the event data.</param>
    protected abstract void OnInnerResultCompleted(
      CoroutineExecutionContext context,
      IResult innerResult,
      ResultCompletionEventArgs args);

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };

    /// <summary>
    /// Raises the <see cref="E:Caliburn.Micro.ResultDecoratorBase.Completed" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Caliburn.Micro.ResultCompletionEventArgs" /> instance containing the event data.</param>
    protected void OnCompleted(ResultCompletionEventArgs args)
    {
      this.Completed((object) this, args);
    }
  }
}
