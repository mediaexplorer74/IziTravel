// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableDictionary`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerTypeProxy(typeof (ImmutableDictionary<,>.DebuggerProxy))]
  [DebuggerDisplay("Count = {Count}")]
  public sealed class ImmutableDictionary<TKey, TValue> : 
    IImmutableDictionary<TKey, TValue>,
    IReadOnlyDictionary<TKey, TValue>,
    IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
    IImmutableDictionaryInternal<TKey, TValue>,
    IHashKeyCollection<TKey>,
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IDictionary,
    ICollection,
    IEnumerable
  {
    public static readonly ImmutableDictionary<TKey, TValue> Empty = new ImmutableDictionary<TKey, TValue>();
    private static readonly Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>> FreezeBucketAction = (Action<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>>) (kv => kv.Value.Freeze());
    private readonly int count;
    private readonly ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root;
    private readonly ImmutableDictionary<TKey, TValue>.Comparers comparers;

    private ImmutableDictionary(
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
      ImmutableDictionary<TKey, TValue>.Comparers comparers,
      int count)
      : this(Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, nameof (comparers)))
    {
      Requires.NotNull<ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node>(root, nameof (root));
      root.Freeze(ImmutableDictionary<TKey, TValue>.FreezeBucketAction);
      this.root = root;
      this.count = count;
    }

    private ImmutableDictionary(
      ImmutableDictionary<TKey, TValue>.Comparers comparers = null)
    {
      this.comparers = comparers ?? ImmutableDictionary<TKey, TValue>.Comparers.Get((IEqualityComparer<TKey>) EqualityComparer<TKey>.Default, (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default);
      this.root = ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node.EmptyNode;
    }

    public ImmutableDictionary<TKey, TValue> Clear()
    {
      return !this.IsEmpty ? ImmutableDictionary<TKey, TValue>.EmptyWithComparers(this.comparers) : this;
    }

    public int Count => this.count;

    public bool IsEmpty => this.Count == 0;

    public IEqualityComparer<TKey> KeyComparer => this.comparers.KeyComparer;

    public IEqualityComparer<TValue> ValueComparer => this.comparers.ValueComparer;

    public IEnumerable<TKey> Keys
    {
      get
      {
        foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> bucket in this.root)
        {
          foreach (KeyValuePair<TKey, TValue> item in bucket.Value)
            yield return item.Key;
        }
      }
    }

    public IEnumerable<TValue> Values
    {
      get
      {
        foreach (KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket> bucket in this.root)
        {
          foreach (KeyValuePair<TKey, TValue> item in bucket.Value)
            yield return item.Value;
        }
      }
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Clear()
    {
      return (IImmutableDictionary<TKey, TValue>) this.Clear();
    }

    ICollection<TKey> IDictionary<TKey, TValue>.Keys
    {
      get
      {
        return (ICollection<TKey>) new KeysCollectionAccessor<TKey, TValue>((IImmutableDictionary<TKey, TValue>) this);
      }
    }

    ICollection<TValue> IDictionary<TKey, TValue>.Values
    {
      get
      {
        return (ICollection<TValue>) new ValuesCollectionAccessor<TKey, TValue>((IImmutableDictionary<TKey, TValue>) this);
      }
    }

    private ImmutableDictionary<TKey, TValue>.MutationInput Origin
    {
      get => new ImmutableDictionary<TKey, TValue>.MutationInput(this);
    }

    public TValue this[TKey key]
    {
      get
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        TValue obj;
        if (this.TryGetValue(key, out obj))
          return obj;
        throw new KeyNotFoundException();
      }
    }

    TValue IDictionary<TKey, TValue>.this[TKey key]
    {
      get => this[key];
      set => throw new NotSupportedException();
    }

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;

    public ImmutableDictionary<TKey, TValue>.Builder ToBuilder()
    {
      return new ImmutableDictionary<TKey, TValue>.Builder(this);
    }

    public ImmutableDictionary<TKey, TValue> Add(TKey key, TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
    }

    public ImmutableDictionary<TKey, TValue> AddRange(IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, nameof (pairs));
      return this.AddRange(pairs, false);
    }

    public ImmutableDictionary<TKey, TValue> SetItem(TKey key, TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
    }

    public ImmutableDictionary<TKey, TValue> SetItems(IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      return ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue).Finalize(this);
    }

    public ImmutableDictionary<TKey, TValue> Remove(TKey key)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return ImmutableDictionary<TKey, TValue>.Remove(key, new ImmutableDictionary<TKey, TValue>.MutationInput(this)).Finalize(this);
    }

    public ImmutableDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
    {
      Requires.NotNull<IEnumerable<TKey>>(keys, nameof (keys));
      int count = this.count;
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root = this.root;
      foreach (TKey key in keys)
      {
        int hashCode = this.KeyComparer.GetHashCode(key);
        ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
        if (root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket))
        {
          ImmutableDictionary<TKey, TValue>.OperationResult result;
          ImmutableDictionary<TKey, TValue>.HashBucket newBucket = hashBucket.Remove(key, this.comparers.KeyOnlyComparer, out result);
          root = ImmutableDictionary<TKey, TValue>.UpdateRoot(root, hashCode, newBucket, this.comparers.HashBucketEqualityComparer);
          if (result == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
            --count;
        }
      }
      return this.Wrap(root, count);
    }

    public bool ContainsKey(TKey key)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return ImmutableDictionary<TKey, TValue>.ContainsKey(key, new ImmutableDictionary<TKey, TValue>.MutationInput(this));
    }

    public bool Contains(KeyValuePair<TKey, TValue> pair)
    {
      return ImmutableDictionary<TKey, TValue>.Contains(pair, this.Origin);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
    }

    public bool TryGetKey(TKey equalKey, out TKey actualKey)
    {
      Requires.NotNullAllowStructs<TKey>(equalKey, nameof (equalKey));
      return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
    }

    public ImmutableDictionary<TKey, TValue> WithComparers(
      IEqualityComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      if (keyComparer == null)
        keyComparer = (IEqualityComparer<TKey>) EqualityComparer<TKey>.Default;
      if (valueComparer == null)
        valueComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
      if (this.KeyComparer != keyComparer)
        return new ImmutableDictionary<TKey, TValue>(ImmutableDictionary<TKey, TValue>.Comparers.Get(keyComparer, valueComparer)).AddRange((IEnumerable<KeyValuePair<TKey, TValue>>) this, true);
      return this.ValueComparer == valueComparer ? this : new ImmutableDictionary<TKey, TValue>(this.root, this.comparers.WithValueComparer(valueComparer), this.count);
    }

    public ImmutableDictionary<TKey, TValue> WithComparers(IEqualityComparer<TKey> keyComparer)
    {
      return this.WithComparers(keyComparer, this.comparers.ValueComparer);
    }

    public bool ContainsValue(TValue value)
    {
      return this.Values.Contains<TValue>(value, this.ValueComparer);
    }

    public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
      return new ImmutableDictionary<TKey, TValue>.Enumerator(this.root);
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Add(
      TKey key,
      TValue value)
    {
      return (IImmutableDictionary<TKey, TValue>) this.Add(key, value);
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItem(
      TKey key,
      TValue value)
    {
      return (IImmutableDictionary<TKey, TValue>) this.SetItem(key, value);
    }

    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.SetItems(
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      return (IImmutableDictionary<TKey, TValue>) this.SetItems(items);
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.AddRange(
      IEnumerable<KeyValuePair<TKey, TValue>> pairs)
    {
      return (IImmutableDictionary<TKey, TValue>) this.AddRange(pairs);
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.RemoveRange(
      IEnumerable<TKey> keys)
    {
      return (IImmutableDictionary<TKey, TValue>) this.RemoveRange(keys);
    }

    [ExcludeFromCodeCoverage]
    IImmutableDictionary<TKey, TValue> IImmutableDictionary<TKey, TValue>.Remove(TKey key)
    {
      return (IImmutableDictionary<TKey, TValue>) this.Remove(key);
    }

    void IDictionary<TKey, TValue>.Add(TKey key, TValue value) => throw new NotSupportedException();

    bool IDictionary<TKey, TValue>.Remove(TKey key) => throw new NotSupportedException();

    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
    {
      throw new NotSupportedException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.Clear() => throw new NotSupportedException();

    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
      throw new NotSupportedException();
    }

    void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
      KeyValuePair<TKey, TValue>[] array,
      int arrayIndex)
    {
      Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
        array[arrayIndex++] = keyValuePair;
    }

    bool IDictionary.IsFixedSize => true;

    bool IDictionary.IsReadOnly => true;

    ICollection IDictionary.Keys
    {
      get
      {
        return (ICollection) new KeysCollectionAccessor<TKey, TValue>((IImmutableDictionary<TKey, TValue>) this);
      }
    }

    ICollection IDictionary.Values
    {
      get
      {
        return (ICollection) new ValuesCollectionAccessor<TKey, TValue>((IImmutableDictionary<TKey, TValue>) this);
      }
    }

    void IDictionary.Add(object key, object value) => throw new NotSupportedException();

    bool IDictionary.Contains(object key) => this.ContainsKey((TKey) key);

    IDictionaryEnumerator IDictionary.GetEnumerator()
    {
      return (IDictionaryEnumerator) new DictionaryEnumerator<TKey, TValue>((IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator());
    }

    void IDictionary.Remove(object key) => throw new NotSupportedException();

    object IDictionary.this[object key]
    {
      get => (object) this[(TKey) key];
      set => throw new NotSupportedException();
    }

    void IDictionary.Clear() => throw new NotSupportedException();

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      Requires.NotNull<Array>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
        array.SetValue((object) new DictionaryEntry((object) keyValuePair.Key, (object) keyValuePair.Value), arrayIndex++);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
    }

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    private static ImmutableDictionary<TKey, TValue> EmptyWithComparers(
      ImmutableDictionary<TKey, TValue>.Comparers comparers)
    {
      Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, nameof (comparers));
      return ImmutableDictionary<TKey, TValue>.Empty.comparers != comparers ? new ImmutableDictionary<TKey, TValue>(comparers) : ImmutableDictionary<TKey, TValue>.Empty;
    }

    private static bool TryCastToImmutableMap(
      IEnumerable<KeyValuePair<TKey, TValue>> sequence,
      out ImmutableDictionary<TKey, TValue> other)
    {
      other = sequence as ImmutableDictionary<TKey, TValue>;
      if (other != null)
        return true;
      if (!(sequence is ImmutableDictionary<TKey, TValue>.Builder builder))
        return false;
      other = builder.ToImmutable();
      return true;
    }

    private static bool ContainsKey(
      TKey key,
      ImmutableDictionary<TKey, TValue>.MutationInput origin)
    {
      int hashCode = origin.KeyComparer.GetHashCode(key);
      ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
      return origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket) && hashBucket.TryGetValue(key, origin.KeyOnlyComparer, out TValue _);
    }

    private static bool Contains(
      KeyValuePair<TKey, TValue> keyValuePair,
      ImmutableDictionary<TKey, TValue>.MutationInput origin)
    {
      int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
      ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
      TValue x;
      return origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket) && hashBucket.TryGetValue(keyValuePair.Key, origin.KeyOnlyComparer, out x) && origin.ValueComparer.Equals(x, keyValuePair.Value);
    }

    private static bool TryGetValue(
      TKey key,
      ImmutableDictionary<TKey, TValue>.MutationInput origin,
      out TValue value)
    {
      int hashCode = origin.KeyComparer.GetHashCode(key);
      ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
      if (origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket))
        return hashBucket.TryGetValue(key, origin.KeyOnlyComparer, out value);
      value = default (TValue);
      return false;
    }

    private static bool TryGetKey(
      TKey equalKey,
      ImmutableDictionary<TKey, TValue>.MutationInput origin,
      out TKey actualKey)
    {
      int hashCode = origin.KeyComparer.GetHashCode(equalKey);
      ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
      if (origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket))
        return hashBucket.TryGetKey(equalKey, origin.KeyOnlyComparer, out actualKey);
      actualKey = equalKey;
      return false;
    }

    private static ImmutableDictionary<TKey, TValue>.MutationResult Add(
      TKey key,
      TValue value,
      ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior,
      ImmutableDictionary<TKey, TValue>.MutationInput origin)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      int hashCode = origin.KeyComparer.GetHashCode(key);
      ImmutableDictionary<TKey, TValue>.OperationResult result;
      ImmutableDictionary<TKey, TValue>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode, (IComparer<int>) Comparer<int>.Default).Add(key, value, origin.KeyOnlyComparer, origin.ValueComparer, behavior, out result);
      return result == ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired ? new ImmutableDictionary<TKey, TValue>.MutationResult(origin) : new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, newBucket, origin.HashBucketComparer), result == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged ? 1 : 0);
    }

    private static ImmutableDictionary<TKey, TValue>.MutationResult AddRange(
      IEnumerable<KeyValuePair<TKey, TValue>> items,
      ImmutableDictionary<TKey, TValue>.MutationInput origin,
      ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior collisionBehavior = ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      int countAdjustment = 0;
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root = origin.Root;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
      {
        int hashCode = origin.KeyComparer.GetHashCode(keyValuePair.Key);
        ImmutableDictionary<TKey, TValue>.OperationResult result;
        ImmutableDictionary<TKey, TValue>.HashBucket newBucket = root.GetValueOrDefault(hashCode, (IComparer<int>) Comparer<int>.Default).Add(keyValuePair.Key, keyValuePair.Value, origin.KeyOnlyComparer, origin.ValueComparer, collisionBehavior, out result);
        root = ImmutableDictionary<TKey, TValue>.UpdateRoot(root, hashCode, newBucket, origin.HashBucketComparer);
        if (result == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged)
          ++countAdjustment;
      }
      return new ImmutableDictionary<TKey, TValue>.MutationResult(root, countAdjustment);
    }

    private static ImmutableDictionary<TKey, TValue>.MutationResult Remove(
      TKey key,
      ImmutableDictionary<TKey, TValue>.MutationInput origin)
    {
      int hashCode = origin.KeyComparer.GetHashCode(key);
      ImmutableDictionary<TKey, TValue>.HashBucket hashBucket;
      ImmutableDictionary<TKey, TValue>.OperationResult result;
      return origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket) ? new ImmutableDictionary<TKey, TValue>.MutationResult(ImmutableDictionary<TKey, TValue>.UpdateRoot(origin.Root, hashCode, hashBucket.Remove(key, origin.KeyOnlyComparer, out result), origin.HashBucketComparer), result == ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged ? -1 : 0) : new ImmutableDictionary<TKey, TValue>.MutationResult(origin);
    }

    private static ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node UpdateRoot(
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
      int hashCode,
      ImmutableDictionary<TKey, TValue>.HashBucket newBucket,
      IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> hashBucketComparer)
    {
      bool mutated;
      return newBucket.IsEmpty ? root.Remove(hashCode, (IComparer<int>) Comparer<int>.Default, out mutated) : root.SetItem(hashCode, newBucket, (IComparer<int>) Comparer<int>.Default, hashBucketComparer, out bool _, out mutated);
    }

    private static ImmutableDictionary<TKey, TValue> Wrap(
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
      ImmutableDictionary<TKey, TValue>.Comparers comparers,
      int count)
    {
      Requires.NotNull<ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node>(root, nameof (root));
      Requires.NotNull<ImmutableDictionary<TKey, TValue>.Comparers>(comparers, nameof (comparers));
      Requires.Range(count >= 0, nameof (count));
      return new ImmutableDictionary<TKey, TValue>(root, comparers, count);
    }

    private ImmutableDictionary<TKey, TValue> Wrap(
      ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
      int adjustedCountIfDifferentRoot)
    {
      if (root == null)
        return this.Clear();
      if (this.root == root)
        return this;
      return !root.IsEmpty ? new ImmutableDictionary<TKey, TValue>(root, this.comparers, adjustedCountIfDifferentRoot) : this.Clear();
    }

    private ImmutableDictionary<TKey, TValue> AddRange(
      IEnumerable<KeyValuePair<TKey, TValue>> pairs,
      bool avoidToHashMap)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(pairs, nameof (pairs));
      ImmutableDictionary<TKey, TValue> other;
      return this.IsEmpty && !avoidToHashMap && ImmutableDictionary<TKey, TValue>.TryCastToImmutableMap(pairs, out other) ? other.WithComparers(this.KeyComparer, this.ValueComparer) : ImmutableDictionary<TKey, TValue>.AddRange(pairs, this.Origin).Finalize(this);
    }

    internal class Comparers : 
      IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>,
      IEqualityComparer<KeyValuePair<TKey, TValue>>
    {
      internal static readonly ImmutableDictionary<TKey, TValue>.Comparers Default = new ImmutableDictionary<TKey, TValue>.Comparers((IEqualityComparer<TKey>) EqualityComparer<TKey>.Default, (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default);
      private readonly IEqualityComparer<TKey> keyComparer;
      private readonly IEqualityComparer<TValue> valueComparer;

      internal Comparers(
        IEqualityComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer)
      {
        Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, nameof (keyComparer));
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        this.keyComparer = keyComparer;
        this.valueComparer = valueComparer;
      }

      internal IEqualityComparer<TKey> KeyComparer => this.keyComparer;

      internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
      {
        get => (IEqualityComparer<KeyValuePair<TKey, TValue>>) this;
      }

      internal IEqualityComparer<TValue> ValueComparer => this.valueComparer;

      internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketEqualityComparer
      {
        get => (IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket>) this;
      }

      public bool Equals(
        ImmutableDictionary<TKey, TValue>.HashBucket x,
        ImmutableDictionary<TKey, TValue>.HashBucket y)
      {
        return object.ReferenceEquals((object) x.AdditionalElements, (object) y.AdditionalElements) && this.KeyComparer.Equals(x.FirstValue.Key, y.FirstValue.Key) && this.ValueComparer.Equals(x.FirstValue.Value, y.FirstValue.Value);
      }

      public int GetHashCode(ImmutableDictionary<TKey, TValue>.HashBucket obj)
      {
        return this.KeyComparer.GetHashCode(obj.FirstValue.Key);
      }

      bool IEqualityComparer<KeyValuePair<TKey, TValue>>.Equals(
        KeyValuePair<TKey, TValue> x,
        KeyValuePair<TKey, TValue> y)
      {
        return this.keyComparer.Equals(x.Key, y.Key);
      }

      int IEqualityComparer<KeyValuePair<TKey, TValue>>.GetHashCode(KeyValuePair<TKey, TValue> obj)
      {
        return this.keyComparer.GetHashCode(obj.Key);
      }

      internal static ImmutableDictionary<TKey, TValue>.Comparers Get(
        IEqualityComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer)
      {
        Requires.NotNull<IEqualityComparer<TKey>>(keyComparer, nameof (keyComparer));
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        return keyComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.KeyComparer || valueComparer != ImmutableDictionary<TKey, TValue>.Comparers.Default.ValueComparer ? new ImmutableDictionary<TKey, TValue>.Comparers(keyComparer, valueComparer) : ImmutableDictionary<TKey, TValue>.Comparers.Default;
      }

      internal ImmutableDictionary<TKey, TValue>.Comparers WithValueComparer(
        IEqualityComparer<TValue> valueComparer)
      {
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        return this.valueComparer != valueComparer ? ImmutableDictionary<TKey, TValue>.Comparers.Get(this.KeyComparer, valueComparer) : this;
      }
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof (ImmutableDictionary<,>.Builder.DebuggerProxy))]
    public sealed class Builder : 
      IDictionary<TKey, TValue>,
      ICollection<KeyValuePair<TKey, TValue>>,
      IReadOnlyDictionary<TKey, TValue>,
      IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
      IEnumerable<KeyValuePair<TKey, TValue>>,
      IDictionary,
      ICollection,
      IEnumerable
    {
      private ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root = ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node.EmptyNode;
      private ImmutableDictionary<TKey, TValue>.Comparers comparers;
      private int count;
      private ImmutableDictionary<TKey, TValue> immutable;
      private int version;
      private object syncRoot;

      internal Builder(ImmutableDictionary<TKey, TValue> map)
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, nameof (map));
        this.root = map.root;
        this.count = map.count;
        this.comparers = map.comparers;
        this.immutable = map;
      }

      public IEqualityComparer<TKey> KeyComparer
      {
        get => this.comparers.KeyComparer;
        set
        {
          Requires.NotNull<IEqualityComparer<TKey>>(value, nameof (value));
          if (value == this.KeyComparer)
            return;
          ImmutableDictionary<TKey, TValue>.Comparers comparers = ImmutableDictionary<TKey, TValue>.Comparers.Get(value, this.ValueComparer);
          ImmutableDictionary<TKey, TValue>.MutationResult mutationResult = ImmutableDictionary<TKey, TValue>.AddRange((IEnumerable<KeyValuePair<TKey, TValue>>) this, new ImmutableDictionary<TKey, TValue>.MutationInput(ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node.EmptyNode, comparers, 0));
          this.immutable = (ImmutableDictionary<TKey, TValue>) null;
          this.comparers = comparers;
          this.count = mutationResult.CountAdjustment;
          this.Root = mutationResult.Root;
        }
      }

      public IEqualityComparer<TValue> ValueComparer
      {
        get => this.comparers.ValueComparer;
        set
        {
          Requires.NotNull<IEqualityComparer<TValue>>(value, nameof (value));
          if (value == this.ValueComparer)
            return;
          this.comparers = this.comparers.WithValueComparer(value);
          this.immutable = (ImmutableDictionary<TKey, TValue>) null;
        }
      }

      public int Count => this.count;

      bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

      public IEnumerable<TKey> Keys
      {
        get
        {
          return this.root.Values.SelectMany<ImmutableDictionary<TKey, TValue>.HashBucket, KeyValuePair<TKey, TValue>>((Func<ImmutableDictionary<TKey, TValue>.HashBucket, IEnumerable<KeyValuePair<TKey, TValue>>>) (b => (IEnumerable<KeyValuePair<TKey, TValue>>) b)).Select<KeyValuePair<TKey, TValue>, TKey>((Func<KeyValuePair<TKey, TValue>, TKey>) (kv => kv.Key));
        }
      }

      ICollection<TKey> IDictionary<TKey, TValue>.Keys
      {
        get => (ICollection<TKey>) this.Keys.ToArray<TKey>(this.Count);
      }

      public IEnumerable<TValue> Values
      {
        get
        {
          return (IEnumerable<TValue>) this.root.Values.SelectMany<ImmutableDictionary<TKey, TValue>.HashBucket, KeyValuePair<TKey, TValue>>((Func<ImmutableDictionary<TKey, TValue>.HashBucket, IEnumerable<KeyValuePair<TKey, TValue>>>) (b => (IEnumerable<KeyValuePair<TKey, TValue>>) b)).Select<KeyValuePair<TKey, TValue>, TValue>((Func<KeyValuePair<TKey, TValue>, TValue>) (kv => kv.Value)).ToArray<TValue>(this.Count);
        }
      }

      ICollection<TValue> IDictionary<TKey, TValue>.Values
      {
        get => (ICollection<TValue>) this.Values.ToArray<TValue>(this.Count);
      }

      bool IDictionary.IsFixedSize => false;

      bool IDictionary.IsReadOnly => false;

      ICollection IDictionary.Keys => (ICollection) this.Keys.ToArray<TKey>(this.Count);

      ICollection IDictionary.Values => (ICollection) this.Values.ToArray<TValue>(this.Count);

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      object ICollection.SyncRoot
      {
        get
        {
          if (this.syncRoot == null)
            Interlocked.CompareExchange<object>(ref this.syncRoot, new object(), (object) null);
          return this.syncRoot;
        }
      }

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      bool ICollection.IsSynchronized => false;

      void IDictionary.Add(object key, object value) => this.Add((TKey) key, (TValue) value);

      bool IDictionary.Contains(object key) => this.ContainsKey((TKey) key);

      IDictionaryEnumerator IDictionary.GetEnumerator()
      {
        return (IDictionaryEnumerator) new DictionaryEnumerator<TKey, TValue>((IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator());
      }

      void IDictionary.Remove(object key) => this.Remove((TKey) key);

      object IDictionary.this[object key]
      {
        get => (object) this[(TKey) key];
        set => this[(TKey) key] = (TValue) value;
      }

      void ICollection.CopyTo(Array array, int arrayIndex)
      {
        Requires.NotNull<Array>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
          array.SetValue((object) new DictionaryEntry((object) keyValuePair.Key, (object) keyValuePair.Value), arrayIndex++);
      }

      internal int Version => this.version;

      private ImmutableDictionary<TKey, TValue>.MutationInput Origin
      {
        get
        {
          return new ImmutableDictionary<TKey, TValue>.MutationInput(this.Root, this.comparers, this.count);
        }
      }

      private ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node Root
      {
        get => this.root;
        set
        {
          ++this.version;
          if (this.root == value)
            return;
          this.root = value;
          this.immutable = (ImmutableDictionary<TKey, TValue>) null;
        }
      }

      public TValue this[TKey key]
      {
        get
        {
          TValue obj;
          if (this.TryGetValue(key, out obj))
            return obj;
          throw new KeyNotFoundException();
        }
        set
        {
          this.Apply(ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue, this.Origin));
        }
      }

      public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
      {
        this.Apply(ImmutableDictionary<TKey, TValue>.AddRange(items, this.Origin));
      }

      public void RemoveRange(IEnumerable<TKey> keys)
      {
        Requires.NotNull<IEnumerable<TKey>>(keys, nameof (keys));
        foreach (TKey key in keys)
          this.Remove(key);
      }

      public ImmutableDictionary<TKey, TValue>.Enumerator GetEnumerator()
      {
        return new ImmutableDictionary<TKey, TValue>.Enumerator(this.root, this);
      }

      public TValue GetValueOrDefault(TKey key) => this.GetValueOrDefault(key, default (TValue));

      public TValue GetValueOrDefault(TKey key, TValue defaultValue)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        TValue obj;
        return this.TryGetValue(key, out obj) ? obj : defaultValue;
      }

      public ImmutableDictionary<TKey, TValue> ToImmutable()
      {
        if (this.immutable == null)
          this.immutable = ImmutableDictionary<TKey, TValue>.Wrap(this.root, this.comparers, this.count);
        return this.immutable;
      }

      public void Add(TKey key, TValue value)
      {
        this.Apply(ImmutableDictionary<TKey, TValue>.Add(key, value, ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent, this.Origin));
      }

      public bool ContainsKey(TKey key)
      {
        return ImmutableDictionary<TKey, TValue>.ContainsKey(key, this.Origin);
      }

      public bool ContainsValue(TValue value)
      {
        return this.Values.Contains<TValue>(value, this.ValueComparer);
      }

      public bool Remove(TKey key)
      {
        return this.Apply(ImmutableDictionary<TKey, TValue>.Remove(key, this.Origin));
      }

      public bool TryGetValue(TKey key, out TValue value)
      {
        return ImmutableDictionary<TKey, TValue>.TryGetValue(key, this.Origin, out value);
      }

      public bool TryGetKey(TKey equalKey, out TKey actualKey)
      {
        return ImmutableDictionary<TKey, TValue>.TryGetKey(equalKey, this.Origin, out actualKey);
      }

      public void Add(KeyValuePair<TKey, TValue> item) => this.Add(item.Key, item.Value);

      public void Clear()
      {
        this.Root = ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node.EmptyNode;
        this.count = 0;
      }

      public bool Contains(KeyValuePair<TKey, TValue> item)
      {
        return ImmutableDictionary<TKey, TValue>.Contains(item, this.Origin);
      }

      void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
        KeyValuePair<TKey, TValue>[] array,
        int arrayIndex)
      {
        Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, nameof (array));
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
          array[arrayIndex++] = keyValuePair;
      }

      public bool Remove(KeyValuePair<TKey, TValue> item)
      {
        return this.Contains(item) && this.Remove(item.Key);
      }

      IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      private bool Apply(
        ImmutableDictionary<TKey, TValue>.MutationResult result)
      {
        this.Root = result.Root;
        this.count += result.CountAdjustment;
        return result.CountAdjustment != 0;
      }

      [ExcludeFromCodeCoverage]
      private class DebuggerProxy
      {
        private readonly ImmutableDictionary<TKey, TValue>.Builder map;
        private KeyValuePair<TKey, TValue>[] contents;

        public DebuggerProxy(ImmutableDictionary<TKey, TValue>.Builder map)
        {
          Requires.NotNull<ImmutableDictionary<TKey, TValue>.Builder>(map, nameof (map));
          this.map = map;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public KeyValuePair<TKey, TValue>[] Contents
        {
          get
          {
            if (this.contents == null)
              this.contents = this.map.ToArray<KeyValuePair<TKey, TValue>>(this.map.Count);
            return this.contents;
          }
        }
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableDictionary<TKey, TValue> map;
      private KeyValuePair<TKey, TValue>[] contents;

      public DebuggerProxy(ImmutableDictionary<TKey, TValue> map)
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(map, nameof (map));
        this.map = map;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public KeyValuePair<TKey, TValue>[] Contents
      {
        get
        {
          if (this.contents == null)
            this.contents = this.map.ToArray<KeyValuePair<TKey, TValue>>(this.map.Count);
          return this.contents;
        }
      }
    }

    public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
    {
      private readonly ImmutableDictionary<TKey, TValue>.Builder builder;
      private ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator mapEnumerator;
      private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator bucketEnumerator;
      private int enumeratingBuilderVersion;

      internal Enumerator(
        ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
        ImmutableDictionary<TKey, TValue>.Builder builder = null)
      {
        this.builder = builder;
        this.mapEnumerator = new ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Enumerator((IBinaryTree<KeyValuePair<int, ImmutableDictionary<TKey, TValue>.HashBucket>>) root);
        this.bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator();
        this.enumeratingBuilderVersion = builder != null ? builder.Version : -1;
      }

      public KeyValuePair<TKey, TValue> Current
      {
        get
        {
          this.mapEnumerator.ThrowIfDisposed();
          return this.bucketEnumerator.Current;
        }
      }

      object IEnumerator.Current => (object) this.Current;

      public bool MoveNext()
      {
        this.ThrowIfChanged();
        if (this.bucketEnumerator.MoveNext())
          return true;
        if (!this.mapEnumerator.MoveNext())
          return false;
        this.bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this.mapEnumerator.Current.Value);
        return this.bucketEnumerator.MoveNext();
      }

      public void Reset()
      {
        this.enumeratingBuilderVersion = this.builder != null ? this.builder.Version : -1;
        this.mapEnumerator.Reset();
        this.bucketEnumerator.Dispose();
        this.bucketEnumerator = new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator();
      }

      public void Dispose()
      {
        this.mapEnumerator.Dispose();
        this.bucketEnumerator.Dispose();
      }

      private void ThrowIfChanged()
      {
        if (this.builder != null && this.builder.Version != this.enumeratingBuilderVersion)
          throw new InvalidOperationException(Strings.CollectionModifiedDuringEnumeration);
      }
    }

    internal struct HashBucket : 
      IEnumerable<KeyValuePair<TKey, TValue>>,
      IEnumerable,
      IEquatable<ImmutableDictionary<TKey, TValue>.HashBucket>
    {
      private readonly KeyValuePair<TKey, TValue> firstValue;
      private readonly ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements;

      private HashBucket(
        KeyValuePair<TKey, TValue> firstElement,
        ImmutableList<KeyValuePair<TKey, TValue>>.Node additionalElements = null)
      {
        this.firstValue = firstElement;
        this.additionalElements = additionalElements ?? ImmutableList<KeyValuePair<TKey, TValue>>.Node.EmptyNode;
      }

      internal bool IsEmpty => this.additionalElements == null;

      internal KeyValuePair<TKey, TValue> FirstValue
      {
        get
        {
          if (this.IsEmpty)
            throw new InvalidOperationException();
          return this.firstValue;
        }
      }

      internal ImmutableList<KeyValuePair<TKey, TValue>>.Node AdditionalElements
      {
        get => this.additionalElements;
      }

      public ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator GetEnumerator()
      {
        return new ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator(this);
      }

      IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      bool IEquatable<ImmutableDictionary<TKey, TValue>.HashBucket>.Equals(
        ImmutableDictionary<TKey, TValue>.HashBucket other)
      {
        throw new Exception();
      }

      internal ImmutableDictionary<TKey, TValue>.HashBucket Add(
        TKey key,
        TValue value,
        IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer,
        IEqualityComparer<TValue> valueComparer,
        ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior behavior,
        out ImmutableDictionary<TKey, TValue>.OperationResult result)
      {
        KeyValuePair<TKey, TValue> keyValuePair = new KeyValuePair<TKey, TValue>(key, value);
        if (this.IsEmpty)
        {
          result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
          return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair);
        }
        if (keyOnlyComparer.Equals(keyValuePair, this.firstValue))
        {
          switch (behavior)
          {
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
              result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
              return new ImmutableDictionary<TKey, TValue>.HashBucket(keyValuePair, this.additionalElements);
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
              result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
              return this;
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
              if (!valueComparer.Equals(this.firstValue.Value, value))
                throw new ArgumentException(Strings.DuplicateKey);
              result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
              return this;
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
              throw new ArgumentException(Strings.DuplicateKey);
            default:
              throw new InvalidOperationException();
          }
        }
        else
        {
          int index = this.additionalElements.IndexOf(keyValuePair, keyOnlyComparer);
          if (index < 0)
          {
            result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
            return new ImmutableDictionary<TKey, TValue>.HashBucket(this.firstValue, this.additionalElements.Add(keyValuePair));
          }
          switch (behavior)
          {
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.SetValue:
              result = ImmutableDictionary<TKey, TValue>.OperationResult.AppliedWithoutSizeChange;
              return new ImmutableDictionary<TKey, TValue>.HashBucket(this.firstValue, this.additionalElements.ReplaceAt(index, keyValuePair));
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.Skip:
              result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
              return this;
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowIfValueDifferent:
              KeyValuePair<TKey, TValue> additionalElement = this.additionalElements[index];
              if (!valueComparer.Equals(additionalElement.Value, value))
                throw new ArgumentException(Strings.DuplicateKey);
              result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
              return this;
            case ImmutableDictionary<TKey, TValue>.KeyCollisionBehavior.ThrowAlways:
              throw new ArgumentException(Strings.DuplicateKey);
            default:
              throw new InvalidOperationException();
          }
        }
      }

      internal ImmutableDictionary<TKey, TValue>.HashBucket Remove(
        TKey key,
        IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer,
        out ImmutableDictionary<TKey, TValue>.OperationResult result)
      {
        if (this.IsEmpty)
        {
          result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
          return this;
        }
        KeyValuePair<TKey, TValue> y = new KeyValuePair<TKey, TValue>(key, default (TValue));
        if (keyOnlyComparer.Equals(this.firstValue, y))
        {
          if (this.additionalElements.IsEmpty)
          {
            result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
            return new ImmutableDictionary<TKey, TValue>.HashBucket();
          }
          int count = ((IBinaryTree<KeyValuePair<TKey, TValue>>) this.additionalElements).Left.Count;
          result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
          return new ImmutableDictionary<TKey, TValue>.HashBucket(this.additionalElements.Key, this.additionalElements.RemoveAt(count));
        }
        int index = this.additionalElements.IndexOf(y, keyOnlyComparer);
        if (index < 0)
        {
          result = ImmutableDictionary<TKey, TValue>.OperationResult.NoChangeRequired;
          return this;
        }
        result = ImmutableDictionary<TKey, TValue>.OperationResult.SizeChanged;
        return new ImmutableDictionary<TKey, TValue>.HashBucket(this.firstValue, this.additionalElements.RemoveAt(index));
      }

      internal bool TryGetValue(
        TKey key,
        IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer,
        out TValue value)
      {
        if (this.IsEmpty)
        {
          value = default (TValue);
          return false;
        }
        KeyValuePair<TKey, TValue> y = new KeyValuePair<TKey, TValue>(key, default (TValue));
        if (keyOnlyComparer.Equals(this.firstValue, y))
        {
          value = this.firstValue.Value;
          return true;
        }
        int index = this.additionalElements.IndexOf(y, keyOnlyComparer);
        if (index < 0)
        {
          value = default (TValue);
          return false;
        }
        value = this.additionalElements[index].Value;
        return true;
      }

      internal bool TryGetKey(
        TKey equalKey,
        IEqualityComparer<KeyValuePair<TKey, TValue>> keyOnlyComparer,
        out TKey actualKey)
      {
        if (this.IsEmpty)
        {
          actualKey = equalKey;
          return false;
        }
        KeyValuePair<TKey, TValue> y = new KeyValuePair<TKey, TValue>(equalKey, default (TValue));
        if (keyOnlyComparer.Equals(this.firstValue, y))
        {
          actualKey = this.firstValue.Key;
          return true;
        }
        int index = this.additionalElements.IndexOf(y, keyOnlyComparer);
        if (index < 0)
        {
          actualKey = equalKey;
          return false;
        }
        actualKey = this.additionalElements[index].Key;
        return true;
      }

      internal void Freeze()
      {
        if (this.additionalElements == null)
          return;
        this.additionalElements.Freeze();
      }

      internal struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator, IDisposable
      {
        private readonly ImmutableDictionary<TKey, TValue>.HashBucket bucket;
        private ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position currentPosition;
        private ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator additionalEnumerator;

        internal Enumerator(
          ImmutableDictionary<TKey, TValue>.HashBucket bucket)
        {
          this.bucket = bucket;
          this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
          this.additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator();
        }

        object IEnumerator.Current => (object) this.Current;

        public KeyValuePair<TKey, TValue> Current
        {
          get
          {
            switch (this.currentPosition)
            {
              case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
                return this.bucket.firstValue;
              case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
                return this.additionalEnumerator.Current;
              default:
                throw new InvalidOperationException();
            }
          }
        }

        public bool MoveNext()
        {
          if (this.bucket.IsEmpty)
          {
            this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
            return false;
          }
          switch (this.currentPosition)
          {
            case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst:
              this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First;
              return true;
            case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.First:
              if (this.bucket.additionalElements.IsEmpty)
              {
                this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End;
                return false;
              }
              this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional;
              this.additionalEnumerator = new ImmutableList<KeyValuePair<TKey, TValue>>.Enumerator((IBinaryTree<KeyValuePair<TKey, TValue>>) this.bucket.additionalElements);
              return this.additionalEnumerator.MoveNext();
            case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.Additional:
              return this.additionalEnumerator.MoveNext();
            case ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.End:
              return false;
            default:
              throw new InvalidOperationException();
          }
        }

        public void Reset()
        {
          this.additionalEnumerator.Dispose();
          this.currentPosition = ImmutableDictionary<TKey, TValue>.HashBucket.Enumerator.Position.BeforeFirst;
        }

        public void Dispose() => this.additionalEnumerator.Dispose();

        private enum Position
        {
          BeforeFirst,
          First,
          Additional,
          End,
        }
      }
    }

    private struct MutationInput
    {
      private readonly ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root;
      private readonly ImmutableDictionary<TKey, TValue>.Comparers comparers;
      private readonly int count;

      internal MutationInput(
        ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
        ImmutableDictionary<TKey, TValue>.Comparers comparers,
        int count)
      {
        this.root = root;
        this.comparers = comparers;
        this.count = count;
      }

      internal MutationInput(ImmutableDictionary<TKey, TValue> map)
      {
        this.root = map.root;
        this.comparers = map.comparers;
        this.count = map.count;
      }

      internal ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node Root
      {
        get => this.root;
      }

      internal IEqualityComparer<TKey> KeyComparer => this.comparers.KeyComparer;

      internal IEqualityComparer<KeyValuePair<TKey, TValue>> KeyOnlyComparer
      {
        get => this.comparers.KeyOnlyComparer;
      }

      internal IEqualityComparer<TValue> ValueComparer => this.comparers.ValueComparer;

      internal IEqualityComparer<ImmutableDictionary<TKey, TValue>.HashBucket> HashBucketComparer
      {
        get => this.comparers.HashBucketEqualityComparer;
      }

      internal int Count => this.count;
    }

    private struct MutationResult
    {
      private readonly ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root;
      private readonly int countAdjustment;

      internal MutationResult(
        ImmutableDictionary<TKey, TValue>.MutationInput unchangedInput)
      {
        this.root = unchangedInput.Root;
        this.countAdjustment = 0;
      }

      internal MutationResult(
        ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node root,
        int countAdjustment)
      {
        Requires.NotNull<ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node>(root, nameof (root));
        this.root = root;
        this.countAdjustment = countAdjustment;
      }

      internal ImmutableSortedDictionary<int, ImmutableDictionary<TKey, TValue>.HashBucket>.Node Root
      {
        get => this.root;
      }

      internal int CountAdjustment => this.countAdjustment;

      internal ImmutableDictionary<TKey, TValue> Finalize(ImmutableDictionary<TKey, TValue> priorMap)
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(priorMap, nameof (priorMap));
        return priorMap.Wrap(this.Root, priorMap.count + this.CountAdjustment);
      }
    }

    internal enum KeyCollisionBehavior
    {
      SetValue,
      Skip,
      ThrowIfValueDifferent,
      ThrowAlways,
    }

    internal enum OperationResult
    {
      AppliedWithoutSizeChange,
      SizeChanged,
      NoChangeRequired,
    }
  }
}
