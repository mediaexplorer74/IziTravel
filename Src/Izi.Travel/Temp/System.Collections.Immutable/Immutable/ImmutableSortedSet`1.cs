// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableSortedSet`1
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
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (ImmutableSortedSet<>.DebuggerProxy))]
  public sealed class ImmutableSortedSet<T> : 
    IImmutableSet<T>,
    ISortKeyCollection<T>,
    IReadOnlyList<T>,
    IReadOnlyCollection<T>,
    IList<T>,
    ISet<T>,
    ICollection<T>,
    IEnumerable<T>,
    IList,
    ICollection,
    IEnumerable
  {
    private const float RefillOverIncrementalThreshold = 0.15f;
    public static readonly ImmutableSortedSet<T> Empty = new ImmutableSortedSet<T>();
    private readonly ImmutableSortedSet<T>.Node root;
    private readonly IComparer<T> comparer;

    internal ImmutableSortedSet(IComparer<T> comparer = null)
    {
      this.root = ImmutableSortedSet<T>.Node.EmptyNode;
      this.comparer = comparer ?? (IComparer<T>) Comparer<T>.Default;
    }

    private ImmutableSortedSet(ImmutableSortedSet<T>.Node root, IComparer<T> comparer)
    {
      Requires.NotNull<ImmutableSortedSet<T>.Node>(root, nameof (root));
      Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
      root.Freeze();
      this.root = root;
      this.comparer = comparer;
    }

    public ImmutableSortedSet<T> Clear()
    {
      return !this.root.IsEmpty ? ImmutableSortedSet<T>.Empty.WithComparer(this.comparer) : this;
    }

    public T Max => this.root.Max;

    public T Min => this.root.Min;

    public bool IsEmpty => this.root.IsEmpty;

    public int Count => this.root.Count;

    public IComparer<T> KeyComparer => this.comparer;

    public T this[int index] => this.root[index];

    public ImmutableSortedSet<T>.Builder ToBuilder() => new ImmutableSortedSet<T>.Builder(this);

    public ImmutableSortedSet<T> Add(T value)
    {
      Requires.NotNullAllowStructs<T>(value, nameof (value));
      return this.Wrap(this.root.Add(value, this.comparer, out bool _));
    }

    public ImmutableSortedSet<T> Remove(T value)
    {
      Requires.NotNullAllowStructs<T>(value, nameof (value));
      return this.Wrap(this.root.Remove(value, this.comparer, out bool _));
    }

    public bool TryGetValue(T equalValue, out T actualValue)
    {
      Requires.NotNullAllowStructs<T>(equalValue, nameof (equalValue));
      ImmutableSortedSet<T>.Node node = this.root.Search(equalValue, this.comparer);
      if (node.IsEmpty)
      {
        actualValue = equalValue;
        return false;
      }
      actualValue = node.Key;
      return true;
    }

    public ImmutableSortedSet<T> Intersect(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableSortedSet<T> immutableSortedSet = this.Clear();
      foreach (T obj in other)
      {
        if (this.Contains(obj))
          immutableSortedSet = immutableSortedSet.Add(obj);
      }
      return immutableSortedSet;
    }

    public ImmutableSortedSet<T> Except(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableSortedSet<T>.Node root = this.root;
      foreach (T key in other)
        root = root.Remove(key, this.comparer, out bool _);
      return this.Wrap(root);
    }

    public ImmutableSortedSet<T> SymmetricExcept(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableSortedSet<T> immutableSortedSet1 = ImmutableSortedSet<T>.Empty.Union(other);
      ImmutableSortedSet<T> immutableSortedSet2 = this.Clear();
      foreach (T obj in this)
      {
        if (!immutableSortedSet1.Contains(obj))
          immutableSortedSet2 = immutableSortedSet2.Add(obj);
      }
      foreach (T obj in immutableSortedSet1)
      {
        if (!this.Contains(obj))
          immutableSortedSet2 = immutableSortedSet2.Add(obj);
      }
      return immutableSortedSet2;
    }

    public ImmutableSortedSet<T> Union(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableSortedSet<T> other1;
      if (ImmutableSortedSet<T>.TryCastToImmutableSortedSet(other, out other1) && other1.KeyComparer == this.KeyComparer)
      {
        if (other1.IsEmpty)
          return this;
        if (this.IsEmpty)
          return other1;
        if (other1.Count > this.Count)
          return other1.Union((IEnumerable<T>) this);
      }
      int count;
      return this.IsEmpty || other.TryGetCount<T>(out count) && (double) (this.Count + count) * 0.15000000596046448 > (double) this.Count ? this.LeafToRootRefill(other) : this.UnionIncremental(other);
    }

    public ImmutableSortedSet<T> WithComparer(IComparer<T> comparer)
    {
      if (comparer == null)
        comparer = (IComparer<T>) Comparer<T>.Default;
      return comparer == this.comparer ? this : new ImmutableSortedSet<T>(ImmutableSortedSet<T>.Node.EmptyNode, comparer).Union((IEnumerable<T>) this);
    }

    public bool SetEquals(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
      if (this.Count != sortedSet.Count)
        return false;
      int num = 0;
      foreach (T obj in sortedSet)
      {
        if (!this.Contains(obj))
          return false;
        ++num;
      }
      return num == this.Count;
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (this.IsEmpty)
        return other.Any<T>();
      SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
      if (this.Count >= sortedSet.Count)
        return false;
      int num = 0;
      bool flag = false;
      foreach (T obj in sortedSet)
      {
        if (this.Contains(obj))
          ++num;
        else
          flag = true;
        if (num == this.Count && flag)
          return true;
      }
      return false;
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (this.IsEmpty)
        return false;
      int num = 0;
      foreach (T obj in other)
      {
        ++num;
        if (!this.Contains(obj))
          return false;
      }
      return this.Count > num;
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (this.IsEmpty)
        return true;
      SortedSet<T> sortedSet = new SortedSet<T>(other, this.KeyComparer);
      int num = 0;
      foreach (T obj in sortedSet)
      {
        if (this.Contains(obj))
          ++num;
      }
      return num == this.Count;
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      foreach (T obj in other)
      {
        if (!this.Contains(obj))
          return false;
      }
      return true;
    }

    public bool Overlaps(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (this.IsEmpty)
        return false;
      foreach (T obj in other)
      {
        if (this.Contains(obj))
          return true;
      }
      return false;
    }

    public IEnumerable<T> Reverse()
    {
      return (IEnumerable<T>) new ImmutableSortedSet<T>.ReverseEnumerable(this.root);
    }

    public int IndexOf(T item)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      return this.root.IndexOf(item, this.comparer);
    }

    public bool Contains(T value)
    {
      Requires.NotNullAllowStructs<T>(value, nameof (value));
      return this.root.Contains(value, this.comparer);
    }

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Clear() => (IImmutableSet<T>) this.Clear();

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Add(T value) => (IImmutableSet<T>) this.Add(value);

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Remove(T value) => (IImmutableSet<T>) this.Remove(value);

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Intersect(IEnumerable<T> other)
    {
      return (IImmutableSet<T>) this.Intersect(other);
    }

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Except(IEnumerable<T> other)
    {
      return (IImmutableSet<T>) this.Except(other);
    }

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.SymmetricExcept(IEnumerable<T> other)
    {
      return (IImmutableSet<T>) this.SymmetricExcept(other);
    }

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
    {
      return (IImmutableSet<T>) this.Union(other);
    }

    bool ISet<T>.Add(T item) => throw new NotSupportedException();

    void ISet<T>.ExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.IntersectWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.UnionWith(IEnumerable<T> other) => throw new NotSupportedException();

    bool ICollection<T>.IsReadOnly => true;

    void ICollection<T>.CopyTo(T[] array, int arrayIndex) => this.root.CopyTo(array, arrayIndex);

    void ICollection<T>.Add(T item) => throw new NotSupportedException();

    void ICollection<T>.Clear() => throw new NotSupportedException();

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

    T IList<T>.this[int index]
    {
      get => this[index];
      set => throw new NotSupportedException();
    }

    void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

    void IList<T>.RemoveAt(int index) => throw new NotSupportedException();

    bool IList.IsFixedSize => true;

    bool IList.IsReadOnly => true;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    int IList.Add(object value) => throw new NotSupportedException();

    void IList.Clear() => throw new NotSupportedException();

    bool IList.Contains(object value) => this.Contains((T) value);

    int IList.IndexOf(object value) => this.IndexOf((T) value);

    void IList.Insert(int index, object value) => throw new NotSupportedException();

    void IList.Remove(object value) => throw new NotSupportedException();

    void IList.RemoveAt(int index) => throw new NotSupportedException();

    object IList.this[int index]
    {
      get => (object) this[index];
      set => throw new NotSupportedException();
    }

    void ICollection.CopyTo(Array array, int index) => this.root.CopyTo(array, index);

    [ExcludeFromCodeCoverage]
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

    [ExcludeFromCodeCoverage]
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public ImmutableSortedSet<T>.Enumerator GetEnumerator() => this.root.GetEnumerator();

    private static bool TryCastToImmutableSortedSet(
      IEnumerable<T> sequence,
      out ImmutableSortedSet<T> other)
    {
      other = sequence as ImmutableSortedSet<T>;
      if (other != null)
        return true;
      if (!(sequence is ImmutableSortedSet<T>.Builder builder))
        return false;
      other = builder.ToImmutable();
      return true;
    }

    private static ImmutableSortedSet<T> Wrap(
      ImmutableSortedSet<T>.Node root,
      IComparer<T> comparer)
    {
      return !root.IsEmpty ? new ImmutableSortedSet<T>(root, comparer) : ImmutableSortedSet<T>.Empty.WithComparer(comparer);
    }

    private ImmutableSortedSet<T> UnionIncremental(IEnumerable<T> items)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      ImmutableSortedSet<T>.Node root = this.root;
      foreach (T key in items)
        root = root.Add(key, this.comparer, out bool _);
      return this.Wrap(root);
    }

    private ImmutableSortedSet<T> Wrap(ImmutableSortedSet<T>.Node root)
    {
      if (root == this.root)
        return this;
      return !root.IsEmpty ? new ImmutableSortedSet<T>(root, this.comparer) : this.Clear();
    }

    private ImmutableSortedSet<T> LeafToRootRefill(IEnumerable<T> addedItems)
    {
      Requires.NotNull<IEnumerable<T>>(addedItems, nameof (addedItems));
      return this.Wrap(ImmutableSortedSet<T>.Node.NodeTreeFromSortedSet(new SortedSet<T>(this.Concat<T>(addedItems), this.KeyComparer)));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser
    {
      private static readonly SecureObjectPool<Stack<RefAsValueType<IBinaryTree<T>>>, ImmutableSortedSet<T>.Enumerator> enumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<IBinaryTree<T>>>, ImmutableSortedSet<T>.Enumerator>();
      private readonly ImmutableSortedSet<T>.Builder builder;
      private readonly Guid poolUserId;
      private readonly bool reverse;
      private IBinaryTree<T> root;
      private SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>> stack;
      private IBinaryTree<T> current;
      private int enumeratingBuilderVersion;

      internal Enumerator(IBinaryTree<T> root, ImmutableSortedSet<T>.Builder builder = null, bool reverse = false)
      {
        Requires.NotNull<IBinaryTree<T>>(root, nameof (root));
        this.root = root;
        this.builder = builder;
        this.current = (IBinaryTree<T>) null;
        this.reverse = reverse;
        this.enumeratingBuilderVersion = builder != null ? builder.Version : -1;
        this.poolUserId = Guid.NewGuid();
        this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>) null;
        if (!ImmutableSortedSet<T>.Enumerator.enumeratingStacks.TryTake(this, out this.stack))
          this.stack = ImmutableSortedSet<T>.Enumerator.enumeratingStacks.PrepNew(this, new Stack<RefAsValueType<IBinaryTree<T>>>(root.Height));
        this.Reset();
      }

      Guid ISecurePooledObjectUser.PoolUserId => this.poolUserId;

      public T Current
      {
        get
        {
          this.ThrowIfDisposed();
          if (this.current != null)
            return this.current.Value;
          throw new InvalidOperationException();
        }
      }

      object IEnumerator.Current => (object) this.Current;

      public void Dispose()
      {
        this.root = (IBinaryTree<T>) null;
        this.current = (IBinaryTree<T>) null;
        if (this.stack == null || !(this.stack.Owner == this.poolUserId))
          return;
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedSet<T>.Enumerator>(this))
          pooledObjectUser.Value.Clear();
        ImmutableSortedSet<T>.Enumerator.enumeratingStacks.TryAdd(this, this.stack);
        this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>) null;
      }

      public bool MoveNext()
      {
        this.ThrowIfDisposed();
        this.ThrowIfChanged();
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedSet<T>.Enumerator>(this))
        {
          if (pooledObjectUser.Value.Count > 0)
          {
            IBinaryTree<T> binaryTree = pooledObjectUser.Value.Pop().Value;
            this.current = binaryTree;
            this.PushNext(this.reverse ? binaryTree.Left : binaryTree.Right);
            return true;
          }
          this.current = (IBinaryTree<T>) null;
          return false;
        }
      }

      public void Reset()
      {
        this.ThrowIfDisposed();
        this.enumeratingBuilderVersion = this.builder != null ? this.builder.Version : -1;
        this.current = (IBinaryTree<T>) null;
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedSet<T>.Enumerator>(this))
          pooledObjectUser.Value.Clear();
        this.PushNext(this.root);
      }

      private void ThrowIfDisposed()
      {
        if (this.root == null)
          throw new ObjectDisposedException(this.GetType().FullName);
        if (this.stack == null)
          return;
        this.stack.ThrowDisposedIfNotOwned<ImmutableSortedSet<T>.Enumerator>(this);
      }

      private void ThrowIfChanged()
      {
        if (this.builder != null && this.builder.Version != this.enumeratingBuilderVersion)
          throw new InvalidOperationException(Strings.CollectionModifiedDuringEnumeration);
      }

      private void PushNext(IBinaryTree<T> node)
      {
        Requires.NotNull<IBinaryTree<T>>(node, nameof (node));
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableSortedSet<T>.Enumerator>(this))
        {
          for (; !node.IsEmpty; node = this.reverse ? node.Right : node.Left)
            pooledObjectUser.Value.Push(new RefAsValueType<IBinaryTree<T>>(node));
        }
      }
    }

    private class ReverseEnumerable : IEnumerable<T>, IEnumerable
    {
      private readonly ImmutableSortedSet<T>.Node root;

      internal ReverseEnumerable(ImmutableSortedSet<T>.Node root)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(root, nameof (root));
        this.root = root;
      }

      public IEnumerator<T> GetEnumerator() => this.root.Reverse();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }

    [DebuggerDisplay("{key}")]
    private sealed class Node : IBinaryTree<T>, IEnumerable<T>, IEnumerable
    {
      internal static readonly ImmutableSortedSet<T>.Node EmptyNode = new ImmutableSortedSet<T>.Node();
      private readonly T key;
      private bool frozen;
      private int height;
      private int count;
      private ImmutableSortedSet<T>.Node left;
      private ImmutableSortedSet<T>.Node right;

      private Node() => this.frozen = true;

      private Node(
        T key,
        ImmutableSortedSet<T>.Node left,
        ImmutableSortedSet<T>.Node right,
        bool frozen = false)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<ImmutableSortedSet<T>.Node>(left, nameof (left));
        Requires.NotNull<ImmutableSortedSet<T>.Node>(right, nameof (right));
        this.key = key;
        this.left = left;
        this.right = right;
        this.height = 1 + Math.Max(left.height, right.height);
        this.count = 1 + left.count + right.count;
        this.frozen = frozen;
      }

      public bool IsEmpty => this.left == null;

      int IBinaryTree<T>.Height => this.height;

      IBinaryTree<T> IBinaryTree<T>.Left => (IBinaryTree<T>) this.left;

      IBinaryTree<T> IBinaryTree<T>.Right => (IBinaryTree<T>) this.right;

      T IBinaryTree<T>.Value => this.key;

      public int Count => this.count;

      internal T Key => this.key;

      internal T Max
      {
        get
        {
          if (this.IsEmpty)
            return default (T);
          ImmutableSortedSet<T>.Node node = this;
          while (!node.right.IsEmpty)
            node = node.right;
          return node.key;
        }
      }

      internal T Min
      {
        get
        {
          if (this.IsEmpty)
            return default (T);
          ImmutableSortedSet<T>.Node node = this;
          while (!node.left.IsEmpty)
            node = node.left;
          return node.key;
        }
      }

      internal T this[int index]
      {
        get
        {
          Requires.Range(index >= 0 && index < this.Count, nameof (index));
          if (index < this.left.count)
            return this.left[index];
          return index > this.left.count ? this.right[index - this.left.count - 1] : this.key;
        }
      }

      public ImmutableSortedSet<T>.Enumerator GetEnumerator()
      {
        return new ImmutableSortedSet<T>.Enumerator((IBinaryTree<T>) this);
      }

      [ExcludeFromCodeCoverage]
      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

      [ExcludeFromCodeCoverage]
      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      internal ImmutableSortedSet<T>.Enumerator GetEnumerator(ImmutableSortedSet<T>.Builder builder)
      {
        return new ImmutableSortedSet<T>.Enumerator((IBinaryTree<T>) this, builder);
      }

      internal static ImmutableSortedSet<T>.Node NodeTreeFromSortedSet(SortedSet<T> collection)
      {
        Requires.NotNull<SortedSet<T>>(collection, nameof (collection));
        if (collection.Count == 0)
          return ImmutableSortedSet<T>.Node.EmptyNode;
        IOrderedCollection<T> items = collection.AsOrderedCollection<T>();
        return ImmutableSortedSet<T>.Node.NodeTreeFromList(items, 0, items.Count);
      }

      internal void CopyTo(T[] array, int arrayIndex)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        foreach (T obj in this)
          array[arrayIndex++] = obj;
      }

      internal void CopyTo(Array array, int arrayIndex)
      {
        Requires.NotNull<Array>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        foreach (T obj in this)
          array.SetValue((object) obj, arrayIndex++);
      }

      internal ImmutableSortedSet<T>.Node Add(T key, IComparer<T> comparer, out bool mutated)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        if (this.IsEmpty)
        {
          mutated = true;
          return new ImmutableSortedSet<T>.Node(key, this, this);
        }
        ImmutableSortedSet<T>.Node tree = this;
        int num = comparer.Compare(key, this.key);
        if (num > 0)
        {
          ImmutableSortedSet<T>.Node right = this.right.Add(key, comparer, out mutated);
          if (mutated)
            tree = this.Mutate(right: right);
        }
        else if (num < 0)
        {
          ImmutableSortedSet<T>.Node left = this.left.Add(key, comparer, out mutated);
          if (mutated)
            tree = this.Mutate(left);
        }
        else
        {
          mutated = false;
          return this;
        }
        return !mutated ? tree : ImmutableSortedSet<T>.Node.MakeBalanced(tree);
      }

      internal ImmutableSortedSet<T>.Node Remove(T key, IComparer<T> comparer, out bool mutated)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        if (this.IsEmpty)
        {
          mutated = false;
          return this;
        }
        ImmutableSortedSet<T>.Node tree = this;
        int num = comparer.Compare(key, this.key);
        if (num == 0)
        {
          mutated = true;
          if (this.right.IsEmpty && this.left.IsEmpty)
            tree = ImmutableSortedSet<T>.Node.EmptyNode;
          else if (this.right.IsEmpty && !this.left.IsEmpty)
            tree = this.left;
          else if (!this.right.IsEmpty && this.left.IsEmpty)
          {
            tree = this.right;
          }
          else
          {
            ImmutableSortedSet<T>.Node node = this.right;
            while (!node.left.IsEmpty)
              node = node.left;
            ImmutableSortedSet<T>.Node right = this.right.Remove(node.key, comparer, out bool _);
            tree = node.Mutate(this.left, right);
          }
        }
        else if (num < 0)
        {
          ImmutableSortedSet<T>.Node left = this.left.Remove(key, comparer, out mutated);
          if (mutated)
            tree = this.Mutate(left);
        }
        else
        {
          ImmutableSortedSet<T>.Node right = this.right.Remove(key, comparer, out mutated);
          if (mutated)
            tree = this.Mutate(right: right);
        }
        return !tree.IsEmpty ? ImmutableSortedSet<T>.Node.MakeBalanced(tree) : tree;
      }

      internal bool Contains(T key, IComparer<T> comparer)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        return !this.Search(key, comparer).IsEmpty;
      }

      internal void Freeze()
      {
        if (this.frozen)
          return;
        this.left.Freeze();
        this.right.Freeze();
        this.frozen = true;
      }

      internal ImmutableSortedSet<T>.Node Search(T key, IComparer<T> comparer)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        if (this.IsEmpty)
          return this;
        int num = comparer.Compare(key, this.key);
        if (num == 0)
          return this;
        return num > 0 ? this.right.Search(key, comparer) : this.left.Search(key, comparer);
      }

      internal int IndexOf(T key, IComparer<T> comparer)
      {
        Requires.NotNullAllowStructs<T>(key, nameof (key));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        if (this.IsEmpty)
          return -1;
        int num1 = comparer.Compare(key, this.key);
        if (num1 == 0)
          return this.left.Count;
        if (num1 <= 0)
          return this.left.IndexOf(key, comparer);
        int num2 = this.right.IndexOf(key, comparer);
        bool flag = num2 < 0;
        if (flag)
          num2 = ~num2;
        int num3 = this.left.Count + 1 + num2;
        if (flag)
          num3 = ~num3;
        return num3;
      }

      internal IEnumerator<T> Reverse()
      {
        return (IEnumerator<T>) new ImmutableSortedSet<T>.Enumerator((IBinaryTree<T>) this, reverse: true);
      }

      private static ImmutableSortedSet<T>.Node RotateLeft(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        if (tree.right.IsEmpty)
          return tree;
        ImmutableSortedSet<T>.Node right = tree.right;
        return right.Mutate(tree.Mutate(right: right.left));
      }

      private static ImmutableSortedSet<T>.Node RotateRight(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        if (tree.left.IsEmpty)
          return tree;
        ImmutableSortedSet<T>.Node left = tree.left;
        return left.Mutate(right: tree.Mutate(left.right));
      }

      private static ImmutableSortedSet<T>.Node DoubleLeft(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        return tree.right.IsEmpty ? tree : ImmutableSortedSet<T>.Node.RotateLeft(tree.Mutate(right: ImmutableSortedSet<T>.Node.RotateRight(tree.right)));
      }

      private static ImmutableSortedSet<T>.Node DoubleRight(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        return tree.left.IsEmpty ? tree : ImmutableSortedSet<T>.Node.RotateRight(tree.Mutate(ImmutableSortedSet<T>.Node.RotateLeft(tree.left)));
      }

      private static int Balance(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        return tree.right.height - tree.left.height;
      }

      private static bool IsRightHeavy(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        return ImmutableSortedSet<T>.Node.Balance(tree) >= 2;
      }

      private static bool IsLeftHeavy(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        return ImmutableSortedSet<T>.Node.Balance(tree) <= -2;
      }

      private static ImmutableSortedSet<T>.Node MakeBalanced(ImmutableSortedSet<T>.Node tree)
      {
        Requires.NotNull<ImmutableSortedSet<T>.Node>(tree, nameof (tree));
        if (ImmutableSortedSet<T>.Node.IsRightHeavy(tree))
          return !ImmutableSortedSet<T>.Node.IsLeftHeavy(tree.right) ? ImmutableSortedSet<T>.Node.RotateLeft(tree) : ImmutableSortedSet<T>.Node.DoubleLeft(tree);
        if (!ImmutableSortedSet<T>.Node.IsLeftHeavy(tree))
          return tree;
        return !ImmutableSortedSet<T>.Node.IsRightHeavy(tree.left) ? ImmutableSortedSet<T>.Node.RotateRight(tree) : ImmutableSortedSet<T>.Node.DoubleRight(tree);
      }

      private static ImmutableSortedSet<T>.Node NodeTreeFromList(
        IOrderedCollection<T> items,
        int start,
        int length)
      {
        Requires.NotNull<IOrderedCollection<T>>(items, nameof (items));
        if (length == 0)
          return ImmutableSortedSet<T>.Node.EmptyNode;
        int length1 = (length - 1) / 2;
        int length2 = length - 1 - length1;
        ImmutableSortedSet<T>.Node left = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start, length2);
        ImmutableSortedSet<T>.Node right = ImmutableSortedSet<T>.Node.NodeTreeFromList(items, start + length2 + 1, length1);
        return new ImmutableSortedSet<T>.Node(items[start + length2], left, right, true);
      }

      private ImmutableSortedSet<T>.Node Mutate(
        ImmutableSortedSet<T>.Node left = null,
        ImmutableSortedSet<T>.Node right = null)
      {
        if (this.frozen)
          return new ImmutableSortedSet<T>.Node(this.key, left ?? this.left, right ?? this.right);
        if (left != null)
          this.left = left;
        if (right != null)
          this.right = right;
        this.height = 1 + Math.Max(this.left.height, this.right.height);
        this.count = 1 + this.left.count + this.right.count;
        return this;
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableSortedSet<T> set;
      private T[] contents;

      public DebuggerProxy(ImmutableSortedSet<T> set)
      {
        Requires.NotNull<ImmutableSortedSet<T>>(set, nameof (set));
        this.set = set;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public T[] Contents
      {
        get
        {
          if (this.contents == null)
            this.contents = this.set.ToArray<T>(this.set.Count);
          return this.contents;
        }
      }
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof (ImmutableSortedSet<>.Builder.DebuggerProxy))]
    public sealed class Builder : 
      ISortKeyCollection<T>,
      IReadOnlyCollection<T>,
      ISet<T>,
      ICollection<T>,
      IEnumerable<T>,
      ICollection,
      IEnumerable
    {
      private ImmutableSortedSet<T>.Node root = ImmutableSortedSet<T>.Node.EmptyNode;
      private IComparer<T> comparer = (IComparer<T>) Comparer<T>.Default;
      private ImmutableSortedSet<T> immutable;
      private int version;
      private object syncRoot;

      internal Builder(ImmutableSortedSet<T> set)
      {
        Requires.NotNull<ImmutableSortedSet<T>>(set, nameof (set));
        this.root = set.root;
        this.comparer = set.KeyComparer;
        this.immutable = set;
      }

      public int Count => this.Root.Count;

      bool ICollection<T>.IsReadOnly => false;

      public T Max => this.root.Max;

      public T Min => this.root.Min;

      public IComparer<T> KeyComparer
      {
        get => this.comparer;
        set
        {
          Requires.NotNull<IComparer<T>>(value, nameof (value));
          if (value == this.comparer)
            return;
          ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
          foreach (T key in this)
            node = node.Add(key, value, out bool _);
          this.immutable = (ImmutableSortedSet<T>) null;
          this.comparer = value;
          this.Root = node;
        }
      }

      internal int Version => this.version;

      private ImmutableSortedSet<T>.Node Root
      {
        get => this.root;
        set
        {
          ++this.version;
          if (this.root == value)
            return;
          this.root = value;
          this.immutable = (ImmutableSortedSet<T>) null;
        }
      }

      public bool Add(T item)
      {
        bool mutated;
        this.Root = this.Root.Add(item, this.comparer, out mutated);
        return mutated;
      }

      public void ExceptWith(IEnumerable<T> other)
      {
        Requires.NotNull<IEnumerable<T>>(other, nameof (other));
        foreach (T key in other)
          this.Root = this.Root.Remove(key, this.comparer, out bool _);
      }

      public void IntersectWith(IEnumerable<T> other)
      {
        Requires.NotNull<IEnumerable<T>>(other, nameof (other));
        ImmutableSortedSet<T>.Node node = ImmutableSortedSet<T>.Node.EmptyNode;
        foreach (T key in other)
        {
          if (this.Contains(key))
            node = node.Add(key, this.comparer, out bool _);
        }
        this.Root = node;
      }

      public bool IsProperSubsetOf(IEnumerable<T> other)
      {
        return this.ToImmutable().IsProperSubsetOf(other);
      }

      public bool IsProperSupersetOf(IEnumerable<T> other)
      {
        return this.ToImmutable().IsProperSupersetOf(other);
      }

      public bool IsSubsetOf(IEnumerable<T> other) => this.ToImmutable().IsSubsetOf(other);

      public bool IsSupersetOf(IEnumerable<T> other) => this.ToImmutable().IsSupersetOf(other);

      public bool Overlaps(IEnumerable<T> other) => this.ToImmutable().Overlaps(other);

      public bool SetEquals(IEnumerable<T> other) => this.ToImmutable().SetEquals(other);

      public void SymmetricExceptWith(IEnumerable<T> other)
      {
        this.Root = this.ToImmutable().SymmetricExcept(other).root;
      }

      public void UnionWith(IEnumerable<T> other)
      {
        Requires.NotNull<IEnumerable<T>>(other, nameof (other));
        foreach (T key in other)
          this.Root = this.Root.Add(key, this.comparer, out bool _);
      }

      void ICollection<T>.Add(T item) => this.Add(item);

      public void Clear() => this.Root = ImmutableSortedSet<T>.Node.EmptyNode;

      public bool Contains(T item) => this.Root.Contains(item, this.comparer);

      void ICollection<T>.CopyTo(T[] array, int arrayIndex) => this.root.CopyTo(array, arrayIndex);

      public bool Remove(T item)
      {
        bool mutated;
        this.Root = this.Root.Remove(item, this.comparer, out mutated);
        return mutated;
      }

      public ImmutableSortedSet<T>.Enumerator GetEnumerator() => this.Root.GetEnumerator(this);

      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.Root.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      public IEnumerable<T> Reverse()
      {
        return (IEnumerable<T>) new ImmutableSortedSet<T>.ReverseEnumerable(this.root);
      }

      public ImmutableSortedSet<T> ToImmutable()
      {
        if (this.immutable == null)
          this.immutable = ImmutableSortedSet<T>.Wrap(this.Root, this.comparer);
        return this.immutable;
      }

      void ICollection.CopyTo(Array array, int arrayIndex) => this.Root.CopyTo(array, arrayIndex);

      [DebuggerBrowsable(DebuggerBrowsableState.Never)]
      bool ICollection.IsSynchronized => false;

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

      [ExcludeFromCodeCoverage]
      private class DebuggerProxy
      {
        private readonly ImmutableSortedSet<T>.Node set;
        private T[] contents;

        public DebuggerProxy(ImmutableSortedSet<T>.Builder builder)
        {
          Requires.NotNull<ImmutableSortedSet<T>.Builder>(builder, nameof (builder));
          this.set = builder.Root;
        }

        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public T[] Contents
        {
          get
          {
            if (this.contents == null)
              this.contents = this.set.ToArray<T>(this.set.Count);
            return this.contents;
          }
        }
      }
    }
  }
}
