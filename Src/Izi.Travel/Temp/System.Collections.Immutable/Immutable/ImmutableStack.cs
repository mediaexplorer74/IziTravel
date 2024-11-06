// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableStack
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableStack
  {
    public static ImmutableStack<T> Create<T>() => ImmutableStack<T>.Empty;

    public static ImmutableStack<T> Create<T>(T item) => ImmutableStack<T>.Empty.Push(item);

    public static ImmutableStack<T> CreateRange<T>(IEnumerable<T> items)
    {
      Requires.NotNull<IEnumerable<T>>(items, nameof (items));
      ImmutableStack<T> range = ImmutableStack<T>.Empty;
      foreach (T obj in items)
        range = range.Push(obj);
      return range;
    }

    public static ImmutableStack<T> Create<T>(params T[] items)
    {
      Requires.NotNull<T[]>(items, nameof (items));
      ImmutableStack<T> immutableStack = ImmutableStack<T>.Empty;
      foreach (T obj in items)
        immutableStack = immutableStack.Push(obj);
      return immutableStack;
    }

    public static IImmutableStack<T> Pop<T>(this IImmutableStack<T> stack, out T value)
    {
      Requires.NotNull<IImmutableStack<T>>(stack, nameof (stack));
      value = stack.Peek();
      return stack.Pop();
    }
  }
}
