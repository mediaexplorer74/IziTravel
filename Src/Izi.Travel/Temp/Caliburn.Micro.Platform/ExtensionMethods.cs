﻿// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.ExtensionMethods
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
  /// <summary>Generic extension methods used by the framework.</summary>
  public static class ExtensionMethods
  {
    /// <summary>Get's the name of the assembly.</summary>
    /// <param name="assembly">The assembly.</param>
    /// <returns>The assembly's name.</returns>
    public static string GetAssemblyName(this Assembly assembly)
    {
      return assembly.FullName.Remove(assembly.FullName.IndexOf(','));
    }

    /// <summary>Gets all the attributes of a particular type.</summary>
    /// <typeparam name="T">The type of attributes to get.</typeparam>
    /// <param name="member">The member to inspect for attributes.</param>
    /// <param name="inherit">Whether or not to search for inherited attributes.</param>
    /// <returns>The list of attributes found.</returns>
    public static IEnumerable<T> GetAttributes<T>(this MemberInfo member, bool inherit)
    {
      return Attribute.GetCustomAttributes(member, inherit).OfType<T>();
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
