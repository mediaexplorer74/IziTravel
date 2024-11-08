// Decompiled with JetBrains decompiler
// Type: RestSharp.Deserializers.JsonDeserializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Xml;

#nullable disable
namespace RestSharp.Deserializers
{
  public class JsonDeserializer : IDeserializer
  {
    public string RootElement { get; set; }

    public string Namespace { get; set; }

    public string DateFormat { get; set; }

    public CultureInfo Culture { get; set; }

    public JsonDeserializer() => this.Culture = CultureInfo.InvariantCulture;

    public T Deserialize<T>(IRestResponse response)
    {
      T instance = Activator.CreateInstance<T>();
      T obj;
      if ((object) instance is IList)
      {
        Type type = instance.GetType();
        if (this.RootElement.HasValue())
        {
          object root = this.FindRoot(response.Content);
          obj = (T) this.BuildList(type, root);
        }
        else
        {
          object parent = SimpleJson.DeserializeObject(response.Content);
          obj = (T) this.BuildList(type, parent);
        }
      }
      else if ((object) instance is IDictionary)
      {
        object root = this.FindRoot(response.Content);
        obj = (T) this.BuildDictionary(instance.GetType(), root);
      }
      else
      {
        object root = this.FindRoot(response.Content);
        obj = (T) this.Map((object) instance, (IDictionary<string, object>) root);
      }
      return obj;
    }

    private object FindRoot(string content)
    {
      IDictionary<string, object> dictionary = (IDictionary<string, object>) SimpleJson.DeserializeObject(content);
      return this.RootElement.HasValue() && dictionary.ContainsKey(this.RootElement) ? dictionary[this.RootElement] : (object) dictionary;
    }

    private object Map(object target, IDictionary<string, object> data)
    {
      foreach (PropertyInfo propertyInfo in ((IEnumerable<PropertyInfo>) target.GetType().GetProperties()).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.CanWrite)).ToList<PropertyInfo>())
      {
        Type propertyType = propertyInfo.PropertyType;
        object[] customAttributes = propertyInfo.GetCustomAttributes(typeof (DeserializeAsAttribute), false);
        string[] strArray = (customAttributes.Length <= 0 ? propertyInfo.Name : ((DeserializeAsAttribute) customAttributes[0]).Name).Split('.');
        IDictionary<string, object> dictionary = data;
        object obj = (object) null;
        for (int index = 0; index < strArray.Length; ++index)
        {
          string key = strArray[index].GetNameVariants(this.Culture).FirstOrDefault<string>(new Func<string, bool>(dictionary.ContainsKey));
          if (key != null)
          {
            if (index == strArray.Length - 1)
              obj = dictionary[key];
            else
              dictionary = (IDictionary<string, object>) dictionary[key];
          }
          else
            break;
        }
        if (obj != null)
          propertyInfo.SetValue(target, this.ConvertValue(propertyType, obj), (object[]) null);
      }
      return target;
    }

    private IDictionary BuildDictionary(Type type, object parent)
    {
      IDictionary instance = (IDictionary) Activator.CreateInstance(type);
      Type genericArgument = type.GetGenericArguments()[1];
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) parent)
      {
        string key = keyValuePair.Key;
        object obj = !genericArgument.IsGenericType || genericArgument.GetGenericTypeDefinition() != typeof (List<>) ? this.ConvertValue(genericArgument, keyValuePair.Value) : (object) this.BuildList(genericArgument, keyValuePair.Value);
        instance.Add((object) key, obj);
      }
      return instance;
    }

    private IList BuildList(Type type, object parent)
    {
      IList instance = (IList) Activator.CreateInstance(type);
      Type genericArgument = ((IEnumerable<Type>) type.GetInterfaces()).First<Type>((Func<Type, bool>) (x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (IList<>))).GetGenericArguments()[0];
      if (parent is IList)
      {
        foreach (object obj1 in (IEnumerable) parent)
        {
          if (genericArgument.IsPrimitive)
          {
            object obj2 = this.ConvertValue(genericArgument, obj1);
            instance.Add(obj2);
          }
          else if (genericArgument == typeof (string))
          {
            if (obj1 == null)
              instance.Add((object) null);
            else
              instance.Add((object) obj1.ToString());
          }
          else if (obj1 == null)
          {
            instance.Add((object) null);
          }
          else
          {
            object obj3 = this.ConvertValue(genericArgument, obj1);
            instance.Add(obj3);
          }
        }
      }
      else
        instance.Add(this.ConvertValue(genericArgument, parent));
      return instance;
    }

    private object ConvertValue(Type type, object value)
    {
      string str = Convert.ToString(value, (IFormatProvider) this.Culture);
      if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof (Nullable<>))
      {
        if (string.IsNullOrEmpty(str))
          return (object) null;
        type = type.GetGenericArguments()[0];
      }
      if (type == typeof (object) && value != null)
        type = value.GetType();
      if (type.IsPrimitive)
        return value.ChangeType(type, this.Culture);
      if (type.IsEnum)
        return type.FindEnumValue(str, this.Culture);
      if (type == typeof (Uri))
        return (object) new Uri(str, UriKind.RelativeOrAbsolute);
      if (type == typeof (string))
        return (object) str;
      if (type == typeof (DateTime) || type == typeof (DateTimeOffset))
      {
        DateTime dateTime = !this.DateFormat.HasValue() ? str.ParseJsonDate(this.Culture) : DateTime.ParseExact(str, this.DateFormat, (IFormatProvider) this.Culture);
        if (type == typeof (DateTime))
          return (object) dateTime;
        if (type == typeof (DateTimeOffset))
          return (object) (DateTimeOffset) dateTime;
      }
      else
      {
        if (type == typeof (Decimal))
        {
          if (value is double num)
            return (object) (Decimal) num;
          return str.Contains("e") ? (object) Decimal.Parse(str, NumberStyles.Float, (IFormatProvider) this.Culture) : (object) Decimal.Parse(str, (IFormatProvider) this.Culture);
        }
        if (type == typeof (Guid))
          return (object) (string.IsNullOrEmpty(str) ? Guid.Empty : new Guid(str));
        if (type == typeof (TimeSpan))
        {
          TimeSpan result;
          return TimeSpan.TryParse(str, out result) ? (object) result : (object) XmlConvert.ToTimeSpan(str);
        }
        if (type.IsGenericType)
        {
          Type genericTypeDefinition = type.GetGenericTypeDefinition();
          if (genericTypeDefinition == typeof (List<>))
            return (object) this.BuildList(type, value);
          if (genericTypeDefinition != typeof (Dictionary<,>))
            return this.CreateAndMap(type, value);
          if (type.GetGenericArguments()[0] == typeof (string))
            return (object) this.BuildDictionary(type, value);
        }
        else
        {
          if (type.IsSubclassOfRawGeneric(typeof (List<>)))
            return (object) this.BuildList(type, value);
          return type == typeof (JsonObject) ? (object) this.BuildDictionary(typeof (Dictionary<string, object>), value) : this.CreateAndMap(type, value);
        }
      }
      return (object) null;
    }

    private object CreateAndMap(Type type, object element)
    {
      object instance = Activator.CreateInstance(type);
      this.Map(instance, (IDictionary<string, object>) element);
      return instance;
    }
  }
}
