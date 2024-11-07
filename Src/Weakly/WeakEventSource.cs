// Decompiled with JetBrains decompiler
// Type: Weakly.WeakEventSource
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// A weak event source that does not hold any strong reference to the event listeners.
  /// </summary>
  public class WeakEventSource : WeakEventSourceBase<EventArgs>
  {
    private EventHandler _staticEventHandlers;

    /// <summary>Adds the specified event handler.</summary>
    /// <param name="eventHandler">The event handler.</param>
    public void Add(EventHandler eventHandler)
    {
      if (eventHandler == null)
        return;
      if (eventHandler.Target == null)
        this._staticEventHandlers += eventHandler;
      else
        this.Add((Delegate) eventHandler);
    }

    /// <summary>Removes the specified event handler.</summary>
    /// <param name="eventHandler">The event handler.</param>
    public void Remove(EventHandler eventHandler)
    {
      if (eventHandler == null)
        return;
      if (eventHandler.Target == null)
        this._staticEventHandlers -= eventHandler;
      else
        this.Remove((Delegate) eventHandler);
    }

    /// <summary>Notifies all static event handlers.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">An object that contains the event data.</param>
    protected override void OnRaise(object sender, EventArgs e)
    {
      EventHandler staticEventHandlers = this._staticEventHandlers;
      if (staticEventHandlers == null)
        return;
      staticEventHandlers(sender, e);
    }
  }
}
