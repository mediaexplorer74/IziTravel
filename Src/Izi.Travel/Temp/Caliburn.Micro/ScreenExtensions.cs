// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ScreenExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Hosts extension methods for <see cref="T:Caliburn.Micro.IScreen" /> classes.
  /// </summary>
  public static class ScreenExtensions
  {
    /// <summary>
    /// Activates the item if it implements <see cref="T:Caliburn.Micro.IActivate" />, otherwise does nothing.
    /// </summary>
    /// <param name="potentialActivatable">The potential activatable.</param>
    public static void TryActivate(object potentialActivatable)
    {
      if (!(potentialActivatable is IActivate activate))
        return;
      activate.Activate();
    }

    /// <summary>
    /// Deactivates the item if it implements <see cref="T:Caliburn.Micro.IDeactivate" />, otherwise does nothing.
    /// </summary>
    /// <param name="potentialDeactivatable">The potential deactivatable.</param>
    /// <param name="close">Indicates whether or not to close the item after deactivating it.</param>
    public static void TryDeactivate(object potentialDeactivatable, bool close)
    {
      if (!(potentialDeactivatable is IDeactivate deactivate))
        return;
      deactivate.Deactivate(close);
    }

    /// <summary>Closes the specified item.</summary>
    /// <param name="conductor">The conductor.</param>
    /// <param name="item">The item to close.</param>
    public static void CloseItem(this IConductor conductor, object item)
    {
      conductor.DeactivateItem(item, true);
    }

    /// <summary>Closes the specified item.</summary>
    /// <param name="conductor">The conductor.</param>
    /// <param name="item">The item to close.</param>
    public static void CloseItem<T>(this ConductorBase<T> conductor, T item) where T : class
    {
      conductor.DeactivateItem(item, true);
    }

    /// <summary>
    ///  Activates a child whenever the specified parent is activated.
    /// </summary>
    /// <param name="child">The child to activate.</param>
    /// <param name="parent">The parent whose activation triggers the child's activation.</param>
    public static void ActivateWith(this IActivate child, IActivate parent)
    {
      WeakReference childReference = new WeakReference((object) child);
      EventHandler<ActivationEventArgs> handler = (EventHandler<ActivationEventArgs>) null;
      handler = (EventHandler<ActivationEventArgs>) ((s, e) =>
      {
        IActivate target = (IActivate) childReference.Target;
        if (target == null)
          ((IActivate) s).Activated -= handler;
        else
          target.Activate();
      });
      parent.Activated += handler;
    }

    /// <summary>
    ///  Deactivates a child whenever the specified parent is deactivated.
    /// </summary>
    /// <param name="child">The child to deactivate.</param>
    /// <param name="parent">The parent whose deactivation triggers the child's deactivation.</param>
    public static void DeactivateWith(this IDeactivate child, IDeactivate parent)
    {
      WeakReference childReference = new WeakReference((object) child);
      EventHandler<DeactivationEventArgs> handler = (EventHandler<DeactivationEventArgs>) null;
      handler = (EventHandler<DeactivationEventArgs>) ((s, e) =>
      {
        IDeactivate target = (IDeactivate) childReference.Target;
        if (target == null)
          ((IDeactivate) s).Deactivated -= handler;
        else
          target.Deactivate(e.WasClosed);
      });
      parent.Deactivated += handler;
    }

    /// <summary>
    ///  Activates and Deactivates a child whenever the specified parent is Activated or Deactivated.
    /// </summary>
    /// <param name="child">The child to activate/deactivate.</param>
    /// <param name="parent">The parent whose activation/deactivation triggers the child's activation/deactivation.</param>
    public static void ConductWith<TChild, TParent>(this TChild child, TParent parent)
      where TChild : IActivate, IDeactivate
      where TParent : IActivate, IDeactivate
    {
      child.ActivateWith((IActivate) parent);
      child.DeactivateWith((IDeactivate) parent);
    }
  }
}
