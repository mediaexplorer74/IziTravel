// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.OverrideCancelResultDecorator
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A result decorator that overrides <see cref="F:Caliburn.Micro.ResultCompletionEventArgs.WasCancelled" /> of the decorated <see cref="T:Caliburn.Micro.IResult" /> instance.
  /// </summary>
  public class OverrideCancelResultDecorator : ResultDecoratorBase
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (OverrideCancelResultDecorator));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.OverrideCancelResultDecorator" /> class.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    public OverrideCancelResultDecorator(IResult result)
      : base(result)
    {
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
      if (args.WasCancelled)
        OverrideCancelResultDecorator.Log.Info(string.Format("Overriding WasCancelled from {0}.", (object) innerResult.GetType().Name));
      this.OnCompleted(new ResultCompletionEventArgs()
      {
        Error = args.Error
      });
    }
  }
}
