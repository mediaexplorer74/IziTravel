// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.PurchaseManager
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Exceptions;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.Store;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Business.Managers
{
  public sealed class PurchaseManager
  {
    private static volatile PurchaseManager _instance;
    private static readonly object SyncRoot = new object();
    private readonly ILocalDataService _localDataService = IoC.Get<ILocalDataService>();
    private readonly ILog _log = LogManager.GetLog(typeof (PurchaseManager));
    private ImmutableList<string> _purchaseUids;

    public static PurchaseManager Instance
    {
      get
      {
        if (PurchaseManager._instance == null)
        {
          lock (PurchaseManager.SyncRoot)
          {
            if (PurchaseManager._instance == null)
              PurchaseManager._instance = new PurchaseManager();
          }
        }
        return PurchaseManager._instance;
      }
    }

    public event TypedEventHandler<string, bool> IsPurchasedChanged
    {
      add
      {
        TypedEventHandler<string, bool> typedEventHandler1 = default;//this.IsPurchasedChanged;
        TypedEventHandler<string, bool> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<string, bool>>(ref this.IsPurchasedChanged, (TypedEventHandler<string, bool>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<string, bool> typedEventHandler1 = default;//this.IsPurchasedChanged;
        TypedEventHandler<string, bool> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = default;//Interlocked.CompareExchange<TypedEventHandler<string, bool>>(ref this.IsPurchasedChanged, (TypedEventHandler<string, bool>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public static System.Action NotifyConnectionErrorOccurred { get; set; }

    public bool CanRestorePurchases
    {
      get
      {
        return this._purchaseUids.Count 
                    != CurrentApp.LicenseInformation.ProductLicenses.Count<KeyValuePair<string, 
                    ProductLicense>>((Func<KeyValuePair<string, ProductLicense>, bool>)
                    (x => x.Value.IsActive));
      }
    }

    public void Initialize()
    {
      using (new Profiler("PurchaseManager.Initialize"))
      {
        this._purchaseUids = ImmutableList<string>.Empty;
        try
        {
          this._purchaseUids = this._purchaseUids.AddRange((IEnumerable<string>)
              this._localDataService.GetPurchaseUidList());
        }
        catch (Exception ex)
        {
          this._log.Error(ex);
          if (!Debugger.IsAttached)
            return;
          Debugger.Break();
        }
      }
    }

    public bool IsPurchased(MtgObject mtgObject)
    {
      return mtgObject == null || mtgObject.Uid == null 
                || mtgObject.Purchase == null || mtgObject.Purchase.Price == 0M
                || this.Contains(mtgObject.Uid);
    }

    public bool Contains(string uid)
    {
      return this._purchaseUids.Any<string>((Func<string, bool>)
          (x => string.Equals(x, uid, StringComparison.CurrentCultureIgnoreCase)));
    }

    public async Task Purchase(MtgObject mtgObject)
    {
      try
      {
        if (mtgObject == null || mtgObject.Uid == null)
          return;
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        ProductIdFilter filter = new ProductIdFilter();
        filter.Uid = mtgObject.Uid;
        CancellationToken ct = new CancellationToken();
        string productId = await mtgObjectService.GetProductIdAsync(filter, ct);
        if (string.IsNullOrWhiteSpace(productId))
          return;
        if ((await CurrentApp.LoadListingInformationByProductIdsAsync((IEnumerable<string>) new string[1]
        {
          productId
        })).ProductListings.Count == 0)
          throw new BusinessException();
        string receipt = string.Empty;
        ProductLicense productLicense;
        if (!CurrentApp.LicenseInformation.ProductLicenses.TryGetValue(productId, out productLicense) 
                    || !productLicense.IsActive)
          receipt = await CurrentApp.RequestProductPurchaseAsync(productId, true);
        await this.AddPurchase(mtgObject);
        AnalyticsHelper.SendTransaction(mtgObject, receipt);
        productId = (string) null;
        receipt = (string) null;
      }
      catch (BusinessException ex)
      {
        this._log.Error((Exception) ex);
        
        //RnD
        //Deployment.Current.Dispatcher.BeginInvoke((System.Action) (() =>
        //{
        //  System.Action connectionErrorOccurred = PurchaseManager.NotifyConnectionErrorOccurred;
        //  if (connectionErrorOccurred == null)
        //    return;
        //  connectionErrorOccurred();
        //}));
      }
      catch (Exception ex)
      {
        this._log.Error(ex);
      }
    }

    public async Task RestorePurchases()
    {
      try
      {
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        PaidMtgObjectListFilter filter = new PaidMtgObjectListFilter();
        filter.ProductIds = CurrentApp.LicenseInformation
                    .ProductLicenses.Where<KeyValuePair<string, ProductLicense>>
                    ((Func<KeyValuePair<string, ProductLicense>, bool>) (x => x.Value.IsActive))
                    .Select<KeyValuePair<string, ProductLicense>, string>(
            (Func<KeyValuePair<string, ProductLicense>, string>) (x => x.Value.ProductId))
                    .ToArray<string>();

        filter.Languages = ServiceFacade.CultureService.GetNeutralLanguageCodes();
        CancellationToken ct = new CancellationToken();
        foreach (MtgObject mtgObject in ((IEnumerable<MtgObject>) 
                    await mtgObjectService.GetPaidMtgObjectListAsync(filter, ct))
                    .Where<MtgObject>((Func<MtgObject, bool>) 
                    (x => this._purchaseUids.All<string>((Func<string, bool>)
                    (y => !string.Equals(y, x.Uid, StringComparison.CurrentCultureIgnoreCase))))))
          await this.AddPurchase(mtgObject);
      }
      catch (Exception ex)
      {
        this._log.Error(ex);
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    private async Task AddPurchase(MtgObject mtgObject)
    {
      await ServiceFacade.MtgObjectService.CreatePurchaseAsync(mtgObject);
      this._purchaseUids = this._purchaseUids.Add(mtgObject.Uid);
      this.OnIsPurchasedChanged(mtgObject.Uid, true);
    }

    private void OnIsPurchasedChanged(string uid, bool isPurchased)
    {
      // RnD
      //Deployment.Current.Dispatcher.BeginInvoke((System.Action) 
      //    (() => this.IsPurchasedChanged?.Invoke(uid, isPurchased)));
    }
  }
}
