// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableDictionary
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
  public static class ImmutableDictionary
  {
    public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>()
    {
      return ImmutableDictionary<TKey, TValue>.Empty;
    }

    public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer)
    {
      return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer);
    }

    public static ImmutableDictionary<TKey, TValue> Create<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
    }

    public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableDictionary<TKey, TValue>.Empty.AddRange(items);
    }

    public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer,
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer).AddRange(items);
    }

    public static ImmutableDictionary<TKey, TValue> CreateRange<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer,
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(items);
    }

    public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>()
    {
      return ImmutableDictionary.Create<TKey, TValue>().ToBuilder();
    }

    public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer)
    {
      return ImmutableDictionary.Create<TKey, TValue>(keyComparer).ToBuilder();
    }

    public static ImmutableDictionary<TKey, TValue>.Builder CreateBuilder<TKey, TValue>(
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      return ImmutableDictionary.Create<TKey, TValue>(keyComparer, valueComparer).ToBuilder();
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector,
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      Requires.NotNull<IEnumerable<TSource>>(source, nameof (source));
      Requires.NotNull<Func<TSource, TKey>>(keySelector, nameof (keySelector));
      Requires.NotNull<Func<TSource, TValue>>(elementSelector, nameof (elementSelector));
      return ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source.Select<TSource, KeyValuePair<TKey, TValue>>((Func<TSource, KeyValuePair<TKey, TValue>>) (element => new KeyValuePair<TKey, TValue>(keySelector(element), elementSelector(element)))));
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector,
      IEqualityComparer<TKey> keyComparer)
    {
      return source.ToImmutableDictionary<TSource, TKey, TValue>(keySelector, elementSelector, keyComparer, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<TSource, TKey>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector)
    {
      return source.ToImmutableDictionary<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (v => v), (IEqualityComparer<TKey>) null, (IEqualityComparer<TSource>) null);
    }

    public static ImmutableDictionary<TKey, TSource> ToImmutableDictionary<TSource, TKey>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      IEqualityComparer<TKey> keyComparer)
    {
      return source.ToImmutableDictionary<TSource, TKey, TSource>(keySelector, (Func<TSource, TSource>) (v => v), keyComparer, (IEqualityComparer<TSource>) null);
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TSource, TKey, TValue>(
      this IEnumerable<TSource> source,
      Func<TSource, TKey> keySelector,
      Func<TSource, TValue> elementSelector)
    {
      return source.ToImmutableDictionary<TSource, TKey, TValue>(keySelector, elementSelector, (IEqualityComparer<TKey>) null, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source,
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(source, nameof (source));
      return source is ImmutableDictionary<TKey, TValue> immutableDictionary ? immutableDictionary.WithComparers(keyComparer, valueComparer) : ImmutableDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer).AddRange(source);
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source,
      IEqualityComparer<TKey> keyComparer)
    {
      return source.ToImmutableDictionary<TKey, TValue>(keyComparer, (IEqualityComparer<TValue>) null);
    }

    public static ImmutableDictionary<TKey, TValue> ToImmutableDictionary<TKey, TValue>(
      this IEnumerable<KeyValuePair<TKey, TValue>> source)
    {
      return source.ToImmutableDictionary<TKey, TValue>((IEqualityComparer<TKey>) null, (IEqualityComparer<TValue>) null);
    }

    public static bool Contains<TKey, TValue>(
      this IImmutableDictionary<TKey, TValue> map,
      TKey key,
      TValue value)
    {
      Requires.NotNull<IImmutableDictionary<TKey, TValue>>(map, nameof (map));
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return map.Contains(new KeyValuePair<TKey, TValue>(key, value));
    }

    public static TValue GetValueOrDefault<TKey, TValue>(
      this IImmutableDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      return dictionary.GetValueOrDefault<TKey, TValue>(key, default (TValue));
    }

    public static TValue GetValueOrDefault<TKey, TValue>(
      this IImmutableDictionary<TKey, TValue> dictionary,
      TKey key,
      TValue defaultValue)
    {
      Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, nameof (dictionary));
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      TValue obj;
      return dictionary.TryGetValue(key, out obj) ? obj : defaultValue;
    }
  }
}
