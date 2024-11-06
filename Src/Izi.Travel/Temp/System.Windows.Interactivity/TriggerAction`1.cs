// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TriggerAction`1
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents an attachable object that encapsulates a unit of functionality.
  /// </summary>
  /// <typeparam name="T">The type to which this action can be attached.</typeparam>
  public abstract class TriggerAction<T> : TriggerAction where T : DependencyObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerAction`1" /> class.
    /// </summary>
    protected TriggerAction()
      : base(typeof (T))
    {
    }

    /// <summary>
    /// Gets the object to which this <see cref="T:System.Windows.Interactivity.TriggerAction`1" /> is attached.
    /// </summary>
    /// <value>The associated object.</value>
    protected T AssociatedObject => (T) base.AssociatedObject;

    /// <summary>Gets the associated object type constraint.</summary>
    /// <value>The associated object type constraint.</value>
    protected override sealed Type AssociatedObjectTypeConstraint
    {
      get => base.AssociatedObjectTypeConstraint;
    }
  }
}
