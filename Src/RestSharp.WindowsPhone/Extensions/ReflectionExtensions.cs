// Decompiled with JetBrains decompiler
// Type: RestSharp.Extensions.ReflectionExtensions
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace RestSharp.Extensions
{
  /// <summary>Reflection extensions</summary>
  public static class ReflectionExtensions
  {
    /// <summary>Retrieve an attribute from a member (property)</summary>
    /// <typeparam name="T">Type of attribute to retrieve</typeparam>
    /// <param name="prop">Member to retrieve attribute from</param>
    /// <returns></returns>
    public static T GetAttribute<T>(this MemberInfo prop) where T : Attribute
    {
      return Attribute.GetCustomAttribute(prop, typeof (T)) as T;
    }

    /// <summary>Retrieve an attribute from a type</summary>
    /// <typeparam name="T">Type of attribute to retrieve</typeparam>
    /// <param name="type">Type to retrieve attribute from</param>
    /// <returns></returns>
    public static T GetAttribute<T>(this Type type) where T : Attribute
    {
      return Attribute.GetCustomAttribute((MemberInfo) type, typeof (T)) as T;
    }

    /// <summary>
    /// Checks a type to see if it derives from a raw generic (e.g. List[[]])
    /// </summary>
    /// <param name="toCheck"></param>
    /// <param name="generic"></param>
    /// <returns></returns>
    public static bool IsSubclassOfRawGeneric(this Type toCheck, Type generic)
    {
      for (; toCheck != typeof (object); toCheck = toCheck.BaseType)
      {
        Type type = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
        if (generic == type)
          return true;
      }
      return false;
    }

    public static object ChangeType(this object source, Type newType)
    {
      return Convert.ChangeType(source, newType, (IFormatProvider) null);
    }

    public static object ChangeType(this object source, Type newType, CultureInfo culture)
    {
      return Convert.ChangeType(source, newType, (IFormatProvider) culture);
    }

    /// <summary>
    /// Find a value from a System.Enum by trying several possible variants
    /// of the string value of the enum.
    /// </summary>
    /// <param name="type">Type of enum</param>
    /// <param name="value">Value for which to search</param>
    /// <param name="culture">The culture used to calculate the name variants</param>
    /// <returns></returns>
    public static object FindEnumValue(this Type type, string value, CultureInfo culture)
    {
      return Enum.Parse(type, value, true);
    }
  }
}
