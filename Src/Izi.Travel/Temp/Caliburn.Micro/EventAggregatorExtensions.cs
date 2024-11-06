// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.EventAggregatorExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extensions for <see cref="T:Caliburn.Micro.IEventAggregator" />.
  /// </summary>
  public static class EventAggregatorExtensions
  {
    /// <summary>Publishes a message on the current thread (synchrone).</summary>
    /// <param name="eventAggregator">The event aggregator.</param>
    /// <param name="message">The message instance.</param>
    public static void PublishOnCurrentThread(this IEventAggregator eventAggregator, object message)
    {
      eventAggregator.Publish(message, (Action<Action>) (action => action()));
    }

    /// <summary>Publishes a message on a background thread (async).</summary>
    /// <param name="eventAggregator">The event aggregator.</param>
    /// <param name="message">The message instance.</param>
    public static void PublishOnBackgroundThread(
      this IEventAggregator eventAggregator,
      object message)
    {
      eventAggregator.Publish(message, (Action<Action>) (action => Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)));
    }

    /// <summary>Publishes a message on the UI thread.</summary>
    /// <param name="eventAggregator">The event aggregator.</param>
    /// <param name="message">The message instance.</param>
    public static void PublishOnUIThread(this IEventAggregator eventAggregator, object message)
    {
      eventAggregator.Publish(message, new Action<Action>(Execute.OnUIThread));
    }

    /// <summary>Publishes a message on the UI thread asynchrone.</summary>
    /// <param name="eventAggregator">The event aggregator.</param>
    /// <param name="message">The message instance.</param>
    public static void BeginPublishOnUIThread(this IEventAggregator eventAggregator, object message)
    {
      eventAggregator.Publish(message, new Action<Action>(Execute.BeginOnUIThread));
    }

    /// <summary>Publishes a message on the UI thread asynchrone.</summary>
    /// <param name="eventAggregator">The event aggregator.</param>
    /// <param name="message">The message instance.</param>
    public static Task PublishOnUIThreadAsync(this IEventAggregator eventAggregator, object message)
    {
      Task task = (Task) null;
      eventAggregator.Publish(message, (Action<Action>) (action => task = action.OnUIThreadAsync()));
      return task;
    }
  }
}
