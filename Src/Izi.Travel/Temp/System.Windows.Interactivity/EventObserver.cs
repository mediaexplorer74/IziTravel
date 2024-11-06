// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.EventObserver
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Reflection;

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// EventObserver is designed to help manage event handlers by detatching when disposed. Creating this object will also attach in the constructor.
  /// </summary>
  public sealed class EventObserver : IDisposable
  {
    private EventInfo eventInfo;
    private object target;
    private Delegate handler;

    /// <summary>
    /// Creates an instance of EventObserver and attaches to the supplied event on the supplied target. Call dispose to detach.
    /// </summary>
    /// <param name="eventInfo">The event to attach and detach from.</param>
    /// <param name="target">The target object the event is defined on. Null if the method is static.</param>
    /// <param name="handler">The delegate to attach to the event.</param>
    public EventObserver(EventInfo eventInfo, object target, Delegate handler)
    {
      if (eventInfo == null)
        throw new ArgumentNullException(nameof (eventInfo));
      if ((object) handler == null)
        throw new ArgumentNullException(nameof (handler));
      this.eventInfo = eventInfo;
      this.target = target;
      this.handler = handler;
      this.eventInfo.AddEventHandler(this.target, handler);
    }

    /// <summary>Detaches the handler from the event.</summary>
    public void Dispose() => this.eventInfo.RemoveEventHandler(this.target, this.handler);
  }
}
