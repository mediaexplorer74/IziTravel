﻿// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.IImmutableListQueries`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  internal interface IImmutableListQueries<T> : 
    IReadOnlyList<T>,
    IReadOnlyCollection<T>,
    IEnumerable<T>,
    IEnumerable
  {
    ImmutableList<TOutput> ConvertAll<TOutput>(Func<T, TOutput> converter);

    void ForEach(Action<T> action);

    ImmutableList<T> GetRange(int index, int count);

    void CopyTo(T[] array);

    void CopyTo(T[] array, int arrayIndex);

    void CopyTo(int index, T[] array, int arrayIndex, int count);

    bool Exists(Predicate<T> match);

    T Find(Predicate<T> match);

    ImmutableList<T> FindAll(Predicate<T> match);

    int FindIndex(Predicate<T> match);

    int FindIndex(int startIndex, Predicate<T> match);

    int FindIndex(int startIndex, int count, Predicate<T> match);

    T FindLast(Predicate<T> match);

    int FindLastIndex(Predicate<T> match);

    int FindLastIndex(int startIndex, Predicate<T> match);

    int FindLastIndex(int startIndex, int count, Predicate<T> match);

    bool TrueForAll(Predicate<T> match);

    int BinarySearch(T item);

    int BinarySearch(T item, IComparer<T> comparer);

    int BinarySearch(int index, int count, T item, IComparer<T> comparer);
  }
}
