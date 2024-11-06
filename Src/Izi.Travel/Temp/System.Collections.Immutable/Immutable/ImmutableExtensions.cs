// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableExtensions
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  internal static class ImmutableExtensions
  {
    internal static bool TryGetCount<T>(this IEnumerable<T> sequence, out int count)
    {
      return sequence.TryGetCount<T>(out count);
    }

    internal static bool TryGetCount<T>(this IEnumerable sequence, out int count)
    {
      switch (sequence)
      {
        case ICollection collection:
          count = collection.Count;
          return true;
        case ICollection<T> objs1:
          count = objs1.Count;
          return true;
        case IReadOnlyCollection<T> objs2:
          count = objs2.Count;
          return true;
        default:
          count = 0;
          return false;
      }
    }

    internal static int GetCount<T>(ref IEnumerable<T> sequence)
    {
      int count;
      if (!sequence.TryGetCount<T>(out count))
      {
        List<T> list = sequence.ToList<T>();
        count = list.Count;
        sequence = (IEnumerable<T>) list;
      }
      return count;
    }

    internal static T[] ToArray<T>(this IEnumerable<T> sequence, int count)
    {
      Requires.NotNull<IEnumerable<T>>(sequence, nameof (sequence));
      Requires.Range(count >= 0, nameof (count));
      T[] array = new T[count];
      int num = 0;
      foreach (T obj in sequence)
      {
        Requires.Argument(num < count);
        array[num++] = obj;
      }
      Requires.Argument(num == count);
      return array;
    }

    internal static IOrderedCollection<T> AsOrderedCollection<T>(this IEnumerable<T> sequence)
    {
      Requires.NotNull<IEnumerable<T>>(sequence, nameof (sequence));
      switch (sequence)
      {
        case IOrderedCollection<T> orderedCollection:
          return orderedCollection;
        case IList<T> collection:
          return (IOrderedCollection<T>) new ImmutableExtensions.ListOfTWrapper<T>(collection);
        default:
          return (IOrderedCollection<T>) new ImmutableExtensions.FallbackWrapper<T>(sequence);
      }
    }

    private class ListOfTWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
    {
      private readonly IList<T> collection;

      internal ListOfTWrapper(IList<T> collection)
      {
        Requires.NotNull<IList<T>>(collection, nameof (collection));
        this.collection = collection;
      }

      public int Count => this.collection.Count;

      public T this[int index] => this.collection[index];

      public IEnumerator<T> GetEnumerator() => this.collection.GetEnumerator();

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }

    private class FallbackWrapper<T> : IOrderedCollection<T>, IEnumerable<T>, IEnumerable
    {
      private readonly IEnumerable<T> sequence;
      private IList<T> collection;

      internal FallbackWrapper(IEnumerable<T> sequence)
      {
        Requires.NotNull<IEnumerable<T>>(sequence, nameof (sequence));
        this.sequence = sequence;
      }

      public int Count
      {
        get
        {
          if (this.collection == null)
          {
            int count;
            if (this.sequence.TryGetCount<T>(out count))
              return count;
            this.collection = (IList<T>) this.sequence.ToArray<T>();
          }
          return this.collection.Count;
        }
      }

      public T this[int index]
      {
        get
        {
          if (this.collection == null)
            this.collection = (IList<T>) this.sequence.ToArray<T>();
          return this.collection[index];
        }
      }

      public IEnumerator<T> GetEnumerator() => this.sequence.GetEnumerator();

      [ExcludeFromCodeCoverage]
      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
    }
  }
}
