// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AnalyticsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

namespace Izi.Travel.Business.Services.Implementation
{
    internal class TransactionItem
    {
        public TransactionItem()
        {
        }

        public string TransactionId { get; set; }
        public string Category { get; set; }
        public string SKU { get; set; }
        public string Name { get; set; }
        public long Quantity { get; set; }
        public long PriceInMicros { get; set; }
        public string CurrencyCode { get; set; }
    }
}