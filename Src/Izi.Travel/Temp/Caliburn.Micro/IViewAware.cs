// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IViewAware
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Denotes a class which is aware of its view(s).</summary>
  public interface IViewAware
  {
    /// <summary>Attaches a view to this instance.</summary>
    /// <param name="view">The view.</param>
    /// <param name="context">The context in which the view appears.</param>
    void AttachView(object view, object context = null);

    /// <summary>Gets a view previously attached to this instance.</summary>
    /// <param name="context">The context denoting which view to retrieve.</param>
    /// <returns>The view.</returns>
    object GetView(object context = null);

    /// <summary>Raised when a view is attached.</summary>
    event EventHandler<ViewAttachedEventArgs> ViewAttached;
  }
}
