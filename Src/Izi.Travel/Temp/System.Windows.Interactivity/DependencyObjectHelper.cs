// Decompiled with JetBrains decompiler
// Type: System.Windows.Interactivity.DependencyObjectHelper
// Assembly: System.Windows.Interactivity, Version=3.9.5.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: AF3F364D-9511-45E0-99E0-CAF6B3A2782E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Windows.Interactivity.xml

using System.Collections.Generic;
using System.Windows.Media;

#nullable disable
namespace System.Windows.Interactivity
{
  public static class DependencyObjectHelper
  {
    /// <summary>
    /// This method will use the VisualTreeHelper.GetParent method to do a depth first walk up
    /// the visual tree and return all ancestors of the specified object, including the object itself.
    /// </summary>
    /// <param name="dependencyObject">The object in the visual tree to find ancestors of.</param>
    /// <returns>Returns itself an all ancestors in the visual tree.</returns>
    public static IEnumerable<DependencyObject> GetSelfAndAncestors(
      this DependencyObject dependencyObject)
    {
      for (; dependencyObject != null; dependencyObject = VisualTreeHelper.GetParent(dependencyObject))
        yield return dependencyObject;
    }
  }
}
