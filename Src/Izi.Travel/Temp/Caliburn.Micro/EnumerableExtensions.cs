// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.EnumerableExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Extension methods for <see cref="T:System.Collections.Generic.IEnumerable`1" />
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>Applies the action to each element in the list.</summary>
    /// <typeparam name="T">The enumerable item's type.</typeparam>
    /// <param name="enumerable">The elements to enumerate.</param>
    /// <param name="action">The action to apply to each item in the list.</param>
    public static void Apply<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
      foreach (T obj in enumerable)
        action(obj);
    }
  }
}
