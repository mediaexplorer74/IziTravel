// Decompiled with JetBrains decompiler
// Type: System.Collections.Immutable.ImmutableInterlocked
// Assembly: System.Collections.Immutable, Version=1.0.34.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: BD72C27E-D8D4-45DB-AA51-7FAB6CCBDAA2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\System.Collections.Immutable.xml

using System.Collections.Generic;
using System.Threading;
using Validation;

#nullable disable
namespace System.Collections.Immutable
{
  public static class ImmutableInterlocked
  {
    public static TValue GetOrAdd<TKey, TValue, TArg>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      Func<TKey, TArg, TValue> valueFactory,
      TArg factoryArgument)
    {
      Requires.NotNull<Func<TKey, TArg, TValue>>(valueFactory, nameof (valueFactory));
      ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, nameof (location));
      TValue orAdd;
      if (immutableDictionary.TryGetValue(key, out orAdd))
        return orAdd;
      TValue obj = valueFactory(key, factoryArgument);
      return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, obj);
    }

    public static TValue GetOrAdd<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      Func<TKey, TValue> valueFactory)
    {
      Requires.NotNull<Func<TKey, TValue>>(valueFactory, nameof (valueFactory));
      ImmutableDictionary<TKey, TValue> immutableDictionary = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary, nameof (location));
      TValue orAdd;
      if (immutableDictionary.TryGetValue(key, out orAdd))
        return orAdd;
      TValue obj = valueFactory(key);
      return ImmutableInterlocked.GetOrAdd<TKey, TValue>(ref location, key, obj);
    }

    public static TValue GetOrAdd<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      TValue value)
    {
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        TValue orAdd;
        if (immutableDictionary1.TryGetValue(key, out orAdd))
          return orAdd;
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.Add(key, value);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return value;
    }

    public static TValue AddOrUpdate<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      Func<TKey, TValue> addValueFactory,
      Func<TKey, TValue, TValue> updateValueFactory)
    {
      Requires.NotNull<Func<TKey, TValue>>(addValueFactory, nameof (addValueFactory));
      Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, nameof (updateValueFactory));
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      TValue obj1;
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        TValue obj2;
        obj1 = !immutableDictionary1.TryGetValue(key, out obj2) ? addValueFactory(key) : updateValueFactory(key, obj2);
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.SetItem(key, obj1);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return obj1;
    }

    public static TValue AddOrUpdate<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      TValue addValue,
      Func<TKey, TValue, TValue> updateValueFactory)
    {
      Requires.NotNull<Func<TKey, TValue, TValue>>(updateValueFactory, nameof (updateValueFactory));
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      TValue obj1;
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        TValue obj2;
        obj1 = !immutableDictionary1.TryGetValue(key, out obj2) ? addValue : updateValueFactory(key, obj2);
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.SetItem(key, obj1);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return obj1;
    }

    public static bool TryAdd<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      TValue value)
    {
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        if (immutableDictionary1.ContainsKey(key))
          return false;
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.Add(key, value);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return true;
    }

    public static bool TryUpdate<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      TValue newValue,
      TValue comparisonValue)
    {
      EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        TValue x;
        if (!immutableDictionary1.TryGetValue(key, out x) || !equalityComparer.Equals(x, comparisonValue))
          return false;
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.SetItem(key, newValue);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return true;
    }

    public static bool TryRemove<TKey, TValue>(
      ref ImmutableDictionary<TKey, TValue> location,
      TKey key,
      out TValue value)
    {
      ImmutableDictionary<TKey, TValue> immutableDictionary1 = Volatile.Read<ImmutableDictionary<TKey, TValue>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableDictionary<TKey, TValue>>(immutableDictionary1, nameof (location));
        if (!immutableDictionary1.TryGetValue(key, out value))
          return false;
        ImmutableDictionary<TKey, TValue> immutableDictionary2 = immutableDictionary1.Remove(key);
        ImmutableDictionary<TKey, TValue> objB = Interlocked.CompareExchange<ImmutableDictionary<TKey, TValue>>(ref location, immutableDictionary2, immutableDictionary1);
        flag = object.ReferenceEquals((object) immutableDictionary1, (object) objB);
        immutableDictionary1 = objB;
      }
      while (!flag);
      return true;
    }

    public static bool TryPop<T>(ref ImmutableStack<T> location, out T value)
    {
      ImmutableStack<T> immutableStack1 = Volatile.Read<ImmutableStack<T>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableStack<T>>(immutableStack1, nameof (location));
        if (immutableStack1.IsEmpty)
        {
          value = default (T);
          return false;
        }
        ImmutableStack<T> immutableStack2 = immutableStack1.Pop(out value);
        ImmutableStack<T> objB = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack1);
        flag = object.ReferenceEquals((object) immutableStack1, (object) objB);
        immutableStack1 = objB;
      }
      while (!flag);
      return true;
    }

    public static void Push<T>(ref ImmutableStack<T> location, T value)
    {
      ImmutableStack<T> immutableStack1 = Volatile.Read<ImmutableStack<T>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableStack<T>>(immutableStack1, nameof (location));
        ImmutableStack<T> immutableStack2 = immutableStack1.Push(value);
        ImmutableStack<T> objB = Interlocked.CompareExchange<ImmutableStack<T>>(ref location, immutableStack2, immutableStack1);
        flag = object.ReferenceEquals((object) immutableStack1, (object) objB);
        immutableStack1 = objB;
      }
      while (!flag);
    }

    public static bool TryDequeue<T>(ref ImmutableQueue<T> location, out T value)
    {
      ImmutableQueue<T> immutableQueue1 = Volatile.Read<ImmutableQueue<T>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableQueue<T>>(immutableQueue1, nameof (location));
        if (immutableQueue1.IsEmpty)
        {
          value = default (T);
          return false;
        }
        ImmutableQueue<T> immutableQueue2 = immutableQueue1.Dequeue(out value);
        ImmutableQueue<T> objB = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue1);
        flag = object.ReferenceEquals((object) immutableQueue1, (object) objB);
        immutableQueue1 = objB;
      }
      while (!flag);
      return true;
    }

    public static void Enqueue<T>(ref ImmutableQueue<T> location, T value)
    {
      ImmutableQueue<T> immutableQueue1 = Volatile.Read<ImmutableQueue<T>>(ref location);
      bool flag;
      do
      {
        Requires.NotNull<ImmutableQueue<T>>(immutableQueue1, nameof (location));
        ImmutableQueue<T> immutableQueue2 = immutableQueue1.Enqueue(value);
        ImmutableQueue<T> objB = Interlocked.CompareExchange<ImmutableQueue<T>>(ref location, immutableQueue2, immutableQueue1);
        flag = object.ReferenceEquals((object) immutableQueue1, (object) objB);
        immutableQueue1 = objB;
      }
      while (!flag);
    }
  }
}
