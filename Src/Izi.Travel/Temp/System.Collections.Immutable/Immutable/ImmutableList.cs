// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableList
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableList
  {
    public static ImmutableList<T> Create<T>() => ImmutableList<T>.Empty;

    public static ImmutableList<T> Create<T>(T item) => ImmutableList<T>.Empty.Add(item);

    public static ImmutableList<T> CreateRange<T>(IEnumerable<T> items)
    {
      return ImmutableList<T>.Empty.AddRange(items);
    }

    public static ImmutableList<T> Create<T>(params T[] items)
    {
      return ImmutableList<T>.Empty.AddRange((IEnumerable<T>) items);
    }

    public static ImmutableList<T>.Builder CreateBuilder<T>()
    {
      return ImmutableList.Create<T>().ToBuilder();
    }

    public static ImmutableList<TSource> ToImmutableList<TSource>(this IEnumerable<TSource> source)
    {
      return source is ImmutableList<TSource> immutableList ? immutableList : ImmutableList<TSource>.Empty.AddRange(source);
    }

    public static IImmutableList<T> Replace<T>(this IImmutableList<T> list, T oldValue, T newValue)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.Replace(oldValue, newValue, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static IImmutableList<T> Remove<T>(this IImmutableList<T> list, T value)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.Remove(value, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static IImmutableList<T> RemoveRange<T>(
      this IImmutableList<T> list,
      IEnumerable<T> items)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.RemoveRange(items, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int IndexOf<T>(this IImmutableList<T> list, T item)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.IndexOf(item, 0, list.Count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int IndexOf<T>(
      this IImmutableList<T> list,
      T item,
      IEqualityComparer<T> equalityComparer)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.IndexOf(item, 0, list.Count, equalityComparer);
    }

    public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.IndexOf(item, startIndex, list.Count - startIndex, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int IndexOf<T>(this IImmutableList<T> list, T item, int startIndex, int count)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.IndexOf(item, startIndex, count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int LastIndexOf<T>(this IImmutableList<T> list, T item)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.Count == 0 ? -1 : list.LastIndexOf(item, list.Count - 1, list.Count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int LastIndexOf<T>(
      this IImmutableList<T> list,
      T item,
      IEqualityComparer<T> equalityComparer)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.Count == 0 ? -1 : list.LastIndexOf(item, list.Count - 1, list.Count, equalityComparer);
    }

    public static int LastIndexOf<T>(this IImmutableList<T> list, T item, int startIndex)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.Count == 0 && startIndex == 0 ? -1 : list.LastIndexOf(item, startIndex, startIndex + 1, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public static int LastIndexOf<T>(
      this IImmutableList<T> list,
      T item,
      int startIndex,
      int count)
    {
      Requires.NotNull<IImmutableList<T>>(list, nameof (list));
      return list.LastIndexOf(item, startIndex, count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }
  }
}
