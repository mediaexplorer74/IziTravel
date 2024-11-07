// Decompiled with JetBrains decompiler
// Type: Weakly.WeakAction`3
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
  /// Weak version of <see cref="T:System.Action`3" /> delegate.
  /// </summary>
  /// <typeparam name="T1">The type of the first parameter of the method that this delegate encapsulates.</typeparam>
  /// <typeparam name="T2">The type of the second parameter of the method that this delegate encapsulates.</typeparam>
  /// <typeparam name="T3">The type of the third parameter of the method that this delegate encapsulates.</typeparam>
  public sealed class WeakAction<T1, T2, T3> : WeakDelegate
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakAction`3" /> class.
    /// </summary>
    /// <param name="action">The action delegate to encapsulate.</param>
    public WeakAction(Action<T1, T2, T3> action)
      : this(action.Target, action.GetMethodInfo())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakAction`3" /> class.
    /// </summary>
    /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
    /// <param name="method">The method represented by the delegate.</param>
    public WeakAction(object target, MethodInfo method)
      : base(target, method)
    {
    }

    /// <summary>
    /// Invokes the method represented by the current weak delegate.
    /// </summary>
    /// <param name="arg1">The first parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg2">The second parameter of the method that this delegate encapsulates.</param>
    /// <param name="arg3">The third parameter of the method that this delegate encapsulates.</param>
    public void Invoke(T1 arg1, T2 arg2, T3 arg3)
    {
      object target = this.Target;
      if (target == null)
        return;
      OpenAction.From<T1, T2, T3>(this.Method)(target, arg1, arg2, arg3);
    }
  }
}
