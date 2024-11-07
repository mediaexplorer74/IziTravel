// Decompiled with JetBrains decompiler
// Type: Weakly.WeakFunc`1
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// Weak version of <see cref="T:System.Func`1" /> delegate.
  /// </summary>
  /// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
  public sealed class WeakFunc<TResult> : WeakDelegate
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakFunc`1" /> class.
    /// </summary>
    /// <param name="function">The function delegate to encapsulate.</param>
    public WeakFunc(Func<TResult> function)
      : this(function.Target, function.GetMethodInfo())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakFunc`1" /> class.
    /// </summary>
    /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
    /// <param name="method">The method represented by the delegate.</param>
    public WeakFunc(object target, MethodInfo method)
      : base(target, method)
    {
    }

    /// <summary>
    /// Invokes the method represented by the current weak delegate.
    /// </summary>
    /// <returns>The return value of the method that this delegate encapsulates.</returns>
    public TResult Invoke()
    {
      object target = this.Target;
      return target != null ? OpenFunc.From<TResult>(this.Method)(target) : default (TResult);
    }
  }
}
