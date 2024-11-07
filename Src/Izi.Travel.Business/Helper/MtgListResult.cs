// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.MtgListResult
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public class MtgListResult
  {
    public List<MtgObject> Data { get; private set; }

    public bool RemoteSuccess { get; internal set; }

    public bool LocalSuccess { get; internal set; }

    public MtgListResult() => this.Data = new List<MtgObject>();
  }
}
