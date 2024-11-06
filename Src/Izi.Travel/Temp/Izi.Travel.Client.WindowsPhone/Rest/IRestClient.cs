﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Rest.IRestClient
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Client.Rest
{
  public interface IRestClient
  {
    Uri BaseUri { get; set; }

    List<Parameter> DefaultParameters { get; }

    Task<IRestResponse<T>> ExecuteTaskAsync<T>(IRestRequest request);
  }
}
