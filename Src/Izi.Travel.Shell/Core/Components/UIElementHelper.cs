// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.UIElementHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Components
{
  public class UIElementHelper : DependencyObject
  {
    public static readonly DependencyProperty OpacityMaskProperty = DependencyProperty.RegisterAttached("OpacityMask", typeof (ImageSource), typeof (UIElement), new PropertyMetadata((object) null, new PropertyChangedCallback(UIElementHelper.OpacityMaskPropertyChangedCallback)));

    private static void OpacityMaskPropertyChangedCallback(
      DependencyObject x,
      DependencyPropertyChangedEventArgs y)
    {
      UIElement uiElement = x as UIElement;
      ImageSource newValue = y.NewValue as ImageSource;
      if (uiElement == null || newValue == null)
        return;
      uiElement.OpacityMask = (Brush) new ImageBrush()
      {
        ImageSource = newValue
      };
    }

    public static void SetOpacityMask(UIElement element, ImageSource value)
    {
      element.SetValue(UIElementHelper.OpacityMaskProperty, (object) value);
    }

    public static ImageSource GetOpacityMask(UIElement element)
    {
      return (ImageSource) element.GetValue(UIElementHelper.OpacityMaskProperty);
    }
  }
}
