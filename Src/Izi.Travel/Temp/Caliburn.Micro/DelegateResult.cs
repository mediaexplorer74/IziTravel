// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.DelegateResult
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A result that executes an <see cref="T:System.Action" />.
  /// </summary>
  public class DelegateResult : IResult
  {
    private readonly Action toExecute;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.DelegateResult" /> class.
    /// </summary>
    /// <param name="action">The action.</param>
    public DelegateResult(Action action) => this.toExecute = action;

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      ResultCompletionEventArgs e = new ResultCompletionEventArgs();
      try
      {
        this.toExecute();
      }
      catch (Exception ex)
      {
        e.Error = ex;
      }
      this.Completed((object) this, e);
    }

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };
  }
}
