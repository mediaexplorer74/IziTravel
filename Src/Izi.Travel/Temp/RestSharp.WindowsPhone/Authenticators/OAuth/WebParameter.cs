// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.WebParameter
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System.Diagnostics;

#nullable disable
namespace RestSharp.Authenticators.OAuth
{
  [DebuggerDisplay("{Name}:{Value}")]
  internal class WebParameter : WebPair
  {
    public WebParameter(string name, string value)
      : base(name, value)
    {
    }
  }
}
