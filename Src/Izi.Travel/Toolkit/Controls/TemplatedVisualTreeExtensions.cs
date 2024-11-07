// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.TemplatedVisualTreeExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  public static class TemplatedVisualTreeExtensions
  {
    public static T GetFirstLogicalChildByType<T>(this FrameworkElement parent, bool applyTemplates) where T : FrameworkElement
    {
      Queue<FrameworkElement> frameworkElementQueue = new Queue<FrameworkElement>();
      frameworkElementQueue.Enqueue(parent);
      while (frameworkElementQueue.Count > 0)
      {
        FrameworkElement parent1 = frameworkElementQueue.Dequeue();
        Control control = parent1 as Control;
        if (applyTemplates && control != null)
          control.ApplyTemplate();
        if (parent1 is T && parent1 != parent)
          return (T) parent1;
        foreach (FrameworkElement frameworkElement in parent1.GetVisualChildren().OfType<FrameworkElement>())
          frameworkElementQueue.Enqueue(frameworkElement);
      }
      return default (T);
    }

    public static IEnumerable<T> GetLogicalChildrenByType<T>(
      this FrameworkElement parent,
      bool applyTemplates)
      where T : FrameworkElement
    {
      if (applyTemplates && parent is Control)
        ((Control) parent).ApplyTemplate();
      Queue<FrameworkElement> queue = new Queue<FrameworkElement>(parent.GetVisualChildren().OfType<FrameworkElement>());
      while (queue.Count > 0)
      {
        FrameworkElement element = queue.Dequeue();
        if (applyTemplates && element is Control)
          ((Control) element).ApplyTemplate();
        if (element is T obj)
          yield return obj;
        foreach (FrameworkElement frameworkElement in element.GetVisualChildren().OfType<FrameworkElement>())
          queue.Enqueue(frameworkElement);
        element = (FrameworkElement) null;
      }
    }
  }
}
