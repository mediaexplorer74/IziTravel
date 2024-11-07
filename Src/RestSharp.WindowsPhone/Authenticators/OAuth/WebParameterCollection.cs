// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.WebParameterCollection
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.Collections.Generic;

#nullable disable
namespace RestSharp.Authenticators.OAuth
{
  internal class WebParameterCollection : WebPairCollection
  {
    public WebParameterCollection(IEnumerable<WebPair> parameters)
      : base(parameters)
    {
    }

    public WebParameterCollection()
    {
    }

    public WebParameterCollection(int capacity)
      : base(capacity)
    {
    }

    public WebParameterCollection(IDictionary<string, string> collection)
      : base(collection)
    {
    }

    public override void Add(string name, string value)
    {
      this.Add((WebPair) new WebParameter(name, value));
    }
  }
}
