// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.IAnalyticsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Analytics.Events.Application;
using Izi.Travel.Business.Entities.Analytics.Events.Content;
using Izi.Travel.Business.Entities.Analytics.Transaction;
using Izi.Travel.Business.Entities.Analytics.View;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface IAnalyticsService
  {
    void GoogleAnalyticsLaunchApplication();

    void SendContentEvent(BaseContentEvent eventInfo);

    void SendApplicationEvent(BaseApplicationEvent eventInfo);

    void SendView(ViewInfo viewInfo);

    void SendTransaction(TransactionInfo transactionInfo);

    void AdjustLaunchApplication();

    void AdjustSendEvent(string token);

    void AdjustSendRevenue(string token, double amount);
  }
}
