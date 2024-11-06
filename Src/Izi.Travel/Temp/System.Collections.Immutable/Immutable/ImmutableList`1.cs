// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableList`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (ImmutableList<>.DebuggerProxy))]
  public sealed class ImmutableList<T> : 
    IImmutableList<T>,
    IList<T>,
    ICollection<T>,
    IList,
    ICollection,
    IOrderedCollection<T>,
    IImmutableListQueries<T>,
    IReadOnlyList<T>,
    IReadOnlyCollection<T>,
    IEnumerable<T>,
    IEnumerable
  {
    public static readonly ImmutableList<T> Empty = new ImmutableList<T>();
    private readonly ImmutableList<T>.Node root;

    internal ImmutableList() => this.root = ImmutableList<T>.Node.EmptyNode;

    private ImmutableList(ImmutableList<T>.Node root)
    {
      Requires.NotNull<ImmutableList<T>.Node>(root, nameof (root));
      root.Freeze();
      this.root = root;
    }

    public ImmutableList<T> Clear() => ImmutableList<T>.Empty;

    public int BinarySearch(T item) => this.BinarySearch(item, (IComparer<T>) null);

    public int BinarySearch(T item, IComparer<T> comparer)
    {
      return this.BinarySearch(0, this.Count, item, comparer);
    }

    public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
    {
      return this.root.BinarySearch(index, count, item, comparer);
    }

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    public bool IsEmpty => this.root.IsEmpty;

    IImmutableList<T> IImmutableList<T>.Clear() => (IImmutableList<T>) this.Clear();

    public int Count => this.root.Count;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    public T this[int index] => this.root[index];

    T IOrderedCollection<T>.this[int index] => this[index];

    public ImmutableList<T>.Builder ToBuilder() => new ImmutableList<T>.Builder(this);

    public ImmutableList<T> Add(T value) => this.Wrap(this.root.Add(value));

    public ImmutableList<T> AddRange(IEnumerable<T> items)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      if (this.IsEmpty)
        return this.FillFromEmpty(items);
      ImmutableList<T>.Node root = this.root;
      foreach (T key in items)
        root = root.Add(key);
      return this.Wrap(root);
    }

    public ImmutableList<T> Insert(int index, T item)
    {
      Requires.Range(index >= 0 && index <= this.Count, nameof (index));
      return this.Wrap(this.root.Insert(index, item));
    }

    public ImmutableList<T> InsertRange(int index, IEnumerable<T> items)
    {
      Requires.Range(index >= 0 && index <= this.Count, nameof (index));
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      ImmutableList<T>.Node root = this.root;
      foreach (T key in items)
        root = root.Insert(index++, key);
      return this.Wrap(root);
    }

    public ImmutableList<T> Remove(T value)
    {
      return this.Remove(value, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public ImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer)
    {
      int index = this.IndexOf<T>(value, equalityComparer);
      return index >= 0 ? this.RemoveAt(index) : this;
    }

    public ImmutableList<T> RemoveRange(int index, int count)
    {
      Requires.Range(index >= 0 && (index < this.Count || index == this.Count && count == 0), nameof (index));
      Requires.Range(count >= 0 && index + count <= this.Count, nameof (count));
      ImmutableList<T>.Node root = this.root;
      int num = count;
      while (num-- > 0)
        root = root.RemoveAt(index);
      return this.Wrap(root);
    }

    public ImmutableList<T> RemoveRange(IEnumerable<T> items)
    {
      return this.RemoveRange(items, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public ImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
      if (this.IsEmpty)
        return this;
      ImmutableList<T>.Node root = this.root;
      foreach (T obj in items)
      {
        int index = root.IndexOf(obj, equalityComparer);
        if (index >= 0)
          root = root.RemoveAt(index);
      }
      return this.Wrap(root);
    }

    public ImmutableList<T> RemoveAt(int index)
    {
      Requires.Range(index >= 0 && index < this.Count, nameof (index));
      return this.Wrap(this.root.RemoveAt(index));
    }

    public ImmutableList<T> RemoveAll(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.Wrap(this.root.RemoveAll(match));
    }

    public ImmutableList<T> SetItem(int index, T value)
    {
      return this.Wrap(this.root.ReplaceAt(index, value));
    }

    public ImmutableList<T> Replace(T oldValue, T newValue)
    {
      return this.Replace(oldValue, newValue, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    public ImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer)
    {
      Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
      int index = this.IndexOf<T>(oldValue, equalityComparer);
      return index >= 0 ? this.SetItem(index, newValue) : throw new ArgumentException(Strings.CannotFindOldValue, nameof (oldValue));
    }

    public ImmutableList<T> Reverse() => this.Wrap(this.root.Reverse());

    public ImmutableList<T> Reverse(int index, int count)
    {
      return this.Wrap(this.root.Reverse(index, count));
    }

    public ImmutableList<T> Sort() => this.Wrap(this.root.Sort());

    public ImmutableList<T> Sort(Comparison<T> comparison)
    {
      Requires.NotNull<Comparison<T>>(comparison, nameof (comparison));
      return this.Wrap(this.root.Sort(comparison));
    }

    public ImmutableList<T> Sort(IComparer<T> comparer)
    {
      Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
      return this.Wrap(this.root.Sort(comparer));
    }

    public ImmutableList<T> Sort(int index, int count, IComparer<T> comparer)
    {
      Requires.Range(index >= 0, nameof (index));
      Requires.Range(count >= 0, nameof (count));
      Requires.Range(index + count <= this.Count, nameof (count));
      Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
      return this.Wrap(this.root.Sort(index, count, comparer));
    }

    public void ForEach(Action<T> action)
    {
      Requires.NotNull<Action<T>>(action, nameof (action));
      foreach (T obj in this)
        action(obj);
    }

    public void CopyTo(T[] array)
    {
      Requires.NotNull<T[]>(array, nameof (array));
      Requires.Range(array.Length >= this.Count, nameof (array));
      this.root.CopyTo(array);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      Requires.NotNull<T[]>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      this.root.CopyTo(array, arrayIndex);
    }

    public void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
      this.root.CopyTo(index, array, arrayIndex, count);
    }

    public ImmutableList<T> GetRange(int index, int count)
    {
      Requires.Range(index >= 0, nameof (index));
      Requires.Range(count >= 0, nameof (count));
      Requires.Range(index + count <= this.Count, nameof (count));
      return this.Wrap(ImmutableList<T>.Node.NodeTreeFromList((IOrderedCollection<T>) this, index, count));
    }

    public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
    {
      Requires.NotNull<Func<T, TOutput>>(converter, nameof (converter));
      return ImmutableList<TOutput>.WrapNode(this.root.ConvertAll<TOutput>(converter));
    }

    public bool Exists(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.Exists(match);
    }

    public T Find(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.Find(match);
    }

    public ImmutableList<T> FindAll(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.FindAll(match);
    }

    public int FindIndex(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.FindIndex(match);
    }

    public int FindIndex(int startIndex, Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      Requires.Range(startIndex >= 0, nameof (startIndex));
      Requires.Range(startIndex <= this.Count, nameof (startIndex));
      return this.root.FindIndex(startIndex, match);
    }

    public int FindIndex(int startIndex, int count, Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      Requires.Range(startIndex >= 0, nameof (startIndex));
      Requires.Range(count >= 0, nameof (count));
      Requires.Range(startIndex + count <= this.Count, nameof (count));
      return this.root.FindIndex(startIndex, count, match);
    }

    public T FindLast(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.FindLast(match);
    }

    public int FindLastIndex(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.FindLastIndex(match);
    }

    public int FindLastIndex(int startIndex, Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      Requires.Range(startIndex >= 0, nameof (startIndex));
      Requires.Range(startIndex == 0 || startIndex < this.Count, nameof (startIndex));
      return this.root.FindLastIndex(startIndex, match);
    }

    public int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      Requires.Range(startIndex >= 0, nameof (startIndex));
      Requires.Range(count <= this.Count, nameof (count));
      Requires.Range(startIndex - count + 1 >= 0, nameof (startIndex));
      return this.root.FindLastIndex(startIndex, count, match);
    }

    public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
    {
      return this.root.IndexOf(item, index, count, equalityComparer);
    }

    public int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
    {
      return this.root.LastIndexOf(item, index, count, equalityComparer);
    }

    public bool TrueForAll(Predicate<T> match)
    {
      Requires.NotNull<Predicate<T>>(match, nameof (match));
      return this.root.TrueForAll(match);
    }

    public bool Contains(T value) => this.IndexOf(value) >= 0;

    public int IndexOf(T value)
    {
      return this.IndexOf<T>(value, (IEqualityComparer<T>) EqualityComparer<T>.Default);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.Add(T value) => (IImmutableList<T>) this.Add(value);

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.AddRange(IEnumerable<T> items)
    {
      return (IImmutableList<T>) this.AddRange(items);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.Insert(int index, T item)
    {
      return (IImmutableList<T>) this.Insert(index, item);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.InsertRange(int index, IEnumerable<T> items)
    {
      return (IImmutableList<T>) this.InsertRange(index, items);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.Remove(T value, IEqualityComparer<T> equalityComparer)
    {
      return (IImmutableList<T>) this.Remove(value, equalityComparer);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.RemoveAll(Predicate<T> match)
    {
      return (IImmutableList<T>) this.RemoveAll(match);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.RemoveRange(
      IEnumerable<T> items,
      IEqualityComparer<T> equalityComparer)
    {
      return (IImmutableList<T>) this.RemoveRange(items, equalityComparer);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.RemoveRange(int index, int count)
    {
      return (IImmutableList<T>) this.RemoveRange(index, count);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.RemoveAt(int index)
    {
      return (IImmutableList<T>) this.RemoveAt(index);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.SetItem(int index, T value)
    {
      return (IImmutableList<T>) this.SetItem(index, value);
    }

    [ExcludeFromCodeCoverage]
    IImmutableList<T> IImmutableList<T>.Replace(
      T oldValue,
      T newValue,
      IEqualityComparer<T> equalityComparer)
    {
      return (IImmutableList<T>) this.Replace(oldValue, newValue, equalityComparer);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    void IList<T>.Insert(int index, T item) => throw new NotSupportedException();

    void IList<T>.RemoveAt(int index) => throw new NotSupportedException();

    T IList<T>.this[int index]
    {
      get => this[index];
      set => throw new NotSupportedException();
    }

    void ICollection<T>.Add(T item) => throw new NotSupportedException();

    void ICollection<T>.Clear() => throw new NotSupportedException();

    bool ICollection<T>.IsReadOnly => true;

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

    void ICollection.CopyTo(Array array, int arrayIndex) => this.root.CopyTo(array, arrayIndex);

    int IList.Add(object value) => throw new NotSupportedException();

    void IList.RemoveAt(int index) => throw new NotSupportedException();

    void IList.Clear() => throw new NotSupportedException();

    bool IList.Contains(object value) => this.Contains((T) value);

    int IList.IndexOf(object value) => this.IndexOf((T) value);

    void IList.Insert(int index, object value) => throw new NotSupportedException();

    bool IList.IsFixedSize => true;

    bool IList.IsReadOnly => true;

    void IList.Remove(object value) => throw new NotSupportedException();

    object IList.this[int index]
    {
      get => (object) this[index];
      set => throw new NotSupportedException();
    }

    public ImmutableList<T>.Enumerator GetEnumerator()
    {
      return new ImmutableList<T>.Enumerator((IBinaryTree<T>) this.root);
    }

    private static ImmutableList<T> WrapNode(ImmutableList<T>.Node root)
    {
      return !root.IsEmpty ? new ImmutableList<T>(root) : ImmutableList<T>.Empty;
    }

    private static bool TryCastToImmutableList(IEnumerable<T> sequence, out ImmutableList<T> other)
    {
      other = sequence as ImmutableList<T>;
      if (other != null)
        return true;
      if (!(sequence is ImmutableList<T>.Builder builder))
        return false;
      other = builder.ToImmutable();
      return true;
    }

    private ImmutableList<T> Wrap(ImmutableList<T>.Node root)
    {
      if (root == this.root)
        return this;
      return !root.IsEmpty ? new ImmutableList<T>(root) : this.Clear();
    }

    private ImmutableList<T> FillFromEmpty(IEnumerable<T> items)
    {
      ImmutableList<T> other;
      if (ImmutableList<T>.TryCastToImmutableList(items, out other))
        return other;
      IOrderedCollection<T> items1 = items.AsOrderedCollection<T>();
      return items1.Count == 0 ? this : new ImmutableList<T>(ImmutableList<T>.Node.NodeTreeFromList(items1, 0, items1.Count));
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable, ISecurePooledObjectUser
    {
      private static readonly SecureObjectPool<Stack<RefAsValueType<IBinaryTree<T>>>, ImmutableList<T>.Enumerator> EnumeratingStacks = new SecureObjectPool<Stack<RefAsValueType<IBinaryTree<T>>>, ImmutableList<T>.Enumerator>();
      private readonly ImmutableList<T>.Builder builder;
      private readonly Guid poolUserId;
      private readonly int startIndex;
      private readonly int count;
      private int remainingCount;
      private bool reversed;
      private IBinaryTree<T> root;
      private SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>> stack;
      private IBinaryTree<T> current;
      private int enumeratingBuilderVersion;

      internal Enumerator(
        IBinaryTree<T> root,
        ImmutableList<T>.Builder builder = null,
        int startIndex = -1,
        int count = -1,
        bool reversed = false)
      {
        Requires.NotNull<IBinaryTree<T>>(root, nameof (root));
        Requires.Range(startIndex >= -1, nameof (startIndex));
        Requires.Range(count >= -1, nameof (count));
        Requires.Argument(reversed || count == -1 || (startIndex == -1 ? 0 : startIndex) + count <= root.Count);
        Requires.Argument(!reversed || count == -1 || (startIndex == -1 ? root.Count - 1 : startIndex) - count + 1 >= 0);
        this.root = root;
        this.builder = builder;
        this.current = (IBinaryTree<T>) null;
        this.startIndex = startIndex >= 0 ? startIndex : (reversed ? root.Count - 1 : 0);
        this.count = count == -1 ? root.Count : count;
        this.remainingCount = this.count;
        this.reversed = reversed;
        this.enumeratingBuilderVersion = builder != null ? builder.Version : -1;
        this.poolUserId = Guid.NewGuid();
        if (this.count > 0)
        {
          this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>) null;
          if (!ImmutableList<T>.Enumerator.EnumeratingStacks.TryTake(this, out this.stack))
            this.stack = ImmutableList<T>.Enumerator.EnumeratingStacks.PrepNew(this, new Stack<RefAsValueType<IBinaryTree<T>>>(root.Height));
        }
        else
          this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>) null;
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
        if (this.stack != null && this.stack.Owner == this.poolUserId)
        {
          using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableList<T>.Enumerator>(this))
            pooledObjectUser.Value.Clear();
          ImmutableList<T>.Enumerator.EnumeratingStacks.TryAdd(this, this.stack);
        }
        this.stack = (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>) null;
      }

      public bool MoveNext()
      {
        this.ThrowIfDisposed();
        this.ThrowIfChanged();
        if (this.stack != null)
        {
          using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableList<T>.Enumerator>(this))
          {
            if (this.remainingCount > 0)
            {
              if (pooledObjectUser.Value.Count > 0)
              {
                IBinaryTree<T> node = pooledObjectUser.Value.Pop().Value;
                this.current = node;
                this.PushNext(this.NextBranch(node));
                --this.remainingCount;
                return true;
              }
            }
          }
        }
        this.current = (IBinaryTree<T>) null;
        return false;
      }

      public void Reset()
      {
        this.ThrowIfDisposed();
        this.enumeratingBuilderVersion = this.builder != null ? this.builder.Version : -1;
        this.remainingCount = this.count;
        if (this.stack == null)
          return;
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableList<T>.Enumerator>(this))
        {
          pooledObjectUser.Value.Clear();
          IBinaryTree<T> node = this.root;
          int num = this.reversed ? this.root.Count - this.startIndex - 1 : this.startIndex;
          while (!node.IsEmpty && num != this.PreviousBranch(node).Count)
          {
            if (num < this.PreviousBranch(node).Count)
            {
              pooledObjectUser.Value.Push(new RefAsValueType<IBinaryTree<T>>(node));
              node = this.PreviousBranch(node);
            }
            else
            {
              num -= this.PreviousBranch(node).Count + 1;
              node = this.NextBranch(node);
            }
          }
          if (node.IsEmpty)
            return;
          pooledObjectUser.Value.Push(new RefAsValueType<IBinaryTree<T>>(node));
        }
      }

      private IBinaryTree<T> NextBranch(IBinaryTree<T> node)
      {
        return !this.reversed ? node.Right : node.Left;
      }

      private IBinaryTree<T> PreviousBranch(IBinaryTree<T> node)
      {
        return !this.reversed ? node.Left : node.Right;
      }

      private void ThrowIfDisposed()
      {
        if (this.root == null)
          throw new ObjectDisposedException(this.GetType().FullName);
        if (this.stack == null)
          return;
        this.stack.ThrowDisposedIfNotOwned<ImmutableList<T>.Enumerator>(this);
      }

      private void ThrowIfChanged()
      {
        if (this.builder != null && this.builder.Version != this.enumeratingBuilderVersion)
          throw new InvalidOperationException(Strings.CollectionModifiedDuringEnumeration);
      }

      private void PushNext(IBinaryTree<T> node)
      {
        Requires.NotNull<IBinaryTree<T>>(node, nameof (node));
        if (node.IsEmpty)
          return;
        using (SecurePooledObject<Stack<RefAsValueType<IBinaryTree<T>>>>.SecurePooledObjectUser pooledObjectUser = this.stack.Use<ImmutableList<T>.Enumerator>(this))
        {
          for (; !node.IsEmpty; node = this.PreviousBranch(node))
            pooledObjectUser.Value.Push(new RefAsValueType<IBinaryTree<T>>(node));
        }
      }
    }

    [DebuggerDisplay("{key}")]
    internal sealed class Node : IBinaryTree<T>, IEnumerable<T>, IEnumerable
    {
      internal static readonly ImmutableList<T>.Node EmptyNode = new ImmutableList<T>.Node();
      private T key;
      private bool frozen;
      private int height;
      private int count;
      private ImmutableList<T>.Node left;
      private ImmutableList<T>.Node right;

      private Node() => this.frozen = true;

      private Node(T key, ImmutableList<T>.Node left, ImmutableList<T>.Node right, bool frozen = false)
      {
        Requires.NotNull<ImmutableList<T>.Node>(left, nameof (left));
        Requires.NotNull<ImmutableList<T>.Node>(right, nameof (right));
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

      public ImmutableList<T>.Enumerator GetEnumerator()
      {
        return new ImmutableList<T>.Enumerator((IBinaryTree<T>) this);
      }

      [ExcludeFromCodeCoverage]
      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

      [ExcludeFromCodeCoverage]
      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      internal ImmutableList<T>.Enumerator GetEnumerator(ImmutableList<T>.Builder builder)
      {
        return new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, builder);
      }

      internal static ImmutableList<T>.Node NodeTreeFromList(
        IOrderedCollection<T> items,
        int start,
        int length)
      {
        Requires.NotNull<IOrderedCollection<T>>(items, nameof (items));
        Requires.Range(start >= 0, nameof (start));
        Requires.Range(length >= 0, nameof (length));
        if (length == 0)
          return ImmutableList<T>.Node.EmptyNode;
        int length1 = (length - 1) / 2;
        int length2 = length - 1 - length1;
        ImmutableList<T>.Node left = ImmutableList<T>.Node.NodeTreeFromList(items, start, length2);
        ImmutableList<T>.Node right = ImmutableList<T>.Node.NodeTreeFromList(items, start + length2 + 1, length1);
        return new ImmutableList<T>.Node(items[start + length2], left, right, true);
      }

      internal ImmutableList<T>.Node Add(T key) => this.Insert(this.count, key);

      internal ImmutableList<T>.Node Insert(int index, T key)
      {
        Requires.Range(index >= 0 && index <= this.Count, nameof (index));
        return this.IsEmpty ? new ImmutableList<T>.Node(key, this, this) : ImmutableList<T>.Node.MakeBalanced(index > this.left.count ? this.Mutate(right: this.right.Insert(index - this.left.count - 1, key)) : this.Mutate(this.left.Insert(index, key)));
      }

      internal ImmutableList<T>.Node RemoveAt(int index)
      {
        Requires.Range(index >= 0 && index < this.Count, nameof (index));
        ImmutableList<T>.Node node1 = this;
        ImmutableList<T>.Node tree;
        if (index == this.left.count)
        {
          if (this.right.IsEmpty && this.left.IsEmpty)
            tree = ImmutableList<T>.Node.EmptyNode;
          else if (this.right.IsEmpty && !this.left.IsEmpty)
            tree = this.left;
          else if (!this.right.IsEmpty && this.left.IsEmpty)
          {
            tree = this.right;
          }
          else
          {
            ImmutableList<T>.Node node2 = this.right;
            while (!node2.left.IsEmpty)
              node2 = node2.left;
            ImmutableList<T>.Node right = this.right.RemoveAt(0);
            tree = node2.Mutate(this.left, right);
          }
        }
        else
          tree = index >= this.left.count ? this.Mutate(right: this.right.RemoveAt(index - this.left.count - 1)) : this.Mutate(this.left.RemoveAt(index));
        return !tree.IsEmpty ? ImmutableList<T>.Node.MakeBalanced(tree) : tree;
      }

      internal ImmutableList<T>.Node RemoveAll(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        ImmutableList<T>.Node node = this;
        int index = 0;
        foreach (T obj in this)
        {
          if (match(obj))
            node = node.RemoveAt(index);
          else
            ++index;
        }
        return node;
      }

      internal ImmutableList<T>.Node ReplaceAt(int index, T value)
      {
        Requires.Range(index >= 0 && index < this.Count, nameof (index));
        ImmutableList<T>.Node node = this;
        return index != this.left.count ? (index >= this.left.count ? this.Mutate(right: this.right.ReplaceAt(index - this.left.count - 1, value)) : this.Mutate(this.left.ReplaceAt(index, value))) : this.Mutate(value);
      }

      internal ImmutableList<T>.Node Reverse() => this.Reverse(0, this.Count);

      internal ImmutableList<T>.Node Reverse(int index, int count)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (index));
        ImmutableList<T>.Node node = this;
        int index1 = index;
        for (int index2 = index + count - 1; index1 < index2; --index2)
        {
          T obj1 = node[index1];
          T obj2 = node[index2];
          node = node.ReplaceAt(index2, obj1).ReplaceAt(index1, obj2);
          ++index1;
        }
        return node;
      }

      internal ImmutableList<T>.Node Sort() => this.Sort((IComparer<T>) Comparer<T>.Default);

      internal ImmutableList<T>.Node Sort(Comparison<T> comparison)
      {
        Requires.NotNull<Comparison<T>>(comparison, nameof (comparison));
        T[] objArray = new T[this.Count];
        this.CopyTo(objArray);
        Array.Sort<T>(objArray, comparison);
        return ImmutableList<T>.Node.NodeTreeFromList(((IEnumerable<T>) objArray).AsOrderedCollection<T>(), 0, this.Count);
      }

      internal ImmutableList<T>.Node Sort(IComparer<T> comparer)
      {
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        return this.Sort(0, this.Count, comparer);
      }

      internal ImmutableList<T>.Node Sort(int index, int count, IComparer<T> comparer)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Argument(index + count <= this.Count);
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        T[] objArray = new T[this.Count];
        this.CopyTo(objArray);
        Array.Sort<T>(objArray, index, count, comparer);
        return ImmutableList<T>.Node.NodeTreeFromList(((IEnumerable<T>) objArray).AsOrderedCollection<T>(), 0, this.Count);
      }

      internal int BinarySearch(int index, int count, T item, IComparer<T> comparer)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        comparer = comparer ?? (IComparer<T>) Comparer<T>.Default;
        if (this.IsEmpty || count <= 0)
          return ~index;
        int count1 = this.left.Count;
        if (index + count <= count1)
          return this.left.BinarySearch(index, count, item, comparer);
        if (index > count1)
        {
          int num1 = this.right.BinarySearch(index - count1 - 1, count, item, comparer);
          int num2 = count1 + 1;
          return num1 >= 0 ? num1 + num2 : num1 - num2;
        }
        int num3 = comparer.Compare(item, this.key);
        if (num3 == 0)
          return count1;
        if (num3 > 0)
        {
          int count2 = count - (count1 - index) - 1;
          int num4 = count2 < 0 ? -1 : this.right.BinarySearch(0, count2, item, comparer);
          int num5 = count1 + 1;
          return num4 >= 0 ? num4 + num5 : num4 - num5;
        }
        return index == count1 ? ~index : this.left.BinarySearch(index, count, item, comparer);
      }

      internal int IndexOf(T item, IEqualityComparer<T> equalityComparer)
      {
        return this.IndexOf(item, 0, this.Count, equalityComparer);
      }

      internal int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(count <= this.Count, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (count));
        Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, startIndex: index, count: count))
        {
          while (enumerator.MoveNext())
          {
            if (equalityComparer.Equals(item, enumerator.Current))
              return index;
            ++index;
          }
        }
        return -1;
      }

      internal int LastIndexOf(
        T item,
        int index,
        int count,
        IEqualityComparer<T> equalityComparer)
      {
        Requires.NotNull<IEqualityComparer<T>>(equalityComparer, "ValueComparer");
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0 && count <= this.Count, nameof (count));
        Requires.Argument(index - count + 1 >= 0);
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, startIndex: index, count: count, reversed: true))
        {
          while (enumerator.MoveNext())
          {
            if (equalityComparer.Equals(item, enumerator.Current))
              return index;
            --index;
          }
        }
        return -1;
      }

      internal void CopyTo(T[] array)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Argument(array.Length >= this.Count);
        int num = 0;
        foreach (T obj in this)
          array[num++] = obj;
      }

      internal void CopyTo(T[] array, int arrayIndex)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(arrayIndex <= array.Length, nameof (arrayIndex));
        Requires.Argument(arrayIndex + this.Count <= array.Length);
        foreach (T obj in this)
          array[arrayIndex++] = obj;
      }

      internal void CopyTo(int index, T[] array, int arrayIndex, int count)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (count));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(arrayIndex + count <= array.Length, nameof (arrayIndex));
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, startIndex: index, count: count))
        {
          while (enumerator.MoveNext())
            array[arrayIndex++] = enumerator.Current;
        }
      }

      internal void CopyTo(Array array, int arrayIndex)
      {
        Requires.NotNull<Array>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        foreach (T obj in this)
          array.SetValue((object) obj, arrayIndex++);
      }

      internal ImmutableList<TOutput>.Node ConvertAll<TOutput>(Func<T, TOutput> converter)
      {
        ImmutableList<TOutput>.Node node = ImmutableList<TOutput>.Node.EmptyNode;
        foreach (T obj in this)
          node = node.Add(converter(obj));
        return node;
      }

      internal bool TrueForAll(Predicate<T> match)
      {
        foreach (T obj in this)
        {
          if (!match(obj))
            return false;
        }
        return true;
      }

      internal bool Exists(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        foreach (T obj in this)
        {
          if (match(obj))
            return true;
        }
        return false;
      }

      internal T Find(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        foreach (T obj in this)
        {
          if (match(obj))
            return obj;
        }
        return default (T);
      }

      internal ImmutableList<T> FindAll(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        ImmutableList<T>.Builder builder = ImmutableList<T>.Empty.ToBuilder();
        foreach (T obj in this)
        {
          if (match(obj))
            builder.Add(obj);
        }
        return builder.ToImmutable();
      }

      internal int FindIndex(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.FindIndex(0, this.count, match);
      }

      internal int FindIndex(int startIndex, Predicate<T> match)
      {
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(startIndex <= this.Count, nameof (startIndex));
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.FindIndex(startIndex, this.Count - startIndex, match);
      }

      internal int FindIndex(int startIndex, int count, Predicate<T> match)
      {
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(count >= 0, nameof (count));
        Requires.Argument(startIndex + count <= this.Count);
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, startIndex: startIndex, count: count))
        {
          int index = startIndex;
          while (enumerator.MoveNext())
          {
            if (match(enumerator.Current))
              return index;
            ++index;
          }
        }
        return -1;
      }

      internal T FindLast(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, reversed: true))
        {
          while (enumerator.MoveNext())
          {
            if (match(enumerator.Current))
              return enumerator.Current;
          }
        }
        return default (T);
      }

      internal int FindLastIndex(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.IsEmpty ? -1 : this.FindLastIndex(this.Count - 1, this.Count, match);
      }

      internal int FindLastIndex(int startIndex, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(startIndex == 0 || startIndex < this.Count, nameof (startIndex));
        return this.IsEmpty ? -1 : this.FindLastIndex(startIndex, startIndex + 1, match);
      }

      internal int FindLastIndex(int startIndex, int count, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(count <= this.Count, nameof (count));
        Requires.Argument(startIndex - count + 1 >= 0);
        using (ImmutableList<T>.Enumerator enumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this, startIndex: startIndex, count: count, reversed: true))
        {
          int lastIndex = startIndex;
          while (enumerator.MoveNext())
          {
            if (match(enumerator.Current))
              return lastIndex;
            --lastIndex;
          }
        }
        return -1;
      }

      internal void Freeze()
      {
        if (this.frozen)
          return;
        this.left.Freeze();
        this.right.Freeze();
        this.frozen = true;
      }

      private static ImmutableList<T>.Node RotateLeft(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        if (tree.right.IsEmpty)
          return tree;
        ImmutableList<T>.Node right = tree.right;
        return right.Mutate(tree.Mutate(right: right.left));
      }

      private static ImmutableList<T>.Node RotateRight(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        if (tree.left.IsEmpty)
          return tree;
        ImmutableList<T>.Node left = tree.left;
        return left.Mutate(right: tree.Mutate(left.right));
      }

      private static ImmutableList<T>.Node DoubleLeft(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        return tree.right.IsEmpty ? tree : ImmutableList<T>.Node.RotateLeft(tree.Mutate(right: ImmutableList<T>.Node.RotateRight(tree.right)));
      }

      private static ImmutableList<T>.Node DoubleRight(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        return tree.left.IsEmpty ? tree : ImmutableList<T>.Node.RotateRight(tree.Mutate(ImmutableList<T>.Node.RotateLeft(tree.left)));
      }

      private static int Balance(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        return tree.right.height - tree.left.height;
      }

      private static bool IsRightHeavy(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        return ImmutableList<T>.Node.Balance(tree) >= 2;
      }

      private static bool IsLeftHeavy(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        return ImmutableList<T>.Node.Balance(tree) <= -2;
      }

      private static ImmutableList<T>.Node MakeBalanced(ImmutableList<T>.Node tree)
      {
        Requires.NotNull<ImmutableList<T>.Node>(tree, nameof (tree));
        if (ImmutableList<T>.Node.IsRightHeavy(tree))
          return !ImmutableList<T>.Node.IsLeftHeavy(tree.right) ? ImmutableList<T>.Node.RotateLeft(tree) : ImmutableList<T>.Node.DoubleLeft(tree);
        if (!ImmutableList<T>.Node.IsLeftHeavy(tree))
          return tree;
        return !ImmutableList<T>.Node.IsRightHeavy(tree.left) ? ImmutableList<T>.Node.RotateRight(tree) : ImmutableList<T>.Node.DoubleRight(tree);
      }

      private ImmutableList<T>.Node Mutate(ImmutableList<T>.Node left = null, ImmutableList<T>.Node right = null)
      {
        if (this.frozen)
          return new ImmutableList<T>.Node(this.key, left ?? this.left, right ?? this.right);
        if (left != null)
          this.left = left;
        if (right != null)
          this.right = right;
        this.height = 1 + Math.Max(this.left.height, this.right.height);
        this.count = 1 + this.left.count + this.right.count;
        return this;
      }

      private ImmutableList<T>.Node Mutate(T value)
      {
        if (this.frozen)
          return new ImmutableList<T>.Node(value, this.left, this.right);
        this.key = value;
        return this;
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableList<T>.Node list;
      private T[] cachedContents;

      public DebuggerProxy(ImmutableList<T> list)
      {
        Requires.NotNull<ImmutableList<T>>(list, nameof (list));
        this.list = list.root;
      }

      public DebuggerProxy(ImmutableList<T>.Builder builder)
      {
        Requires.NotNull<ImmutableList<T>.Builder>(builder, nameof (builder));
        this.list = builder.Root;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public T[] Contents
      {
        get
        {
          if (this.cachedContents == null)
            this.cachedContents = this.list.ToArray<T>(this.list.Count);
          return this.cachedContents;
        }
      }
    }

    [DebuggerDisplay("Count = {Count}")]
    [DebuggerTypeProxy(typeof (ImmutableList<>.DebuggerProxy))]
    public sealed class Builder : 
      IList<T>,
      ICollection<T>,
      IList,
      ICollection,
      IOrderedCollection<T>,
      IImmutableListQueries<T>,
      IReadOnlyList<T>,
      IReadOnlyCollection<T>,
      IEnumerable<T>,
      IEnumerable
    {
      private ImmutableList<T>.Node root = ImmutableList<T>.Node.EmptyNode;
      private ImmutableList<T> immutable;
      private int version;
      private object syncRoot;

      internal Builder(ImmutableList<T> list)
      {
        Requires.NotNull<ImmutableList<T>>(list, nameof (list));
        this.root = list.root;
        this.immutable = list;
      }

      public int Count => this.Root.Count;

      bool ICollection<T>.IsReadOnly => false;

      internal int Version => this.version;

      internal ImmutableList<T>.Node Root
      {
        get => this.root;
        private set
        {
          ++this.version;
          if (this.root == value)
            return;
          this.root = value;
          this.immutable = (ImmutableList<T>) null;
        }
      }

      public T this[int index]
      {
        get => this.Root[index];
        set => this.Root = this.Root.ReplaceAt(index, value);
      }

      T IOrderedCollection<T>.this[int index] => this[index];

      public int IndexOf(T item)
      {
        return this.Root.IndexOf(item, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public void Insert(int index, T item) => this.Root = this.Root.Insert(index, item);

      public void RemoveAt(int index) => this.Root = this.Root.RemoveAt(index);

      public void Add(T item) => this.Root = this.Root.Add(item);

      public void Clear() => this.Root = ImmutableList<T>.Node.EmptyNode;

      public bool Contains(T item) => this.IndexOf(item) >= 0;

      public bool Remove(T item)
      {
        int index = this.IndexOf(item);
        if (index < 0)
          return false;
        this.Root = this.Root.RemoveAt(index);
        return true;
      }

      public ImmutableList<T>.Enumerator GetEnumerator() => this.Root.GetEnumerator(this);

      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      public void ForEach(Action<T> action)
      {
        Requires.NotNull<Action<T>>(action, nameof (action));
        foreach (T obj in this)
          action(obj);
      }

      public void CopyTo(T[] array)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(array.Length >= this.Count, nameof (array));
        this.root.CopyTo(array);
      }

      public void CopyTo(T[] array, int arrayIndex)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        this.root.CopyTo(array, arrayIndex);
      }

      public void CopyTo(int index, T[] array, int arrayIndex, int count)
      {
        this.root.CopyTo(index, array, arrayIndex, count);
      }

      public ImmutableList<T> GetRange(int index, int count)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (count));
        return ImmutableList<T>.WrapNode(ImmutableList<T>.Node.NodeTreeFromList((IOrderedCollection<T>) this, index, count));
      }

      public ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter)
      {
        Requires.NotNull<Func<T, TOutput>>(converter, nameof (converter));
        return ImmutableList<TOutput>.WrapNode(this.root.ConvertAll<TOutput>(converter));
      }

      public bool Exists(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.Exists(match);
      }

      public T Find(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.Find(match);
      }

      public ImmutableList<T> FindAll(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.FindAll(match);
      }

      public int FindIndex(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.FindIndex(match);
      }

      public int FindIndex(int startIndex, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(startIndex <= this.Count, nameof (startIndex));
        return this.root.FindIndex(startIndex, match);
      }

      public int FindIndex(int startIndex, int count, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(startIndex + count <= this.Count, nameof (count));
        return this.root.FindIndex(startIndex, count, match);
      }

      public T FindLast(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.FindLast(match);
      }

      public int FindLastIndex(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.FindLastIndex(match);
      }

      public int FindLastIndex(int startIndex, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(startIndex == 0 || startIndex < this.Count, nameof (startIndex));
        return this.root.FindLastIndex(startIndex, match);
      }

      public int FindLastIndex(int startIndex, int count, Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        Requires.Range(startIndex >= 0, nameof (startIndex));
        Requires.Range(count <= this.Count, nameof (count));
        Requires.Range(startIndex - count + 1 >= 0, nameof (startIndex));
        return this.root.FindLastIndex(startIndex, count, match);
      }

      public int IndexOf(T item, int index)
      {
        return this.root.IndexOf(item, index, this.Count - index, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public int IndexOf(T item, int index, int count)
      {
        return this.root.IndexOf(item, index, count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer)
      {
        Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
        return this.root.IndexOf(item, index, count, equalityComparer);
      }

      public int LastIndexOf(T item)
      {
        return this.Count == 0 ? -1 : this.root.LastIndexOf(item, this.Count - 1, this.Count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public int LastIndexOf(T item, int startIndex)
      {
        return this.Count == 0 && startIndex == 0 ? -1 : this.root.LastIndexOf(item, startIndex, startIndex + 1, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public int LastIndexOf(T item, int startIndex, int count)
      {
        return this.root.LastIndexOf(item, startIndex, count, (IEqualityComparer<T>) EqualityComparer<T>.Default);
      }

      public int LastIndexOf(
        T item,
        int startIndex,
        int count,
        IEqualityComparer<T> equalityComparer)
      {
        return this.root.LastIndexOf(item, startIndex, count, equalityComparer);
      }

      public bool TrueForAll(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        return this.root.TrueForAll(match);
      }

      public void InsertRange(int index, IEnumerable<T> items)
      {
        Requires.Range(index >= 0 && index <= this.Count, nameof (index));
        Requires.NotNull<IEnumerable<T>>(items, nameof (items));
        foreach (T key in items)
          this.Root = this.Root.Insert(index++, key);
      }

      public int RemoveAll(Predicate<T> match)
      {
        Requires.NotNull<Predicate<T>>(match, nameof (match));
        int count = this.Count;
        this.Root = this.Root.RemoveAll(match);
        return count - this.Count;
      }

      public void Reverse() => this.Reverse(0, this.Count);

      public void Reverse(int index, int count)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (count));
        this.Root = this.Root.Reverse(index, count);
      }

      public void Sort() => this.Root = this.Root.Sort();

      public void Sort(Comparison<T> comparison)
      {
        Requires.NotNull<Comparison<T>>(comparison, nameof (comparison));
        this.Root = this.Root.Sort(comparison);
      }

      public void Sort(IComparer<T> comparer)
      {
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        this.Root = this.Root.Sort(comparer);
      }

      public void Sort(int index, int count, IComparer<T> comparer)
      {
        Requires.Range(index >= 0, nameof (index));
        Requires.Range(count >= 0, nameof (count));
        Requires.Range(index + count <= this.Count, nameof (count));
        Requires.NotNull<IComparer<T>>(comparer, nameof (comparer));
        this.Root = this.Root.Sort(index, count, comparer);
      }

      public int BinarySearch(T item) => this.BinarySearch(item, (IComparer<T>) null);

      public int BinarySearch(T item, IComparer<T> comparer)
      {
        return this.BinarySearch(0, this.Count, item, comparer);
      }

      public int BinarySearch(int index, int count, T item, IComparer<T> comparer)
      {
        return this.Root.BinarySearch(index, count, item, comparer);
      }

      public ImmutableList<T> ToImmutable()
      {
        if (this.immutable == null)
          this.immutable = ImmutableList<T>.WrapNode(this.Root);
        return this.immutable;
      }

      int IList.Add(object value)
      {
        this.Add((T) value);
        return this.Count - 1;
      }

      void IList.Clear() => this.Clear();

      bool IList.Contains(object value) => this.Contains((T) value);

      int IList.IndexOf(object value) => this.IndexOf((T) value);

      void IList.Insert(int index, object value) => this.Insert(index, (T) value);

      bool IList.IsFixedSize => false;

      bool IList.IsReadOnly => false;

      void IList.Remove(object value) => this.Remove((T) value);

      object IList.this[int index]
      {
        get => (object) this[index];
        set => this[index] = (T) value;
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
    }
  }
}
