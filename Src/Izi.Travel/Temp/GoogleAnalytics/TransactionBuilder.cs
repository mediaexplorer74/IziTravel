// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.TransactionBuilder
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using GoogleAnalytics.Core;
using System;
using System.Globalization;
using Windows.ApplicationModel.Store;

#nullable disable
namespace GoogleAnalytics
{
  public static class TransactionBuilder
  {
    static TransactionBuilder() => TransactionBuilder.StoreName = "Windows Phone 8 App Store";

    public static string StoreName { get; set; }

    public static Transaction GetProductPurchaseTransaction(
      ListingInformation listingInformation,
      string receipt)
    {
      ProductReceipt productReceipt = ProductReceipt.Load(receipt);
      string id = productReceipt.Id;
      string productId = productReceipt.ProductId;
      string productType = productReceipt.ProductType;
      ProductListing productListing = listingInformation.ProductListings[productId];
      string isoCurrencySymbol = RegionInfo.CurrentRegion.ISOCurrencySymbol;
      long num = (long) (double.Parse(productListing.FormattedPrice, NumberStyles.Currency, (IFormatProvider) CultureInfo.CurrentCulture) * 1000000.0);
      Transaction purchaseTransaction = new Transaction(id, num);
      purchaseTransaction.Affiliation = TransactionBuilder.StoreName;
      purchaseTransaction.CurrencyCode = isoCurrencySymbol;
      purchaseTransaction.Items.Add(new TransactionItem(productId, productListing.Name, num, 1L)
      {
        Category = productType
      });
      return purchaseTransaction;
    }

    public static Transaction GetAppPurchaseTransaction(
      ListingInformation listingInformation,
      string receipt)
    {
      AppReceipt appReceipt = AppReceipt.Load(receipt);
      string id = appReceipt.Id;
      string appId = appReceipt.AppId;
      string licenseType = appReceipt.LicenseType;
      string isoCurrencySymbol = RegionInfo.CurrentRegion.ISOCurrencySymbol;
      long num = (long) (double.Parse(listingInformation.FormattedPrice, NumberStyles.Currency, (IFormatProvider) CultureInfo.CurrentCulture) * 1000000.0);
      Transaction purchaseTransaction = new Transaction(id, num);
      purchaseTransaction.Affiliation = TransactionBuilder.StoreName;
      purchaseTransaction.CurrencyCode = isoCurrencySymbol;
      purchaseTransaction.Items.Add(new TransactionItem(appId, listingInformation.Name, num, 1L)
      {
        Category = licenseType
      });
      return purchaseTransaction;
    }
  }
}
