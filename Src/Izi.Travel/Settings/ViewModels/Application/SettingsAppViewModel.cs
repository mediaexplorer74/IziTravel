// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Settings.ViewModels.Items;
using Izi.Travel.Shell.Settings.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  [View(typeof (SettingsListView))]
  public class SettingsAppViewModel : BaseListViewModel<SettingsListItemBaseViewModel>
  {
    private readonly ISettingsService _settingsService;

    public override string DisplayName
    {
      get => AppResources.LabelApplication.ToLower();
      set => throw new NotImplementedException();
    }

    public SettingsAppViewModel(ISettingsService settingsService)
    {
      this._settingsService = settingsService;
    }

    protected override Task<IEnumerable<SettingsListItemBaseViewModel>> GetDataAsync()
    {
      this.Items.Clear();
      return Task<IEnumerable<SettingsListItemBaseViewModel>>.Factory.StartNew((Func<IEnumerable<SettingsListItemBaseViewModel>>) (() =>
      {
        AppSettings appSettings = this._settingsService.GetAppSettings();
        System.Collections.Generic.List<SettingsListItemBaseViewModel> dataAsync = new System.Collections.Generic.List<SettingsListItemBaseViewModel>();
        System.Collections.Generic.List<SettingsListItemBaseViewModel> itemBaseViewModelList = dataAsync;
        string labelLanguages = AppResources.LabelLanguages;
        string info;
        if (appSettings.Languages == null)
          info = (string) null;
        else
          info = ((IEnumerable<string>) appSettings.Languages).Aggregate<string, string>(string.Empty, (Func<string, string, string>) ((x, y) => x + y.ToUpper() + ", ")).TrimEnd(',', ' ');
        SettingsListItemLanguageViewModel languageViewModel = new SettingsListItemLanguageViewModel(labelLanguages, info);
        itemBaseViewModelList.Add((SettingsListItemBaseViewModel) languageViewModel);
        dataAsync.Add((SettingsListItemBaseViewModel) new SettingsListItemLocationViewModel(AppResources.LabelLocation, appSettings.LocationEnabled ? AppResources.StringOn : AppResources.StringOff));
        dataAsync.Add((SettingsListItemBaseViewModel) new SettingsListItemNavigationViewModel<SettingsAppFeedbackMessageViewModel>(AppResources.LabelFeedback, AppResources.SettingsInfoFeedback));
        dataAsync.Add((SettingsListItemBaseViewModel) new SettingsListItemCodeNameViewModel(this.Parent as SettingsViewModel, AppResources.LabelCodeName, AppResources.SettingsInfoCodeName));
        dataAsync.Add((SettingsListItemBaseViewModel) new SettingsListItemLicenseViewModel(AppResources.LabelLicenses, AppResources.SettingsInfoLicenses));
        dataAsync.Add((SettingsListItemBaseViewModel) new SettingsListItemAboutViewModel(AppResources.PageTitleAbout, Assembly.GetExecutingAssembly().GetName().Version.ToString()));
        return (IEnumerable<SettingsListItemBaseViewModel>) dataAsync;
      }));
    }
  }
}
