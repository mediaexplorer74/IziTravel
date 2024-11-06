// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.IImmutableDictionary`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  public interface IImmutableDictionary<TKey, TValue> : 
    IReadOnlyDictionary<TKey, TValue>,
    IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
  {
    IImmutableDictionary<TKey, TValue> Clear();

    IImmutableDictionary<TKey, TValue> Add(TKey key, TValue value);

    IImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs);

    IImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value);

    IImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items);

    IImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys);

    IImmutableDictionary<TKey, TValue> Remove(TKey key);

    bool Contains(KeyValuePair<TKey, TValue> pair);

    bool TryGetKey(TKey equalKey, out TKey actualKey);
  }
}
