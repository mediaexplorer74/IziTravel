// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskController
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Handles <see cref="T:Caliburn.Micro.TaskExecutionRequested" /> messages and ensures that the property handler receives the completion message.
  /// </summary>
  public class TaskController : IHandle<TaskExecutionRequested>, IHandle
  {
    private const string TaskTypeKey = "Caliburn.Micro.TaskType";
    private const string TaskSateKey = "Caliburn.Micro.TaskState";
    private readonly IPhoneService phoneService;
    private readonly IEventAggregator events;
    private TaskExecutionRequested request;
    private bool isResurrecting;
    private object continueWithMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.TaskController" /> class.
    /// </summary>
    /// <param name="phoneService">The phone service.</param>
    /// <param name="events">The event aggregator.</param>
    public TaskController(IPhoneService phoneService, IEventAggregator events)
    {
      this.phoneService = phoneService;
      this.events = events;
    }

    /// <summary>
    /// Starts monitoring for task requests and controlling completion messages.
    /// </summary>
    public void Start()
    {
      this.phoneService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnDeactivated);
      this.phoneService.Resurrected += new System.Action(this.OnResurrected);
      this.phoneService.Continued += new System.Action(this.OnContinued);
      this.events.Subscribe((object) this);
    }

    /// <summary>
    /// Stops monitoring for task requests and controlling completion messages.
    /// </summary>
    public void Stop()
    {
      this.events.Unsubscribe((object) this);
      this.phoneService.Deactivated -= new EventHandler<DeactivatedEventArgs>(this.OnDeactivated);
      this.phoneService.Resurrected -= new System.Action(this.OnResurrected);
      this.phoneService.Continued -= new System.Action(this.OnContinued);
    }

    void IHandle<TaskExecutionRequested>.Handle(TaskExecutionRequested message)
    {
      Type type = message.Task.GetType();
      EventInfo @event = type.GetEvent("Completed");
      if (@event != null)
      {
        this.request = message;
        @event.AddEventHandler(this.request.Task, this.CreateOnTaskCompletedDelegate(@event));
      }
      type.GetMethod("Show").Invoke(message.Task, (object[]) null);
    }

    private void OnDeactivated(object sender, DeactivatedEventArgs e)
    {
      if (this.request == null)
        return;
      this.phoneService.State["Caliburn.Micro.TaskType"] = (object) this.request.Task.GetType().FullName;
      this.phoneService.State["Caliburn.Micro.TaskState"] = this.request.State ?? (object) string.Empty;
    }

    private void OnContinued()
    {
      if (this.continueWithMessage == null)
        return;
      this.events.PublishOnUIThread(this.continueWithMessage);
      this.continueWithMessage = (object) null;
    }

    private void OnResurrected()
    {
      if (!this.phoneService.State.ContainsKey("Caliburn.Micro.TaskType"))
        return;
      this.isResurrecting = true;
      string name = (string) this.phoneService.State["Caliburn.Micro.TaskType"];
      this.phoneService.State.Remove("Caliburn.Micro.TaskType");
      object obj;
      if (this.phoneService.State.TryGetValue("Caliburn.Micro.TaskState", out obj))
        this.phoneService.State.Remove("Caliburn.Micro.TaskState");
      Type type = typeof (TaskEventArgs).Assembly.GetType(name);
      object instance = Activator.CreateInstance(type);
      this.request = new TaskExecutionRequested()
      {
        State = obj,
        Task = instance
      };
      EventInfo @event = type.GetEvent("Completed");
      @event?.AddEventHandler(this.request.Task, this.CreateOnTaskCompletedDelegate(@event));
    }

    private Delegate CreateOnTaskCompletedDelegate(EventInfo @event)
    {
      MethodInfo method = typeof (TaskController).GetMethod("OnTaskComplete", BindingFlags.Instance | BindingFlags.NonPublic);
      return Delegate.CreateDelegate(@event.EventHandlerType, (object) this, method);
    }

    /// <summary>Called when the task is compled.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected void OnTaskComplete(object sender, EventArgs e)
    {
      Type type = typeof (TaskCompleted<>).MakeGenericType(e.GetType());
      object message = Activator.CreateInstance(type);
      if (this.request.State != null)
        type.GetField("State").SetValue(message, this.request.State);
      type.GetField("Result").SetValue(message, (object) e);
      this.request = (TaskExecutionRequested) null;
      if (this.isResurrecting)
        Task.Delay(500).ContinueWith((Action<Task>) (t =>
        {
          this.events.PublishOnUIThread(message);
          this.isResurrecting = false;
        }));
      else
        this.continueWithMessage = message;
    }
  }
}
