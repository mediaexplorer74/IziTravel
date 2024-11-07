// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Extensions.ObservableCollectionExtensions
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace Izi.Travel.Utility.Extensions
{
  public static class ObservableCollectionExtensions
  {
    public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
    {
      if (items == null)
        return;
      foreach (T obj in items)
        collection.Add(obj);
    }
  }
}
