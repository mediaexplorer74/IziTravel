// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Explore.Flyouts.ExploreFlyoutLocationViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Microsoft.Phone.Maps.Controls;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Explore.Flyouts
{
  public sealed class ExploreFlyoutLocationViewModel : ExploreFlyoutViewModel
  {
    private readonly ILog _logger = LogManager.GetLog(typeof (ExploreFlyoutLocationViewModel));
    private string _query;
    private List<ExploreLocationGroup> _locationGroups;
    private IEnumerable<ExploreLocationGroup> _locationGroupsFiltered;
    private ExploreLocationItem _selectedLocationItem = ExploreLocationItem.AroundMe;
    private bool _isDataLoading;
    private bool _isDataLoadError;
    private RelayCommand _refreshCommand;
    private RelayCommand _selectCommand;

    public ExploreLocationItem ExploreLocationItemAroundMe => ExploreLocationItem.AroundMe;

    public ExploreLocationItem SelectedLocationItem
    {
      get => this._selectedLocationItem;
      set
      {
        this.SetProperty<ExploreLocationItem>(ref this._selectedLocationItem, value, propertyName: nameof (SelectedLocationItem));
      }
    }

    public bool LocationSelected { get; set; }

    public string Query
    {
      get => this._query;
      set
      {
        this.SetProperty<string>(ref this._query, value, new System.Action(this.ApplyFilter), nameof (Query));
      }
    }

    public IEnumerable<ExploreLocationGroup> LocationGroupsFiltered
    {
      get => this._locationGroupsFiltered;
      set
      {
        this.SetProperty<IEnumerable<ExploreLocationGroup>>(ref this._locationGroupsFiltered, value, propertyName: nameof (LocationGroupsFiltered));
      }
    }

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      private set
      {
        this.SetProperty<bool>(ref this._isDataLoading, value, (System.Action) (() => this.RefreshCommand.RaiseCanExecuteChanged()), nameof (IsDataLoading));
      }
    }

    public bool IsDataLoadError
    {
      get => this._isDataLoadError;
      private set
      {
        this.SetProperty<bool>(ref this._isDataLoadError, value, propertyName: nameof (IsDataLoadError));
      }
    }

    public ExploreFlyoutLocationViewModel(ExploreViewModel exploreViewModel)
      : base(exploreViewModel)
    {
      this.LocationSelected = true;
    }

    public RelayCommand RefreshCommand
    {
      get
      {
        return this._refreshCommand ?? (this._refreshCommand = new RelayCommand(new Action<object>(this.ExecuteRefreshCommand), new Func<object, bool>(this.CanExecuteRefreshCommand)));
      }
    }

    private bool CanExecuteRefreshCommand(object parameter) => !this.IsDataLoading;

    private async void ExecuteRefreshCommand(object parameter) => await this.LoadDataAsync();

    public RelayCommand SelectCommand
    {
      get
      {
        return this._selectCommand ?? (this._selectCommand = new RelayCommand(new Action<object>(this.ExecuteSelectCommand)));
      }
    }

    private async void ExecuteSelectCommand(object parameter)
    {
      if (!(parameter is ExploreLocationItem exploreLocationItem))
        return;
      this.SelectedLocationItem = exploreLocationItem;
      this.LocationSelected = true;
      this.CloseCommand.Execute((object) null);
      this.ExploreViewModel.SetMapView(this.SelectedLocationItem);
      await this.ExploreViewModel.RefreshItemsDataAsync();
    }

    public async void InitializeAsync() => await this.LoadDataAsync();

    protected override void OnOpening() => this.Query = string.Empty;

    private async Task LoadDataAsync()
    {
      this.IsDataLoadError = false;
      this.IsDataLoading = true;
      this.LocationGroupsFiltered = (IEnumerable<ExploreLocationGroup>) null;
      try
      {
        string[] languages = ServiceFacade.SettingsService.GetAppSettings().Languages;
        MtgObject[] countries = await ServiceFacade.MtgObjectRegionService.GetCountryListAsync(languages);
        MtgObject[] cityListAsync = await ServiceFacade.MtgObjectRegionService.GetCityListAsync(languages);
        this._locationGroups = new List<ExploreLocationGroup>();
        if (cityListAsync != null)
        {
          foreach (IGrouping<string, MtgObject> source in ((IEnumerable<MtgObject>) cityListAsync).Where<MtgObject>((Func<MtgObject, bool>) (x => x.MainContent != null && !string.IsNullOrWhiteSpace(x.MainContent.Title))).OrderBy<MtgObject, string>((Func<MtgObject, string>) (x => x.MainContent.Title)).GroupBy<MtgObject, string>((Func<MtgObject, string>) (x => x.MainContent.Title.Trim().Substring(0, 1))))
          {
            ExploreLocationGroup exploreLocationGroup = new ExploreLocationGroup(source.Key);
            foreach (MtgObject mtgObject1 in source.Where<MtgObject>((Func<MtgObject, bool>) (x => x.MainContent != null)))
            {
              MtgObject city = mtgObject1;
              ExploreLocationItem exploreLocationItem = new ExploreLocationItem()
              {
                Uid = city.Uid,
                CityName = city.MainContent.Title
              };
              if (city.Location != null)
              {
                exploreLocationItem.CountryCode = city.Location.CountryCode;
                MtgObject mtgObject2 = countries != null ? ((IEnumerable<MtgObject>) countries).FirstOrDefault<MtgObject>((Func<MtgObject, bool>) (x => x.Uid == city.Location.CountryUid)) : (MtgObject) null;
                if (mtgObject2 != null && mtgObject2.MainContent != null)
                  exploreLocationItem.CountryName = mtgObject2.MainContent.Title;
              }
              exploreLocationItem.TrySetLocation(city.Location != null ? city.Location.ToGeoCoordinate() : (GeoCoordinate) null, city.Map == null || city.Map.Bounds == null ? (LocationRectangle) null : city.Map.Bounds.ToLocationRectangle());
              exploreLocationGroup.Add(exploreLocationItem);
            }
            this._locationGroups.Add(exploreLocationGroup);
          }
        }
        this.ApplyFilter();
        languages = (string[]) null;
        countries = (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
        this.IsDataLoadError = true;
      }
      finally
      {
        this.IsDataLoading = false;
      }
    }

    private void ApplyFilter()
    {
      if (this._locationGroups == null || string.IsNullOrWhiteSpace(this.Query))
      {
        this.LocationGroupsFiltered = (IEnumerable<ExploreLocationGroup>) this._locationGroups;
      }
      else
      {
        List<ExploreLocationGroup> exploreLocationGroupList = new List<ExploreLocationGroup>();
        foreach (ExploreLocationGroup locationGroup in this._locationGroups)
        {
          ExploreLocationGroup exploreLocationGroup = new ExploreLocationGroup(locationGroup.Key);
          exploreLocationGroup.AddRange(locationGroup.Where<ExploreLocationItem>((Func<ExploreLocationItem, bool>) (x => !string.IsNullOrWhiteSpace(x.CityName) && x.CityName.ToLower().Contains(this.Query.ToLower()))));
          exploreLocationGroupList.Add(exploreLocationGroup);
        }
        this.LocationGroupsFiltered = (IEnumerable<ExploreLocationGroup>) exploreLocationGroupList;
      }
    }
  }
}
