﻿// Decompiled with JetBrains decompiler
// Type: RestSharp.Authenticators.OAuth.WebPair
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Authenticators.OAuth
{
  internal class WebPair
  {
    public WebPair(string name, string value)
    {
      this.Name = name;
      this.Value = value;
    }

    public string Value { get; set; }

    public string Name { get; set; }
  }
}