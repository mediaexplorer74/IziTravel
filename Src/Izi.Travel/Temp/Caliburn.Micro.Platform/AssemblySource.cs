// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.AssemblySource
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A source of assemblies that are inspectable by the framework.
  /// </summary>
  public static class AssemblySource
  {
    /// <summary>
    /// The singleton instance of the AssemblySource used by the framework.
    /// </summary>
    public static readonly IObservableCollection<Assembly> Instance = (IObservableCollection<Assembly>) new BindableCollection<Assembly>();
    /// <summary>
    /// Finds a type which matches one of the elements in the sequence of names.
    /// </summary>
    public static Func<IEnumerable<string>, Type> FindTypeByNames = (Func<IEnumerable<string>, Type>) (names => names == null ? (Type) null : names.Join<string, Type, string, Type>(AssemblySource.Instance.SelectMany<Assembly, Type>((Func<Assembly, IEnumerable<Type>>) (a => (IEnumerable<Type>) a.GetExportedTypes())), (Func<string, string>) (n => n), (Func<Type, string>) (t => t.FullName), (Func<string, Type, Type>) ((n, t) => t)).FirstOrDefault<Type>());
  }
}
