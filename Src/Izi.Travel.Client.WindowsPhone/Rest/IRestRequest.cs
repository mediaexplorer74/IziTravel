// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.IRestRequest
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public interface IRestRequest
  {
    string Resource { get; set; }

    Method Method { get; set; }

    object Content { get; set; }

    List<Parameter> Parameters { get; }
  }
}
