// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.SimpleResult
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A simple result.</summary>
  public sealed class SimpleResult : IResult
  {
    private readonly bool wasCancelled;
    private readonly Exception error;

    private SimpleResult(bool wasCancelled, Exception error)
    {
      this.wasCancelled = wasCancelled;
      this.error = error;
    }

    /// <summary>A result that is always succeeded.</summary>
    public static IResult Succeeded() => (IResult) new SimpleResult(false, (Exception) null);

    /// <summary>A result that is always canceled.</summary>
    /// <returns>The result.</returns>
    public static IResult Cancelled() => (IResult) new SimpleResult(true, (Exception) null);

    /// <summary>A result that is always failed.</summary>
    public static IResult Failed(Exception error) => (IResult) new SimpleResult(false, error);

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      this.Completed((object) this, new ResultCompletionEventArgs()
      {
        WasCancelled = this.wasCancelled,
        Error = this.error
      });
    }

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };
  }
}
