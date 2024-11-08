﻿// Decompiled with JetBrains decompiler
// Type: RestSharp.SimpleJson
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using RestSharp.Reflection;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

#nullable disable
namespace RestSharp
{
  /// <summary>
  /// This class encodes and decodes JSON strings.
  /// Spec. details, see http://www.json.org/
  /// 
  /// JSON uses Arrays and Objects. These correspond here to the datatypes JsonArray(IList&lt;object&gt;) and JsonObject(IDictionary&lt;string,object&gt;).
  /// All numbers are parsed to doubles.
  /// </summary>
  [GeneratedCode("simple-json", "1.0.0")]
  public static class SimpleJson
  {
    private const int TOKEN_NONE = 0;
    private const int TOKEN_CURLY_OPEN = 1;
    private const int TOKEN_CURLY_CLOSE = 2;
    private const int TOKEN_SQUARED_OPEN = 3;
    private const int TOKEN_SQUARED_CLOSE = 4;
    private const int TOKEN_COLON = 5;
    private const int TOKEN_COMMA = 6;
    private const int TOKEN_STRING = 7;
    private const int TOKEN_NUMBER = 8;
    private const int TOKEN_TRUE = 9;
    private const int TOKEN_FALSE = 10;
    private const int TOKEN_NULL = 11;
    private const int BUILDER_CAPACITY = 2000;
    private static readonly char[] EscapeTable;
    private static readonly char[] EscapeCharacters = new char[7]
    {
      '"',
      '\\',
      '\b',
      '\f',
      '\n',
      '\r',
      '\t'
    };
    private static readonly string EscapeCharactersString = new string(SimpleJson.EscapeCharacters);
    private static IJsonSerializerStrategy _currentJsonSerializerStrategy;
    private static PocoJsonSerializerStrategy _pocoJsonSerializerStrategy;

    static SimpleJson()
    {
      SimpleJson.EscapeTable = new char[93];
      SimpleJson.EscapeTable[34] = '"';
      SimpleJson.EscapeTable[92] = '\\';
      SimpleJson.EscapeTable[8] = 'b';
      SimpleJson.EscapeTable[12] = 'f';
      SimpleJson.EscapeTable[10] = 'n';
      SimpleJson.EscapeTable[13] = 'r';
      SimpleJson.EscapeTable[9] = 't';
    }

    /// <summary>Parses the string json into a value</summary>
    /// <param name="json">A JSON string.</param>
    /// <returns>An IList&lt;object&gt;, a IDictionary&lt;string,object&gt;, a double, a string, null, true, or false</returns>
    public static object DeserializeObject(string json)
    {
      object obj;
      if (SimpleJson.TryDeserializeObject(json, out obj))
        return obj;
      throw new SerializationException("Invalid JSON string");
    }

    /// <summary>Try parsing the json string into a value.</summary>
    /// <param name="json">A JSON string.</param>
    /// <param name="obj">The object.</param>
    /// <returns>Returns true if successfull otherwise false.</returns>
    public static bool TryDeserializeObject(string json, out object obj)
    {
      bool success = true;
      if (json != null)
      {
        char[] charArray = json.ToCharArray();
        int index = 0;
        obj = SimpleJson.ParseValue(charArray, ref index, ref success);
      }
      else
        obj = (object) null;
      return success;
    }

    public static object DeserializeObject(
      string json,
      Type type,
      IJsonSerializerStrategy jsonSerializerStrategy)
    {
      object obj = SimpleJson.DeserializeObject(json);
      return type != null && (obj == null || !ReflectionUtils.IsAssignableFrom(obj.GetType(), type)) ? (jsonSerializerStrategy ?? SimpleJson.CurrentJsonSerializerStrategy).DeserializeObject(obj, type) : obj;
    }

    public static object DeserializeObject(string json, Type type)
    {
      return SimpleJson.DeserializeObject(json, type, (IJsonSerializerStrategy) null);
    }

