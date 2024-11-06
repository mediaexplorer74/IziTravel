// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.TypeConstraintAttribute
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Specifies type constraints on the AssociatedObject of TargetedTriggerAction and EventTriggerBase.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public sealed class TypeConstraintAttribute : Attribute
  {
    /// <summary>Gets the constraint type.</summary>
    /// <value>The constraint type.</value>
    public Type Constraint { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.TypeConstraintAttribute" /> class.
    /// </summary>
    /// <param name="constraint">The constraint type.</param>
    public TypeConstraintAttribute(Type constraint) => this.Constraint = constraint;
  }
}
