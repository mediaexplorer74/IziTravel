// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.StringSplitter
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System.Collections.Generic;
using System.Text;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Helper class when splitting strings</summary>
  public static class StringSplitter
  {
    /// <summary>
    /// Splits a string with a chosen separator.
    /// If a substring is contained in [...] it will not be splitted.
    /// </summary>
    /// <param name="message">The message to split</param>
    /// <param name="separator">The separator to use when splitting</param>
    /// <returns></returns>
    public static string[] Split(string message, char separator)
    {
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      int num = 0;
      foreach (char ch in message)
      {
        switch (ch)
        {
          case '[':
            ++num;
            break;
          case ']':
            --num;
            break;
          default:
            if ((int) ch == (int) separator && num == 0)
            {
              if (!string.IsNullOrEmpty(stringBuilder.ToString()))
                stringList.Add(stringBuilder.ToString());
              stringBuilder.Length = 0;
              continue;
            }
            break;
        }
        stringBuilder.Append(ch);
      }
      if (!string.IsNullOrEmpty(stringBuilder.ToString()))
        stringList.Add(stringBuilder.ToString());
      return stringList.ToArray();
    }

    /// <summary>
    /// Splits a string with , as separator.
    /// Does not split within {},[],()
    /// </summary>
    /// <param name="parameters">The string to split</param>
    /// <returns></returns>
    public static string[] SplitParameters(string parameters)
    {
      List<string> stringList = new List<string>();
      StringBuilder stringBuilder = new StringBuilder();
      bool flag = false;
      int num1 = 0;
      int num2 = 0;
      int num3 = 0;
      for (int index = 0; index < parameters.Length; ++index)
      {
        char parameter = parameters[index];
        if (parameter == '"' && (index == 0 || parameters[index - 1] != '\\'))
          flag = !flag;
        if (!flag)
        {
          switch (parameter)
          {
            case '(':
              ++num3;
              break;
            case ')':
              --num3;
              break;
            case '[':
              ++num2;
              break;
            case ']':
              --num2;
              break;
            case '{':
              ++num1;
              break;
            case '}':
              --num1;
              break;
            default:
              if (parameter == ',' && num3 == 0 && num2 == 0 && num1 == 0)
              {
                stringList.Add(stringBuilder.ToString());
                stringBuilder.Length = 0;
                continue;
              }
              break;
          }
        }
        stringBuilder.Append(parameter);
      }
      stringList.Add(stringBuilder.ToString());
      return stringList.ToArray();
    }
  }
}