    public static T DeserializeObject<T>(
      string json,
      IJsonSerializerStrategy jsonSerializerStrategy)
    {
      return (T) SimpleJson.DeserializeObject(json, typeof (T), jsonSerializerStrategy);
    }

    public static T DeserializeObject<T>(string json)
    {
      return (T) SimpleJson.DeserializeObject(json, typeof (T), (IJsonSerializerStrategy) null);
    }

    /// <summary>
    /// Converts a IDictionary&lt;string,object&gt; / IList&lt;object&gt; object into a JSON string
    /// </summary>
    /// <param name="json">A IDictionary&lt;string,object&gt; / IList&lt;object&gt;</param>
    /// <param name="jsonSerializerStrategy">Serializer strategy to use</param>
    /// <returns>A JSON encoded string, or null if object 'json' is not serializable</returns>
    public static string SerializeObject(
      object json,
      IJsonSerializerStrategy jsonSerializerStrategy)
    {
      StringBuilder builder = new StringBuilder(2000);
      return !SimpleJson.SerializeValue(jsonSerializerStrategy, json, builder) ? (string) null : builder.ToString();
    }

    public static string SerializeObject(object json)
    {
      return SimpleJson.SerializeObject(json, SimpleJson.CurrentJsonSerializerStrategy);
    }

    public static string EscapeToJavascriptString(string jsonString)
    {
      if (string.IsNullOrEmpty(jsonString))
        return jsonString;
      StringBuilder stringBuilder = new StringBuilder();
      int index = 0;
      while (index < jsonString.Length)
      {
        char ch = jsonString[index++];
        if (ch == '\\')
        {
          if (jsonString.Length - index >= 2)
          {
            switch (jsonString[index])
            {
              case '"':
                stringBuilder.Append("\"");
                ++index;
                continue;
              case '\\':
                stringBuilder.Append('\\');
                ++index;
                continue;
              case 'b':
                stringBuilder.Append('\b');
                ++index;
                continue;
              case 'n':
                stringBuilder.Append('\n');
                ++index;
                continue;
              case 'r':
                stringBuilder.Append('\r');
                ++index;
                continue;
              case 't':
                stringBuilder.Append('\t');
                ++index;
                continue;
              default:
                continue;
            }
          }
        }
        else
          stringBuilder.Append(ch);
      }
      return stringBuilder.ToString();
    }

    private static IDictionary<string, object> ParseObject(
      char[] json,
      ref int index,
      ref bool success)
    {
      IDictionary<string, object> dictionary = (IDictionary<string, object>) new JsonObject();
      SimpleJson.NextToken(json, ref index);
      bool flag = false;
      while (!flag)
      {
        switch (SimpleJson.LookAhead(json, index))
        {
          case 0:
            success = false;
            return (IDictionary<string, object>) null;
          case 2:
            SimpleJson.NextToken(json, ref index);
            return dictionary;
          case 6:
            SimpleJson.NextToken(json, ref index);
            continue;
          default:
            string key = SimpleJson.ParseString(json, ref index, ref success);
            if (!success)
            {
              success = false;
              return (IDictionary<string, object>) null;
            }
            if (SimpleJson.NextToken(json, ref index) != 5)
            {
              success = false;
              return (IDictionary<string, object>) null;
            }
            object obj = SimpleJson.ParseValue(json, ref index, ref success);
            if (!success)
            {
              success = false;
              return (IDictionary<string, object>) null;
            }
            dictionary[key] = obj;
            continue;
        }
      }
      return dictionary;
    }

    private static JsonArray ParseArray(char[] json, ref int index, ref bool success)
    {
      JsonArray array = new JsonArray();
      SimpleJson.NextToken(json, ref index);
      bool flag = false;
      while (!flag)
      {
        switch (SimpleJson.LookAhead(json, index))
        {
          case 0:
            success = false;
            return (JsonArray) null;
          case 4:
            SimpleJson.NextToken(json, ref index);
            goto label_9;
          case 6:
            SimpleJson.NextToken(json, ref index);
            continue;
          default:
            object obj = SimpleJson.ParseValue(json, ref index, ref success);
            if (!success)
              return (JsonArray) null;
            array.Add(obj);
            continue;
        }
      }
label_9:
      return array;
    }

