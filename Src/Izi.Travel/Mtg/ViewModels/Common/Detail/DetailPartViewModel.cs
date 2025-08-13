// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.DetailPartViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Context;
using Izi.Travel.Core.Controls.Flyout;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Core.Services.Entities;
using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Izi.Travel.Core.Extensions;
#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail
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
        var navigationContext = IoC.Get<IFrameNavigationContext>();
        if (navigationContext?.NavigationMode == NavigationMode.Back)
        {
            var languageKey = GetLanguagePhoneStateKey();
            SelectedLanguage = IoC.Get<IPhoneService>()?.State.Get<string, string>(languageKey);
        }
        base.OnInitialize();
    }

    protected override void OnDeactivate(bool close)
    {
        var languageKey = GetLanguagePhoneStateKey();
        IoC.Get<IPhoneService>()?.State.Set(languageKey, SelectedLanguage);
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
        var viewModel = IoC.Get<DetailViewModel>(MtgObject.Type.ToString());
        if (viewModel != null)
        {
            //viewModel.MtgObject = MtgObject;
            return viewModel;
        }
        
        ShellServiceFacade.DialogService.Show(
            AppResources.ErrorTitleDataLoading, 
            AppResources.ErrorMessageOpenNotSupportedObject, 
            MessageBoxButtonContent.Ok, 
            (d, e) => NavigationHelper.TryGoBack());
            
        return null;
    }

    private string GetLanguagePhoneStateKey() => "DetailPartViewModel.SelectedLanguage." + this.Uid;
  }
}
