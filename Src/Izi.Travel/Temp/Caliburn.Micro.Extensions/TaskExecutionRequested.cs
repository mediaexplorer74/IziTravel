// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskExecutionRequested
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A message that is published to signify a components request for the execution of a particular task.
  /// </summary>
  public class TaskExecutionRequested
  {
    /// <summary>
    /// Optional state to be passed along to the task completion message.
    /// </summary>
    public object State;
    /// <summary>The task instance.</summary>
    public object Task;
  }
}
