// Decompiled with JetBrains decompiler
// Type: Weakly.WeakAction
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
  /// Weak version of <see cref="T:System.Action" /> delegate.
  /// </summary>
  public sealed class WeakAction : WeakDelegate
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakAction" /> class.
    /// </summary>
    /// <param name="action">The action delegate to encapsulate.</param>
    public WeakAction(Action action)
      : this(action.Target, action.GetMethodInfo())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakAction" /> class.
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
    public void Invoke()
    {
      object target = this.Target;
      if (target == null)
        return;
      OpenAction.From(this.Method)(target);
    }
  }
}
