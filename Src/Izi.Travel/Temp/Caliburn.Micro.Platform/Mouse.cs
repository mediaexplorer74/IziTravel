// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Mouse
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System.Windows;
using System.Windows.Input;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A mouse helper utility.</summary>
  public static class Mouse
  {
    /// <summary>The current position of the mouse.</summary>
    public static Point Position { get; set; }

    /// <summary>
    /// Initializes the mouse helper with the UIElement to use in mouse tracking.
    /// </summary>
    /// <param name="element">The UIElement to use for mouse tracking.</param>
    public static void Initialize(UIElement element)
    {
      element.MouseMove += (MouseEventHandler) ((s, e) => Mouse.Position = e.GetPosition((UIElement) null));
    }
  }
}
