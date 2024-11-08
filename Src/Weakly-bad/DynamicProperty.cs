// Decompiled with JetBrains decompiler
// Type: Weakly.DynamicProperty
// Assembly: Weakly, Version=2.1.0.0, Culture=neutral, PublicKeyToken=3e9c206b2200b970
// MVID: 59987104-5B29-48EC-89B5-2E7347C0D910
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Weakly.xml

using System;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace Weakly
{
  /// <summary>Helper to create dynamic (complied) property accessors.</summary>
  public static class DynamicProperty
  {
    private static readonly SimpleCache<PropertyInfo, Func<object, object>> GetterCache = new SimpleCache<PropertyInfo, Func<object, object>>();
    private static readonly SimpleCache<PropertyInfo, Action<object, object>> SetterCache = new SimpleCache<PropertyInfo, Action<object, object>>();

    /// <summary>
    /// Get compiled Getter function from a given <paramref name="property" />.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The function to get the property value.</returns>
    public static Func<object, object> GetterFrom(PropertyInfo property)
    {
      Func<object, object> valueOrDefault = DynamicProperty.GetterCache.GetValueOrDefault(property);
      if (valueOrDefault != null)
        return valueOrDefault;
      Func<object, object> func = DynamicProperty.CompileGetter(property);
      DynamicProperty.GetterCache.AddOrUpdate(property, func);
      return func;
    }

    private static Func<object, object> CompileGetter(PropertyInfo property)
    {
      ParameterExpression parameterExpression = default;
      return Expression.Lambda<Func<object, object>>((Expression) 
          Expression.Convert((Expression) Expression.Property(
              (Expression) Expression.Convert((Expression) parameterExpression, property.DeclaringType),
              property), typeof (object)), parameterExpression).Compile();
    }

    /// <summary>
    /// Get compiled Setter function from a given <paramref name="property" />.
    /// </summary>
    /// <param name="property">The property.</param>
    /// <returns>The function to set the property value.</returns>
    public static Action<object, object> SetterFrom(PropertyInfo property)
    {
      Action<object, object> valueOrDefault = DynamicProperty.SetterCache.GetValueOrDefault(property);
      if (valueOrDefault != null)
        return valueOrDefault;
      Action<object, object> action = DynamicProperty.CompileSetter(property);
      DynamicProperty.SetterCache.AddOrUpdate(property, action);
      return action;
    }

    private static Action<object, object> CompileSetter(PropertyInfo property)
    {
      ParameterExpression parameterExpression3 = Expression.Parameter(typeof (object), "instance");
      ParameterExpression parameterExpression4 = Expression.Parameter(typeof (object), "value");
      UnaryExpression unaryExpression = Expression.Convert((Expression) parameterExpression3, 
          property.DeclaringType);
      UnaryExpression right = Expression.Convert((Expression) parameterExpression4, 
          property.PropertyType);
      return ((Expression<Action<object, object>>) ((parameterExpression1, parameterExpression2)
                => Expression.Assign((Expression) Expression.Property((Expression) unaryExpression,
                property), (Expression) right))).Compile();
    }
  }
}
