// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.IImmutableList`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  public interface IImmutableList<T> : 
    IReadOnlyList<T>,
    IReadOnlyCollection<T>,
    IEnumerable<T>,
    IEnumerable
  {
    IImmutableList<T> Clear();

    int IndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

    int LastIndexOf(T item, int index, int count, IEqualityComparer<T> equalityComparer);

    IImmutableList<T> Add(T value);

    IImmutableList<T> AddRange(IEnumerable<T> items);

    IImmutableList<T> Insert(int index, T element);

    IImmutableList<T> InsertRange(int index, IEnumerable<T> items);

    IImmutableList<T> Remove(T value, IEqualityComparer<T> equalityComparer);

    IImmutableList<T> RemoveAll(Predicate<T> match);

    IImmutableList<T> RemoveRange(IEnumerable<T> items, IEqualityComparer<T> equalityComparer);

    IImmutableList<T> RemoveRange(int index, int count);

    IImmutableList<T> RemoveAt(int index);

    IImmutableList<T> SetItem(int index, T value);

    IImmutableList<T> Replace(T oldValue, T newValue, IEqualityComparer<T> equalityComparer);
  }
}
