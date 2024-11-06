// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.DictionaryEnumerator`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  internal class DictionaryEnumerator<TKey, TValue> : IDictionaryEnumerator, IEnumerator
  {
    private readonly IEnumerator<KeyValuePair<TKey, TValue>> inner;

    internal DictionaryEnumerator(IEnumerator<KeyValuePair<TKey, TValue>> inner)
    {
      Requires.NotNull<IEnumerator<KeyValuePair<TKey, TValue>>>(inner, nameof (inner));
      this.inner = inner;
    }

    public DictionaryEntry Entry
    {
      get
      {
        return new DictionaryEntry((object) this.inner.Current.Key, (object) this.inner.Current.Value);
      }
    }

    public object Key => (object) this.inner.Current.Key;

    public object Value => (object) this.inner.Current.Value;

    public object Current => (object) this.Entry;

    public bool MoveNext() => this.inner.MoveNext();

    public void Reset() => this.inner.Reset();
  }
}
