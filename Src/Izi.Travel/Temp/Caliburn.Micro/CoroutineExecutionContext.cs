// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.CoroutineExecutionContext
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>The context used during the execution of a Coroutine.</summary>
  public class CoroutineExecutionContext
  {
    /// <summary>The source from which the message originates.</summary>
    public object Source;
    /// <summary>The view associated with the target.</summary>
    public object View;
    /// <summary>The instance on which the action is invoked.</summary>
    public object Target;
  }
}
