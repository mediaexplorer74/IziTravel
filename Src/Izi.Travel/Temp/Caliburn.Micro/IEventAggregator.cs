// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IEventAggregator
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Enables loosely-coupled publication of and subscription to events.
  /// </summary>
  public interface IEventAggregator
  {
    /// <summary>
    /// Searches the subscribed handlers to check if we have a handler for
    /// the message type supplied.
    /// </summary>
    /// <param name="messageType">The message type to check with</param>
    /// <returns>True if any handler is found, false if not.</returns>
    bool HandlerExistsFor(Type messageType);

    /// <summary>
    ///   Subscribes an instance to all events declared through implementations of <see cref="T:Caliburn.Micro.IHandle`1" />
    /// </summary>
    /// <param name="subscriber">The instance to subscribe for event publication.</param>
    void Subscribe(object subscriber);

    /// <summary>Unsubscribes the instance from all events.</summary>
    /// <param name="subscriber">The instance to unsubscribe.</param>
    void Unsubscribe(object subscriber);

    /// <summary>Publishes a message.</summary>
    /// <param name="message">The message instance.</param>
    /// <param name="marshal">Allows the publisher to provide a custom thread marshaller for the message publication.</param>
    void Publish(object message, Action<Action> marshal);
  }
}
