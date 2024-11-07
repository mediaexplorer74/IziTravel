// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Extensions.MtgContentContractResolver
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Extensions
{
  public class MtgContentContractResolver : DefaultContractResolver
  {
    protected override IList<JsonProperty> CreateProperties(
      Type type,
      MemberSerialization memberSerialization)
    {
      return type != typeof (Content) ? base.CreateProperties(type, memberSerialization) : (IList<JsonProperty>) base.CreateProperties(type, memberSerialization).Where<JsonProperty>((Func<JsonProperty, bool>) (x => x.PropertyName != "Children")).ToList<JsonProperty>();
    }
  }
}
