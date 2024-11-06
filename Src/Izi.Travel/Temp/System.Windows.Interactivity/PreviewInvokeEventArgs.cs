// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.PreviewInvokeEventArgs
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Argument passed to PreviewInvoke event. Assigning Cancelling to True will cancel the invoking of the trigger.
  /// </summary>
  /// <remarks>This is an infrastructure class. Behavior attached to a trigger base object can add its behavior as a listener to TriggerBase.PreviewInvoke.</remarks>
  public class PreviewInvokeEventArgs : EventArgs
  {
    public bool Cancelling { get; set; }
  }
}
