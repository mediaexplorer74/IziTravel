// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.CustomPropertyValueEditorAttribute
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Associates the given editor type with the property on which the CustomPropertyValueEditor is applied.
  /// </summary>
  /// <remarks>Use this attribute to get improved design-time editing for properties that denote element (by name), storyboards, or states (by name).</remarks>
  [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
  public sealed class CustomPropertyValueEditorAttribute : Attribute
  {
    /// <summary>Gets or sets the custom property value editor.</summary>
    /// <value>The custom property value editor.</value>
    public CustomPropertyValueEditor CustomPropertyValueEditor { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:System.Windows.Interactivity.CustomPropertyValueEditorAttribute" /> class.
    /// </summary>
    /// <param name="customPropertyValueEditor">The custom property value editor.</param>
    public CustomPropertyValueEditorAttribute(
      CustomPropertyValueEditor customPropertyValueEditor)
    {
      this.CustomPropertyValueEditor = customPropertyValueEditor;
    }
  }
}
