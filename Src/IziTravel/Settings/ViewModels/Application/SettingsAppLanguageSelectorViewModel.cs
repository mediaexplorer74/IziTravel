// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppLanguageSelectorViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppLanguageSelectorViewModel : 
    BaseListViewModel<SettingsAppLanguageListItemViewModel>
  {
    private readonly INavigationService _navigationService;
    private readonly ISettingsService _settingsService;
    private string _query;
    private RelayCommand _selectCommand;
    private RelayCommand _cancelCommand;

    public string Query
    {
      get => this._query;
      set
      {
        this.SetProperty<string>(ref this._query, value, (System.Action) (() => this.NotifyOfPropertyChange<IEnumerable<SettingsAppLanguageListItemViewModel>>((Expression<Func<IEnumerable<SettingsAppLanguageListItemViewModel>>>) (() => this.FilteredItems))), nameof (Query));
      }
    }

    public IEnumerable<SettingsAppLanguageListItemViewModel> FilteredItems
    {
      get
      {
        return this.Items == null || string.IsNullOrWhiteSpace(this.Query) ? (IEnumerable<SettingsAppLanguageListItemViewModel>) this.Items : this.Items.Where<SettingsAppLanguageListItemViewModel>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.NativeName.ToLower().Contains(this.Query.ToLower()) || x.EnglishName.ToLower().Contains(this.Query.ToLower())));
      }
    }

    public SettingsAppLanguageSelectorViewModel(
      INavigationService navigationService,
      ISettingsService settingsService)
    {
      this._navigationService = navigationService;
      this._settingsService = settingsService;
    }

    public RelayCommand SelectCommand
    {
      get
      {
        return this._selectCommand ?? (this._selectCommand = new RelayCommand(new Action<object>(this.ExecuteSelectCommand), new Func<object, bool>(this.CanExecuteSelectCommand)));
      }
    }

    private bool CanExecuteSelectCommand(object parameter)
    {
      return this.Items.Any<SettingsAppLanguageListItemViewModel>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.IsSelected));
    }

    private void ExecuteSelectCommand(object parameter)
    {
      AppSettings appSettings = this._settingsService.GetAppSettings();
      appSettings.Languages = this.Items.OrderByDescending<SettingsAppLanguageListItemViewModel, bool>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.IsDefault)).Where<SettingsAppLanguageListItemViewModel>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.IsSelected)).Select<SettingsAppLanguageListItemViewModel, string>((Func<SettingsAppLanguageListItemViewModel, string>) (x => x.Code)).ToArray<string>();
      this._settingsService.SaveAppSettings(appSettings);
      this._navigationService.GoBack();
    }

    public RelayCommand CancelCommand
    {
      get
      {
        return this._cancelCommand ?? (this._cancelCommand = new RelayCommand(new Action<object>(this.ExecuteCancelCommand)));
      }
    }

    private void ExecuteCancelCommand(object parameter) => this._navigationService.GoBack();

    protected override void OnActivate()
    {
      this.Items.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
      base.OnActivate();
    }

    protected override void OnDeactivate(bool close)
    {
      this.Items.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnItemsCollectionChanged);
      base.OnDeactivate(close);
    }

    protected override Task<IEnumerable<SettingsAppLanguageListItemViewModel>> GetDataAsync()
    {
      this.Items.Clear();
      return Task<IEnumerable<SettingsAppLanguageListItemViewModel>>.Factory.StartNew((Func<IEnumerable<SettingsAppLanguageListItemViewModel>>) (() =>
      {
        string[] currentLanguages = this._settingsService.GetAppSettings().Languages ?? new string[0];
        string defaultLanguage = ((IEnumerable<string>) currentLanguages).First<string>();
        return (IEnumerable<SettingsAppLanguageListItemViewModel>) ((IEnumerable<LanguageData>) ServiceFacade.CultureService.GetNeutralLanguages()).OrderBy<LanguageData, string>((Func<LanguageData, string>) (x => x.NativeName)).Select<LanguageData, SettingsAppLanguageListItemViewModel>((Func<LanguageData, SettingsAppLanguageListItemViewModel>) (x => new SettingsAppLanguageListItemViewModel(x)
        {
          IsDefault = defaultLanguage != null && string.Equals(defaultLanguage, x.Code, StringComparison.CurrentCultureIgnoreCase),
          IsSelected = ((IEnumerable<string>) currentLanguages).Any<string>((Func<string, bool>) (y => string.Equals(y, x.Code, StringComparison.CurrentCultureIgnoreCase)))
        })).OrderByDescending<SettingsAppLanguageListItemViewModel, bool>((Func<SettingsAppLanguageListItemViewModel, bool>) (x => x.IsDefault));
      }));
    }

    private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs args)
    {
      if (args.NewItems != null)
      {
        foreach (PropertyChangedBase newItem in (IEnumerable) args.NewItems)
          newItem.PropertyChanged += new PropertyChangedEventHandler(this.OnListItemPropertyChanged);
      }
      if (args.OldItems == null)
        return;
      foreach (PropertyChangedBase oldItem in (IEnumerable) args.OldItems)
        oldItem.PropertyChanged -= new PropertyChangedEventHandler(this.OnListItemPropertyChanged);
    }

    private void OnListItemPropertyChanged(object sender, PropertyChangedEventArgs args)
    {
      if (!(args.PropertyName == "IsSelected"))
        return;
      this.SelectCommand.RaiseCanExecuteChanged();
    }
  }
}
