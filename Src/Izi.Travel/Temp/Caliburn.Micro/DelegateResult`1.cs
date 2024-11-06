// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.DelegateResult`1
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A result that executes a <see cref="T:System.Func`1" />
  /// </summary>
  /// <typeparam name="TResult">The type of the result.</typeparam>
  public class DelegateResult<TResult> : IResult<TResult>, IResult
  {
    private readonly Func<TResult> toExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.DelegateResult`1" /> class.
    /// </summary>
    /// <param name="action">The action.</param>
    public DelegateResult(Func<TResult> action) => this.toExecute = action;

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      ResultCompletionEventArgs e = new ResultCompletionEventArgs();
      try
      {
        this.Result = this.toExecute();
      }
      catch (Exception ex)
      {
        e.Error = ex;
      }
      this.Completed((object) this, e);
    }

    /// <summary>Gets the result.</summary>
    public TResult Result { get; private set; }

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };
  }
}
