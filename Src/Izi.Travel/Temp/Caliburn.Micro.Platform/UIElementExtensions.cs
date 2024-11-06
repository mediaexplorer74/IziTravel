// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.UIElementExtensions
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension methods for <see cref="T:System.Windows.UIElement" />
  /// </summary>
  public static class UIElementExtensions
  {
    private static readonly ILog Log = LogManager.GetLog(typeof (UIElementExtensions));

    /// <summary>
    /// Calls TransformToVisual on the specified element for the specified visual, suppressing the ArgumentException that can occur in some cases.
    /// </summary>
    /// <param name="element">Element on which to call TransformToVisual.</param>
    /// <param name="visual">Visual to pass to the call to TransformToVisual.</param>
    /// <returns>Resulting GeneralTransform object.</returns>
    public static GeneralTransform SafeTransformToVisual(this UIElement element, UIElement visual)
    {
      try
      {
        return element.TransformToVisual(visual);
      }
      catch (ArgumentException ex)
      {
        UIElementExtensions.Log.Error((Exception) ex);
        return (GeneralTransform) new TranslateTransform();
      }
    }
  }
}
