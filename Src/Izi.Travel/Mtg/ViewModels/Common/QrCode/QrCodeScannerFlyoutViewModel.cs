// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.QrCode.QrCodeScannerFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Model;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.QrCode
{
  public class QrCodeScannerFlyoutViewModel : BaseSearchFlyoutViewModel
  {
    private bool _isEmpty;

    public bool IsEmpty
    {
      get => this._isEmpty;
      set => this.SetProperty<bool>(ref this._isEmpty, value, propertyName: nameof (IsEmpty));
    }

    public QrCodeScannerFlyoutViewModel(IScreen parentScreen)
      : base(parentScreen)
    {
    }

    protected override async Task<SearchFlyoutResult> SearchTask(object parameter)
    {
      this.IsEmpty = false;
      string data = parameter as string;
      if (string.IsNullOrWhiteSpace(data))
      {
        this.IsEmpty = true;
        return SearchFlyoutResult.Empty;
      }
      SearchFlyoutResult result = new SearchFlyoutResult();
      MtgLinkInfo mtgLinkInfo = MtgLinkHelper.Parse(data);
      if (mtgLinkInfo != null && mtgLinkInfo.Type != MtgLinkType.Unknown)
      {
        string lower = !string.IsNullOrWhiteSpace(mtgLinkInfo.Uid) ? mtgLinkInfo.Uid.ToLower() : (string) null;
        string parentUid = mtgLinkInfo.ParentUid ?? this.ParentUid;
        AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
        List<string> languages = new List<string>();
        if (!string.IsNullOrWhiteSpace(mtgLinkInfo.Language))
          languages.Add(mtgLinkInfo.Language);
        if (!string.IsNullOrWhiteSpace(this.ParentLanguage))
          languages.Add(this.ParentLanguage);
        languages.AddRange(((IList<string>) ServiceFacade.CultureService.GetNeutralLanguageCodes()).OrderAs((IList<string>) ServiceFacade.SettingsService.GetAppSettings().Languages).Where<string>((Func<string, bool>) (x => !languages.Contains<string>(x, (IEqualityComparer<string>) StringComparer.InvariantCultureIgnoreCase))));
        if (!string.IsNullOrWhiteSpace(mtgLinkInfo.Passcode))
        {
          appSettings.CodeName = mtgLinkInfo.Passcode;
          ServiceFacade.SettingsService.SaveAppSettings(appSettings);
        }
        MtgObject mtgObject = (MtgObject) null;
        try
        {
          MtgObjectFilter filter = new MtgObjectFilter();
          filter.Uid = lower;
          filter.Languages = languages.ToArray();
          filter.Includes = ContentSection.None;
          filter.Excludes = ContentSection.All;
          filter.Form = MtgObjectForm.Full;
          mtgObject = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
          if (parentUid != null)
          {
            if (mtgObject.ParentUid != parentUid)
              mtgObject = (MtgObject) null;
          }
        }
        catch (Exception ex)
        {
          this.Logger.Error(ex);
          this.IsEmpty = true;
          return SearchFlyoutResult.Empty;
        }
        if (mtgObject == null)
        {
          this.IsEmpty = true;
          return SearchFlyoutResult.Empty;
        }
        result.Success = true;
        result.MtgObject = mtgObject;
        if (string.IsNullOrWhiteSpace(parentUid))
          parentUid = mtgObject.ParentUid;
        MtgObject mtgObject1 = new MtgObject();
        mtgObject1.Uid = parentUid;
        mtgObject1.Type = this.ParentType;
        MtgObject mtgObjectParent = mtgObject1;
        result.MtgObjectParent = mtgObjectParent;
        this.ActivateInternal(mtgObjectParent, mtgObject, ActivationTypeParameter.QrCode);
        this.IsBusy = false;
        this.NavigateInternal(mtgObject, parentUid);
        parentUid = (string) null;
        mtgObject = (MtgObject) null;
      }
      else
      {
        Uri result1;
        if (Uri.TryCreate(data, UriKind.RelativeOrAbsolute, out result1))
        {
          try
          {
            if (!result1.OriginalString.StartsWith("http://", StringComparison.InvariantCultureIgnoreCase))
              result1 = new Uri("http://" + result1.OriginalString, UriKind.Absolute);
            new WebBrowserTask() { Uri = result1 }.Show();
          }
          catch (Exception ex)
          {
            this.Logger.Error(ex);
            ShellServiceFacade.DialogService.Show(AppResources.ErrorInvalidExternalLinkTitle, AppResources.ErrorInvalidExternalLinkInfo, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) null);
            this.IsEmpty = true;
            return SearchFlyoutResult.Empty;
          }
        }
        else
        {
          ShellServiceFacade.DialogService.Show(AppResources.LabelQrScanner, string.Format(AppResources.ErrorBarcodeIncorrectData, (object) data), MessageBoxButtonContent.Ok, (Action<FlyoutDialog>) (x => this.IsBusy = true), (Action<FlyoutDialog, MessageBoxResult>) ((d, x) => this.IsBusy = false));
          this.IsEmpty = true;
          return SearchFlyoutResult.Empty;
        }
      }
      return result;
    }
  }
}
