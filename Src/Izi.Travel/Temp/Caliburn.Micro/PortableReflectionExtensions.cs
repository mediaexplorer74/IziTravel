// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PortableReflectionExtensions
// Assembly: Caliburn.Micro, Version=2.0.1.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f
// MVID: 7F5F8939-06B6-4E9F-9AED-A5320EED3930
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A collection of extension methods to help with differing reflection between the portable library and SL5
  /// </summary>
  internal static class PortableReflectionExtensions
  {
    public static bool IsAssignableFrom(this Type t, Type c)
    {
      return t.GetTypeInfo().IsAssignableFrom(c.GetTypeInfo());
    }

    public static Type[] GetGenericArguments(this Type t) => t.GetTypeInfo().GenericTypeArguments;

    public static IEnumerable<PropertyInfo> GetProperties(this Type t) => t.GetRuntimeProperties();

    public static IEnumerable<ConstructorInfo> GetConstructors(this Type t)
    {
      return t.GetTypeInfo().DeclaredConstructors;
    }

    public static IEnumerable<Type> GetInterfaces(this Type t)
    {
      return t.GetTypeInfo().ImplementedInterfaces;
    }

    public static IEnumerable<Type> GetTypes(this Assembly a)
    {
      return a.DefinedTypes.Select<TypeInfo, Type>((Func<TypeInfo, Type>) (t => t.AsType()));
    }

    public static bool IsAbstract(this Type t) => t.GetTypeInfo().IsAbstract;

    public static bool IsInterface(this Type t) => t.GetTypeInfo().IsInterface;

    public static bool IsGenericType(this Type t) => t.GetTypeInfo().IsGenericType;

    public static MethodInfo GetMethod(this Type t, string name, Type[] parameters)
    {
      return t.GetRuntimeMethod(name, parameters);
    }
  }
}
