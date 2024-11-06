// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Transaction.TransactionInfo
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Transaction
{
  public class TransactionInfo
  {
    public const string Affilation = "Windows Phone Store";
    public const string CategoryContent = "Content";
    private readonly List<TransactionItemInfo> _items = new List<TransactionItemInfo>();

    public string Id { get; set; }

    public double TotalCost { get; set; }

    public double TotalTax { get; set; }

    public string CurrencyCode { get; set; }

    public IList<TransactionItemInfo> Items => (IList<TransactionItemInfo>) this._items;
  }
}
