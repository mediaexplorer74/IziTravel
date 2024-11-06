// Decompiled with JetBrains decompiler
// Type: HtmlAgilityPack.NameValuePairList
// Assembly: HtmlAgilityPack-PCL, Version=1.4.6.0, Culture=neutral, PublicKeyToken=null
// MVID: A611BE5D-A211-439D-AF0B-7D7BA44DC844
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\HtmlAgilityPack-PCL.xml

using System;
using System.Collections.Generic;

#nullable disable
namespace HtmlAgilityPack
{
  internal class NameValuePairList
  {
    internal readonly string Text;
    private List<KeyValuePair<string, string>> _allPairs;
    private Dictionary<string, List<KeyValuePair<string, string>>> _pairsWithName;

    internal NameValuePairList()
      : this((string) null)
    {
    }

    internal NameValuePairList(string text)
    {
      this.Text = text;
      this._allPairs = new List<KeyValuePair<string, string>>();
      this._pairsWithName = new Dictionary<string, List<KeyValuePair<string, string>>>();
      this.Parse(text);
    }

    internal static string GetNameValuePairsValue(string text, string name)
    {
      return new NameValuePairList(text).GetNameValuePairValue(name);
    }

    internal List<KeyValuePair<string, string>> GetNameValuePairs(string name)
    {
      if (name == null)
        return this._allPairs;
      return !this._pairsWithName.ContainsKey(name) ? new List<KeyValuePair<string, string>>() : this._pairsWithName[name];
    }

    internal string GetNameValuePairValue(string name)
    {
      List<KeyValuePair<string, string>> keyValuePairList = name != null ? this.GetNameValuePairs(name) : throw new ArgumentNullException();
      return keyValuePairList.Count == 0 ? string.Empty : keyValuePairList[0].Value.Trim();
    }

    private void Parse(string text)
    {
      this._allPairs.Clear();
      this._pairsWithName.Clear();
      if (text == null)
        return;
      string str = text;
      char[] chArray = new char[1]{ ';' };
      foreach (string Subject in str.Split(chArray))
      {
        if (Subject.Length != 0)
        {
          string[] strArray = Subject.Split(new char[1]
          {
            '='
          }, 2);
          if (strArray.Length != 0)
          {
            KeyValuePair<string, string> keyValuePair = new KeyValuePair<string, string>(strArray[0].Trim().ToLower(), strArray.Length < 2 ? "" : strArray[1]);
            this._allPairs.Add(keyValuePair);
            List<KeyValuePair<string, string>> keyValuePairList;
            if (!this._pairsWithName.ContainsKey(keyValuePair.Key))
            {
              keyValuePairList = new List<KeyValuePair<string, string>>();
              this._pairsWithName[keyValuePair.Key] = keyValuePairList;
            }
            else
              keyValuePairList = this._pairsWithName[keyValuePair.Key];
            keyValuePairList.Add(keyValuePair);
          }
        }
      }
    }
  }
}
