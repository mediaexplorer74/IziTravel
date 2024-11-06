// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.VisualTreeExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  public static class VisualTreeExtensions
  {
    public static IEnumerable<DependencyObject> GetVisualChildren(this DependencyObject parent)
    {
      int childCount = VisualTreeHelper.GetChildrenCount(parent);
      for (int counter = 0; counter < childCount; ++counter)
        yield return VisualTreeHelper.GetChild(parent, counter);
    }

    public static IEnumerable<FrameworkElement> GetLogicalChildrenBreadthFirst(
      this FrameworkElement parent)
    {
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        yield return element;
        foreach (FrameworkElement frameworkElement in element.GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
        element = (FrameworkElement) null;
      }
    }

    internal static IEnumerable<FrameworkElement> GetVisualAncestors(this FrameworkElement node)
    {
      for (FrameworkElement parent = node.GetVisualParent(); parent != null; parent = parent.GetVisualParent())
        yield return parent;
    }

    internal static FrameworkElement GetVisualParent(this FrameworkElement node)
    {
      return VisualTreeHelper.GetParent((DependencyObject) node) as FrameworkElement;
    }

    internal static T GetParentByType<T>(this DependencyObject element) where T : FrameworkElement
    {
      T obj = default (T);
      for (DependencyObject parent = VisualTreeHelper.GetParent(element); parent != null; parent = VisualTreeHelper.GetParent(parent))
      {
        if (parent is T parentByType)
          return parentByType;
      }
      return default (T);
    }
  }
}
