// Decompiled with JetBrains decompiler
// Type: UserVoice.NameValueCollection
// Assembly: Uservoice, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 038B5345-2117-47AA-93A0-4A054BBF5C1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Uservoice.dll

using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace UserVoice
{
  internal class NameValueCollection
  {
    private readonly Dictionary<string, string> _dictionary;

    public string this[string name]
    {
      get
      {
        string str = (string) null;
        this._dictionary.TryGetValue(name, out str);
        return str;
      }
      set
      {
        if (this._dictionary.ContainsKey(name))
          this._dictionary[name] = value;
        else
          this._dictionary.Add(name, value);
      }
    }

    public List<string> AllKeys => this._dictionary.Keys.ToList<string>();

    public NameValueCollection() => this._dictionary = new Dictionary<string, string>();

    public void Add(string name, string value) => this._dictionary[name] = value;
  }
}
