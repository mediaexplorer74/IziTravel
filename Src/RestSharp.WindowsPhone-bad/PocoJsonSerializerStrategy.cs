// Decompiled with JetBrains decompiler
// Type: RestSharp.PocoJsonSerializerStrategy
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Reflection;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

#nullable disable
namespace RestSharp
{
  [GeneratedCode("simple-json", "1.0.0")]
  public class PocoJsonSerializerStrategy : IJsonSerializerStrategy
  {
    internal IDictionary<Type, ReflectionUtils.ConstructorDelegate> ConstructorCache;
    internal IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>> GetCache;
    internal IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> SetCache;
    internal static readonly Type[] EmptyTypes = new Type[0];
    internal static readonly Type[] ArrayConstructorParameterTypes = new Type[1]
    {
      typeof (int)
    };
    private static readonly string[] Iso8601Format = new string[3]
    {
      "yyyy-MM-dd\\THH:mm:ss.FFFFFFF\\Z",
      "yyyy-MM-dd\\THH:mm:ss\\Z",
      "yyyy-MM-dd\\THH:mm:ssK"
    };

    public PocoJsonSerializerStrategy()
    {
      this.ConstructorCache = (IDictionary<Type, ReflectionUtils.ConstructorDelegate>) new ReflectionUtils.ThreadSafeDictionary<Type, ReflectionUtils.ConstructorDelegate>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, ReflectionUtils.ConstructorDelegate>(this.ContructorDelegateFactory));
      this.GetCache = (IDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, ReflectionUtils.GetDelegate>>(this.GetterValueFactory));
      this.SetCache = (IDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>) new ReflectionUtils.ThreadSafeDictionary<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(new ReflectionUtils.ThreadSafeDictionaryValueFactory<Type, IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>>(this.SetterValueFactory));
    }

    protected virtual string MapClrMemberNameToJsonFieldName(string clrPropertyName)
    {
      return clrPropertyName;
    }

    internal virtual ReflectionUtils.ConstructorDelegate ContructorDelegateFactory(Type key)
    {
      return ReflectionUtils.GetContructor(key, key.IsArray ? PocoJsonSerializerStrategy.ArrayConstructorParameterTypes : PocoJsonSerializerStrategy.EmptyTypes);
    }

