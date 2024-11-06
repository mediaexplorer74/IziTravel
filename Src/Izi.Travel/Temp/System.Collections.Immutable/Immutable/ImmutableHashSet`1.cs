// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableHashSet`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerDisplay("Count = {Count}")]
  [DebuggerTypeProxy(typeof (ImmutableHashSet<>.DebuggerProxy))]
  public sealed class ImmutableHashSet<T> : 
    IImmutableSet<T>,
    IHashKeyCollection<T>,
    IReadOnlyCollection<T>,
    ISet<T>,
    ICollection<T>,
    IEnumerable<T>,
    ICollection,
    IEnumerable
  {
    public static readonly ImmutableHashSet<T> Empty = new ImmutableHashSet<T>(ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode, (IEqualityComparer<T>) EqualityComparer<T>.Default, 0);
    private static readonly Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>> FreezeBucketAction = (Action<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>>) (kv => kv.Value.Freeze());
    private readonly IEqualityComparer<T> equalityComparer;
    private readonly int count;
    private readonly ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root;

    internal ImmutableHashSet(IEqualityComparer<T> equalityComparer)
      : this(ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode, equalityComparer, 0)
    {
    }

    private ImmutableHashSet(
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
      IEqualityComparer<T> equalityComparer,
      int count)
    {
      Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
      Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
      root.Freeze(ImmutableHashSet<T>.FreezeBucketAction);
      this.root = root;
      this.count = count;
      this.equalityComparer = equalityComparer;
    }

    public ImmutableHashSet<T> Clear()
    {
      return !this.IsEmpty ? ImmutableHashSet<T>.Empty.WithComparer(this.equalityComparer) : this;
    }

    public int Count => this.count;

    public bool IsEmpty => this.Count == 0;

    public IEqualityComparer<T> KeyComparer => this.equalityComparer;

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Clear() => (IImmutableSet<T>) this.Clear();

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    object ICollection.SyncRoot => (object) this;

    [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    bool ICollection.IsSynchronized => true;

    private ImmutableHashSet<T>.MutationInput Origin => new ImmutableHashSet<T>.MutationInput(this);

    public ImmutableHashSet<T>.Builder ToBuilder() => new ImmutableHashSet<T>.Builder(this);

    public ImmutableHashSet<T> Add(T item)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      return ImmutableHashSet<T>.Add(item, this.Origin).Finalize(this);
    }

    public ImmutableHashSet<T> Remove(T item)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      return ImmutableHashSet<T>.Remove(item, this.Origin).Finalize(this);
    }

    public bool TryGetValue(T equalValue, out T actualValue)
    {
      Requires.NotNullAllowStructs<T>(equalValue, "value");
      ImmutableHashSet<T>.HashBucket hashBucket;
      if (this.root.TryGetValue(this.equalityComparer.GetHashCode(equalValue), (IComparer<int>) Comparer<int>.Default, out hashBucket))
        return hashBucket.TryExchange(equalValue, this.equalityComparer, out actualValue);
      actualValue = equalValue;
      return false;
    }

    public ImmutableHashSet<T> Union(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return this.Union(other, false);
    }

    public ImmutableHashSet<T> Intersect(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.Intersect(other, this.Origin).Finalize(this);
    }

    public ImmutableHashSet<T> Except(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.Except(other, this.equalityComparer, this.root).Finalize(this);
    }

    public ImmutableHashSet<T> SymmetricExcept(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.SymmetricExcept(other, this.Origin).Finalize(this);
    }

    public bool SetEquals(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.SetEquals(other, this.Origin);
    }

    public bool IsProperSubsetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
    }

    public bool IsProperSupersetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
    }

    public bool IsSubsetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
    }

    public bool IsSupersetOf(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
    }

    public bool Overlaps(IEnumerable<T> other)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      return ImmutableHashSet<T>.Overlaps(other, this.Origin);
    }

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Add(T item) => (IImmutableSet<T>) this.Add(item);

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Remove(T item) => (IImmutableSet<T>) this.Remove(item);

    [ExcludeFromCodeCoverage]
    IImmutableSet<T> IImmutableSet<T>.Union(IEnumerable<T> other)
    {
      return (IImmutableSet<T>) this.Union(other);
    }

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

    public bool Contains(T item)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      return ImmutableHashSet<T>.Contains(item, this.Origin);
    }

    public ImmutableHashSet<T> WithComparer(IEqualityComparer<T> equalityComparer)
    {
      if (equalityComparer == null)
        equalityComparer = (IEqualityComparer<T>) EqualityComparer<T>.Default;
      return equalityComparer == this.equalityComparer ? this : new ImmutableHashSet<T>(equalityComparer).Union((IEnumerable<T>) this, true);
    }

    bool ISet<T>.Add(T item) => throw new NotSupportedException();

    void ISet<T>.ExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.IntersectWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.SymmetricExceptWith(IEnumerable<T> other) => throw new NotSupportedException();

    void ISet<T>.UnionWith(IEnumerable<T> other) => throw new NotSupportedException();

    bool ICollection<T>.IsReadOnly => true;

    void ICollection<T>.CopyTo(T[] array, int arrayIndex)
    {
      Requires.NotNull<T[]>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (T obj in this)
        array[arrayIndex++] = obj;
    }

    void ICollection<T>.Add(T item) => throw new NotSupportedException();

    void ICollection<T>.Clear() => throw new NotSupportedException();

    bool ICollection<T>.Remove(T item) => throw new NotSupportedException();

    void ICollection.CopyTo(Array array, int arrayIndex)
    {
      Requires.NotNull<Array>(array, nameof (array));
      Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
      Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
      foreach (T obj in this)
        array.SetValue((object) obj, arrayIndex++);
    }

    public ImmutableHashSet<T>.Enumerator GetEnumerator()
    {
      return new ImmutableHashSet<T>.Enumerator(this.root);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    private static bool IsSupersetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      foreach (T obj in other)
      {
        if (!ImmutableHashSet<T>.Contains(obj, origin))
          return false;
      }
      return true;
    }

    private static ImmutableHashSet<T>.MutationResult Add(
      T item,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      int hashCode = origin.EqualityComparer.GetHashCode(item);
      ImmutableHashSet<T>.OperationResult result;
      ImmutableHashSet<T>.HashBucket newBucket = origin.Root.GetValueOrDefault(hashCode, (IComparer<int>) Comparer<int>.Default).Add(item, origin.EqualityComparer, out result);
      return result == ImmutableHashSet<T>.OperationResult.NoChangeRequired ? new ImmutableHashSet<T>.MutationResult(origin.Root, 0) : new ImmutableHashSet<T>.MutationResult(ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, newBucket), 1);
    }

    private static ImmutableHashSet<T>.MutationResult Remove(
      T item,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNullAllowStructs<T>(item, nameof (item));
      ImmutableHashSet<T>.OperationResult result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
      int hashCode = origin.EqualityComparer.GetHashCode(item);
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root = origin.Root;
      ImmutableHashSet<T>.HashBucket hashBucket;
      if (origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket))
      {
        ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(item, origin.EqualityComparer, out result);
        if (result == ImmutableHashSet<T>.OperationResult.NoChangeRequired)
          return new ImmutableHashSet<T>.MutationResult(origin.Root, 0);
        root = ImmutableHashSet<T>.UpdateRoot(origin.Root, hashCode, newBucket);
      }
      return new ImmutableHashSet<T>.MutationResult(root, result == ImmutableHashSet<T>.OperationResult.SizeChanged ? -1 : 0);
    }

    private static bool Contains(T item, ImmutableHashSet<T>.MutationInput origin)
    {
      int hashCode = origin.EqualityComparer.GetHashCode(item);
      ImmutableHashSet<T>.HashBucket hashBucket;
      return origin.Root.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket) && hashBucket.Contains(item, origin.EqualityComparer);
    }

    private static ImmutableHashSet<T>.MutationResult Union(
      IEnumerable<T> other,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      int count = 0;
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root = origin.Root;
      foreach (T obj in other)
      {
        int hashCode = origin.EqualityComparer.GetHashCode(obj);
        ImmutableHashSet<T>.OperationResult result;
        ImmutableHashSet<T>.HashBucket newBucket = root.GetValueOrDefault(hashCode, (IComparer<int>) Comparer<int>.Default).Add(obj, origin.EqualityComparer, out result);
        if (result == ImmutableHashSet<T>.OperationResult.SizeChanged)
        {
          root = ImmutableHashSet<T>.UpdateRoot(root, hashCode, newBucket);
          ++count;
        }
      }
      return new ImmutableHashSet<T>.MutationResult(root, count);
    }

    private static bool Overlaps(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (origin.Root.IsEmpty)
        return false;
      foreach (T obj in other)
      {
        if (ImmutableHashSet<T>.Contains(obj, origin))
          return true;
      }
      return false;
    }

    private static bool SetEquals(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      HashSet<T> objSet = new HashSet<T>(other, origin.EqualityComparer);
      if (origin.Count != objSet.Count)
        return false;
      int num = 0;
      foreach (T obj in objSet)
      {
        if (!ImmutableHashSet<T>.Contains(obj, origin))
          return false;
        ++num;
      }
      return num == origin.Count;
    }

    private static ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node UpdateRoot(
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
      int hashCode,
      ImmutableHashSet<T>.HashBucket newBucket)
    {
      bool mutated;
      return newBucket.IsEmpty ? root.Remove(hashCode, (IComparer<int>) Comparer<int>.Default, out mutated) : root.SetItem(hashCode, newBucket, (IComparer<int>) Comparer<int>.Default, (IEqualityComparer<ImmutableHashSet<T>.HashBucket>) EqualityComparer<ImmutableHashSet<T>.HashBucket>.Default, out bool _, out mutated);
    }

    private static ImmutableHashSet<T>.MutationResult Intersect(
      IEnumerable<T> other,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root = ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode;
      int count = 0;
      foreach (T obj in other)
      {
        if (ImmutableHashSet<T>.Contains(obj, origin))
        {
          ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(obj, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, count));
          root = mutationResult.Root;
          count += mutationResult.Count;
        }
      }
      return new ImmutableHashSet<T>.MutationResult(root, count, ImmutableHashSet<T>.CountType.FinalValue);
    }

    private static ImmutableHashSet<T>.MutationResult Except(
      IEnumerable<T> other,
      IEqualityComparer<T> equalityComparer,
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
      Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
      int count = 0;
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root1 = root;
      foreach (T obj in other)
      {
        int hashCode = equalityComparer.GetHashCode(obj);
        ImmutableHashSet<T>.HashBucket hashBucket;
        if (root1.TryGetValue(hashCode, (IComparer<int>) Comparer<int>.Default, out hashBucket))
        {
          ImmutableHashSet<T>.OperationResult result;
          ImmutableHashSet<T>.HashBucket newBucket = hashBucket.Remove(obj, equalityComparer, out result);
          if (result == ImmutableHashSet<T>.OperationResult.SizeChanged)
          {
            --count;
            root1 = ImmutableHashSet<T>.UpdateRoot(root1, hashCode, newBucket);
          }
        }
      }
      return new ImmutableHashSet<T>.MutationResult(root1, count);
    }

    private static ImmutableHashSet<T>.MutationResult SymmetricExcept(
      IEnumerable<T> other,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      ImmutableHashSet<T> immutableHashSet = ImmutableHashSet<T>.Empty.Union(other);
      int count = 0;
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root = ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode;
      foreach (T obj in new ImmutableHashSet<T>.NodeEnumerable(origin.Root))
      {
        if (!immutableHashSet.Contains(obj))
        {
          ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(obj, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, count));
          root = mutationResult.Root;
          count += mutationResult.Count;
        }
      }
      foreach (T obj in immutableHashSet)
      {
        if (!ImmutableHashSet<T>.Contains(obj, origin))
        {
          ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Add(obj, new ImmutableHashSet<T>.MutationInput(root, origin.EqualityComparer, count));
          root = mutationResult.Root;
          count += mutationResult.Count;
        }
      }
      return new ImmutableHashSet<T>.MutationResult(root, count, ImmutableHashSet<T>.CountType.FinalValue);
    }

    private static bool IsProperSubsetOf(
      IEnumerable<T> other,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (origin.Root.IsEmpty)
        return other.Any<T>();
      HashSet<T> objSet = new HashSet<T>(other, origin.EqualityComparer);
      if (origin.Count >= objSet.Count)
        return false;
      int num = 0;
      bool flag = false;
      foreach (T obj in objSet)
      {
        if (ImmutableHashSet<T>.Contains(obj, origin))
          ++num;
        else
          flag = true;
        if (num == origin.Count && flag)
          return true;
      }
      return false;
    }

    private static bool IsProperSupersetOf(
      IEnumerable<T> other,
      ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (origin.Root.IsEmpty)
        return false;
      int num = 0;
      foreach (T obj in other)
      {
        ++num;
        if (!ImmutableHashSet<T>.Contains(obj, origin))
          return false;
      }
      return origin.Count > num;
    }

    private static bool IsSubsetOf(IEnumerable<T> other, ImmutableHashSet<T>.MutationInput origin)
    {
      Requires.NotNull<IEnumerable<T>>(other, nameof (other));
      if (origin.Root.IsEmpty)
        return true;
      HashSet<T> objSet = new HashSet<T>(other, origin.EqualityComparer);
      int num = 0;
      foreach (T obj in objSet)
      {
        if (ImmutableHashSet<T>.Contains(obj, origin))
          ++num;
      }
      return num == origin.Count;
    }

    private static ImmutableHashSet<T> Wrap(
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
      IEqualityComparer<T> equalityComparer,
      int count)
    {
      Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
      Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
      Requires.Range(count >= 0, nameof (count));
      return new ImmutableHashSet<T>(root, equalityComparer, count);
    }

    private ImmutableHashSet<T> Wrap(
      ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
      int adjustedCountIfDifferentRoot)
    {
      return root == this.root ? this : new ImmutableHashSet<T>(root, this.equalityComparer, adjustedCountIfDifferentRoot);
    }

    private ImmutableHashSet<T> Union(IEnumerable<T> items, bool avoidWithComparer)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      return this.IsEmpty && !avoidWithComparer && items is ImmutableHashSet<T> immutableHashSet ? immutableHashSet.WithComparer(this.KeyComparer) : ImmutableHashSet<T>.Union(items, this.Origin).Finalize(this);
    }

    [DebuggerDisplay("Count = {Count}")]
    public sealed class Builder : 
      IReadOnlyCollection<T>,
      ISet<T>,
      ICollection<T>,
      IEnumerable<T>,
      IEnumerable
    {
      private ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root = ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode;
      private IEqualityComparer<T> equalityComparer;
      private int count;
      private ImmutableHashSet<T> immutable;
      private int version;

      internal Builder(ImmutableHashSet<T> set)
      {
        Requires.NotNull<ImmutableHashSet<T>>(set, nameof (set));
        this.root = set.root;
        this.count = set.count;
        this.equalityComparer = set.equalityComparer;
        this.immutable = set;
      }

      public int Count => this.count;

      bool ICollection<T>.IsReadOnly => false;

      public IEqualityComparer<T> KeyComparer
      {
        get => this.equalityComparer;
        set
        {
          Requires.NotNull<IEqualityComparer<T>>(value, nameof (value));
          if (value == this.equalityComparer)
            return;
          ImmutableHashSet<T>.MutationResult mutationResult = ImmutableHashSet<T>.Union((IEnumerable<T>) this, new ImmutableHashSet<T>.MutationInput(ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode, value, 0));
          this.immutable = (ImmutableHashSet<T>) null;
          this.equalityComparer = value;
          this.Root = mutationResult.Root;
          this.count = mutationResult.Count;
        }
      }

      internal int Version => this.version;

      private ImmutableHashSet<T>.MutationInput Origin
      {
        get => new ImmutableHashSet<T>.MutationInput(this.Root, this.equalityComparer, this.count);
      }

      private ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node Root
      {
        get => this.root;
        set
        {
          ++this.version;
          if (this.root == value)
            return;
          this.root = value;
          this.immutable = (ImmutableHashSet<T>) null;
        }
      }

      public ImmutableHashSet<T>.Enumerator GetEnumerator()
      {
        return new ImmutableHashSet<T>.Enumerator(this.root, this);
      }

      public ImmutableHashSet<T> ToImmutable()
      {
        if (this.immutable == null)
          this.immutable = ImmutableHashSet<T>.Wrap(this.root, this.equalityComparer, this.count);
        return this.immutable;
      }

      public bool Add(T item)
      {
        ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Add(item, this.Origin);
        this.Apply(result);
        return result.Count != 0;
      }

      public bool Remove(T item)
      {
        ImmutableHashSet<T>.MutationResult result = ImmutableHashSet<T>.Remove(item, this.Origin);
        this.Apply(result);
        return result.Count != 0;
      }

      public bool Contains(T item) => ImmutableHashSet<T>.Contains(item, this.Origin);

      public void Clear()
      {
        this.count = 0;
        this.Root = ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node.EmptyNode;
      }

      public void ExceptWith(IEnumerable<T> other)
      {
        this.Apply(ImmutableHashSet<T>.Except(other, this.equalityComparer, this.root));
      }

      public void IntersectWith(IEnumerable<T> other)
      {
        this.Apply(ImmutableHashSet<T>.Intersect(other, this.Origin));
      }

      public bool IsProperSubsetOf(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.IsProperSubsetOf(other, this.Origin);
      }

      public bool IsProperSupersetOf(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.IsProperSupersetOf(other, this.Origin);
      }

      public bool IsSubsetOf(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.IsSubsetOf(other, this.Origin);
      }

      public bool IsSupersetOf(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.IsSupersetOf(other, this.Origin);
      }

      public bool Overlaps(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.Overlaps(other, this.Origin);
      }

      public bool SetEquals(IEnumerable<T> other)
      {
        return ImmutableHashSet<T>.SetEquals(other, this.Origin);
      }

      public void SymmetricExceptWith(IEnumerable<T> other)
      {
        this.Apply(ImmutableHashSet<T>.SymmetricExcept(other, this.Origin));
      }

      public void UnionWith(IEnumerable<T> other)
      {
        this.Apply(ImmutableHashSet<T>.Union(other, this.Origin));
      }

      void ICollection<T>.Add(T item) => this.Add(item);

      void ICollection<T>.CopyTo(T[] array, int arrayIndex)
      {
        Requires.NotNull<T[]>(array, nameof (array));
        Requires.Range(arrayIndex >= 0, nameof (arrayIndex));
        Requires.Range(array.Length >= arrayIndex + this.Count, nameof (arrayIndex));
        foreach (T obj in this)
          array[arrayIndex++] = obj;
      }

      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

      private void Apply(ImmutableHashSet<T>.MutationResult result)
      {
        this.Root = result.Root;
        if (result.CountType == ImmutableHashSet<T>.CountType.Adjustment)
          this.count += result.Count;
        else
          this.count = result.Count;
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableHashSet<T> set;
      private T[] contents;

      public DebuggerProxy(ImmutableHashSet<T> set)
      {
        Requires.NotNull<ImmutableHashSet<T>>(set, nameof (set));
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

    public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
    {
      private readonly ImmutableHashSet<T>.Builder builder;
      private ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Enumerator mapEnumerator;
      private ImmutableHashSet<T>.HashBucket.Enumerator bucketEnumerator;
      private int enumeratingBuilderVersion;

      internal Enumerator(
        ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
        ImmutableHashSet<T>.Builder builder = null)
      {
        this.builder = builder;
        this.mapEnumerator = new ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Enumerator((IBinaryTree<KeyValuePair<int, ImmutableHashSet<T>.HashBucket>>) root);
        this.bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator();
        this.enumeratingBuilderVersion = builder != null ? builder.Version : -1;
      }

      public T Current
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
        this.bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator(this.mapEnumerator.Current.Value);
        return this.bucketEnumerator.MoveNext();
      }

      public void Reset()
      {
        this.enumeratingBuilderVersion = this.builder != null ? this.builder.Version : -1;
        this.mapEnumerator.Reset();
        this.bucketEnumerator.Dispose();
        this.bucketEnumerator = new ImmutableHashSet<T>.HashBucket.Enumerator();
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

    internal enum OperationResult
    {
      SizeChanged,
      NoChangeRequired,
    }

    internal struct HashBucket
    {
      private readonly T firstValue;
      private readonly ImmutableList<T>.Node additionalElements;

      private HashBucket(T firstElement, ImmutableList<T>.Node additionalElements = null)
      {
        this.firstValue = firstElement;
        this.additionalElements = additionalElements ?? ImmutableList<T>.Node.EmptyNode;
      }

      internal bool IsEmpty => this.additionalElements == null;

      public ImmutableHashSet<T>.HashBucket.Enumerator GetEnumerator()
      {
        return new ImmutableHashSet<T>.HashBucket.Enumerator(this);
      }

      internal ImmutableHashSet<T>.HashBucket Add(
        T value,
        IEqualityComparer<T> valueComparer,
        out ImmutableHashSet<T>.OperationResult result)
      {
        if (this.IsEmpty)
        {
          result = ImmutableHashSet<T>.OperationResult.SizeChanged;
          return new ImmutableHashSet<T>.HashBucket(value);
        }
        if (valueComparer.Equals(value, this.firstValue) || this.additionalElements.IndexOf(value, valueComparer) >= 0)
        {
          result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
          return this;
        }
        result = ImmutableHashSet<T>.OperationResult.SizeChanged;
        return new ImmutableHashSet<T>.HashBucket(this.firstValue, this.additionalElements.Add(value));
      }

      internal bool Contains(T value, IEqualityComparer<T> valueComparer)
      {
        if (this.IsEmpty)
          return false;
        return valueComparer.Equals(value, this.firstValue) || this.additionalElements.IndexOf(value, valueComparer) >= 0;
      }

      internal bool TryExchange(T value, IEqualityComparer<T> valueComparer, out T existingValue)
      {
        if (!this.IsEmpty)
        {
          if (valueComparer.Equals(value, this.firstValue))
          {
            existingValue = this.firstValue;
            return true;
          }
          int index = this.additionalElements.IndexOf(value, valueComparer);
          if (index >= 0)
          {
            existingValue = this.additionalElements[index];
            return true;
          }
        }
        existingValue = value;
        return false;
      }

      internal ImmutableHashSet<T>.HashBucket Remove(
        T value,
        IEqualityComparer<T> equalityComparer,
        out ImmutableHashSet<T>.OperationResult result)
      {
        if (this.IsEmpty)
        {
          result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
          return this;
        }
        if (equalityComparer.Equals(this.firstValue, value))
        {
          if (this.additionalElements.IsEmpty)
          {
            result = ImmutableHashSet<T>.OperationResult.SizeChanged;
            return new ImmutableHashSet<T>.HashBucket();
          }
          int count = ((IBinaryTree<T>) this.additionalElements).Left.Count;
          result = ImmutableHashSet<T>.OperationResult.SizeChanged;
          return new ImmutableHashSet<T>.HashBucket(this.additionalElements.Key, this.additionalElements.RemoveAt(count));
        }
        int index = this.additionalElements.IndexOf(value, equalityComparer);
        if (index < 0)
        {
          result = ImmutableHashSet<T>.OperationResult.NoChangeRequired;
          return this;
        }
        result = ImmutableHashSet<T>.OperationResult.SizeChanged;
        return new ImmutableHashSet<T>.HashBucket(this.firstValue, this.additionalElements.RemoveAt(index));
      }

      internal void Freeze()
      {
        if (this.additionalElements == null)
          return;
        this.additionalElements.Freeze();
      }

      internal struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
      {
        private readonly ImmutableHashSet<T>.HashBucket bucket;
        private bool disposed;
        private ImmutableHashSet<T>.HashBucket.Enumerator.Position currentPosition;
        private ImmutableList<T>.Enumerator additionalEnumerator;

        internal Enumerator(ImmutableHashSet<T>.HashBucket bucket)
        {
          this.disposed = false;
          this.bucket = bucket;
          this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
          this.additionalEnumerator = new ImmutableList<T>.Enumerator();
        }

        object IEnumerator.Current => (object) this.Current;

        public T Current
        {
          get
          {
            this.ThrowIfDisposed();
            switch (this.currentPosition)
            {
              case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
                return this.bucket.firstValue;
              case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
                return this.additionalEnumerator.Current;
              default:
                throw new InvalidOperationException();
            }
          }
        }

        public bool MoveNext()
        {
          this.ThrowIfDisposed();
          if (this.bucket.IsEmpty)
          {
            this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
            return false;
          }
          switch (this.currentPosition)
          {
            case ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst:
              this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.First;
              return true;
            case ImmutableHashSet<T>.HashBucket.Enumerator.Position.First:
              if (this.bucket.additionalElements.IsEmpty)
              {
                this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.End;
                return false;
              }
              this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional;
              this.additionalEnumerator = new ImmutableList<T>.Enumerator((IBinaryTree<T>) this.bucket.additionalElements);
              return this.additionalEnumerator.MoveNext();
            case ImmutableHashSet<T>.HashBucket.Enumerator.Position.Additional:
              return this.additionalEnumerator.MoveNext();
            case ImmutableHashSet<T>.HashBucket.Enumerator.Position.End:
              return false;
            default:
              throw new InvalidOperationException();
          }
        }

        public void Reset()
        {
          this.ThrowIfDisposed();
          this.additionalEnumerator.Dispose();
          this.currentPosition = ImmutableHashSet<T>.HashBucket.Enumerator.Position.BeforeFirst;
        }

        public void Dispose()
        {
          this.disposed = true;
          this.additionalEnumerator.Dispose();
        }

        private void ThrowIfDisposed()
        {
          if (this.disposed)
            throw new ObjectDisposedException(this.GetType().FullName);
        }

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
      private readonly ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root;
      private readonly IEqualityComparer<T> equalityComparer;
      private readonly int count;

      internal MutationInput(ImmutableHashSet<T> set)
      {
        Requires.NotNull<ImmutableHashSet<T>>(set, nameof (set));
        this.root = set.root;
        this.equalityComparer = set.equalityComparer;
        this.count = set.count;
      }

      internal MutationInput(
        ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
        IEqualityComparer<T> equalityComparer,
        int count)
      {
        Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
        Requires.NotNull<IEqualityComparer<T>>(equalityComparer, nameof (equalityComparer));
        Requires.Range(count >= 0, nameof (count));
        this.root = root;
        this.equalityComparer = equalityComparer;
        this.count = count;
      }

      internal ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node Root
      {
        get => this.root;
      }

      internal IEqualityComparer<T> EqualityComparer => this.equalityComparer;

      internal int Count => this.count;
    }

    private enum CountType
    {
      Adjustment,
      FinalValue,
    }

    private struct MutationResult
    {
      private readonly ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root;
      private readonly int count;
      private readonly ImmutableHashSet<T>.CountType countType;

      internal MutationResult(
        ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root,
        int count,
        ImmutableHashSet<T>.CountType countType = ImmutableHashSet<T>.CountType.Adjustment)
      {
        Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
        this.root = root;
        this.count = count;
        this.countType = countType;
      }

      internal ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node Root
      {
        get => this.root;
      }

      internal int Count => this.count;

      internal ImmutableHashSet<T>.CountType CountType => this.countType;

      internal ImmutableHashSet<T> Finalize(ImmutableHashSet<T> priorSet)
      {
        Requires.NotNull<ImmutableHashSet<T>>(priorSet, nameof (priorSet));
        int count = this.Count;
        if (this.CountType == ImmutableHashSet<T>.CountType.Adjustment)
          count += priorSet.count;
        return priorSet.Wrap(this.Root, count);
      }
    }

    private struct NodeEnumerable : IEnumerable<T>, IEnumerable
    {
      private readonly ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root;

      internal NodeEnumerable(
        ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node root)
      {
        Requires.NotNull<ImmutableSortedDictionary<int, ImmutableHashSet<T>.HashBucket>.Node>(root, nameof (root));
        this.root = root;
      }

      public ImmutableHashSet<T>.Enumerator GetEnumerator()
      {
        return new ImmutableHashSet<T>.Enumerator(this.root);
      }

      [ExcludeFromCodeCoverage]
      IEnumerator<T> IEnumerable<T>.GetEnumerator() => (IEnumerator<T>) this.GetEnumerator();

      [ExcludeFromCodeCoverage]
      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }
  }
}
