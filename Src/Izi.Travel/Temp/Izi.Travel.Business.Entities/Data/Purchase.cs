// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Purchase
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System;
using System.Globalization;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Purchase
  {
    public Decimal Price { get; set; }

    public string Currency { get; set; }

    public string ProductId { get; set; }

    public string PriceString
    {
      get => this.Price.ToString("C", (IFormatProvider) new CultureInfo("nl")).Replace(' ', ' ');
    }
  }
}
