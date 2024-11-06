// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Common.ControlHelper
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System;
using System.Windows;
using System.Windows.Media.Animation;

#nullable disable
namespace Coding4Fun.Toolkit.Controls.Common
{
  public class ControlHelper
  {
    internal static int MagicSpacingNumber = 12;

    [Obsolete("Made into extension")]
    public static double CheckBound(double value, double max) => value.CheckBound(max);

    [Obsolete("Made into extension")]
    public static double CheckBound(double value, double min, double max)
    {
      return value.CheckBound(min, max);
    }

    public static void CreateDoubleAnimations(
      Storyboard sb,
      DependencyObject target,
      string propertyPath,
      double fromValue = 0.0,
      double toValue = 0.0,
      int speed = 500)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.To = new double?(toValue);
      doubleAnimation.From = new double?(fromValue);
      doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds((double) speed));
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTarget((Timeline) element, target);
      Storyboard.SetTargetProperty((Timeline) element, new PropertyPath(propertyPath, new object[0]));
      sb.Children.Add((Timeline) element);
    }
  }
}
