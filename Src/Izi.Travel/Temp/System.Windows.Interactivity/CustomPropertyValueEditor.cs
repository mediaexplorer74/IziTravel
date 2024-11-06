// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.CustomPropertyValueEditor
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

#nullable disable
namespace System.Windows.Interactivity
{
  /// <summary>
  /// Enumerates possible values for reusable property value editors.
  /// </summary>
  public enum CustomPropertyValueEditor
  {
    /// <summary>
    /// Uses the element picker, if supported, to edit this property at design time.
    /// </summary>
    Element,
    /// <summary>
    /// Uses the storyboard picker, if supported, to edit this property at design time.
    /// </summary>
    Storyboard,
    /// <summary>
    /// Uses the state picker, if supported, to edit this property at design time.
    /// </summary>
    StateName,
    /// <summary>
    /// Uses the element-binding picker, if supported, to edit this property at design time.
    /// </summary>
    ElementBinding,
    /// <summary>
    /// Uses the property-binding picker, if supported, to edit this property at design time.
    /// </summary>
    PropertyBinding,
  }
}
