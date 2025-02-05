// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.RedirectViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.ViewModels
{
  public class RedirectViewModel : Screen
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (RedirectViewModel));

    public string Uid { get; set; }

    public string Language { get; set; }

    protected override async void OnActivate()
    {
      INavigationService navigationService = ShellServiceFacade.NavigationService;
      try
      {
        MtgObjectFilter filter = new MtgObjectFilter();
        filter.Uid = this.Uid;
        MtgObjectFilter mtgObjectFilter = filter;
        string[] strArray;
        if (this.Language == null)
          strArray = RedirectViewModel.GetDefaultLanguages();
        else
          strArray = new string[1]{ this.Language };
        mtgObjectFilter.Languages = strArray;
        MtgObject mtgObjectAsync = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
        NavigationHelper.NavigateToDetails(mtgObjectAsync.Type, mtgObjectAsync.Uid, mtgObjectAsync.Language, mtgObjectAsync.ParentUid);
        navigationService.RemoveBackEntry();
      }
      catch (Exception ex)
      {
        RedirectViewModel.Logger.Error(ex);
        ShellServiceFacade.DialogService.Show(AppResources.ErrorTitleDataLoading, AppResources.ErrorMessageDataLoading, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) =>
        {
          if (navigationService.CanGoBack)
            navigationService.GoBack();
          else
            Application.Current.Terminate();
        }));
      }
    }

    private static string[] GetDefaultLanguages()
    {
      string[] languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
      return ((IList<string>) ServiceFacade.CultureService.GetNeutralLanguageCodes()).OrderAs((IList<string>) languages).ToArray<string>();
    }
  }
}
