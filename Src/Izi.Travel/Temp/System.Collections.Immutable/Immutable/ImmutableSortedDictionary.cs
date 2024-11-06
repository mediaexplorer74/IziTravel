// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableSortedDictionary
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Linq;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableSortedDictionary
  {
    public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>()
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty;
    }

    public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(
      IComparer<TKey> keyComparer)
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
    }

    public static ImmutableSortedDictionary<TKey, TValue> Create<TKey, TValue>(
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
    }

    public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty.AddRange(items);
    }

    public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IComparer<TKey> keyComparer,
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
    }

    public static ImmutableSortedDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer,
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
    }

    public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
    {
      return ImmutableSortedDictionary.Create<TKey, TValue>().ToBuilder();
    }

    public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
      IComparer<TKey> keyComparer)
    {
      return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
    }

    public static ImmutableSortedDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      return ImmutableSortedDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector,
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      Requires.NotNull<IEnumerable<TSource>>(source, nameof (source));
      Requires.NotNull<Func<TSource, TKey>>(keySelector, nameof (keySelector));
      Requires.NotNull<Func<TSource, TValue>>(elementSelector, nameof (elementSelector));
      return ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Select<TSource, KeyValuePair<TKey, TValue>>((Func<TSource, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)))));
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector,
      IComparer<TKey> keyComparer)
    {
      return source.ToImmutableSortedDictionary<TSource, TKey, TValue>(keySelector, elementSelector, keyComparer, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector)
    {
      return source.ToImmutableSortedDictionary<TSource, TKey, TValue>(keySelector, elementSelector, (IComparer<TKey>) null, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source,
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, nameof (source));
      return source is ImmutableSortedDictionary<TKey, TValue> sortedDictionary ? sortedDictionary.WithComparers(keyComparer, valueComparer) : ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source,
      IComparer<TKey> keyComparer)
    {
      return source.ToImmutableSortedDictionary<TKey, TValue>(keyComparer, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableSortedDictionary<TKey, TValue> ToImmutableSortedDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
      return source.ToImmutableSortedDictionary<TKey, TValue>((IComparer<TKey>) null, (IEqualityComparer<TValue>) null);
    }
  }
}
