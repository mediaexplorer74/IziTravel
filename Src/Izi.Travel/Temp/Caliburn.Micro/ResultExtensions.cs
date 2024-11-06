// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ResultExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension methods for <see cref="T:Caliburn.Micro.IResult" /> instances.
  /// </summary>
  public static class ResultExtensions
  {
    /// <summary>
    /// Adds behavior to the result which is executed when the <paramref name="result" /> was cancelled.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="coroutine">The coroutine to execute when <paramref name="result" /> was canceled.</param>
    /// <returns></returns>
    public static IResult WhenCancelled(this IResult result, Func<IResult> coroutine)
    {
      return (IResult) new ContinueResultDecorator(result, coroutine);
    }

    /// <summary>
    /// Overrides <see cref="F:Caliburn.Micro.ResultCompletionEventArgs.WasCancelled" /> of the decorated <paramref name="result" /> instance.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <returns></returns>
    public static IResult OverrideCancel(this IResult result)
    {
      return (IResult) new OverrideCancelResultDecorator(result);
    }

    /// <summary>
    /// Rescues <typeparamref name="TException" /> from the decorated <paramref name="result" /> by executing a <paramref name="rescue" /> coroutine.
    /// </summary>
    /// <typeparam name="TException">The type of the exception we want to perform the rescue on.</typeparam>
    /// <param name="result">The result to decorate.</param>
    /// <param name="rescue">The rescue coroutine.</param>
    /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
    /// <returns></returns>
    public static IResult Rescue<TException>(
      this IResult result,
      Func<TException, IResult> rescue,
      bool cancelResult = true)
      where TException : Exception
    {
      return (IResult) new RescueResultDecorator<TException>(result, rescue, cancelResult);
    }

    /// <summary>
    /// Rescues any exception from the decorated <paramref name="result" /> by executing a <paramref name="rescue" /> coroutine.
    /// </summary>
    /// <param name="result">The result to decorate.</param>
    /// <param name="rescue">The rescue coroutine.</param>
    /// <param name="cancelResult">Set to true to cancel the result after executing rescue.</param>
    /// <returns></returns>
    public static IResult Rescue(
      this IResult result,
      Func<Exception, IResult> rescue,
      bool cancelResult = true)
    {
      return result.Rescue<Exception>(rescue, cancelResult);
    }
  }
}