    private static object ParseValue(char[] json, ref int index, ref bool success)
    {
      switch (SimpleJson.LookAhead(json, index))
      {
        case 1:
          return (object) SimpleJson.ParseObject(json, ref index, ref success);
        case 3:
          return (object) SimpleJson.ParseArray(json, ref index, ref success);
        case 7:
          return (object) SimpleJson.ParseString(json, ref index, ref success);
        case 8:
          return SimpleJson.ParseNumber(json, ref index, ref success);
        case 9:
          SimpleJson.NextToken(json, ref index);
          return (object) true;
        case 10:
          SimpleJson.NextToken(json, ref index);
          return (object) false;
        case 11:
          SimpleJson.NextToken(json, ref index);
          return (object) null;
        default:
          success = false;
          return (object) null;
      }
    }

    private static string ParseString(char[] json, ref int index, ref bool success)
    {
      StringBuilder stringBuilder = new StringBuilder(2000);
      SimpleJson.EatWhitespace(json, ref index);
      char ch1 = json[index++];
      bool flag = false;
      while (!flag && index != json.Length)
      {
        char ch2 = json[index++];
        switch (ch2)
        {
          case '"':
            flag = true;
            goto label_23;
          case '\\':
            if (index != json.Length)
            {
              switch (json[index++])
              {
                case '"':
                  stringBuilder.Append('"');
                  continue;
                case '/':
                  stringBuilder.Append('/');
                  continue;
                case '\\':
                  stringBuilder.Append('\\');
                  continue;
                case 'b':
                  stringBuilder.Append('\b');
                  continue;
                case 'f':
                  stringBuilder.Append('\f');
                  continue;
                case 'n':
                  stringBuilder.Append('\n');
                  continue;
                case 'r':
                  stringBuilder.Append('\r');
                  continue;
                case 't':
                  stringBuilder.Append('\t');
                  continue;
                case 'u':
                  if (json.Length - index >= 4)
                  {
                    uint result1;
                    if (!(success = uint.TryParse(new string(json, index, 4), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result1)))
                      return "";
                    if (55296U <= result1 && result1 <= 56319U)
                    {
                      index += 4;
                      uint result2;
                      if (json.Length - index >= 6 && new string(json, index, 2) == "\\u" && uint.TryParse(new string(json, index + 2, 4), NumberStyles.HexNumber, (IFormatProvider) CultureInfo.InvariantCulture, out result2) && 56320U <= result2 && result2 <= 57343U)
                      {
                        stringBuilder.Append((char) result1);
                        stringBuilder.Append((char) result2);
                        index += 6;
                        continue;
                      }
                      success = false;
                      return "";
                    }
                    stringBuilder.Append(SimpleJson.ConvertFromUtf32((int) result1));
                    index += 4;
                    continue;
                  }
                  goto label_23;
                default:
                  continue;
              }
            }
            else
              goto label_23;
          default:
            stringBuilder.Append(ch2);
            continue;
        }
      }
label_23:
      if (flag)
        return stringBuilder.ToString();
      success = false;
      return (string) null;
    }

    private static string ConvertFromUtf32(int utf32)
    {
      if (utf32 < 0 || utf32 > 1114111)
        throw new ArgumentOutOfRangeException(nameof (utf32), "The argument must be from 0 to 0x10FFFF.");
      if (55296 <= utf32 && utf32 <= 57343)
        throw new ArgumentOutOfRangeException(nameof (utf32), "The argument must not be in surrogate pair range.");
      if (utf32 < 65536)
        return new string((char) utf32, 1);
      utf32 -= 65536;
      return new string(new char[2]
      {
        (char) ((utf32 >> 10) + 55296),
        (char) (utf32 % 1024 + 56320)
      });
    }

