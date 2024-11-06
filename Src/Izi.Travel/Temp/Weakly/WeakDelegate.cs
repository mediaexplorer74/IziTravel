// Decompiled with JetBrains decompiler
// Type: Weakly.WeakDelegate
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>Base class for all weak delegates.</summary>
  public abstract class WeakDelegate
  {
    private readonly WeakReference _instance;
    private readonly MethodInfo _method;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Weakly.WeakDelegate" /> class.
    /// </summary>
    /// <param name="target">The class instance on which the current delegate invokes the instance method.</param>
    /// <param name="method">The method represented by the delegate.</param>
    protected WeakDelegate(object target, MethodInfo method)
    {
      this._instance = new WeakReference(target);
      this._method = method;
    }

    /// <summary>
    /// Gets an indication whether the object referenced by the current <see cref="T:Weakly.WeakDelegate" /> object has been garbage collected.
    /// </summary>
    public bool IsAlive => this._instance.IsAlive;

    /// <summary>
    /// Gets the class instance on which the current <see cref="T:Weakly.WeakDelegate" /> invokes the instance method.
    /// </summary>
    public object Target => this._instance.Target;

    /// <summary>Gets the method represented by this delegate.</summary>
    public MethodInfo Method => this._method;
  }
}
