// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.KeysCollectionAccessor`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

#nullable disable
namespace System.Collections.Immutable
{
  internal class KeysCollectionAccessor<TKey, TValue> : 
    KeysOrValuesCollectionAccessor<TKey, TValue, TKey>
  {
    internal KeysCollectionAccessor(IImmutableDictionary<TKey, TValue> dictionary)
      : base(dictionary, dictionary.Keys)
    {
    }

    public override bool Contains(TKey item) => this.Dictionary.ContainsKey(item);
  }
}
