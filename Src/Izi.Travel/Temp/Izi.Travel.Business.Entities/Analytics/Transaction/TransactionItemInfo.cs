// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Transaction.TransactionItemInfo
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Transaction
{
  public class TransactionItemInfo
  {
    public string Sku { get; set; }

    public string Name { get; set; }

    public double Price { get; set; }

    public long Quantity { get; set; }

    public string CurrencyCode { get; set; }
  }
}
