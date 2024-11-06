// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.TypeDescriptor
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Provides information about the characteristics for a component, such as its attributes, properties, and events. This class cannot be inherited.
  /// </summary>
  public static class TypeDescriptor
  {
    private static readonly Dictionary<Type, TypeConverter> Cache = new Dictionary<Type, TypeConverter>();

    /// <summary>Returns a type converter for the specified type.</summary>
    /// <param name="type">The System.Type of the target component.</param>
    /// <returns>A System.ComponentModel.TypeConverter for the specified type.</returns>
    public static TypeConverter GetConverter(Type type)
    {
      TypeConverter instance;
      if (!TypeDescriptor.Cache.TryGetValue(type, out instance))
      {
        IEnumerable<TypeConverterAttribute> attributes = type.GetAttributes<TypeConverterAttribute>(true);
        if (!attributes.Any<TypeConverterAttribute>())
          return new TypeConverter();
        instance = Activator.CreateInstance(Type.GetType(attributes.First<TypeConverterAttribute>().ConverterTypeName)) as TypeConverter;
        TypeDescriptor.Cache[type] = instance;
      }
      return instance;
    }
  }
}
