// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppLanguageViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppLanguageViewModel : BaseListViewModel<SettingsAppLanguageListItemViewModel>
  {
    private RelayCommand _addLanguagesCommand;
    private RelayCommand _setDefaultLanguageCommand;
    private RelayCommand _deleteLanguageCommand;

    public RelayCommand AddLanguagesCommand
    {
      get
      {
        return this._addLanguagesCommand ?? (this._addLanguagesCommand = new RelayCommand(new Action<object>(this.ExecuteAddLanguagesCommand)));
      }
    }

    private void ExecuteAddLanguagesCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<SettingsAppLanguageSelectorViewModel>().Navigate();
    }

    public RelayCommand SetDefaultLanguageCommand
    {
      get
      {
        return this._setDefaultLanguageCommand ?? (this._setDefaultLanguageCommand = new RelayCommand(new Action<object>(this.ExecuteSetDefaultLanguageCommand), new Func<object, bool>(this.CanExecuteSetDefaultLanguageCommand)));
      }
    }

    private bool CanExecuteSetDefaultLanguageCommand(object parameter)
    {
      return parameter is SettingsAppLanguageListItemViewModel listItemViewModel && this.Items.Count > 1 && this.Items.IndexOf(listItemViewModel) > 0;
    }

    private void ExecuteSetDefaultLanguageCommand(object parameter)
    {
      if (!(parameter is SettingsAppLanguageListItemViewModel listItemViewModel))
        return;
      this.Items.Remove(listItemViewModel);
      this.Items.Insert(0, listItemViewModel);
      this.Update();
    }

    public RelayCommand DeleteLanguageCommand
    {
      get
      {
        return this._deleteLanguageCommand ?? (this._deleteLanguageCommand = new RelayCommand(new Action<object>(this.ExecuteDeleteLanguageCommand), new Func<object, bool>(this.CanExecuteDeleteLanguageCommand)));
      }
    }

    private bool CanExecuteDeleteLanguageCommand(object parameter) => this.Items.Count > 1;

    private void ExecuteDeleteLanguageCommand(object parameter)
    {
      if (!(parameter is SettingsAppLanguageListItemViewModel listItemViewModel))
        return;
      this.Items.Remove(listItemViewModel);
      this.Update();
    }

    private void Update()
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      if (appSettings.Languages != null)
      {
        appSettings.Languages = this.Items.Select<SettingsAppLanguageListItemViewModel, string>((Func<SettingsAppLanguageListItemViewModel, string>) (x => x.Code)).ToArray<string>();
        ServiceFacade.SettingsService.SaveAppSettings(appSettings);
      }
      this.Items.ForEach<SettingsAppLanguageListItemViewModel>((Action<SettingsAppLanguageListItemViewModel, int>) ((x, i) => x.IsDefault = i == 0));
      this.SetDefaultLanguageCommand.RaiseCanExecuteChanged();
      this.DeleteLanguageCommand.RaiseCanExecuteChanged();
    }

    protected override Task<IEnumerable<SettingsAppLanguageListItemViewModel>> GetDataAsync()
    {
      this.Items.Clear();
      return Task<IEnumerable<SettingsAppLanguageListItemViewModel>>.Factory.StartNew((Func<IEnumerable<SettingsAppLanguageListItemViewModel>>) (() =>
      {
        AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
        return appSettings.Languages == null || appSettings.Languages.Length == 0 ? (IEnumerable<SettingsAppLanguageListItemViewModel>) null : (IEnumerable<SettingsAppLanguageListItemViewModel>) ((IEnumerable<string>) appSettings.Languages).Select<string, LanguageData>((Func<string, LanguageData>) (language => ServiceFacade.CultureService.GetLanguageByIsoCode(language))).Where<LanguageData>((Func<LanguageData, bool>) (languageData => languageData != null)).Select<LanguageData, SettingsAppLanguageListItemViewModel>((Func<LanguageData, SettingsAppLanguageListItemViewModel>) (languageData => new SettingsAppLanguageListItemViewModel(languageData)
        {
          IsDefault = languageData.Code.Equals(appSettings.Languages[0], StringComparison.InvariantCultureIgnoreCase)
        })).OrderByDescending<SettingsAppLanguageListItemViewModel, bool>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.IsDefault)).ThenBy<SettingsAppLanguageListItemViewModel, string>((Func<SettingsAppLanguageListItemViewModel, string>) (x => x.NativeName));
      }));
    }
  }
}
