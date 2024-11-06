// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TaskCompleted`1
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A message which is published when a task completes.</summary>
  /// <typeparam name="TTaskEventArgs">The type of the task event args.</typeparam>
  public class TaskCompleted<TTaskEventArgs>
  {
    /// <summary>Optional state provided by the original sender.</summary>
    public object State;
    /// <summary>The results of the task.</summary>
    public TTaskEventArgs Result;
  }
}
