﻿// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TriggerBase`1
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Represents an object that can invoke actions conditionally.
  /// </summary>
  /// <typeparam name="T">The type to which this trigger can be attached.</typeparam>
  /// <remarks>
  /// 	TriggerBase is the base class for controlling actions. Override OnAttached() and
  /// 	OnDetaching() to hook and unhook handlers on the AssociatedObject. You may
  /// 	constrain the types that a derived TriggerBase may be attached to by specifying
  /// 	the generic parameter. Call InvokeActions() to fire all Actions associated with
  /// 	this TriggerBase.
  /// </remarks>
  public abstract class TriggerBase<T> : TriggerBase where T : DependencyObject
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TriggerBase`1" /> class.
    /// </summary>
    protected TriggerBase()
      : base(typeof (T))
    {
    }

    /// <summary>Gets the object to which the trigger is attached.</summary>
    /// <value>The associated object.</value>
    protected T AssociatedObject => (T) base.AssociatedObject;

    /// <summary>Gets the type constraint of the associated object.</summary>
    /// <value>The associated object type constraint.</value>
    protected override sealed Type AssociatedObjectTypeConstraint
    {
      get => base.AssociatedObjectTypeConstraint;
    }
  }
}
