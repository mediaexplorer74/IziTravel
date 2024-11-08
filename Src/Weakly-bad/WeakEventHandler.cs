// Decompiled with JetBrains decompiler
// Type: Weakly.WeakEventHandler
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
  /// A weak event handler using reflection to register and unregister.
  /// </summary>
  public static class WeakEventHandler
  {
    /// <summary>
    /// Registers for the specified event without holding a strong reference to the <paramref name="handler" />.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    /// <param name="eventSource">The event source.</param>
    /// <param name="eventName">The event name.</param>
    /// <param name="handler">The handler to register.</param>
    /// <returns>A registration object that can be used to deregister from the event.</returns>
    public static IDisposable Register<TEventArgs>(
      object eventSource,
      string eventName,
      Action<object, TEventArgs> handler)
    {
      if (eventSource == null)
        throw new ArgumentNullException(nameof (eventSource));
      EventInfo eventInfo = !string.IsNullOrEmpty(eventName) ? eventSource.GetType().GetRuntimeEvent(eventName) : throw new ArgumentNullException(nameof (eventName));
      return WeakEventHandler.Register<TEventArgs>(eventSource, eventInfo, handler);
    }

    /// <summary>
    /// Registers for the specified static event without holding a strong reference to the <paramref name="handler" />.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    /// <param name="sourceType">The event source type.</param>
    /// <param name="eventName">The event name.</param>
    /// <param name="handler">The handler to register.</param>
    /// <returns>A registration object that can be used to deregister from the event.</returns>
    public static IDisposable Register<TEventArgs>(
      Type sourceType,
      string eventName,
      Action<object, TEventArgs> handler)
    {
      if (sourceType == null)
        throw new ArgumentNullException(nameof (sourceType));
      if (string.IsNullOrEmpty(eventName))
        throw new ArgumentNullException(nameof (eventName));
      return WeakEventHandler.Register<TEventArgs>((object) null, sourceType.GetRuntimeEvent(eventName), handler);
    }

    /// <summary>
    /// Registers for the specified event without holding a strong reference to the <paramref name="handler" />.
    /// </summary>
    /// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
    /// <param name="eventSource">The event source.</param>
    /// <param name="eventInfo">The event information.</param>
    /// <param name="handler">The handler to register.</param>
    /// <returns>A registration object that can be used to deregister from the event.</returns>
    public static IDisposable Register<TEventArgs>(
      object eventSource,
      EventInfo eventInfo,
      Action<object, TEventArgs> handler)
    {
      if (eventInfo == null)
        throw new ArgumentNullException(nameof (eventInfo));
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      if (handler.Target == null)
        throw new ArgumentException("Handler delegate must point to instance method.", nameof (handler));
      if (eventInfo.EventHandlerType.IsWindowsRuntimeType())
        throw new ArgumentException("Windows Runtime events are not supported.", nameof (eventInfo));
      bool isStatic = eventInfo.AddMethod.IsStatic;
      if (!isStatic && eventSource == null)
        throw new ArgumentNullException(nameof (eventSource));
      if (isStatic && eventSource != null)
        throw new ArgumentException("Event source for static event has to be null.", nameof (eventSource));
      return (IDisposable) new WeakEventHandler.WeakEventHandlerImpl<TEventArgs>(eventSource, eventInfo, handler);
    }

    private sealed class WeakEventHandlerImpl<TEventArgs> : IDisposable
    {
      private readonly WeakReference _source;
      private readonly WeakReference _target;
      private readonly MethodInfo _handler;
      private readonly EventInfo _eventInfo;
      private readonly Delegate _eventHandler;
      private bool _disposed;

      public WeakEventHandlerImpl(
        object eventSource,
        EventInfo eventInfo,
        Action<object, TEventArgs> handler)
      {
        if (eventSource != null)
          this._source = new WeakReference(eventSource);
        this._eventInfo = eventInfo;
        this._target = new WeakReference(handler.Target);
        this._handler = handler.GetMethodInfo();
        this._eventHandler = new Action<object, TEventArgs>(this.Invoke).GetMethodInfo().CreateDelegate(eventInfo.EventHandlerType, (object) this);
        DynamicEvent.GetAddMethod(eventInfo)(eventSource, this._eventHandler);
      }

      public void Invoke(object sender, TEventArgs args)
      {
        object target = this._target.Target;
        if (target != null)
          OpenAction.From<object, TEventArgs>(this._handler)(target, sender, args);
        else
          this.Dispose();
      }

      public void Dispose()
      {
        if (this._disposed)
          return;
        bool flag = this._source == null;
        object target = !flag ? this._source.Target : (object) null;
        if (!flag && target == null)
          return;
        DynamicEvent.GetRemoveMethod(this._eventInfo)(target, this._eventHandler);
        this._disposed = true;
      }
    }
  }
}
