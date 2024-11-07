// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Review
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Review
  {
    public int Id { get; set; }

    public string Language { get; set; }

    public int Rating { get; set; }

    public string Text { get; set; }

    public string ReviewerName { get; set; }

    public DateTime Date { get; set; }
  }
}
