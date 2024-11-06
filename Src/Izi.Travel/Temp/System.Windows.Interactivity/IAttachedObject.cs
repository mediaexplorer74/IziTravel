// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.IAttachedObject
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// An interface for an object that can be attached to another object.
  /// </summary>
  public interface IAttachedObject
  {
    /// <summary>Gets the associated object.</summary>
    /// <value>The associated object.</value>
    /// <remarks>Represents the object the instance is attached to.</remarks>
    DependencyObject AssociatedObject { get; }

    /// <summary>Attaches to the specified object.</summary>
    /// <param name="dependencyObject">The object to attach to.</param>
    void Attach(DependencyObject dependencyObject);

    /// <summary>Detaches this instance from its associated object.</summary>
    void Detach();
  }
}
