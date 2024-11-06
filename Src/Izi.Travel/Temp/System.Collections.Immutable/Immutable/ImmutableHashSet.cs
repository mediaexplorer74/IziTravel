// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableHashSet
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableHashSet
  {
    public static ImmutableHashSet<T> Create<T>() => ImmutableHashSet<T>.Empty;

    public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer)
    {
      return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer);
    }

    public static ImmutableHashSet<T> Create<T>(T item) => ImmutableHashSet<T>.Empty.Add(item);

    public static ImmutableHashSet<T> Create<T>(IEqualityComparer<T> equalityComparer, T item)
    {
      return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Add(item);
    }

    public static ImmutableHashSet<T> CreateRange<T>(IEnumerable<T> items)
    {
      return ImmutableHashSet<T>.Empty.Union(items);
    }

    public static ImmutableHashSet<T> CreateRange<T>(
      IEqualityComparer<T> equalityComparer,
      IEnumerable<T> items)
    {
      return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union(items);
    }

    public static ImmutableHashSet<T> Create<T>(params T[] items)
    {
      return ImmutableHashSet<T>.Empty.Union((IEnumerable<T>) items);
    }

    public static ImmutableHashSet<T> Create<T>(
      IEqualityComparer<T> equalityComparer,
      params T[] items)
    {
      return ImmutableHashSet<T>.Empty.WithComparer(equalityComparer).Union((IEnumerable<T>) items);
    }

    public static ImmutableHashSet<T>.Builder CreateBuilder<T>()
    {
      return ImmutableHashSet.Create<T>().ToBuilder();
    }

    public static ImmutableHashSet<T>.Builder CreateBuilder<T>(IEqualityComparer<T> equalityComparer)
    {
      return ImmutableHashSet.Create<T>(equalityComparer).ToBuilder();
    }

    public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(
      this IEnumerable<TSource> source,
      IEqualityComparer<TSource> equalityComparer)
    {
      return source is ImmutableHashSet<TSource> immutableHashSet ? immutableHashSet.WithComparer(equalityComparer) : ImmutableHashSet<TSource>.Empty.WithComparer(equalityComparer).Union(source);
    }

    public static ImmutableHashSet<TSource> ToImmutableHashSet<TSource>(
      this IEnumerable<TSource> source)
    {
      return source.ToImmutableHashSet<TSource>((IEqualityComparer<TSource>) null);
    }
  }
}
