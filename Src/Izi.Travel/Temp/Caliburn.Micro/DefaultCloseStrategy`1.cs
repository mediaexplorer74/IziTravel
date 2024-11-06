// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.DefaultCloseStrategy`1
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
  /// Used to gather the results from multiple child elements which may or may not prevent closing.
  /// </summary>
  /// <typeparam name="T">The type of child element.</typeparam>
  public class DefaultCloseStrategy<T> : ICloseStrategy<T>
  {
    private readonly bool closeConductedItemsWhenConductorCannotClose;

    /// <summary>Creates an instance of the class.</summary>
    /// <param name="closeConductedItemsWhenConductorCannotClose">Indicates that even if all conducted items are not closable, those that are should be closed. The default is FALSE.</param>
    public DefaultCloseStrategy(bool closeConductedItemsWhenConductorCannotClose = false)
    {
      this.closeConductedItemsWhenConductorCannotClose = closeConductedItemsWhenConductorCannotClose;
    }

    /// <summary>Executes the strategy.</summary>
    /// <param name="toClose">Items that are requesting close.</param>
    /// <param name="callback">The action to call when all enumeration is complete and the close results are aggregated.
    /// The bool indicates whether close can occur. The enumerable indicates which children should close if the parent cannot.</param>
    public void Execute(IEnumerable<T> toClose, Action<bool, IEnumerable<T>> callback)
    {
      this.Evaluate(new DefaultCloseStrategy<T>.EvaluationState(), toClose.GetEnumerator(), callback);
    }

    private void Evaluate(
      DefaultCloseStrategy<T>.EvaluationState state,
      IEnumerator<T> enumerator,
      Action<bool, IEnumerable<T>> callback)
    {
      bool guardPending = false;
      do
      {
        if (!enumerator.MoveNext())
        {
          callback(state.FinalResult, this.closeConductedItemsWhenConductorCannotClose ? (IEnumerable<T>) state.Closable : (IEnumerable<T>) new List<T>());
          break;
        }
        T current = enumerator.Current;
        if (current is IGuardClose guardClose)
        {
          guardPending = true;
          Action<bool> callback1 = (Action<bool>) (canClose =>
          {
            guardPending = false;
            if (canClose)
              state.Closable.Add(current);
            state.FinalResult = state.FinalResult && canClose;
            if (!state.GuardMustCallEvaluate)
              return;
            state.GuardMustCallEvaluate = false;
            this.Evaluate(state, enumerator, callback);
          });
          guardClose.CanClose(callback1);
          state.GuardMustCallEvaluate = state.GuardMustCallEvaluate || guardPending;
        }
        else
          state.Closable.Add(current);
      }
      while (!guardPending);
    }

    private class EvaluationState
    {
      public readonly List<T> Closable = new List<T>();
      public bool FinalResult = true;
      public bool GuardMustCallEvaluate;
    }
  }
}
