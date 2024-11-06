// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableQueue`1
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
  [DebuggerTypeProxy(typeof (ImmutableQueue<>.DebuggerProxy))]
  [DebuggerDisplay("IsEmpty = {IsEmpty}")]
  public sealed class ImmutableQueue<T> : IImmutableQueue<T>, IEnumerable<T>, IEnumerable
  {
    private static readonly ImmutableQueue<T> EmptyField = new ImmutableQueue<T>(ImmutableStack<T>.Empty, ImmutableStack<T>.Empty);
    private readonly ImmutableStack<T> backwards;
    private readonly ImmutableStack<T> forwards;
    private ImmutableStack<T> backwardsReversed;

    private ImmutableQueue(ImmutableStack<T> forward, ImmutableStack<T> backward)
    {
      Requires.NotNull<ImmutableStack<T>>(forward, nameof (forward));
      Requires.NotNull<ImmutableStack<T>>(backward, nameof (backward));
      this.forwards = forward;
      this.backwards = backward;
      this.backwardsReversed = (ImmutableStack<T>) null;
    }

    public ImmutableQueue<T> Clear() => ImmutableQueue<T>.Empty;

    public bool IsEmpty => this.forwards.IsEmpty && this.backwards.IsEmpty;

    public static ImmutableQueue<T> Empty => ImmutableQueue<T>.EmptyField;

    IImmutableQueue<T> IImmutableQueue<T>.Clear() => (IImmutableQueue<T>) this.Clear();

    private ImmutableStack<T> BackwardsReversed
    {
      get
      {
        if (this.backwardsReversed == null)
          this.backwardsReversed = this.backwards.Reverse();
        return this.backwardsReversed;
      }
    }

    public T Peek()
    {
      if (this.IsEmpty)
        throw new InvalidOperationException(Strings.InvalidEmptyOperation);
      return this.forwards.Peek();
    }

    public ImmutableQueue<T> Enqueue(T value)
    {
      return this.IsEmpty ? new ImmutableQueue<T>(ImmutableStack<T>.Empty.Push(value), ImmutableStack<T>.Empty) : new ImmutableQueue<T>(this.forwards, this.backwards.Push(value));
    }

    IImmutableQueue<T> IImmutableQueue<T>.Enqueue(T value)
    {
      return (IImmutableQueue<T>) this.Enqueue(value);
    }

    public ImmutableQueue<T> Dequeue()
    {
      if (this.IsEmpty)
        throw new InvalidOperationException(Strings.InvalidEmptyOperation);
      ImmutableStack<T> forward = this.forwards.Pop();
      if (!forward.IsEmpty)
        return new ImmutableQueue<T>(forward, this.backwards);
      return this.backwards.IsEmpty ? ImmutableQueue<T>.Empty : new ImmutableQueue<T>(this.BackwardsReversed, ImmutableStack<T>.Empty);
    }

    public ImmutableQueue<T> Dequeue(out T value)
    {
      value = this.Peek();
      return this.Dequeue();
    }

    IImmutableQueue<T> IImmutableQueue<T>.Dequeue() => (IImmutableQueue<T>) this.Dequeue();

    public ImmutableQueue<T>.Enumerator GetEnumerator() => new ImmutableQueue<T>.Enumerator(this);

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
      return (IEnumerator<T>) new ImmutableQueue<T>.EnumeratorObject(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return (IEnumerator) new ImmutableQueue<T>.EnumeratorObject(this);
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public struct Enumerator
    {
      private readonly ImmutableQueue<T> originalQueue;
      private ImmutableStack<T> remainingForwardsStack;
      private ImmutableStack<T> remainingBackwardsStack;

      internal Enumerator(ImmutableQueue<T> queue)
      {
        this.originalQueue = queue;
        this.remainingForwardsStack = (ImmutableStack<T>) null;
        this.remainingBackwardsStack = (ImmutableStack<T>) null;
      }

      public T Current
      {
        get
        {
          if (this.remainingForwardsStack == null)
            throw new InvalidOperationException();
          if (!this.remainingForwardsStack.IsEmpty)
            return this.remainingForwardsStack.Peek();
          if (!this.remainingBackwardsStack.IsEmpty)
            return this.remainingBackwardsStack.Peek();
          throw new InvalidOperationException();
        }
      }

      public bool MoveNext()
      {
        if (this.remainingForwardsStack == null)
        {
          this.remainingForwardsStack = this.originalQueue.forwards;
          this.remainingBackwardsStack = this.originalQueue.BackwardsReversed;
        }
        else if (!this.remainingForwardsStack.IsEmpty)
          this.remainingForwardsStack = this.remainingForwardsStack.Pop();
        else if (!this.remainingBackwardsStack.IsEmpty)
          this.remainingBackwardsStack = this.remainingBackwardsStack.Pop();
        return !this.remainingForwardsStack.IsEmpty || !this.remainingBackwardsStack.IsEmpty;
      }
    }

    private class EnumeratorObject : IEnumerator<T>, IEnumerator, IDisposable
    {
      private readonly ImmutableQueue<T> originalQueue;
      private ImmutableStack<T> remainingForwardsStack;
      private ImmutableStack<T> remainingBackwardsStack;
      private bool disposed;

      internal EnumeratorObject(ImmutableQueue<T> queue) => this.originalQueue = queue;

      public T Current
      {
        get
        {
          this.ThrowIfDisposed();
          if (this.remainingForwardsStack == null)
            throw new InvalidOperationException();
          if (!this.remainingForwardsStack.IsEmpty)
            return this.remainingForwardsStack.Peek();
          return !this.remainingBackwardsStack.IsEmpty ? this.remainingBackwardsStack.Peek() : throw new InvalidOperationException();
        }
      }

      object IEnumerator.Current => (object) this.Current;

      public bool MoveNext()
      {
        this.ThrowIfDisposed();
        if (this.remainingForwardsStack == null)
        {
          this.remainingForwardsStack = this.originalQueue.forwards;
          this.remainingBackwardsStack = this.originalQueue.BackwardsReversed;
        }
        else if (!this.remainingForwardsStack.IsEmpty)
          this.remainingForwardsStack = this.remainingForwardsStack.Pop();
        else if (!this.remainingBackwardsStack.IsEmpty)
          this.remainingBackwardsStack = this.remainingBackwardsStack.Pop();
        return !this.remainingForwardsStack.IsEmpty || !this.remainingBackwardsStack.IsEmpty;
      }

      public void Reset()
      {
        this.ThrowIfDisposed();
        this.remainingBackwardsStack = (ImmutableStack<T>) null;
        this.remainingForwardsStack = (ImmutableStack<T>) null;
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
      private readonly ImmutableQueue<T> queue;
      private T[] contents;

      public DebuggerProxy(ImmutableQueue<T> queue) => this.queue = queue;

      [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
      public T[] Contents
      {
        get
        {
          if (this.contents == null)
            this.contents = this.queue.ToArray<T>();
          return this.contents;
        }
      }
    }
  }
}
