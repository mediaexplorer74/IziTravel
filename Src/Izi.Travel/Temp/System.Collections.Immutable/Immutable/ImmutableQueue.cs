// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableQueue
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableQueue
  {
    public static ImmutableQueue<T> Create<T>() => ImmutableQueue<T>.Empty;

    public static ImmutableQueue<T> Create<T>(T item) => ImmutableQueue<T>.Empty.Enqueue(item);

    public static ImmutableQueue<T> CreateRange<T>(IEnumerable<T> items)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      ImmutableQueue<T> range = ImmutableQueue<T>.Empty;
      foreach (T obj in items)
        range = range.Enqueue(obj);
      return range;
    }

    public static ImmutableQueue<T> Create<T>(params T[] items)
    {
      Requires.NotNull<T[]>(items, nameof (items));
      ImmutableQueue<T> immutableQueue = ImmutableQueue<T>.Empty;
      foreach (T obj in items)
        immutableQueue = immutableQueue.Enqueue(obj);
      return immutableQueue;
    }

    public static IImmutableQueue<T> Dequeue<T>(this IImmutableQueue<T> queue, out T value)
    {
      Requires.NotNull<IImmutableQueue<T>>(queue, nameof (queue));
      value = queue.Peek();
      return queue.Dequeue();
    }
  }
}
