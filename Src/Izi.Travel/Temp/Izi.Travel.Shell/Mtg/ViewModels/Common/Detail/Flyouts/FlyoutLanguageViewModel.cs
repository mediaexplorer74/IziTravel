// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Flyouts.FlyoutLanguageViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.Flyout;
using Izi.Travel.Shell.Core.Command;
using System;
using System.Collections.ObjectModel;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Flyouts
{
  public class FlyoutLanguageViewModel : FlyoutViewModel
  {
    private readonly DetailViewModel _detailViewModel;
    private readonly ObservableCollection<FlyoutLanguageItemViewModel> _languages;
    private FlyoutLanguageItemViewModel _selectedLanguage;
    private RelayCommand _selectCommand;

    public ObservableCollection<FlyoutLanguageItemViewModel> Languages => this._languages;

    public FlyoutLanguageViewModel(DetailViewModel detailViewModel)
    {
      this._languages = new ObservableCollection<FlyoutLanguageItemViewModel>();
      this._detailViewModel = detailViewModel;
    }

    public RelayCommand SelectCommand
    {
      get
      {
        return this._selectCommand ?? (this._selectCommand = new RelayCommand(new Action<object>(this.ExecuteSelectCommand)));
      }
    }

    private void ExecuteSelectCommand(object obj)
    {
      this._selectedLanguage = (FlyoutLanguageItemViewModel) obj;
      this.IsOpen = false;
    }

    public void Initialize(MtgObject mtgObject)
    {
      this.Languages.Clear();
      if (mtgObject == null || mtgObject.MainContent == null || mtgObject.Languages == null || mtgObject.Languages.Length == 0)
        return;
      foreach (string language in mtgObject.Languages)
      {
        LanguageData languageByIsoCode = ServiceFacade.CultureService.GetLanguageByIsoCode(language);
        if (languageByIsoCode != null)
          this.Languages.Add(new FlyoutLanguageItemViewModel()
          {
            Name = languageByIsoCode.Name,
            Code = languageByIsoCode.Code,
            Title = languageByIsoCode.NativeName,
            IsSelected = languageByIsoCode.Name == mtgObject.MainContent.Language
          });
      }
    }

    protected override void OnClosed()
    {
      if (this._detailViewModel.DetailPartViewModel == null || this._selectedLanguage == null || this._detailViewModel.DetailPartViewModel.SelectedLanguage == this._selectedLanguage.Name)
        return;
      this._detailViewModel.DetailPartViewModel.SelectedLanguage = this._selectedLanguage.Name;
      if (this._detailViewModel.DetailPartViewModel.RefreshCommand.CanExecute((object) null))
        this._detailViewModel.DetailPartViewModel.RefreshCommand.Execute((object) null);
      this._selectedLanguage = (FlyoutLanguageItemViewModel) null;
    }
  }
}
