// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.NameResolvedEventArgs
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Provides data about which objects were affected when resolving a name change.
  /// </summary>
  internal sealed class NameResolvedEventArgs : EventArgs
  {
    private object oldObject;
    private object newObject;

    public object OldObject => this.oldObject;

    public object NewObject => this.newObject;

    public NameResolvedEventArgs(object oldObject, object newObject)
    {
      this.oldObject = oldObject;
      this.newObject = newObject;
    }
  }
}