    internal virtual IDictionary<string, ReflectionUtils.GetDelegate> GetterValueFactory(Type type)
    {
      IDictionary<string, ReflectionUtils.GetDelegate> dictionary = (IDictionary<string, ReflectionUtils.GetDelegate>) new Dictionary<string, ReflectionUtils.GetDelegate>();
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (property.CanRead)
        {
          MethodInfo getterMethodInfo = ReflectionUtils.GetGetterMethodInfo(property);
          if (!getterMethodInfo.IsStatic && getterMethodInfo.IsPublic)
            dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] = ReflectionUtils.GetGetMethod(property);
        }
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsStatic && field.IsPublic)
          dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] = ReflectionUtils.GetGetMethod(field);
      }
      return dictionary;
    }

    internal virtual IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> SetterValueFactory(
      Type type)
    {
      IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> dictionary = (IDictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>) new Dictionary<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>();
      foreach (PropertyInfo property in ReflectionUtils.GetProperties(type))
      {
        if (property.CanWrite)
        {
          MethodInfo setterMethodInfo = ReflectionUtils.GetSetterMethodInfo(property);
          if (!setterMethodInfo.IsStatic && setterMethodInfo.IsPublic)
            dictionary[this.MapClrMemberNameToJsonFieldName(property.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(property.PropertyType, ReflectionUtils.GetSetMethod(property));
        }
      }
      foreach (FieldInfo field in ReflectionUtils.GetFields(type))
      {
        if (!field.IsInitOnly && !field.IsStatic && field.IsPublic)
          dictionary[this.MapClrMemberNameToJsonFieldName(field.Name)] = new KeyValuePair<Type, ReflectionUtils.SetDelegate>(field.FieldType, ReflectionUtils.GetSetMethod(field));
      }
      return dictionary;
    }

    public virtual bool TrySerializeNonPrimitiveObject(object input, out object output)
    {
      return this.TrySerializeKnownTypes(input, out output) || this.TrySerializeUnknownTypes(input, out output);
    }

    public virtual object DeserializeObject(object value, Type type)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      string str = value as string;
      if (type == typeof (Guid) && string.IsNullOrEmpty(str))
        return (object) new Guid();
      if (value == null)
        return (object) null;
      object source = (object) null;
      switch (str)
      {
        case null:
          if (value is bool)
            return value;
          break;
        case "":
          source = type != typeof (Guid) ? (!ReflectionUtils.IsNullableType(type) || Nullable.GetUnderlyingType(type) != typeof (Guid) ? (object) str : (object) null) : (object) new Guid();
          if (!ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (Guid))
            return (object) str;
          break;
        default:
          if (type == typeof (DateTime) || ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (DateTime))
            return (object) DateTime.ParseExact(str, PocoJsonSerializerStrategy.Iso8601Format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
          if (type == typeof (DateTimeOffset) || ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (DateTimeOffset))
            return (object) DateTimeOffset.ParseExact(str, PocoJsonSerializerStrategy.Iso8601Format, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
          if (type == typeof (Guid) || ReflectionUtils.IsNullableType(type) && Nullable.GetUnderlyingType(type) == typeof (Guid))
            return (object) new Guid(str);
          Uri result;
          return type == typeof (Uri) ? (Uri.IsWellFormedUriString(str, UriKind.RelativeOrAbsolute) && Uri.TryCreate(str, UriKind.RelativeOrAbsolute, out result) ? (object) result : (object) null) : (type == typeof (string) ? (object) str : Convert.ChangeType((object) str, type, (IFormatProvider) CultureInfo.InvariantCulture));
      }
      bool flag1 = value is long;
      bool flag2 = value is double;
      if (flag1 && type == typeof (long) || flag2 && type == typeof (double))
        return value;
      if (flag2 && type != typeof (double) || flag1 && type != typeof (long))
      {
        object obj = type == typeof (int) || type == typeof (long) || type == typeof (double) || type == typeof (float) || type == typeof (bool) || type == typeof (Decimal) || type == typeof (byte) || type == typeof (short) ? Convert.ChangeType(value, type, (IFormatProvider) CultureInfo.InvariantCulture) : value;
        return ReflectionUtils.IsNullableType(type) ? ReflectionUtils.ToNullableType(obj, type) : obj;
      }
      switch (value)
      {
        case IDictionary<string, object> dictionary1:
          if (ReflectionUtils.IsTypeDictionary(type))
          {
            Type[] genericTypeArguments = ReflectionUtils.GetGenericTypeArguments(type);
            Type type1 = genericTypeArguments[0];
            Type type2 = genericTypeArguments[1];
            IDictionary dictionary = (IDictionary) this.ConstructorCache[typeof (Dictionary<,>).MakeGenericType(type1, type2)](new object[0]);
            foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary1)
              dictionary.Add((object) keyValuePair.Key, this.DeserializeObject(keyValuePair.Value, type2));
            source = (object) dictionary;
            break;
          }
          if (type == typeof (object))
          {
            source = value;
            break;
          }
          source = this.ConstructorCache[type](new object[0]);
          using (IEnumerator<KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>>> enumerator = this.SetCache[type].GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, KeyValuePair<Type, ReflectionUtils.SetDelegate>> current = enumerator.Current;
              object obj;
              if (dictionary1.TryGetValue(current.Key, out obj))
              {
                obj = this.DeserializeObject(obj, current.Value.Key);
                current.Value.Value(source, obj);
              }
            }
            break;
          }
        case IList<object> objectList:
          IList list = (IList) null;
          if (type.IsArray)
          {
            list = (IList) this.ConstructorCache[type](new object[1]
            {
              (object) objectList.Count
            });
            int num = 0;
            foreach (object obj in (IEnumerable<object>) objectList)
              list[num++] = this.DeserializeObject(obj, type.GetElementType());
          }
          else if (ReflectionUtils.IsTypeGenericeCollectionInterface(type) || ReflectionUtils.IsAssignableFrom(typeof (IList), type))
          {
            Type genericListElementType = ReflectionUtils.GetGenericListElementType(type);
            ReflectionUtils.ConstructorDelegate constructorDelegate = this.ConstructorCache[type];
            if (constructorDelegate == null)
              constructorDelegate = this.ConstructorCache[typeof (List<>).MakeGenericType(genericListElementType)];
            list = (IList) constructorDelegate(new object[1]
            {
              (object) objectList.Count
            });
            foreach (object obj in (IEnumerable<object>) objectList)
              list.Add(this.DeserializeObject(obj, genericListElementType));
          }
          source = (object) list;
          break;
      }
      return source;
    }

    protected virtual object SerializeEnum(Enum p)
    {
      return (object) Convert.ToDouble((object) p, (IFormatProvider) CultureInfo.InvariantCulture);
    }

    protected virtual bool TrySerializeKnownTypes(object input, out object output)
    {
      bool flag = true;
      switch (input)
      {
        case DateTime dateTime:
          output = (object) dateTime.ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case DateTimeOffset dateTimeOffset:
          output = (object) dateTimeOffset.ToUniversalTime().ToString(PocoJsonSerializerStrategy.Iso8601Format[0], (IFormatProvider) CultureInfo.InvariantCulture);
          break;
        case Guid guid:
          output = (object) guid.ToString("D");
          break;
        default:
          if ((object) (input as Uri) != null)
          {
            output = (object) input.ToString();
            break;
          }
          if (input is Enum p)
          {
            output = this.SerializeEnum(p);
            break;
          }
          flag = false;
          output = (object) null;
          break;
      }
      return flag;
    }

    protected virtual bool TrySerializeUnknownTypes(object input, out object output)
    {
      if (input == null)
        throw new ArgumentNullException(nameof (input));
      output = (object) null;
      Type type = input.GetType();
      if (type.FullName == null)
        return false;
      IDictionary<string, object> dictionary = (IDictionary<string, object>) new JsonObject();
      foreach (KeyValuePair<string, ReflectionUtils.GetDelegate> keyValuePair in (IEnumerable<KeyValuePair<string, ReflectionUtils.GetDelegate>>) this.GetCache[type])
      {
        if (keyValuePair.Value != null)
          dictionary.Add(this.MapClrMemberNameToJsonFieldName(keyValuePair.Key), keyValuePair.Value(input));
      }
      output = (object) dictionary;
      return true;
    }
  }
}
