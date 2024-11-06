// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.SequentialResult
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   An implementation of <see cref="T:Caliburn.Micro.IResult" /> that enables sequential execution of multiple results.
  /// </summary>
  public class SequentialResult : IResult
  {
    private readonly IEnumerator<IResult> enumerator;
    private CoroutineExecutionContext context;

    /// <summary>
    ///   Initializes a new instance of the <see cref="T:Caliburn.Micro.SequentialResult" /> class.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    public SequentialResult(IEnumerator<IResult> enumerator) => this.enumerator = enumerator;

    /// <summary>Occurs when execution has completed.</summary>
    public event EventHandler<ResultCompletionEventArgs> Completed = (param0, param1) => { };

    /// <summary>Executes the result using the specified context.</summary>
    /// <param name="context">The context.</param>
    public void Execute(CoroutineExecutionContext context)
    {
      this.context = context;
      this.ChildCompleted((object) null, new ResultCompletionEventArgs());
    }

    private void ChildCompleted(object sender, ResultCompletionEventArgs args)
    {
      if (sender is IResult result)
        result.Completed -= new EventHandler<ResultCompletionEventArgs>(this.ChildCompleted);
      if (args.Error != null || args.WasCancelled)
      {
        this.OnComplete(args.Error, args.WasCancelled);
      }
      else
      {
        bool flag;
        try
        {
          flag = this.enumerator.MoveNext();
        }
        catch (Exception ex)
        {
          this.OnComplete(ex, false);
          return;
        }
        if (flag)
        {
          try
          {
            IResult current = this.enumerator.Current;
            IoC.BuildUp((object) current);
            current.Completed += new EventHandler<ResultCompletionEventArgs>(this.ChildCompleted);
            current.Execute(this.context);
          }
          catch (Exception ex)
          {
            this.OnComplete(ex, false);
          }
        }
        else
          this.OnComplete((Exception) null, false);
      }
    }

    private void OnComplete(Exception error, bool wasCancelled)
    {
      this.enumerator.Dispose();
      this.Completed((object) this, new ResultCompletionEventArgs()
      {
        Error = error,
        WasCancelled = wasCancelled
      });
    }
  }
}
