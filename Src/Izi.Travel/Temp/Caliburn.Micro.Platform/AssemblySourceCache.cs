// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AssemblySourceCache
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A caching subsystem for <see cref="T:Caliburn.Micro.AssemblySource" />.
  /// </summary>
  public static class AssemblySourceCache
  {
    private static bool isInstalled;
    private static readonly IDictionary<string, Type> TypeNameCache = (IDictionary<string, Type>) new Dictionary<string, Type>();
    /// <summary>
    /// Extracts the types from the spezified assembly for storing in the cache.
    /// </summary>
    public static Func<Assembly, IEnumerable<Type>> ExtractTypes = (Func<Assembly, IEnumerable<Type>>) (assembly => ((IEnumerable<Type>) assembly.GetExportedTypes()).Where<Type>((Func<Type, bool>) (t => typeof (UIElement).IsAssignableFrom(t) || typeof (INotifyPropertyChanged).IsAssignableFrom(t))));

    /// <summary>Installs the caching subsystem.</summary>
    public static void Install()
    {
      if (AssemblySourceCache.isInstalled)
        return;
      AssemblySourceCache.isInstalled = true;
      AssemblySource.Instance.CollectionChanged += (NotifyCollectionChangedEventHandler) ((s, e) =>
      {
        switch (e.Action)
        {
          case NotifyCollectionChangedAction.Add:
            e.NewItems.OfType<Assembly>().SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>) (a => AssemblySourceCache.ExtractTypes(a))).Apply<Type>((Action<Type>) (t => AssemblySourceCache.TypeNameCache.Add(t.FullName, t)));
            break;
          case NotifyCollectionChangedAction.Remove:
          case NotifyCollectionChangedAction.Replace:
          case NotifyCollectionChangedAction.Reset:
            AssemblySourceCache.TypeNameCache.Clear();
            AssemblySource.Instance.SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>) (a => AssemblySourceCache.ExtractTypes(a))).Apply<Type>((Action<Type>) (t => AssemblySourceCache.TypeNameCache.Add(t.FullName, t)));
            break;
        }
      });
      AssemblySource.Instance.Refresh();
      AssemblySource.FindTypeByNames = (Func<IEnumerable<string>, Type>) (names => names == null ? (Type) null : names.Select<string, Type>((Func<string, Type>) (n => AssemblySourceCache.TypeNameCache.GetValueOrDefault<string, Type>(n))).FirstOrDefault<Type>((Func<Type, bool>) (t => t != null)));
    }
  }
}
