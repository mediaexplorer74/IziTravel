// Decompiled with JetBrains decompiler
// Type: RestSharp.IJsonSerializerStrategy
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

using System;
using System.CodeDom.Compiler;

#nullable disable
namespace RestSharp
{
  [GeneratedCode("simple-json", "1.0.0")]
  public interface IJsonSerializerStrategy
  {
    bool TrySerializeNonPrimitiveObject(object input, out object output);

    object DeserializeObject(object value, Type type);
  }
}
