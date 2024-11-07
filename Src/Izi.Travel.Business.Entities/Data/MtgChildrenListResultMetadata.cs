// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.MtgChildrenListResultMetadata
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class MtgChildrenListResultMetadata
  {
    public int Offset { get; set; }

    public int Limit { get; set; }

    public int TotalCount { get; set; }

    public int ReturnedCount { get; set; }

    public double Spent { get; set; }

    public int PageTotal { get; set; }

    public int PageCurrent { get; set; }

    public bool PageLeft { get; set; }

    public bool PageRight { get; set; }

    public bool PageOutOfBounds { get; set; }
  }
}
