// ********************************************************************
// Type: Izi.Travel.Core.Controls.SplitPanel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Core.Controls
{
  public class SplitPanel : Panel
  {
    protected override Size MeasureOverride(Size availableSize)
    {
      List<UIElement> list = this.Children.Where<UIElement>((Func<UIElement, bool>) (x => x.Visibility == Visibility.Visible)).ToList<UIElement>();
      Size size = new Size() { Width = availableSize.Width };
      if (list.Count != 0)
        availableSize.Width /= (double) list.Count;
      foreach (UIElement uiElement in list)
      {
        uiElement.Measure(availableSize);
        Size desiredSize = uiElement.DesiredSize;
        size.Height = Math.Max(size.Height, desiredSize.Height);
      }
      return double.IsPositiveInfinity(size.Height) || double.IsPositiveInfinity(size.Width) ? Size.Empty : size;
    }

    protected override Size ArrangeOverride(Size arrangeSize)
    {
      List<UIElement> list = this.Children.Where<UIElement>((Func<UIElement, bool>) (x => x.Visibility == Visibility.Visible)).ToList<UIElement>();
      Rect finalRect = new Rect(new Point(), arrangeSize);
      double num = arrangeSize.Width / (double) list.Count;
      foreach (UIElement uiElement in list)
      {
        finalRect.Height = Math.Max(arrangeSize.Height, uiElement.DesiredSize.Height);
        finalRect.Width = num;
        uiElement.Arrange(finalRect);
        finalRect.X += num;
      }
      return arrangeSize;
    }
  }
}

