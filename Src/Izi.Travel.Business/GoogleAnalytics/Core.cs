﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AnalyticsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System.Collections.Generic;

namespace GoogleAnalytics
{
    public class Core
    {
        public class Transaction
        {
            public string TransactionId;
            public string Affiliation;
            public List<object> Items;
            public long TotalCostInMicros;
            public long TotalTaxInMicros;
            public int ShippingCostInMicros;
            public string CurrencyCode;
        }
    }
}