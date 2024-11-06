// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableSortedSet
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableSortedSet
  {
    public static ImmutableSortedSet<T> Create<T>() => ImmutableSortedSet<T>.Empty;

    public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer)
    {
      return ImmutableSortedSet<T>.Empty.WithComparer(comparer);
    }

    public static ImmutableSortedSet<T> Create<T>(T item) => ImmutableSortedSet<T>.Empty.Add(item);

    public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, T item)
    {
      return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Add(item);
    }

    public static ImmutableSortedSet<T> CreateRange<T>(IEnumerable<T> items)
    {
      return ImmutableSortedSet<T>.Empty.Union(items);
    }

    public static ImmutableSortedSet<T> CreateRange<T>(IComparer<T> comparer, IEnumerable<T> items)
    {
      return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union(items);
    }

    public static ImmutableSortedSet<T> Create<T>(params T[] items)
    {
      return ImmutableSortedSet<T>.Empty.Union((IEnumerable<T>) items);
    }

    public static ImmutableSortedSet<T> Create<T>(IComparer<T> comparer, params T[] items)
    {
      return ImmutableSortedSet<T>.Empty.WithComparer(comparer).Union((IEnumerable<T>) items);
    }

    public static ImmutableSortedSet<T>.Builder CreateBuilder<T>()
    {
      return ImmutableSortedSet.Create<T>().ToBuilder();
    }

    public static ImmutableSortedSet<T>.Builder CreateBuilder<T>(IComparer<T> comparer)
    {
      return ImmutableSortedSet.Create<T>(comparer).ToBuilder();
    }

    public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(
      this IEnumerable<TSource> source,
      IComparer<TSource> comparer)
    {
      return source is ImmutableSortedSet<TSource> immutableSortedSet ? immutableSortedSet.WithComparer(comparer) : ImmutableSortedSet<TSource>.Empty.WithComparer(comparer).Union(source);
    }

    public static ImmutableSortedSet<TSource> ToImmutableSortedSet<TSource>(
      this IEnumerable<TSource> source)
    {
      return source.ToImmutableSortedSet<TSource>((IComparer<TSource>) null);
    }
  }
}
