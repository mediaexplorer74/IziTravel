// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.DetailPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Context;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public class DetailPartViewModel : 
    MtgObjectPartViewModel,
    IDetailPartViewModel,
    IMtgObjectPartViewModel,
    IMtgObjectProvider
  {
    public string SelectedLanguage { get; set; }

    public string ActivationType { get; set; }

    public bool AutoPlay { get; set; }

    protected override void ExecuteRefreshCommand(object parameter)
    {
      this.AutoPlay = false;
      base.ExecuteRefreshCommand(parameter);
    }

    protected override Task<MtgObject> LoadMtgObjectAsync()
    {
      string[] languages;
      if (!string.IsNullOrWhiteSpace(this.SelectedLanguage))
        languages = new string[1]{ this.SelectedLanguage };
      else if (!string.IsNullOrWhiteSpace(this.Language))
        languages = new string[1]{ this.Language };
      else
        languages = ((IList<string>) ServiceFacade.CultureService.GetNeutralLanguageCodes()).OrderAs((IList<string>) ServiceFacade.SettingsService.GetAppSettings().Languages).ToArray<string>();
      MtgObjectFilter filter = new MtgObjectFilter(this.Uid, languages);
      filter.Form = MtgObjectForm.Full;
      filter.Includes = ContentSection.References | ContentSection.News | ContentSection.Sponsors;
      return MtgObjectServiceHelper.GetMtgObjectAsync(filter);
    }

    protected override void OnInitialize()
    {
      if (IoC.Get<IFrameNavigationContext>().NavigationMode == NavigationMode.Back)
        this.SelectedLanguage = IoC.Get<IPhoneService>().State.Get<string, object>(this.GetLanguagePhoneStateKey()) as string;
      base.OnInitialize();
    }

    protected override void OnDeactivate(bool close)
    {
      IoC.Get<IPhoneService>().State.Set<string, object>(this.GetLanguagePhoneStateKey(), (object) this.SelectedLanguage);
      base.OnDeactivate(close);
    }

    protected override async void OnLoadedFirst()
    {
      if (this.MtgObject != null && this.MtgObject.MainContent != null)
        this.SelectedLanguage = this.MtgObject.MainContent.Language;
      AnalyticsHelper.SendOpen(this.MtgObject);
      await ServiceFacade.MtgObjectService.CreateOrUpdateHistoryAsync(this.MtgObject, this.ParentUid);
    }

    protected override IScreen CreateScreenItem()
    {
      DetailViewModel screenItem = IoC.Get<DetailViewModel>(this.MtgObject.Type.ToString());
      if (screenItem != null)
        return (IScreen) screenItem;
      ShellServiceFacade.DialogService.Show(AppResources.ErrorTitleDataLoading, AppResources.ErrorMessageOpenNotSupportedObject, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) => NavigationHelper.TryGoBack()));
      return (IScreen) screenItem;
    }

    private string GetLanguagePhoneStateKey() => "DetailPartViewModel.SelectedLanguage." + this.Uid;
  }
}
