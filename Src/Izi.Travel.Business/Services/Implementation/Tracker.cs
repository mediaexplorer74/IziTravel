// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.AnalyticsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using GoogleAnalytics;
using System;

namespace Izi.Travel.Business.Services.Implementation
{
    internal class Tracker
    {
        internal void SendEvent(string value1, string value2, string label, long v)
        {
            throw new NotImplementedException();
        }

        internal void SendTransaction(Core.Transaction transaction)
        {
            throw new NotImplementedException();
        }

        internal void SendView(string name)
        {
            throw new NotImplementedException();
        }

        internal void SetCustomDimension(int index, string value)
        {
            throw new NotImplementedException();
        }
    }
}