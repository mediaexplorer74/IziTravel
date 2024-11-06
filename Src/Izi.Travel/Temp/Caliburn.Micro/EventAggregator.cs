// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.EventAggregator
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Enables loosely-coupled publication of and subscription to events.
  /// </summary>
  public class EventAggregator : IEventAggregator
  {
    private readonly List<EventAggregator.Handler> handlers = new List<EventAggregator.Handler>();
    /// <summary>Processing of handler results on publication thread.</summary>
    public static Action<object, object> HandlerResultProcessing = (Action<object, object>) ((target, result) => { });

    /// <summary>
    /// Searches the subscribed handlers to check if we have a handler for
    /// the message type supplied.
    /// </summary>
    /// <param name="messageType">The message type to check with</param>
    /// <returns>True if any handler is found, false if not.</returns>
    public bool HandlerExistsFor(Type messageType)
    {
      return this.handlers.Any<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (handler => handler.Handles(messageType) & !handler.IsDead));
    }

    /// <summary>
    /// Subscribes an instance to all events declared through implementations of <see cref="T:Caliburn.Micro.IHandle`1" />
    /// </summary>
    /// <param name="subscriber">The instance to subscribe for event publication.</param>
    public virtual void Subscribe(object subscriber)
    {
      if (subscriber == null)
        throw new ArgumentNullException(nameof (subscriber));
      lock (this.handlers)
      {
        if (this.handlers.Any<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (x => x.Matches(subscriber))))
          return;
        this.handlers.Add(new EventAggregator.Handler(subscriber));
      }
    }

    /// <summary>Unsubscribes the instance from all events.</summary>
    /// <param name="subscriber">The instance to unsubscribe.</param>
    public virtual void Unsubscribe(object subscriber)
    {
      if (subscriber == null)
        throw new ArgumentNullException(nameof (subscriber));
      lock (this.handlers)
      {
        EventAggregator.Handler handler = this.handlers.FirstOrDefault<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (x => x.Matches(subscriber)));
        if (handler == null)
          return;
        this.handlers.Remove(handler);
      }
    }

    /// <summary>Publishes a message.</summary>
    /// <param name="message">The message instance.</param>
    /// <param name="marshal">Allows the publisher to provide a custom thread marshaller for the message publication.</param>
    public virtual void Publish(object message, Action<Action> marshal)
    {
      if (message == null)
        throw new ArgumentNullException(nameof (message));
      if (marshal == null)
        throw new ArgumentNullException(nameof (marshal));
      EventAggregator.Handler[] toNotify;
      lock (this.handlers)
        toNotify = this.handlers.ToArray();
      marshal((Action) (() =>
      {
        Type messageType = message.GetType();
        List<EventAggregator.Handler> list = ((IEnumerable<EventAggregator.Handler>) toNotify).Where<EventAggregator.Handler>((Func<EventAggregator.Handler, bool>) (handler => !handler.Handle(messageType, message))).ToList<EventAggregator.Handler>();
        if (!list.Any<EventAggregator.Handler>())
          return;
        lock (this.handlers)
          list.Apply<EventAggregator.Handler>((Action<EventAggregator.Handler>) (x => this.handlers.Remove(x)));
      }));
    }

    private class Handler
    {
      private readonly WeakReference reference;
      private readonly Dictionary<Type, MethodInfo> supportedHandlers = new Dictionary<Type, MethodInfo>();

      public bool IsDead => this.reference.Target == null;

      public Handler(object handler)
      {
        this.reference = new WeakReference(handler);
        foreach (Type t in handler.GetType().GetInterfaces().Where<Type>((Func<Type, bool>) (x => typeof (IHandle).IsAssignableFrom(x) && x.IsGenericType())))
        {
          Type genericArgument = t.GetGenericArguments()[0];
          MethodInfo method = t.GetMethod("Handle", new Type[1]
          {
            genericArgument
          });
          this.supportedHandlers[genericArgument] = method;
        }
      }

      public bool Matches(object instance) => this.reference.Target == instance;

      public bool Handle(Type messageType, object message)
      {
        object target = this.reference.Target;
        if (target == null)
          return false;
        foreach (KeyValuePair<Type, MethodInfo> supportedHandler in this.supportedHandlers)
        {
          if (supportedHandler.Key.IsAssignableFrom(messageType))
          {
            object obj = supportedHandler.Value.Invoke(target, new object[1]
            {
              message
            });
            if (obj != null)
              EventAggregator.HandlerResultProcessing(target, obj);
          }
        }
        return true;
      }

      public bool Handles(Type messageType)
      {
        return this.supportedHandlers.Any<KeyValuePair<Type, MethodInfo>>((Func<KeyValuePair<Type, MethodInfo>, bool>) (pair => pair.Key.IsAssignableFrom(messageType)));
      }
    }
  }
}
