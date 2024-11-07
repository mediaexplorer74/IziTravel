// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Filters.MtgObjectFilter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters.Base;

#nullable disable
namespace Izi.Travel.Business.Entities.Filters
{
  public class MtgObjectFilter : MtgObjectFilterBase
  {
    public string Uid { get; set; }

    public MtgObjectFilter() => this.Form = MtgObjectForm.Full;

    public MtgObjectFilter(string uid, string[] languages)
      : this()
    {
      this.Uid = uid;
      this.Languages = languages;
    }

    public MtgObjectFilter(string uid, string language)
      : this()
    {
      this.Uid = uid;
      this.Languages = new string[1]{ language };
    }
  }
}
