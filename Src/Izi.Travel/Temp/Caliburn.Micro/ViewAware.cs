// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ViewAware
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
  /// A base implementation of <see cref="T:Caliburn.Micro.IViewAware" /> which is capable of caching views by context.
  /// </summary>
  public class ViewAware : PropertyChangedBase, IViewAware
  {
    private readonly IDictionary<object, object> views;
    /// <summary>The default view context.</summary>
    public static readonly object DefaultContext = new object();

    /// <summary>The view chache for this instance.</summary>
    protected IDictionary<object, object> Views => this.views;

    /// <summary>
    /// Creates an instance of <see cref="T:Caliburn.Micro.ViewAware" />.
    /// </summary>
    public ViewAware()
    {
      this.views = (IDictionary<object, object>) new WeakValueDictionary<object, object>();
    }

    /// <summary>Raised when a view is attached.</summary>
    public event EventHandler<ViewAttachedEventArgs> ViewAttached = (param0, param1) => { };

    void IViewAware.AttachView(object view, object context)
    {
      this.Views[context ?? ViewAware.DefaultContext] = view;
      object nonGeneratedView = PlatformProvider.Current.GetFirstNonGeneratedView(view);
      PlatformProvider.Current.ExecuteOnFirstLoad(nonGeneratedView, new Action<object>(this.OnViewLoaded));
      this.OnViewAttached(nonGeneratedView, context);
      this.ViewAttached((object) this, new ViewAttachedEventArgs()
      {
        View = nonGeneratedView,
        Context = context
      });
      if (!(this is IActivate activatable) || activatable.IsActive)
        PlatformProvider.Current.ExecuteOnLayoutUpdated(nonGeneratedView, new Action<object>(this.OnViewReady));
      else
        ViewAware.AttachViewReadyOnActivated(activatable, nonGeneratedView);
    }

    private static void AttachViewReadyOnActivated(IActivate activatable, object nonGeneratedView)
    {
      WeakReference viewReference = new WeakReference(nonGeneratedView);
      EventHandler<ActivationEventArgs> handler = (EventHandler<ActivationEventArgs>) null;
      handler = (EventHandler<ActivationEventArgs>) ((s, e) =>
      {
        ((IActivate) s).Activated -= handler;
        object target = viewReference.Target;
        if (target == null)
          return;
        PlatformProvider.Current.ExecuteOnLayoutUpdated(target, new Action<object>(((ViewAware) s).OnViewReady));
      });
      activatable.Activated += handler;
    }

    /// <summary>Called when a view is attached.</summary>
    /// <param name="view">The view.</param>
    /// <param name="context">The context in which the view appears.</param>
    protected virtual void OnViewAttached(object view, object context)
    {
    }

    /// <summary>Called when an attached view's Loaded event fires.</summary>
    /// <param name="view"></param>
    protected virtual void OnViewLoaded(object view)
    {
    }

    /// <summary>
    /// Called the first time the page's LayoutUpdated event fires after it is navigated to.
    /// </summary>
    /// <param name="view"></param>
    protected virtual void OnViewReady(object view)
    {
    }

    /// <summary>Gets a view previously attached to this instance.</summary>
    /// <param name="context">The context denoting which view to retrieve.</param>
    /// <returns>The view.</returns>
    public object GetView(object context = null)
    {
      object view;
      this.Views.TryGetValue(context ?? ViewAware.DefaultContext, out view);
      return view;
    }
  }
}