    private static object ParseNumber(char[] json, ref int index, ref bool success)
    {
      SimpleJson.EatWhitespace(json, ref index);
      int lastIndexOfNumber = SimpleJson.GetLastIndexOfNumber(json, index);
      int length = lastIndexOfNumber - index + 1;
      string str = new string(json, index, length);
      object number;
      if (str.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 || str.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
      {
        double result;
        success = double.TryParse(new string(json, index, length), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result);
        number = (object) result;
      }
      else
      {
        long result;
        success = long.TryParse(new string(json, index, length), NumberStyles.Any, (IFormatProvider) CultureInfo.InvariantCulture, out result);
        number = (object) result;
      }
      index = lastIndexOfNumber + 1;
      return number;
    }

    private static int GetLastIndexOfNumber(char[] json, int index)
    {
      int index1 = index;
      while (index1 < json.Length && "0123456789+-.eE".IndexOf(json[index1]) != -1)
        ++index1;
      return index1 - 1;
    }

    private static void EatWhitespace(char[] json, ref int index)
    {
      while (index < json.Length && " \t\n\r\b\f".IndexOf(json[index]) != -1)
        ++index;
    }

    private static int LookAhead(char[] json, int index)
    {
      int index1 = index;
      return SimpleJson.NextToken(json, ref index1);
    }

    private static int NextToken(char[] json, ref int index)
    {
      SimpleJson.EatWhitespace(json, ref index);
      if (index == json.Length)
        return 0;
      char ch = json[index];
      ++index;
      switch (ch)
      {
        case '"':
          return 7;
        case ',':
          return 6;
        case '-':
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
          return 8;
        case ':':
          return 5;
        case '[':
          return 3;
        case ']':
          return 4;
        case '{':
          return 1;
        case '}':
          return 2;
        default:
          --index;
          int num = json.Length - index;
          if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
          {
            index += 5;
            return 10;
          }
          if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
          {
            index += 4;
            return 9;
          }
          if (num < 4 || json[index] != 'n' || json[index + 1] != 'u' || json[index + 2] != 'l' || json[index + 3] != 'l')
            return 0;
          index += 4;
          return 11;
      }
    }

    private static bool SerializeValue(
      IJsonSerializerStrategy jsonSerializerStrategy,
      object value,
      StringBuilder builder)
    {
      bool flag1 = true;
      switch (value)
      {
        case string aString:
          flag1 = SimpleJson.SerializeString(aString, builder);
          break;
        case IDictionary<string, object> dictionary1:
          flag1 = SimpleJson.SerializeObject(jsonSerializerStrategy, (IEnumerable) dictionary1.Keys, (IEnumerable) dictionary1.Values, builder);
          break;
        case IDictionary<string, string> dictionary2:
          flag1 = SimpleJson.SerializeObject(jsonSerializerStrategy, (IEnumerable) dictionary2.Keys, (IEnumerable) dictionary2.Values, builder);
          break;
        case IEnumerable anArray:
          flag1 = SimpleJson.SerializeArray(jsonSerializerStrategy, anArray, builder);
          break;
        default:
          if (SimpleJson.IsNumeric(value))
          {
            flag1 = SimpleJson.SerializeNumber(value, builder);
            break;
          }
          if (value is bool flag2)
          {
            builder.Append(flag2 ? "true" : "false");
            break;
          }
          if (value == null)
          {
            builder.Append("null");
            break;
          }
          object output;
          flag1 = jsonSerializerStrategy.TrySerializeNonPrimitiveObject(value, out output);
          if (flag1)
          {
            SimpleJson.SerializeValue(jsonSerializerStrategy, output, builder);
            break;
          }
          break;
      }
      return flag1;
    }

