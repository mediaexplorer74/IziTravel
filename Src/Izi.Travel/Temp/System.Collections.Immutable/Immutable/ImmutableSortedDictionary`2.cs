// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableSortedDictionary`2
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerTypeProxy(typeof (ImmutableSortedDictionary<,>.DebuggerProxy))]
  [DebuggerDisplay("Count = {Count}")]
  public sealed class ImmutableSortedDictionary<TKey, TValue> : 
    IImmutableDictionary<TKey, TValue>,
    IReadOnlyDictionary<TKey, TValue>,
    IReadOnlyCollection<KeyValuePair<TKey, TValue>>,
    ISortKeyCollection<TKey>,
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IDictionary,
    ICollection,
    IEnumerable
  {
    public static readonly ImmutableSortedDictionary<TKey, TValue> Empty = new ImmutableSortedDictionary<TKey, TValue>();
    private readonly ImmutableSortedDictionary<TKey, TValue>.Node root;
    private readonly int count;
    private readonly IComparer<TKey> keyComparer;
    private readonly IEqualityComparer<TValue> valueComparer;

    internal ImmutableSortedDictionary(
      IComparer<TKey> keyComparer = null,
      IEqualityComparer<TValue> valueComparer = null)
    {
      this.keyComparer = keyComparer ?? (IComparer<TKey>) Comparer<TKey>.Default;
      this.valueComparer = valueComparer ?? (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
      this.root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
    }

    private ImmutableSortedDictionary(
      ImmutableSortedDictionary<TKey, TValue>.Node root,
      int count,
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(root, nameof (root));
      Requires.Range(count >= 0, nameof (count));
      Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
      Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
      root.Freeze();
      this.root = root;
      this.count = count;
      this.keyComparer = keyComparer;
      this.valueComparer = valueComparer;
    }

    public ImmutableSortedDictionary<TKey, TValue> Clear()
    {
      return !this.root.IsEmpty ? ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(this.keyComparer, this.valueComparer) : this;
    }

    public IEqualityComparer<TValue> ValueComparer => this.valueComparer;

    public bool IsEmpty => this.root.IsEmpty;

    public int Count => this.count;

    public IEnumerable<TKey> Keys => this.root.Keys;

    public IEnumerable<TValue> Values => this.root.Values;

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

    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;

    public IComparer<TKey> KeyComparer => this.keyComparer;

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

    public ImmutableSortedDictionary<TKey, TValue>.Builder ToBuilder()
    {
      return new ImmutableSortedDictionary<TKey, TValue>.Builder(this);
    }

    public ImmutableSortedDictionary<TKey, TValue> Add(TKey key, TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return this.Wrap(this.root.Add(key, value, this.keyComparer, this.valueComparer, out bool _), this.count + 1);
    }

    public ImmutableSortedDictionary<TKey, TValue> SetItem(TKey key, TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      bool replacedExistingValue;
      return this.Wrap(this.root.SetItem(key, value, this.keyComparer, this.valueComparer, out replacedExistingValue, out bool _), replacedExistingValue ? this.count : this.count + 1);
    }

    public ImmutableSortedDictionary<TKey, TValue> SetItems(
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      return this.AddRange(items, true, false);
    }

    public ImmutableSortedDictionary<TKey, TValue> AddRange(
      IEnumerable<KeyValuePair<TKey, TValue>> items)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      return this.AddRange(items, false, false);
    }

    public ImmutableSortedDictionary<TKey, TValue> Remove(TKey value)
    {
      Requires.NotNullAllowStructs<TKey>(value, nameof (value));
      return this.Wrap(this.root.Remove(value, this.keyComparer, out bool _), this.count - 1);
    }

    public ImmutableSortedDictionary<TKey, TValue> RemoveRange(IEnumerable<TKey> keys)
    {
      Requires.NotNull<IEnumerable<TKey>>(keys, nameof (keys));
      ImmutableSortedDictionary<TKey, TValue>.Node root = this.root;
      int count = this.count;
      foreach (TKey key in keys)
      {
        bool mutated;
        ImmutableSortedDictionary<TKey, TValue>.Node node = root.Remove(key, this.keyComparer, out mutated);
        if (mutated)
        {
          root = node;
          --count;
        }
      }
      return this.Wrap(root, count);
    }

    public ImmutableSortedDictionary<TKey, TValue> WithComparers(
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      if (keyComparer == null)
        keyComparer = (IComparer<TKey>) Comparer<TKey>.Default;
      if (valueComparer == null)
        valueComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
      if (keyComparer != this.keyComparer)
        return new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode, 0, keyComparer, valueComparer).AddRange((IEnumerable<KeyValuePair<TKey, TValue>>) this, false, true);
      return valueComparer == this.valueComparer ? this : new ImmutableSortedDictionary<TKey, TValue>(this.root, this.count, this.keyComparer, valueComparer);
    }

    public ImmutableSortedDictionary<TKey, TValue> WithComparers(IComparer<TKey> keyComparer)
    {
      return this.WithComparers(keyComparer, this.valueComparer);
    }

    public bool ContainsValue(TValue value) => this.root.ContainsValue(value, this.valueComparer);

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

    public bool ContainsKey(TKey key)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return this.root.ContainsKey(key, this.keyComparer);
    }

    public bool Contains(KeyValuePair<TKey, TValue> pair)
    {
      return this.root.Contains(pair, this.keyComparer, this.valueComparer);
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
      Requires.NotNullAllowStructs<TKey>(key, nameof (key));
      return this.root.TryGetValue(key, this.keyComparer, out value);
    }

    public bool TryGetKey(TKey equalKey, out TKey actualKey)
    {
      Requires.NotNullAllowStructs<TKey>(equalKey, nameof (equalKey));
      return this.root.TryGetKey(equalKey, this.keyComparer, out actualKey);
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

    void ICollection.CopyTo(Array array, int index) => this.root.CopyTo(array, index, this.Count);

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    [ExcludeFromCodeCoverage]
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
    }

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
    {
      return this.root.GetEnumerator();
    }

    private static ImmutableSortedDictionary<TKey, TValue> Wrap(
      ImmutableSortedDictionary<TKey, TValue>.Node root,
      int count,
      IComparer<TKey> keyComparer,
      IEqualityComparer<TValue> valueComparer)
    {
      return !root.IsEmpty ? new ImmutableSortedDictionary<TKey, TValue>(root, count, keyComparer, valueComparer) : ImmutableSortedDictionary<TKey, TValue>.Empty.WithComparers(keyComparer, valueComparer);
    }

    private static bool TryCastToImmutableMap(
      IEnumerable<KeyValuePair<TKey, TValue>> sequence,
      out ImmutableSortedDictionary<TKey, TValue> other)
    {
      other = sequence as ImmutableSortedDictionary<TKey, TValue>;
      if (other != null)
        return true;
      if (!(sequence is ImmutableSortedDictionary<TKey, TValue>.Builder builder))
        return false;
      other = builder.ToImmutable();
      return true;
    }

    private ImmutableSortedDictionary<TKey, TValue> AddRange(
      IEnumerable<KeyValuePair<TKey, TValue>> items,
      bool overwriteOnCollision,
      bool avoidToSortedMap)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      if (this.IsEmpty && !avoidToSortedMap)
        return this.FillFromEmpty(items, overwriteOnCollision);
      ImmutableSortedDictionary<TKey, TValue>.Node root = this.root;
      int count = this.count;
      foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
      {
        bool replacedExistingValue = false;
        bool mutated;
        ImmutableSortedDictionary<TKey, TValue>.Node node = overwriteOnCollision ? root.SetItem(keyValuePair.Key, keyValuePair.Value, this.keyComparer, this.valueComparer, out replacedExistingValue, out mutated) : root.Add(keyValuePair.Key, keyValuePair.Value, this.keyComparer, this.valueComparer, out mutated);
        if (mutated)
        {
          root = node;
          if (!replacedExistingValue)
            ++count;
        }
      }
      return this.Wrap(root, count);
    }

    private ImmutableSortedDictionary<TKey, TValue> Wrap(
      ImmutableSortedDictionary<TKey, TValue>.Node root,
      int adjustedCountIfDifferentRoot)
    {
      if (this.root == root)
        return this;
      return !root.IsEmpty ? new ImmutableSortedDictionary<TKey, TValue>(root, adjustedCountIfDifferentRoot, this.keyComparer, this.valueComparer) : this.Clear();
    }

    private ImmutableSortedDictionary<TKey, TValue> FillFromEmpty(
      IEnumerable<KeyValuePair<TKey, TValue>> items,
      bool overwriteOnCollision)
    {
      Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
      ImmutableSortedDictionary<TKey, TValue> other;
      if (ImmutableSortedDictionary<TKey, TValue>.TryCastToImmutableMap(items, out other))
        return other.WithComparers(this.KeyComparer, this.ValueComparer);
      SortedDictionary<TKey, TValue> dictionary1;
      if (items is IDictionary<TKey, TValue> dictionary2)
      {
        dictionary1 = new SortedDictionary<TKey, TValue>(dictionary2, this.KeyComparer);
      }
      else
      {
        dictionary1 = new SortedDictionary<TKey, TValue>(this.KeyComparer);
        foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
        {
          if (overwriteOnCollision)
          {
            dictionary1[keyValuePair.Key] = keyValuePair.Value;
          }
          else
          {
            TValue x;
            if (dictionary1.TryGetValue(keyValuePair.Key, out x))
            {
              if (!this.valueComparer.Equals(x, keyValuePair.Value))
                throw new ArgumentException(Strings.DuplicateKey);
            }
            else
              dictionary1.Add(keyValuePair.Key, keyValuePair.Value);
          }
        }
      }
      return dictionary1.Count == 0 ? this : new ImmutableSortedDictionary<TKey, TValue>(ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromSortedDictionary(dictionary1), dictionary1.Count, this.KeyComparer, this.ValueComparer);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct Enumerator : 
      IEnumerator<KeyValuePair<TKey, TValue>>,
      IEnumerator,
      IDisposable,
      ISecurePooledObjectUser
    {
      private static readonly SecureObjectPool<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator> enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>, ImmutableSortedDictionary<TKey, TValue>.Enumerator>();
      private readonly ImmutableSortedDictionary<TKey, TValue>.Builder builder;
      private readonly Guid poolUserId;
      private IBinaryTree<KeyValuePair<TKey, TValue>> root;
      private SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>> stack;
      private IBinaryTree<KeyValuePair<TKey, TValue>> current;
      private int enumeratingBuilderVersion;

      internal Enumerator(
        IBinaryTree<KeyValuePair<TKey, TValue>> root,
        ImmutableSortedDictionary<TKey, TValue>.Builder builder = null)
      {
        Requires.NotNull<IBinaryTree<KeyValuePair<TKey, TValue>>>(root, nameof (root));
        this.root = root;
        this.builder = builder;
        this.current = (IBinaryTree<KeyValuePair<TKey, TValue>>) null;
        this.enumeratingBuilderVersion = builder != null ? builder.Version : -1;
        this.poolUserId = Guid.NewGuid();
        this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>) null;
        if (!ImmutableSortedDictionary<TKey, TValue>.Enumerator.enumeratingStacks.TryTake(this, out this.stack))
          this.stack = ImmutableSortedDictionary<TKey, TValue>.Enumerator.enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>(root.Height));
        this.Reset();
      }

      public KeyValuePair<TKey, TValue> Current
      {
        get
        {
          this.ThrowIfDisposed();
          if (this.current != null)
            return this.current.Value;
          throw new InvalidOperationException();
        }
      }

      Guid ISecurePooledObjectUser.PoolUserId => this.poolUserId;

      object IEnumerator.Current => (object) this.Current;

      public void Dispose()
      {
        this.root = (IBinaryTree<KeyValuePair<TKey, TValue>>) null;
        this.current = (IBinaryTree<KeyValuePair<TKey, TValue>>) null;
        if (this.stack != null && this.stack.Owner == this.poolUserId)
        {
          using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this))
            pooledObjectUser.Value.Clear();
          ImmutableSortedDictionary<TKey, TValue>.Enumerator.enumeratingStacks.TryAdd(this, this.stack);
        }
        this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>) null;
      }

      public bool MoveNext()
      {
        this.ThrowIfDisposed();
        this.ThrowIfChanged();
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this))
        {
          if (pooledObjectUser.Value.Count > 0)
          {
            IBinaryTree<KeyValuePair<TKey, TValue>> binaryTree = pooledObjectUser.Value.Pop().Value;
            this.current = binaryTree;
            this.PushLeft(binaryTree.Right);
            return true;
          }
          this.current = (IBinaryTree<KeyValuePair<TKey, TValue>>) null;
          return false;
        }
      }

      public void Reset()
      {
        this.ThrowIfDisposed();
        this.enumeratingBuilderVersion = this.builder != null ? this.builder.Version : -1;
        this.current = (IBinaryTree<KeyValuePair<TKey, TValue>>) null;
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this))
          pooledObjectUser.Value.Clear();
        this.PushLeft(this.root);
      }

      internal void ThrowIfDisposed()
      {
        if (this.root == null)
          throw new ObjectDisposedException(this.GetType().FullName);
        if (this.stack == null)
          return;
        this.stack.ThrowDisposedIfNotOwned<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this);
      }

      private void ThrowIfChanged()
      {
        if (this.builder != null && this.builder.Version != this.enumeratingBuilderVersion)
          throw new InvalidOperationException(Strings.CollectionModifiedDuringEnumeration);
      }

      private void PushLeft(IBinaryTree<KeyValuePair<TKey, TValue>> node)
      {
        Requires.NotNull<IBinaryTree<KeyValuePair<TKey, TValue>>>(node, nameof (node));
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedDictionary<TKey, TValue>.Enumerator>(this))
        {
          for (; !node.IsEmpty; node = node.Left)
            pooledObjectUser.Value.Push(new RefAsValueType<IBinaryTree<KeyValuePair<TKey, TValue>>>(node));
        }
      }
    }

    [DebuggerDisplay("{key} = {value}")]
    internal sealed class Node : 
      IBinaryTree<KeyValuePair<TKey, TValue>>,
      IEnumerable<KeyValuePair<TKey, TValue>>,
      IEnumerable
    {
      internal static readonly ImmutableSortedDictionary<TKey, TValue>.Node EmptyNode = new ImmutableSortedDictionary<TKey, TValue>.Node();
      private readonly TKey key;
      private TValue value;
      private bool frozen;
      private int height;
      private ImmutableSortedDictionary<TKey, TValue>.Node left;
      private ImmutableSortedDictionary<TKey, TValue>.Node right;

      private Node() => this.frozen = true;

      private Node(
        TKey key,
        TValue value,
        ImmutableSortedDictionary<TKey, TValue>.Node left,
        ImmutableSortedDictionary<TKey, TValue>.Node right,
        bool frozen = false)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(left, nameof (left));
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(right, nameof (right));
        this.key = key;
        this.value = value;
        this.left = left;
        this.right = right;
        this.height = 1 + Math.Max(left.height, right.height);
        this.frozen = frozen;
      }

      public bool IsEmpty => this.left == null;

      IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Left
      {
        get => (IBinaryTree<KeyValuePair<TKey, TValue>>) this.left;
      }

      IBinaryTree<KeyValuePair<TKey, TValue>> IBinaryTree<KeyValuePair<TKey, TValue>>.Right
      {
        get => (IBinaryTree<KeyValuePair<TKey, TValue>>) this.right;
      }

      int IBinaryTree<KeyValuePair<TKey, TValue>>.Height => this.height;

      KeyValuePair<TKey, TValue> IBinaryTree<KeyValuePair<TKey, TValue>>.Value
      {
        get => new KeyValuePair<TKey, TValue>(this.key, this.value);
      }

      int IBinaryTree<KeyValuePair<TKey, TValue>>.Count => throw new NotSupportedException();

      internal IEnumerable<TKey> Keys
      {
        get
        {
          return this.Select<KeyValuePair<TKey, TValue>, TKey>((Func<KeyValuePair<TKey, TValue>, TKey>) (p => p.Key));
        }
      }

      internal IEnumerable<TValue> Values
      {
        get
        {
          return this.Select<KeyValuePair<TKey, TValue>, TValue>((Func<KeyValuePair<TKey, TValue>, TValue>) (p => p.Value));
        }
      }

      public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
      {
        return new ImmutableSortedDictionary<TKey, TValue>.Enumerator((IBinaryTree<KeyValuePair<TKey, TValue>>) this);
      }

      IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      internal ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator(
        ImmutableSortedDictionary<TKey, TValue>.Builder builder)
      {
        return new ImmutableSortedDictionary<TKey, TValue>.Enumerator((IBinaryTree<KeyValuePair<TKey, TValue>>) this, builder);
      }

      internal void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex, int dictionarySize)
      {
        Requires.NotNull<KeyValuePair<TKey, TValue>[]>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + dictionarySize, nameof (arrayIndex));
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
          array[arrayIndex++] = keyValuePair;
      }

      internal void CopyTo(Array array, int arrayIndex, int dictionarySize)
      {
        Requires.NotNull<Array>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + dictionarySize, nameof (arrayIndex));
        foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
          array.SetValue((object) new DictionaryEntry((object) keyValuePair.Key, (object) keyValuePair.Value), arrayIndex++);
      }

      internal static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromSortedDictionary(
        SortedDictionary<TKey, TValue> dictionary)
      {
        Requires.NotNull<SortedDictionary<TKey, TValue>>(dictionary, nameof (dictionary));
        IOrderedCollection<KeyValuePair<TKey, TValue>> items = dictionary.AsOrderedCollection<KeyValuePair<TKey, TValue>>();
        return ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, 0, items.Count);
      }

      internal ImmutableSortedDictionary<TKey, TValue>.Node Add(
        TKey key,
        TValue value,
        IComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer,
        out bool mutated)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        return this.SetOrAdd(key, value, keyComparer, valueComparer, false, out bool _, out mutated);
      }

      internal ImmutableSortedDictionary<TKey, TValue>.Node SetItem(
        TKey key,
        TValue value,
        IComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer,
        out bool replacedExistingValue,
        out bool mutated)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        return this.SetOrAdd(key, value, keyComparer, valueComparer, true, out replacedExistingValue, out mutated);
      }

      internal ImmutableSortedDictionary<TKey, TValue>.Node Remove(
        TKey key,
        IComparer<TKey> keyComparer,
        out bool mutated)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        return this.RemoveRecursive(key, keyComparer, out mutated);
      }

      internal TValue GetValueOrDefault(TKey key, IComparer<TKey> keyComparer)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
        return !node.IsEmpty ? node.value : default (TValue);
      }

      internal bool TryGetValue(TKey key, IComparer<TKey> keyComparer, out TValue value)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(key, keyComparer);
        if (node.IsEmpty)
        {
          value = default (TValue);
          return false;
        }
        value = node.value;
        return true;
      }

      internal bool TryGetKey(TKey equalKey, IComparer<TKey> keyComparer, out TKey actualKey)
      {
        Requires.NotNullAllowStructs<TKey>(equalKey, nameof (equalKey));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(equalKey, keyComparer);
        if (node.IsEmpty)
        {
          actualKey = equalKey;
          return false;
        }
        actualKey = node.key;
        return true;
      }

      internal bool ContainsKey(TKey key, IComparer<TKey> keyComparer)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        return !this.Search(key, keyComparer).IsEmpty;
      }

      internal bool ContainsValue(TValue value, IEqualityComparer<TValue> valueComparer)
      {
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        return this.Values.Contains<TValue>(value, valueComparer);
      }

      internal bool Contains(
        KeyValuePair<TKey, TValue> pair,
        IComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer)
      {
        Requires.NotNullAllowStructs<bool>((object) pair.Key != null, "key");
        Requires.NotNull<IComparer<TKey>>(keyComparer, nameof (keyComparer));
        Requires.NotNull<IEqualityComparer<TValue>>(valueComparer, nameof (valueComparer));
        ImmutableSortedDictionary<TKey, TValue>.Node node = this.Search(pair.Key, keyComparer);
        return !node.IsEmpty && valueComparer.Equals(node.value, pair.Value);
      }

      internal void Freeze(Action<KeyValuePair<TKey, TValue>> freezeAction = null)
      {
        if (this.frozen)
          return;
        if (freezeAction != null)
          freezeAction(new KeyValuePair<TKey, TValue>(this.key, this.value));
        this.left.Freeze(freezeAction);
        this.right.Freeze(freezeAction);
        this.frozen = true;
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node RotateLeft(
        ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        if (tree.right.IsEmpty)
          return tree;
        ImmutableSortedDictionary<TKey, TValue>.Node right = tree.right;
        return right.Mutate(tree.Mutate(right: right.left));
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node RotateRight(
        ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        if (tree.left.IsEmpty)
          return tree;
        ImmutableSortedDictionary<TKey, TValue>.Node left = tree.left;
        return left.Mutate(right: tree.Mutate(left.right));
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleLeft(
        ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        return tree.right.IsEmpty ? tree : ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree.Mutate(right: ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree.right)));
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node DoubleRight(
        ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        return tree.left.IsEmpty ? tree : ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree.Mutate(ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree.left)));
      }

      private static int Balance(ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        return tree.right.height - tree.left.height;
      }

      private static bool IsRightHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) >= 2;
      }

      private static bool IsLeftHeavy(ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        return ImmutableSortedDictionary<TKey, TValue>.Node.Balance(tree) <= -2;
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node MakeBalanced(
        ImmutableSortedDictionary<TKey, TValue>.Node tree)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Node>(tree, nameof (tree));
        if (ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree))
          return !ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree.right) ? ImmutableSortedDictionary<TKey, TValue>.Node.RotateLeft(tree) : ImmutableSortedDictionary<TKey, TValue>.Node.DoubleLeft(tree);
        if (!ImmutableSortedDictionary<TKey, TValue>.Node.IsLeftHeavy(tree))
          return tree;
        return !ImmutableSortedDictionary<TKey, TValue>.Node.IsRightHeavy(tree.left) ? ImmutableSortedDictionary<TKey, TValue>.Node.RotateRight(tree) : ImmutableSortedDictionary<TKey, TValue>.Node.DoubleRight(tree);
      }

      private static ImmutableSortedDictionary<TKey, TValue>.Node NodeTreeFromList(
        IOrderedCollection<KeyValuePair<TKey, TValue>> items,
        int start,
        int length)
      {
        Requires.NotNull<IOrderedCollection<KeyValuePair<TKey, TValue>>>(items, nameof (items));
        Requires.Range(start >= 0, nameof (start));
        Requires.Range(length >= 0, nameof (length));
        if (length == 0)
          return ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
        int length1 = (length - 1) / 2;
        int length2 = length - 1 - length1;
        ImmutableSortedDictionary<TKey, TValue>.Node left = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start, length2);
        ImmutableSortedDictionary<TKey, TValue>.Node right = ImmutableSortedDictionary<TKey, TValue>.Node.NodeTreeFromList(items, start + length2 + 1, length1);
        KeyValuePair<TKey, TValue> keyValuePair = items[start + length2];
        return new ImmutableSortedDictionary<TKey, TValue>.Node(keyValuePair.Key, keyValuePair.Value, left, right, true);
      }

      private ImmutableSortedDictionary<TKey, TValue>.Node SetOrAdd(
        TKey key,
        TValue value,
        IComparer<TKey> keyComparer,
        IEqualityComparer<TValue> valueComparer,
        bool overwriteExistingValue,
        out bool replacedExistingValue,
        out bool mutated)
      {
        replacedExistingValue = false;
        if (this.IsEmpty)
        {
          mutated = true;
          return new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this, this);
        }
        ImmutableSortedDictionary<TKey, TValue>.Node tree = this;
        int num = keyComparer.Compare(key, this.key);
        if (num > 0)
        {
          ImmutableSortedDictionary<TKey, TValue>.Node right = this.right.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
          if (mutated)
            tree = this.Mutate(right: right);
        }
        else if (num < 0)
        {
          ImmutableSortedDictionary<TKey, TValue>.Node left = this.left.SetOrAdd(key, value, keyComparer, valueComparer, overwriteExistingValue, out replacedExistingValue, out mutated);
          if (mutated)
            tree = this.Mutate(left);
        }
        else
        {
          if (valueComparer.Equals(this.value, value))
          {
            mutated = false;
            return this;
          }
          if (!overwriteExistingValue)
            throw new ArgumentException(Strings.DuplicateKey);
          mutated = true;
          replacedExistingValue = true;
          tree = new ImmutableSortedDictionary<TKey, TValue>.Node(key, value, this.left, this.right);
        }
        return !mutated ? tree : ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(tree);
      }

      private ImmutableSortedDictionary<TKey, TValue>.Node RemoveRecursive(
        TKey key,
        IComparer<TKey> keyComparer,
        out bool mutated)
      {
        if (this.IsEmpty)
        {
          mutated = false;
          return this;
        }
        ImmutableSortedDictionary<TKey, TValue>.Node tree = this;
        int num = keyComparer.Compare(key, this.key);
        if (num == 0)
        {
          mutated = true;
          if (this.right.IsEmpty && this.left.IsEmpty)
            tree = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
          else if (this.right.IsEmpty && !this.left.IsEmpty)
            tree = this.left;
          else if (!this.right.IsEmpty && this.left.IsEmpty)
          {
            tree = this.right;
          }
          else
          {
            ImmutableSortedDictionary<TKey, TValue>.Node node = this.right;
            while (!node.left.IsEmpty)
              node = node.left;
            ImmutableSortedDictionary<TKey, TValue>.Node right = this.right.Remove(node.key, keyComparer, out bool _);
            tree = node.Mutate(this.left, right);
          }
        }
        else if (num < 0)
        {
          ImmutableSortedDictionary<TKey, TValue>.Node left = this.left.Remove(key, keyComparer, out mutated);
          if (mutated)
            tree = this.Mutate(left);
        }
        else
        {
          ImmutableSortedDictionary<TKey, TValue>.Node right = this.right.Remove(key, keyComparer, out mutated);
          if (mutated)
            tree = this.Mutate(right: right);
        }
        return !tree.IsEmpty ? ImmutableSortedDictionary<TKey, TValue>.Node.MakeBalanced(tree) : tree;
      }

      private ImmutableSortedDictionary<TKey, TValue>.Node Mutate(
        ImmutableSortedDictionary<TKey, TValue>.Node left = null,
        ImmutableSortedDictionary<TKey, TValue>.Node right = null)
      {
        if (this.frozen)
          return new ImmutableSortedDictionary<TKey, TValue>.Node(this.key, this.value, left ?? this.left, right ?? this.right);
        if (left != null)
          this.left = left;
        if (right != null)
          this.right = right;
        this.height = 1 + Math.Max(this.left.height, this.right.height);
        return this;
      }

      private ImmutableSortedDictionary<TKey, TValue>.Node Search(
        TKey key,
        IComparer<TKey> keyComparer)
      {
        if (this.left == null)
          return this;
        int num = keyComparer.Compare(key, this.key);
        if (num == 0)
          return this;
        return num > 0 ? this.right.Search(key, keyComparer) : this.left.Search(key, keyComparer);
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableSortedDictionary<TKey, TValue> map;
      private KeyValuePair<TKey, TValue>[] contents;

      public DebuggerProxy(ImmutableSortedDictionary<TKey, TValue> map)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, nameof (map));
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

    [DebuggerTypeProxy(typeof (ImmutableSortedDictionary<,>.Builder.DebuggerProxy))]
    [DebuggerDisplay("Count = {Count}")]
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
      private ImmutableSortedDictionary<TKey, TValue>.Node root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
      private IComparer<TKey> keyComparer = (IComparer<TKey>) Comparer<TKey>.Default;
      private IEqualityComparer<TValue> valueComparer = (IEqualityComparer<TValue>) EqualityComparer<TValue>.Default;
      private int count;
      private ImmutableSortedDictionary<TKey, TValue> immutable;
      private int version;
      private object syncRoot;

      internal Builder(ImmutableSortedDictionary<TKey, TValue> map)
      {
        Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>>(map, nameof (map));
        this.root = map.root;
        this.keyComparer = map.KeyComparer;
        this.valueComparer = map.ValueComparer;
        this.count = map.Count;
        this.immutable = map;
      }

      ICollection<TKey> IDictionary<TKey, TValue>.Keys
      {
        get => (ICollection<TKey>) this.Root.Keys.ToArray<TKey>(this.Count);
      }

      public IEnumerable<TKey> Keys => this.Root.Keys;

      ICollection<TValue> IDictionary<TKey, TValue>.Values
      {
        get => (ICollection<TValue>) this.Root.Values.ToArray<TValue>(this.Count);
      }

      public IEnumerable<TValue> Values => this.Root.Values;

      public int Count => this.count;

      bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

      internal int Version => this.version;

      private ImmutableSortedDictionary<TKey, TValue>.Node Root
      {
        get => this.root;
        set
        {
          ++this.version;
          if (this.root == value)
            return;
          this.root = value;
          this.immutable = (ImmutableSortedDictionary<TKey, TValue>) null;
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
          bool replacedExistingValue;
          bool mutated;
          this.Root = this.root.SetItem(key, value, this.keyComparer, this.valueComparer, out replacedExistingValue, out mutated);
          if (!mutated || replacedExistingValue)
            return;
          ++this.count;
        }
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

      public IComparer<TKey> KeyComparer
      {
        get => this.keyComparer;
        set
        {
          Requires.NotNull<IComparer<TKey>>(value, nameof (value));
          if (value == this.keyComparer)
            return;
          ImmutableSortedDictionary<TKey, TValue>.Node node = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
          int num = 0;
          foreach (KeyValuePair<TKey, TValue> keyValuePair in this)
          {
            bool mutated;
            node = node.Add(keyValuePair.Key, keyValuePair.Value, value, this.valueComparer, out mutated);
            if (mutated)
              ++num;
          }
          this.keyComparer = value;
          this.Root = node;
          this.count = num;
        }
      }

      public IEqualityComparer<TValue> ValueComparer
      {
        get => this.valueComparer;
        set
        {
          Requires.NotNull<IEqualityComparer<TValue>>(value, nameof (value));
          if (value == this.valueComparer)
            return;
          this.valueComparer = value;
          this.immutable = (ImmutableSortedDictionary<TKey, TValue>) null;
        }
      }

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

      void ICollection.CopyTo(Array array, int index) => this.Root.CopyTo(array, index, this.Count);

      public void Add(TKey key, TValue value)
      {
        bool mutated;
        this.Root = this.Root.Add(key, value, this.keyComparer, this.valueComparer, out mutated);
        if (!mutated)
          return;
        ++this.count;
      }

      public bool ContainsKey(TKey key) => this.Root.ContainsKey(key, this.keyComparer);

      public bool Remove(TKey key)
      {
        bool mutated;
        this.Root = this.Root.Remove(key, this.keyComparer, out mutated);
        if (mutated)
          --this.count;
        return mutated;
      }

      public bool TryGetValue(TKey key, out TValue value)
      {
        return this.Root.TryGetValue(key, this.keyComparer, out value);
      }

      public bool TryGetKey(TKey equalKey, out TKey actualKey)
      {
        Requires.NotNullAllowStructs<TKey>(equalKey, nameof (equalKey));
        return this.Root.TryGetKey(equalKey, this.keyComparer, out actualKey);
      }

      public void Add(KeyValuePair<TKey, TValue> item) => this.Add(item.Key, item.Value);

      public void Clear()
      {
        this.Root = ImmutableSortedDictionary<TKey, TValue>.Node.EmptyNode;
        this.count = 0;
      }

      public bool Contains(KeyValuePair<TKey, TValue> item)
      {
        return this.Root.Contains(item, this.keyComparer, this.valueComparer);
      }

      void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(
        KeyValuePair<TKey, TValue>[] array,
        int arrayIndex)
      {
        this.Root.CopyTo(array, arrayIndex, this.Count);
      }

      public bool Remove(KeyValuePair<TKey, TValue> item)
      {
        return this.Contains(item) && this.Remove(item.Key);
      }

      public ImmutableSortedDictionary<TKey, TValue>.Enumerator GetEnumerator()
      {
        return this.Root.GetEnumerator(this);
      }

      IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      public bool ContainsValue(TValue value) => this.root.ContainsValue(value, this.valueComparer);

      public void AddRange(IEnumerable<KeyValuePair<TKey, TValue>> items)
      {
        Requires.NotNull<IEnumerable<KeyValuePair<TKey, TValue>>>(items, nameof (items));
        foreach (KeyValuePair<TKey, TValue> keyValuePair in items)
          this.Add(keyValuePair);
      }

      public void RemoveRange(IEnumerable<TKey> keys)
      {
        Requires.NotNull<IEnumerable<TKey>>(keys, nameof (keys));
        foreach (TKey key in keys)
          this.Remove(key);
      }

      public TValue GetValueOrDefault(TKey key) => this.GetValueOrDefault(key, default (TValue));

      public TValue GetValueOrDefault(TKey key, TValue defaultValue)
      {
        Requires.NotNullAllowStructs<TKey>(key, nameof (key));
        TValue obj;
        return this.TryGetValue(key, out obj) ? obj : defaultValue;
      }

      public ImmutableSortedDictionary<TKey, TValue> ToImmutable()
      {
        if (this.immutable == null)
          this.immutable = ImmutableSortedDictionary<TKey, TValue>.Wrap(this.Root, this.count, this.keyComparer, this.valueComparer);
        return this.immutable;
      }

      [ExcludeFromCodeCoverage]
      private class DebuggerProxy
      {
        private readonly ImmutableSortedDictionary<TKey, TValue>.Builder map;
        private KeyValuePair<TKey, TValue>[] contents;

        public DebuggerProxy(
          ImmutableSortedDictionary<TKey, TValue>.Builder map)
        {
          Requires.NotNull<ImmutableSortedDictionary<TKey, TValue>.Builder>(map, nameof (map));
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
  }
}
