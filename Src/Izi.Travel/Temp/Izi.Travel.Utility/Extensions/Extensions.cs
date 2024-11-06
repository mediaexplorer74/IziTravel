// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Extensions.Extensions
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Utility.Extensions
{
  public static class Extensions
  {
    public static void Add<T>(this List<T> list, T item, bool condition)
    {
      if (!condition)
        return;
      list.Add(item);
    }

    public static void Split(
      this string source,
      char separator,
      out string item1,
      out string item2)
    {
      item1 = (string) null;
      item2 = (string) null;
      if (source == null)
        return;
      string[] strArray = source.Split(separator);
      item1 = strArray.Length != 0 ? strArray[0] : (string) null;
      item2 = strArray.Length > 1 ? strArray[1] : (string) null;
    }

    public static int Clamp(this int value, int min, int max)
    {
      return Math.Min(Math.Max(value, min), max);
    }

    public static TimeSpan Clamp(this TimeSpan value, TimeSpan min, TimeSpan max)
    {
      return TimeSpan.FromSeconds(value.TotalSeconds.Clamp(min.TotalSeconds, max.TotalSeconds));
    }

    public static double Clamp(this double value, double min, double max)
    {
      return Math.Min(Math.Max(value, min), max);
    }

    public static Visibility ToVisibility(this bool value)
    {
      return !value ? Visibility.Collapsed : Visibility.Visible;
    }

    public static void ForEach<T>(this ObservableCollection<T> collection, Action<T, int> action)
    {
      int num = 0;
      foreach (T obj in (Collection<T>) collection)
        action(obj, num++);
    }

    public static void Add(
      this Storyboard storyboard,
      DependencyObject target,
      string path,
      Timeline timeline)
    {
      storyboard.Children.Add(timeline);
      Storyboard.SetTarget(timeline, target);
      Storyboard.SetTargetProperty(timeline, new PropertyPath(path, new object[0]));
    }

    public static void ForEach<T1, T2>(this Dictionary<T1, T2> dictionary, Action<T1, T2> action)
    {
      foreach (KeyValuePair<T1, T2> keyValuePair in dictionary)
        action(keyValuePair.Key, keyValuePair.Value);
    }

    public static List<string> SplitBy(this string source, int chunkSize)
    {
      return source == null ? (List<string>) null : Enumerable.Range(0, (int) Math.Ceiling((double) source.Length / (double) chunkSize)).Select<int, string>((Func<int, string>) (x => source.Substring(x * chunkSize, Math.Min(chunkSize, source.Length - x * chunkSize)))).ToList<string>();
    }

    public static void Set<TKey, TValue>(
      this IDictionary<TKey, TValue> dictionary,
      TKey key,
      TValue value)
    {
      if (dictionary.ContainsKey(key))
        dictionary[key] = value;
      else
        dictionary.Add(key, value);
    }

    public static void Unset<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      if (!dictionary.ContainsKey(key))
        return;
      dictionary.Remove(key);
    }

    public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
    {
      TValue obj;
      dictionary.TryGetValue(key, out obj);
      return obj;
    }

    public static IEnumerable<string> OrderAs(this IList<string> collection, IList<string> order)
    {
      if (collection == null)
        return (IEnumerable<string>) null;
      return order == null ? (IEnumerable<string>) collection : (IEnumerable<string>) collection.OrderBy<string, int>((Func<string, int>) (x => Izi.Travel.Utility.Extensions.Extensions.IndexOf<string>(order, x, int.MaxValue))).ThenBy<string, string>((Func<string, string>) (x => x));
    }

    private static int IndexOf<T>(IList<T> list, T item, int defaultIndex)
    {
      int num = list.IndexOf(item);
      return num >= 0 ? num : defaultIndex;
    }
  }
}
