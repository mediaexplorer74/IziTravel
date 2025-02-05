// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Extensions.DependencyObjectExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Extensions
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
