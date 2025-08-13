// ********************************************************************
// Type: Izi.Travel.Core.Extensions.DependencyObjectExtensions
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

#nullable disable
namespace Izi.Travel.Core.Extensions
{
  public static class DependencyObjectExtensions
  {
    public static DependencyObject FindParent(
      this DependencyObject dp,
      Func<DependencyObject, bool> predicate)
    {
      for (DependencyObject reference = dp; reference != null; reference = VisualTreeHelper.GetParent(reference))
      {
        if (predicate(reference))
          return reference;
      }
      return (DependencyObject) null;
    }
  }
}

