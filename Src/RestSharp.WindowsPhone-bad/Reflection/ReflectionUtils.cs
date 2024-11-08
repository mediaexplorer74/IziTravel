// Decompiled with JetBrains decompiler
// Type: RestSharp.Reflection.ReflectionUtils
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

#nullable disable
namespace RestSharp.Reflection
{
  [GeneratedCode("reflection-utils", "1.0.0")]
  internal class ReflectionUtils
  {
    private static readonly object[] EmptyObjects = new object[0];

    public static Type GetTypeInfo(Type type) => type;

    public static Attribute GetAttribute(MemberInfo info, Type type)
    {
       return (Attribute)null; /*info == null || type == null 
                || !Attribute.IsDefined(info, type) 
                ? (Attribute) null 
                : Attribute.GetCustomAttribute(info, type);*/
    }

    public static Type GetGenericListElementType(Type type)
    {
      foreach (Type type1 in (IEnumerable<Type>) type.GetInterfaces())
      {
        if (ReflectionUtils.IsTypeGeneric(type1) && type1.GetGenericTypeDefinition() == typeof (IList<>))
          return ReflectionUtils.GetGenericTypeArguments(type1)[0];
      }
      return ReflectionUtils.GetGenericTypeArguments(type)[0];
    }

    public static Attribute GetAttribute(Type objectType, Type attributeType)
    {
        return (Attribute)null;/*objectType == null || attributeType == null 
                || !Attribute.IsDefined((MemberInfo) objectType, attributeType)
                ? (Attribute) null 
                : Attribute.GetCustomAttribute((MemberInfo) objectType, attributeType);*/
    }

    public static Type[] GetGenericTypeArguments(Type type) 
            => type.GetGenericArguments();

    public static bool IsTypeGeneric(Type type)
    {
        return default;//ReflectionUtils.GetTypeInfo(type).IsGenericType; 
    }

    public static bool IsTypeGenericeCollectionInterface(Type type)
    {
      if (!ReflectionUtils.IsTypeGeneric(type))
        return false;
      Type genericTypeDefinition = type.GetGenericTypeDefinition();

      return genericTypeDefinition == typeof (IList<>) 
                || genericTypeDefinition == typeof (ICollection<>) 
                || genericTypeDefinition == typeof (IEnumerable<>);
    }

    public static bool IsAssignableFrom(Type type1, Type type2)
    {
      return ReflectionUtils.GetTypeInfo(type1).IsAssignableFrom(ReflectionUtils.GetTypeInfo(type2));
    }

    public static bool IsTypeDictionary(Type type)
    {
      if (typeof (IDictionary).IsAssignableFrom(type))
        return true;
      return /*ReflectionUtils.GetTypeInfo(type).IsGenericType && */
                type.GetGenericTypeDefinition() == typeof (IDictionary<,>);
    }

    public static bool IsNullableType(Type type)
    {
      return /*ReflectionUtils.GetTypeInfo(type).IsGenericType &&*/ 
                type.GetGenericTypeDefinition() == typeof (Nullable<>);
    }

