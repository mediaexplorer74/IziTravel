// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableStack`1
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  [DebuggerDisplay("IsEmpty = {IsEmpty}; Top = {head}")]
  [DebuggerTypeProxy(typeof (ImmutableStack<>.DebuggerProxy))]
  public sealed class ImmutableStack<T> : IImmutableStack<T>, IEnumerable<T>, IEnumerable
  {
    private static readonly ImmutableStack<T> EmptyField = new ImmutableStack<T>();
    private readonly T head;
    private readonly ImmutableStack<T> tail;

    private ImmutableStack()
    {
    }

    private ImmutableStack(T head, ImmutableStack<T> tail)
    {
      Requires.NotNull<ImmutableStack<T>>(tail, nameof (tail));
      this.head = head;
      this.tail = tail;
    }

    public static ImmutableStack<T> Empty => ImmutableStack<T>.EmptyField;

    public ImmutableStack<T> Clear() => ImmutableStack<T>.Empty;

    IImmutableStack<T> IImmutableStack<T>.Clear() => (IImmutableStack<T>) this.Clear();

    public bool IsEmpty => this.tail == null;

    public T Peek()
    {
      if (this.IsEmpty)
        throw new InvalidOperationException(Strings.InvalidEmptyOperation);
      return this.head;
    }

    public ImmutableStack<T> Push(T value) => new ImmutableStack<T>(value, this);

    IImmutableStack<T> IImmutableStack<T>.Push(T value) => (IImmutableStack<T>) this.Push(value);

    public ImmutableStack<T> Pop()
    {
      if (this.IsEmpty)
        throw new InvalidOperationException(Strings.InvalidEmptyOperation);
      return this.tail;
    }

    public ImmutableStack<T> Pop(out T value)
    {
      value = this.Peek();
      return this.Pop();
    }

    IImmutableStack<T> IImmutableStack<T>.Pop() => (IImmutableStack<T>) this.Pop();

    public ImmutableStack<T>.Enumerator GetEnumerator() => new ImmutableStack<T>.Enumerator(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new ImmutableStack<T>.EnumeratorObject(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ImmutableStack<T>.EnumeratorObject(this);
    }

    internal ImmutableStack<T> Reverse()
    {
      ImmutableStack<T> immutableStack1 = this.Clear();
      for (ImmutableStack<T> immutableStack2 = this; !immutableStack2.IsEmpty; immutableStack2 = immutableStack2.Pop())
        immutableStack1 = immutableStack1.Push(immutableStack2.Peek());
      return immutableStack1;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct Enumerator
    {
      private readonly ImmutableStack<T> originalStack;
      private ImmutableStack<T> remainingStack;

      internal Enumerator(ImmutableStack<T> stack)
      {
        Requires.NotNull<ImmutableStack<T>>(stack, nameof (stack));
        this.originalStack = stack;
        this.remainingStack = (ImmutableStack<T>) null;
      }

      public T Current
      {
        get
        {
          if (this.remainingStack == null || this.remainingStack.IsEmpty)
            throw new InvalidOperationException();
          return this.remainingStack.Peek();
        }
      }

      public bool MoveNext()
      {
        if (this.remainingStack == null)
          this.remainingStack = this.originalStack;
        else if (!this.remainingStack.IsEmpty)
          this.remainingStack = this.remainingStack.Pop();
        return !this.remainingStack.IsEmpty;
      }
    }

    private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
    {
      private readonly ImmutableStack<T> originalStack;
      private ImmutableStack<T> remainingStack;
      private bool disposed;

      internal EnumeratorObject(ImmutableStack<T> stack)
      {
        Requires.NotNull<ImmutableStack<T>>(stack, nameof (stack));
        this.originalStack = stack;
      }

      public T Current
      {
        get
        {
          this.ThrowIfDisposed();
          return this.remainingStack != null && !this.remainingStack.IsEmpty ? this.remainingStack.Peek() : throw new InvalidOperationException();
        }
      }

      object IEnumerator.Current => (object) this.Current;

      public bool MoveNext()
      {
        this.ThrowIfDisposed();
        if (this.remainingStack == null)
          this.remainingStack = this.originalStack;
        else if (!this.remainingStack.IsEmpty)
          this.remainingStack = this.remainingStack.Pop();
        return !this.remainingStack.IsEmpty;
      }

      public void Reset()
      {
        this.ThrowIfDisposed();
        this.remainingStack = (ImmutableStack<T>) null;
      }

      public void Dispose() => this.disposed = true;

      private void ThrowIfDisposed()
      {
        if (this.disposed)
          throw new ObjectDisposedException(this.GetType().FullName);
      }
    }

    [ExcludeFromCodeCoverage]
    private class DebuggerProxy
    {
      private readonly ImmutableStack<T> stack;
      private T[] contents;

      public DebuggerProxy(ImmutableStack<T> stack)
      {
        Requires.NotNull<ImmutableStack<T>>(stack, nameof (stack));
        this.stack = stack;
      }

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public T[] Contents
      {
        get
        {
          if (this.contents == null)
            this.contents = this.stack.ToArray<T>();
          return this.contents;
        }
      }
    }
  }
}
