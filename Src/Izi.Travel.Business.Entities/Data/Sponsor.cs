// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Sponsor
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Sponsor
  {
    public string Name { get; set; }

    public string Website { get; set; }

    public int Order { get; set; }

    public Media[] Images { get; set; }
  }
}
