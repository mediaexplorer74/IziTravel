﻿// Decompiled with JetBrains decompiler
// Type: RestSharp.Serializers.ISerializer
// Assembly: RestSharp.WindowsPhone, Version=105.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: EBEB4E14-2430-4192-BEF2-49D8C49DBCCE
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\RestSharp.WindowsPhone.xml

#nullable disable
namespace RestSharp.Serializers
{
  public interface ISerializer
  {
    string Serialize(object obj);

    string RootElement { get; set; }

    string Namespace { get; set; }

    string DateFormat { get; set; }

    string ContentType { get; set; }
  }
}