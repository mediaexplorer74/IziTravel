// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.WeakValueDictionary`2
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A dictionary in which the values are weak references.</summary>
  /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
  /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
  internal class WeakValueDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
    where TValue : class
  {
    private readonly Dictionary<TKey, WeakReference> inner;
    private readonly WeakReference gcSentinel = new WeakReference(new object());

    private bool IsCleanupNeeded()
    {
      if (this.gcSentinel.Target != null)
        return false;
      this.gcSentinel.Target = new object();
      return true;
    }

    private void CleanAbandonedItems()
    {
      this.inner.Where<KeyValuePair<TKey, WeakReference>>((Func<KeyValuePair<TKey, WeakReference>, bool>) (pair => !pair.Value.IsAlive)).Select<KeyValuePair<TKey, WeakReference>, TKey>((Func<KeyValuePair<TKey, WeakReference>, TKey>) (pair => pair.Key)).ToList<TKey>().Apply<TKey>((Action<TKey>) (key => this.inner.Remove(key)));
    }

    private void CleanIfNeeded()
    {
      if (!this.IsCleanupNeeded())
        return;
      this.CleanAbandonedItems();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that is empty, has the default initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    public WeakValueDictionary() => this.inner = new Dictionary<TKey, WeakReference>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.</param>
    public WeakValueDictionary(IDictionary<TKey, TValue> dictionary)
    {
      this.inner = new Dictionary<TKey, WeakReference>();
      dictionary.Apply<KeyValuePair<TKey, TValue>>((Action<KeyValuePair<TKey, TValue>>) (item => this.inner.Add(item.Key, new WeakReference((object) item.Value))));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that contains elements copied from the specified <see cref="T:System.Collections.Generic.IDictionary`2" /> and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="dictionary">The <see cref="T:System.Collections.Generic.IDictionary`2" /> whose elements are copied to the new <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.</param>
    /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
    public WeakValueDictionary(
      IDictionary<TKey, TValue> dictionary,
      IEqualityComparer<TKey> comparer)
    {
      this.inner = new Dictionary<TKey, WeakReference>(comparer);
      dictionary.Apply<KeyValuePair<TKey, TValue>>((Action<KeyValuePair<TKey, TValue>>) (item => this.inner.Add(item.Key, new WeakReference((object) item.Value))));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that is empty, has the default initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
    public WeakValueDictionary(IEqualityComparer<TKey> comparer)
    {
      this.inner = new Dictionary<TKey, WeakReference>(comparer);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that is empty, has the specified initial capacity, and uses the default equality comparer for the key type.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> can contain.</param>
    public WeakValueDictionary(int capacity)
    {
      this.inner = new Dictionary<TKey, WeakReference>(capacity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> class that is empty, has the specified initial capacity, and uses the specified <see cref="T:System.Collections.Generic.IEqualityComparer`1" />.
    /// </summary>
    /// <param name="capacity">The initial number of elements that the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> can contain.</param>
    /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing keys, or null to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" /> for the type of the key.</param>
    public WeakValueDictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
      this.inner = new Dictionary<TKey, WeakReference>(capacity, comparer);
    }

    /// <summary>
    /// Returns an enumerator that iterates through the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    /// <returns>The enumerator.</returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
      this.CleanIfNeeded();
      return this.inner.Select<KeyValuePair<TKey, WeakReference>, KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, WeakReference>, KeyValuePair<TKey, TValue>>) (pair => new KeyValuePair<TKey, TValue>(pair.Key, (TValue) pair.Value.Target))).Where<KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, bool>) (pair => (object) pair.Value != null)).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      this.Add(item.Key, item.Value);
    }

    /// <summary>
    /// Removes all keys and values from the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    public void Clear() => this.inner.Clear();

    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
      TValue obj;
      return this.TryGetValue(item.Key, out obj) && (object) obj == (object) item.Value;
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      if (array == null)
        throw new ArgumentNullException(nameof (array));
      if (arrayIndex < 0 || arrayIndex >= array.Length)
        throw new ArgumentOutOfRangeException(nameof (arrayIndex));
      if (arrayIndex + this.Count > array.Length)
        throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
      this.ToArray<KeyValuePair<TKey, TValue>>().CopyTo((Array) array, arrayIndex);
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      TValue obj;
      return this.TryGetValue(item.Key, out obj) && (object) obj == (object) item.Value && this.inner.Remove(item.Key);
    }

    /// <summary>
    /// Gets the number of key/value pairs contained in the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    /// <remarks>
    /// Since the items in the dictionary are held by weak reference, the count value
    /// cannot be relied upon to guarantee the number of objects that would be discovered via
    /// enumeration. Treat the Count as an estimate only.
    /// </remarks>
    public int Count
    {
      get
      {
        this.CleanIfNeeded();
        return this.inner.Count;
      }
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

    /// <summary>Adds the specified key and value to the dictionary.</summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">The value of the element to add. The value can be null for reference types.</param>
    public void Add(TKey key, TValue value)
    {
      this.CleanIfNeeded();
      this.inner.Add(key, new WeakReference((object) value));
    }

    /// <summary>
    /// Determines whether the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> contains the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.</param>
    /// <returns></returns>
    public bool ContainsKey(TKey key) => this.TryGetValue(key, out TValue _);

    /// <summary>
    /// Removes the value with the specified key from the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>true if the element is successfully found and removed; otherwise, false. This method returns false if key is not found in the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.</returns>
    public bool Remove(TKey key)
    {
      this.CleanIfNeeded();
      return this.inner.Remove(key);
    }

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key,
    /// if the key is found; otherwise, the default value for the type of the value parameter.
    /// This parameter is passed uninitialized.</param>
    /// <returns>true if the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" /> contains an element with the specified key; otherwise, false.</returns>
    public bool TryGetValue(TKey key, out TValue value)
    {
      this.CleanIfNeeded();
      WeakReference weakReference;
      if (!this.inner.TryGetValue(key, out weakReference))
      {
        value = default (TValue);
        return false;
      }
      TValue target = (TValue) weakReference.Target;
      if ((object) target == null)
      {
        this.inner.Remove(key);
        value = default (TValue);
        return false;
      }
      value = target;
      return true;
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <param name="key">The key of the value to get or set.</param>
    /// <returns>
    /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />,
    /// and a set operation creates a new element with the specified key.
    /// </returns>
    public TValue this[TKey key]
    {
      get
      {
        TValue obj;
        if (!this.TryGetValue(key, out obj))
          throw new KeyNotFoundException();
        return obj;
      }
      set
      {
        this.CleanIfNeeded();
        this.inner[key] = new WeakReference((object) value);
      }
    }

    /// <summary>
    /// Gets a collection containing the keys in the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    public ICollection<TKey> Keys => (ICollection<TKey>) this.inner.Keys;

    /// <summary>
    /// Gets a collection containing the values in the <see cref="T:Caliburn.Micro.WeakValueDictionary`2" />.
    /// </summary>
    public ICollection<TValue> Values
    {
      get => (ICollection<TValue>) new WeakValueDictionary<TKey, TValue>.ValueCollection(this);
    }

    private sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable
    {
      private readonly WeakValueDictionary<TKey, TValue> inner;

      public ValueCollection(WeakValueDictionary<TKey, TValue> dictionary)
      {
        this.inner = dictionary;
      }

      public IEnumerator<TValue> GetEnumerator()
      {
        return this.inner.Select<KeyValuePair<TKey, TValue>, TValue>((Func<KeyValuePair<TKey, TValue>, TValue>) (pair => pair.Value)).GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      void ICollection<TValue>.Add(TValue item) => throw new NotSupportedException();

      void ICollection<TValue>.Clear() => throw new NotSupportedException();

      bool ICollection<TValue>.Contains(TValue item)
      {
        return this.inner.Any<KeyValuePair<TKey, TValue>>((Func<KeyValuePair<TKey, TValue>, bool>) (pair => (object) pair.Value == (object) item));
      }

      public void CopyTo(TValue[] array, int arrayIndex)
      {
        if (array == null)
          throw new ArgumentNullException(nameof (array));
        if (arrayIndex < 0 || arrayIndex >= array.Length)
          throw new ArgumentOutOfRangeException(nameof (arrayIndex));
        if (arrayIndex + this.Count > array.Length)
          throw new ArgumentException("The number of elements in the source collection is greater than the available space from arrayIndex to the end of the destination array.");
        this.ToArray<TValue>().CopyTo((Array) array, arrayIndex);
      }

      bool ICollection<TValue>.Remove(TValue item) => throw new NotSupportedException();

      public int Count => this.inner.Count;

      bool ICollection<TValue>.IsReadOnly => true;
    }
  }
}
