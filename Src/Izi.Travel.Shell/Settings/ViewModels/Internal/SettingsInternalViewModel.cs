// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Internal.SettingsInternalViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Settings.ViewModels.Items;
using Izi.Travel.Shell.Settings.Views;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Internal
{
  [View(typeof (SettingsListView))]
  public class SettingsInternalViewModel : BaseListViewModel<SettingsListItemBaseViewModel>
  {
    private readonly ISettingsService _settingsService;

    public override string DisplayName
    {
      get => AppResources.LabelSystem.ToLower();
      set => throw new NotImplementedException();
    }

    public SettingsInternalViewModel(ISettingsService settingsService)
    {
      this._settingsService = settingsService;
    }

    protected override Task<IEnumerable<SettingsListItemBaseViewModel>> GetDataAsync()
    {
      this.Items.Clear();
      return Task<IEnumerable<SettingsListItemBaseViewModel>>.Factory.StartNew((Func<IEnumerable<SettingsListItemBaseViewModel>>) (() =>
      {
        AppSettings appSettings = this._settingsService.GetAppSettings();
        return (IEnumerable<SettingsListItemBaseViewModel>) new System.Collections.Generic.List<SettingsListItemBaseViewModel>()
        {
          (SettingsListItemBaseViewModel) new SettingsListItemServerViewModel(AppResources.LabelServer, Enum.GetName(typeof (ServerEnvironment), (object) appSettings.ServerEnvironment)),
          (SettingsListItemBaseViewModel) new SettingsListItemTourEmulationViewModel(AppResources.LabelTourEmulation, appSettings.TourEmulationEnabled ? string.Format("{0}, {1:F2} km/h", (object) AppResources.StringOn, (object) appSettings.TourEmulationSpeed) : AppResources.StringOff),
          (SettingsListItemBaseViewModel) new SettingsListItemContentViewModel("Content", SettingsInternalViewModel.GetContentInfo())
        };
      }));
    }

    private static string GetContentInfo()
    {
      double availableSize = IsolatedStorageFileHelper.GetAvailableSize();
      return string.Format("{0:F1} MB used, {1:F1} MB free", (object) IsolatedStorageFileHelper.GetDirectorySize(ServiceFacade.MediaService.GetLocalDirectory()).ToMegabytes(), (object) availableSize.ToMegabytes());
    }
  }
}
