// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.Extensions.CollectionExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Collections.Generic;
using System.Text;

#nullable disable
namespace RestSharp.Authenticators.OAuth.Extensions
{
  internal static class CollectionExtensions
  {
    public static IEnumerable<T> AsEnumerable<T>(this T item)
    {
      return (IEnumerable<T>) new T[1]{ item };
    }

    public static IEnumerable<T> And<T>(this T item, T other)
    {
      return (IEnumerable<T>) new T[2]{ item, other };
    }

    public static IEnumerable<T> And<T>(this IEnumerable<T> items, T item)
    {
      foreach (T i in items)
        yield return i;
      yield return item;
    }

    public static K TryWithKey<T, K>(this IDictionary<T, K> dictionary, T key)
    {
      return !dictionary.ContainsKey(key) ? default (K) : dictionary[key];
    }

    public static IEnumerable<T> ToEnumerable<T>(this object[] items) where T : class
    {
      foreach (object obj in items)
      {
        T record = obj as T;
        yield return record;
      }
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
      foreach (T obj in items)
        action(obj);
    }

    public static string Concatenate(
      this WebParameterCollection collection,
      string separator,
      string spacer)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int count = collection.Count;
      int num = 0;
      foreach (WebPair webPair in (WebPairCollection) collection)
      {
        stringBuilder.Append(webPair.Name);
        stringBuilder.Append(separator);
        stringBuilder.Append(webPair.Value);
        ++num;
        if (num < count)
          stringBuilder.Append(spacer);
      }
      return stringBuilder.ToString();
    }
  }
}
