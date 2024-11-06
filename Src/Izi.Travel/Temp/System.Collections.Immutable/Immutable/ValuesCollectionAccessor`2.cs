// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ValuesCollectionAccessor`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

#nullable disable
namespace System.Collections.Immutable
{
  internal class ValuesCollectionAccessor<TKey, TValue> : 
    KeysOrValuesCollectionAccessor<TKey, TValue, TValue>
  {
    internal ValuesCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
      : base(dictionary, dictionary.Values)
    {
    }

    public override bool Contains(TValue item)
    {
      if (this.Dictionary is ImmutableSortedDictionary<TKey, TValue> dictionary1)
        return dictionary1.ContainsValue(item);
      return this.Dictionary is IImmutableDictionaryInternal<TKey, TValue> dictionary2 ? dictionary2.ContainsValue(item) : throw new NotSupportedException();
    }
  }
}