    public static object ToNullableType(object obj, Type nullableType)
    {
      return obj != null
                ? Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType),
                (IFormatProvider) CultureInfo.InvariantCulture) 
                : (object) null;
    }

        public static bool IsValueType(Type type)
                => default;//ReflectionUtils.GetTypeInfo(type).IsValueType;

    public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
    {
      return (IEnumerable<ConstructorInfo>) type.GetConstructors();
    }

    public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
    {
      foreach (ConstructorInfo constructor in ReflectionUtils.GetConstructors(type))
      {
        ParameterInfo[] parameters = constructor.GetParameters();
        if (argsType.Length == parameters.Length)
        {
          int index = 0;
          bool flag = true;
          foreach (ParameterInfo parameter in constructor.GetParameters())
          {
            if (parameter.ParameterType != argsType[index])
            {
              flag = false;
              break;
            }
          }
          if (flag)
            return constructor;
        }
      }
      return (ConstructorInfo) null;
    }

    public static IEnumerable<PropertyInfo> GetProperties(Type type)
    {
      return (IEnumerable<PropertyInfo>) type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
    }

    public static IEnumerable<FieldInfo> GetFields(Type type)
    {
      return (IEnumerable<FieldInfo>) type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static);
    }

    public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
    {
      return propertyInfo.GetGetMethod(true);
    }

    public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
    {
      return propertyInfo.GetSetMethod(true);
    }

    public static ReflectionUtils.ConstructorDelegate GetContructor(ConstructorInfo constructorInfo)
    {
      return ReflectionUtils.GetConstructorByExpression(constructorInfo);
    }

    public static ReflectionUtils.ConstructorDelegate GetContructor(
      Type type,
      params Type[] argsType)
    {
      return ReflectionUtils.GetConstructorByExpression(type, argsType);
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(
      ConstructorInfo constructorInfo)
    {
      return (ReflectionUtils.ConstructorDelegate) (args => constructorInfo.Invoke(args));
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(
      Type type,
      params Type[] argsType)
    {
      ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
      return constructorInfo != null ? ReflectionUtils.GetConstructorByReflection(constructorInfo) : (ReflectionUtils.ConstructorDelegate) null;
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByExpression(
      ConstructorInfo constructorInfo)
    {
      ParameterInfo[] parameters = constructorInfo.GetParameters();
      ParameterExpression array = Expression.Parameter(typeof (object[]), "args");
      Expression[] expressionArray = new Expression[parameters.Length];
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        Expression index2 = (Expression) Expression.Constant((object) index1);
        Type parameterType = parameters[index1].ParameterType;
        Expression expression = (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) array, index2), parameterType);
        expressionArray[index1] = expression;
      }
      Func<object[], object> compiledLambda = Expression.Lambda<Func<object[], object>>((Expression) Expression.New(constructorInfo, expressionArray), array).Compile();
      return (ReflectionUtils.ConstructorDelegate) (args => compiledLambda(args));
    }

    public static ReflectionUtils.ConstructorDelegate GetConstructorByExpression(
      Type type,
      params Type[] argsType)
    {
      ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
      return constructorInfo != null ? ReflectionUtils.GetConstructorByExpression(constructorInfo) : (ReflectionUtils.ConstructorDelegate) null;
    }

    public static ReflectionUtils.GetDelegate GetGetMethod(PropertyInfo propertyInfo)
    {
      return ReflectionUtils.GetGetMethodByExpression(propertyInfo);
    }

    public static ReflectionUtils.GetDelegate GetGetMethod(FieldInfo fieldInfo)
    {
      return ReflectionUtils.GetGetMethodByExpression(fieldInfo);
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
    {
      MethodInfo methodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
      return (ReflectionUtils.GetDelegate) (source => methodInfo.Invoke(source, ReflectionUtils.EmptyObjects));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
    {
      return (ReflectionUtils.GetDelegate) (source => fieldInfo.GetValue(source));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByExpression(PropertyInfo propertyInfo)
    {
      MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
      ParameterExpression parameterExpression = default;
      Func<object, object> compiled = ((Expression<Func<object, object>>) (instance => Expression.Call((Expression) (!ReflectionUtils.IsValueType(propertyInfo.DeclaringType) ? Expression.TypeAs((Expression) parameterExpression, propertyInfo.DeclaringType) : Expression.Convert((Expression) parameterExpression, propertyInfo.DeclaringType)), getterMethodInfo) as object)).Compile();
      return (ReflectionUtils.GetDelegate) (source => compiled(source));
    }

    public static ReflectionUtils.GetDelegate GetGetMethodByExpression(FieldInfo fieldInfo)
    {
      ParameterExpression parameterExpression = default;
      ReflectionUtils.GetDelegate compiled = Expression.Lambda<ReflectionUtils.GetDelegate>((Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Convert((Expression) parameterExpression, fieldInfo.DeclaringType), fieldInfo), typeof (object)), parameterExpression).Compile();
      return (ReflectionUtils.GetDelegate) (source => compiled(source));
    }

    public static ReflectionUtils.SetDelegate GetSetMethod(PropertyInfo propertyInfo)
    {
      return ReflectionUtils.GetSetMethodByExpression(propertyInfo);
    }

    public static ReflectionUtils.SetDelegate GetSetMethod(FieldInfo fieldInfo)
    {
      return ReflectionUtils.GetSetMethodByExpression(fieldInfo);
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
    {
      MethodInfo methodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
      return (ReflectionUtils.SetDelegate) ((source, value) => methodInfo.Invoke(source, new object[1]
      {
        value
      }));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
    {
      return (ReflectionUtils.SetDelegate) ((source, value) => fieldInfo.SetValue(source, value));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByExpression(PropertyInfo propertyInfo)
    {
      MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
      ParameterExpression parameterExpression1 = Expression.Parameter(typeof (object), "instance");
      ParameterExpression parameterExpression2 = Expression.Parameter(typeof (object), "value");
      UnaryExpression instance = !ReflectionUtils.IsValueType(propertyInfo.DeclaringType) ? Expression.TypeAs((Expression) parameterExpression1, propertyInfo.DeclaringType) : Expression.Convert((Expression) parameterExpression1, propertyInfo.DeclaringType);
      UnaryExpression unaryExpression = !ReflectionUtils.IsValueType(propertyInfo.PropertyType) ? Expression.TypeAs((Expression) parameterExpression2, propertyInfo.PropertyType) : Expression.Convert((Expression) parameterExpression2, propertyInfo.PropertyType);
      Action<object, object> compiled = Expression.Lambda<Action<object, object>>((Expression) Expression.Call((Expression) instance, setterMethodInfo, (Expression) unaryExpression), parameterExpression1, parameterExpression2).Compile();
      return (ReflectionUtils.SetDelegate) ((source, val) => compiled(source, val));
    }

    public static ReflectionUtils.SetDelegate GetSetMethodByExpression(FieldInfo fieldInfo)
    {
      ParameterExpression parameterExpression1 = default;
      ParameterExpression parameterExpression2 = default;
      Action<object, object> compiled = Expression.Lambda<Action<object, object>>(
          (Expression) ReflectionUtils.Assign((Expression) Expression.Field((Expression) Expression.Convert(
              (Expression) parameterExpression1, fieldInfo.DeclaringType), fieldInfo), (Expression) Expression.Convert((Expression) parameterExpression2, fieldInfo.FieldType)), parameterExpression1, parameterExpression2).Compile();
      return (ReflectionUtils.SetDelegate) ((source, val) => compiled(source, val));
    }

    public static BinaryExpression Assign(Expression left, Expression right)
    {
      MethodInfo method = typeof (ReflectionUtils.Assigner<>).MakeGenericType(left.Type).GetMethod(nameof (Assign));
      return Expression.Add(left, right, method);
    }

    public delegate object GetDelegate(object source);

    public delegate void SetDelegate(object source, object value);

    public delegate object ConstructorDelegate(params object[] args);

    public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

    private static class Assigner<T>
    {
      public static T Assign(ref T left, T right) => left = right;
    }

    public sealed class ThreadSafeDictionary<TKey, TValue> : 
      IDictionary<TKey, TValue>,
      ICollection<KeyValuePair<TKey, TValue>>,
      IEnumerable<KeyValuePair<TKey, TValue>>,
      IEnumerable
    {
      private readonly object _lock = new object();
      private readonly ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;
      private Dictionary<TKey, TValue> _dictionary;

      public ThreadSafeDictionary(
        ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
      {
        this._valueFactory = valueFactory;
      }

      private TValue Get(TKey key)
      {
        TValue obj;
        return this._dictionary == null || !this._dictionary.TryGetValue(key, out obj) ? this.AddValue(key) : obj;
      }

      private TValue AddValue(TKey key)
      {
        TValue obj1 = this._valueFactory(key);
        lock (this._lock)
        {
          if (this._dictionary == null)
          {
            this._dictionary = new Dictionary<TKey, TValue>();
            this._dictionary[key] = obj1;
          }
          else
          {
            TValue obj2;
            if (this._dictionary.TryGetValue(key, out obj2))
              return obj2;
            this._dictionary = new Dictionary<TKey, TValue>((IDictionary<TKey, TValue>) this._dictionary)
            {
              [key] = obj1
            };
          }
        }
        return obj1;
      }

      public void Add(TKey key, TValue value) => throw new NotImplementedException();

      public bool ContainsKey(TKey key) => this._dictionary.ContainsKey(key);

      public ICollection<TKey> Keys => (ICollection<TKey>) this._dictionary.Keys;

      public bool Remove(TKey key) => throw new NotImplementedException();

      public bool TryGetValue(TKey key, out TValue value)
      {
        value = this[key];
        return true;
      }

      public ICollection<TValue> Values => (ICollection<TValue>) this._dictionary.Values;

      public TValue this[TKey key]
      {
        get => this.Get(key);
        set => throw new NotImplementedException();
      }

      public void Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public void Clear() => throw new NotImplementedException();

      public bool Contains(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
      {
        throw new NotImplementedException();
      }

      public int Count => this._dictionary.Count;

      public bool IsReadOnly => throw new NotImplementedException();

      public bool Remove(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

      public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
      {
        return (IEnumerator<KeyValuePair<TKey, TValue>>) this._dictionary.GetEnumerator();
      }

      IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._dictionary.GetEnumerator();
    }
  }
}
