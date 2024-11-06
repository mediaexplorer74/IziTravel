// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.Behavior`1
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Encapsulates state information and zero or more ICommands into an attachable object.
  /// </summary>
  /// <typeparam name="T">The type the <see cref="T:System.Windows.Interactivity.Behavior`1" /> can be attached to.</typeparam>
  /// <remarks>
  /// 	Behavior is the base class for providing attachable state and commands to an object.
  /// 	The types the Behavior can be attached to can be controlled by the generic parameter.
  /// 	Override OnAttached() and OnDetaching() methods to hook and unhook any necessary handlers
  /// 	from the AssociatedObject.
  /// </remarks>
  public abstract class Behavior<T> : Behavior where T : DependencyObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.Behavior`1" /> class.
    /// </summary>
    protected Behavior()
      : base(typeof (T))
    {
    }

    /// <summary>
    /// Gets the object to which this <see cref="T:System.Windows.Interactivity.Behavior`1" /> is attached.
    /// </summary>
    protected T AssociatedObject => (T) base.AssociatedObject;
  }
}
