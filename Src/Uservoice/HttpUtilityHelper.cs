// Decompiled with JetBrains decompiler
// Type: UserVoice.HttpUtilityHelper
// Assembly: Uservoice, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 038B5345-2117-47AA-93A0-4A054BBF5C1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Uservoice.dll

using RestSharp.Extensions.MonoHttp;
using System.Net;

#nullable disable
namespace UserVoice
{
  internal static class HttpUtilityHelper
  {
    public static NameValueCollection ParseQueryString(string query)
    {
      NameValueCollection queryString = new NameValueCollection();
      if (query.Length == 0)
        return (NameValueCollection) null;
      string str1 = HttpUtility.HtmlDecode(query);
      int length = str1.Length;
      int num1 = 0;
      bool flag = true;
      while (num1 <= length)
      {
        int startIndex = -1;
        int num2 = -1;
        for (int index = num1; index < length; ++index)
        {
          if (startIndex == -1 && str1[index] == '=')
            startIndex = index + 1;
          else if (str1[index] == '&')
          {
            num2 = index;
            break;
          }
        }
        if (flag)
        {
          flag = false;
          if (str1[num1] == '?')
            ++num1;
        }
        string name;
        if (startIndex == -1)
        {
          name = (string) null;
          startIndex = num1;
        }
        else
          name = HttpUtility.UrlDecode(str1.Substring(num1, startIndex - num1 - 1));
        if (num2 < 0)
        {
          num1 = -1;
          num2 = str1.Length;
        }
        else
          num1 = num2 + 1;
        string str2 = HttpUtility.UrlDecode(str1.Substring(startIndex, num2 - startIndex));
        queryString.Add(name, str2);
        if (num1 == -1)
          break;
      }
      return queryString;
    }
  }
}
