// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.Transaction
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System.Collections.Generic;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class Transaction
  {
    public Transaction() => this.Items = (IList<TransactionItem>) new List<TransactionItem>();

    public Transaction(string transactionId, long totalCostInMicros)
      : this()
    {
      this.TransactionId = transactionId;
      this.TotalCostInMicros = totalCostInMicros;
    }

    public string TransactionId { get; set; }

    public string Affiliation { get; set; }

    public long TotalCostInMicros { get; set; }

    public long ShippingCostInMicros { get; set; }

    public long TotalTaxInMicros { get; set; }

    public string CurrencyCode { get; set; }

    public IList<TransactionItem> Items { get; private set; }
  }
}
