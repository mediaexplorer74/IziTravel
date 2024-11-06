// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.IImmutableSet`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;

#nullable disable
namespace System.Collections.Immutable
{
  public interface IImmutableSet<T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable
  {
    IImmutableSet<T> Clear();

    bool Contains(T value);

    IImmutableSet<T> Add(T value);

    IImmutableSet<T> Remove(T value);

    bool TryGetValue(T equalValue, out T actualValue);

    IImmutableSet<T> Intersect(IEnumerable<T> other);

    IImmutableSet<T> Except(IEnumerable<T> other);

    IImmutableSet<T> SymmetricExcept(IEnumerable<T> other);

    IImmutableSet<T> Union(IEnumerable<T> other);

    bool SetEquals(IEnumerable<T> other);

    bool IsProperSubsetOf(IEnumerable<T> other);

    bool IsProperSupersetOf(IEnumerable<T> other);

    bool IsSubsetOf(IEnumerable<T> other);

    bool IsSupersetOf(IEnumerable<T> other);

    bool Overlaps(IEnumerable<T> other);
  }
}
