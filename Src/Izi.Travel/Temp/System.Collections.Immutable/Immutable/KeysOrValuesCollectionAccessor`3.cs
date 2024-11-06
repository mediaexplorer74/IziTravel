// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.KeysOrValuesCollectionAccessor`3
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Diagnostics;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  internal abstract class KeysOrValuesCollectionAccessor<TKey, TValue, T> : 
    ICollection<T>,
    IEnumerable<T>,
    ICollection,
    IEnumerable
  {
    private readonly IImmutableDictionary<TKey, TValue> dictionary;
    private readonly IEnumerable<T> keysOrValues;

    protected KeysOrValuesCollectionAccessor(
      IImmutableDictionary<TKey, TValue> dictionary,
      IEnumerable<T> keysOrValues)
    {
      Requires.NotNull<IImmutableDictionary<TKey, TValue>>(dictionary, nameof (dictionary));
      Requires.NotNull<IEnumerable<T>>(keysOrValues, nameof (keysOrValues));
      this.dictionary = dictionary;
      this.keysOrValues = keysOrValues;
    }

    public bool IsReadOnly => true;

    public int Count => this.dictionary.Count;

    protected IImmutableDictionary<TKey, TValue> Dictionary => this.dictionary;

    public void Add(T item) => throw new NotSupportedException();

    public void Clear() => throw new NotSupportedException();

    public abstract bool Contains(T item);

    public void CopyTo(T[] array, int arrayIndex)
    {
      Requires.NotNull<T[]>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (T obj in this)
        array[arrayIndex++] = obj;
    }

    public bool Remove(T item) => throw new NotSupportedException();

    public IEnumerator<T> GetEnumerator() => this.keysOrValues.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      Requires.NotNull<Array>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (T obj in this)
        array.SetValue((object) obj, arrayIndex++);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;
  }
}
