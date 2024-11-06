// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskExtensionMethods
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Extension methods related to phone tasks.</summary>
  public static class TaskExtensionMethods
  {
    /// <summary>
    /// Creates a task and publishes it using the <see cref="T:Caliburn.Micro.EventAggregator" />.
    /// </summary>
    /// <typeparam name="TTask">The task to create.</typeparam>
    /// <param name="events">The event aggregator.</param>
    /// <param name="configure">Optional configuration for the task.</param>
    /// <param name="state">Optional state to be passed along to the task completion message.</param>
    public static void RequestTask<TTask>(
      this IEventAggregator events,
      Action<TTask> configure = null,
      object state = null)
      where TTask : new()
    {
      TTask task = new TTask();
      if (configure != null)
        configure(task);
      events.PublishOnUIThread((object) new TaskExecutionRequested()
      {
        State = state,
        Task = (object) task
      });
    }
  }
}
