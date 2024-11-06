// Decompiled with JetBrains decompiler
// Type: RestSharp.SimpleFactory`1
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp
{
  public class SimpleFactory<T> : IHttpFactory where T : IHttp, new()
  {
    public IHttp Create() => (IHttp) new T();
  }
}
