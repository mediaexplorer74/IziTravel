// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.TransactionItem
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class TransactionItem
  {
    public TransactionItem()
    {
    }

    public TransactionItem(string sku, string name, long priceInMicros, long quantity)
    {
      this.Name = name;
      this.PriceInMicros = priceInMicros;
      this.Quantity = quantity;
      this.SKU = sku;
    }

    public TransactionItem(
      string transactionId,
      string sku,
      string name,
      long priceInMicros,
      long quantity)
    {
      this.TransactionId = transactionId;
      this.Name = name;
      this.PriceInMicros = priceInMicros;
      this.Quantity = quantity;
      this.SKU = sku;
    }

    public string Name { get; set; }

    public long PriceInMicros { get; set; }

    public long Quantity { get; set; }

    public string SKU { get; set; }

    public string Category { get; set; }

    public string TransactionId { get; set; }

    public string CurrencyCode { get; set; }
  }
}
