﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.AccessTypeParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class AccessTypeParameter : BaseParameter
  {
    private const int ParamIndex = 4;
    public static readonly AccessTypeParameter Online = new AccessTypeParameter(nameof (Online));
    public static readonly AccessTypeParameter Offline = new AccessTypeParameter(nameof (Offline));

    private AccessTypeParameter(string value)
      : base(4, value)
    {
    }
  }
}
