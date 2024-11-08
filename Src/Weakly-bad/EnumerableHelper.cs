// Decompiled with JetBrains decompiler
// Type: Weakly.EnumerableHelper
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Weakly
{
  /// <summary>
  /// Common extensions to <see cref="T:System.Collections.Generic.IEnumerable`1" />
  /// </summary>
  public static class EnumerableHelper
  {
    /// <summary>
    /// Performs the specified action on each element of the <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    /// <param name="source">The enumerable.</param>
    /// <param name="action">The <see cref="T:System.Action`1" /> delegate to perform on each element of the <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
      foreach (T obj in source)
        action(obj);
    }

    /// <summary>
    /// Performs the specified asynchronous action on each element of the <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    /// <param name="source">The enumerable.</param>
    /// <param name="asyncAction">The <see cref="T:System.Func`2" /> delegate to perform on each element of the <see cref="T:System.Collections.Generic.IEnumerable`1" />.</param>
    /// <returns>A task that represents the completion.</returns>
    public static Task ForEachAsync<T>(this IEnumerable<T> source, Func<T, Task> asyncAction)
    {
      return Task.WhenAll(source.Select<T, Task>(asyncAction));
    }

    /// <summary>
    /// Projects asynchronous each element of a sequence into a new form.
    /// </summary>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by asyncSelector.</typeparam>
    /// <param name="source">A sequence of values to invoke a transform function on.</param>
    /// <param name="asyncSelector">An asynchronous transform function to apply to each element.</param>
    /// <returns></returns>
    public static Task<TResult[]> SelectAsync<TSource, TResult>(
      this IEnumerable<TSource> source,
      Func<TSource, Task<TResult>> asyncSelector)
    {
      return Task.WhenAll<TResult>(source.Select<TSource, Task<TResult>>(asyncSelector));
    }

    /// <summary>
    /// Gets the value for a key. If the key does not exist, return default(TValue);
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of the values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary to call this method on.</param>
    /// <param name="key">The key to look up.</param>
    /// <returns>The key value. default(TValue) if this key is not in the dictionary.</returns>
    public static TValue GetValueOrDefault<TKey, TValue>(
      this IDictionary<TKey, TValue> dictionary,
      TKey key)
    {
      TValue obj;
      return !dictionary.TryGetValue(key, out obj) ? default (TValue) : obj;
    }
  }
}