    private static bool SerializeObject(
      IJsonSerializerStrategy jsonSerializerStrategy,
      IEnumerable keys,
      IEnumerable values,
      StringBuilder builder)
    {
      builder.Append("{");
      IEnumerator enumerator1 = keys.GetEnumerator();
      IEnumerator enumerator2 = values.GetEnumerator();
      bool flag = true;
      while (enumerator1.MoveNext() && enumerator2.MoveNext())
      {
        object current1 = enumerator1.Current;
        object current2 = enumerator2.Current;
        if (!flag)
          builder.Append(",");
        if (current1 is string aString)
          SimpleJson.SerializeString(aString, builder);
        else if (!SimpleJson.SerializeValue(jsonSerializerStrategy, current2, builder))
          return false;
        builder.Append(":");
        if (!SimpleJson.SerializeValue(jsonSerializerStrategy, current2, builder))
          return false;
        flag = false;
      }
      builder.Append("}");
      return true;
    }

    private static bool SerializeArray(
      IJsonSerializerStrategy jsonSerializerStrategy,
      IEnumerable anArray,
      StringBuilder builder)
    {
      builder.Append("[");
      bool flag = true;
      foreach (object an in anArray)
      {
        if (!flag)
          builder.Append(",");
        if (!SimpleJson.SerializeValue(jsonSerializerStrategy, an, builder))
          return false;
        flag = false;
      }
      builder.Append("]");
      return true;
    }

    private static bool SerializeString(string aString, StringBuilder builder)
    {
      if (aString.IndexOfAny(SimpleJson.EscapeCharacters) == -1)
      {
        builder.Append('"');
        builder.Append(aString);
        builder.Append('"');
        return true;
      }
      builder.Append('"');
      int charCount = 0;
      char[] charArray = aString.ToCharArray();
      for (int index1 = 0; index1 < charArray.Length; ++index1)
      {
        char index2 = charArray[index1];
        if ((int) index2 >= SimpleJson.EscapeTable.Length || SimpleJson.EscapeTable[(int) index2] == char.MinValue)
        {
          ++charCount;
        }
        else
        {
          if (charCount > 0)
          {
            builder.Append(charArray, index1 - charCount, charCount);
            charCount = 0;
          }
          builder.Append('\\');
          builder.Append(SimpleJson.EscapeTable[(int) index2]);
        }
      }
      if (charCount > 0)
        builder.Append(charArray, charArray.Length - charCount, charCount);
      builder.Append('"');
      return true;
    }

    private static bool SerializeNumber(object number, StringBuilder builder)
    {
      switch (number)
      {
        case long num1:
          builder.Append(num1.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case ulong num2:
          builder.Append(num2.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case int num3:
          builder.Append(num3.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case uint num4:
          builder.Append(num4.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case Decimal num5:
          builder.Append(num5.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        case float num6:
          builder.Append(num6.ToString((IFormatProvider) CultureInfo.InvariantCulture));
          break;
        default:
          builder.Append(Convert.ToDouble(number, (IFormatProvider) CultureInfo.InvariantCulture).ToString("r", (IFormatProvider) CultureInfo.InvariantCulture));
          break;
      }
      return true;
    }

    /// <summary>
    /// Determines if a given object is numeric in any way
    /// (can be integer, double, null, etc).
    /// </summary>
    private static bool IsNumeric(object value)
    {
      switch (value)
      {
        case sbyte _:
          return true;
        case byte _:
          return true;
        case short _:
          return true;
        case ushort _:
          return true;
        case int _:
          return true;
        case uint _:
          return true;
        case long _:
          return true;
        case ulong _:
          return true;
        case float _:
          return true;
        case double _:
          return true;
        case Decimal _:
          return true;
        default:
          return false;
      }
    }

    public static IJsonSerializerStrategy CurrentJsonSerializerStrategy
    {
      get
      {
        return SimpleJson._currentJsonSerializerStrategy ?? (SimpleJson._currentJsonSerializerStrategy = (IJsonSerializerStrategy) SimpleJson.PocoJsonSerializerStrategy);
      }
      set => SimpleJson._currentJsonSerializerStrategy = value;
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public static PocoJsonSerializerStrategy PocoJsonSerializerStrategy
    {
      get
      {
        return SimpleJson._pocoJsonSerializerStrategy ?? (SimpleJson._pocoJsonSerializerStrategy = new PocoJsonSerializerStrategy());
      }
    }
  }
}