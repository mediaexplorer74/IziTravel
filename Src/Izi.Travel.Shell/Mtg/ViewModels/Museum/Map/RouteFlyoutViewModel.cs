// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Museum.Map.RouteFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Common.ViewModels.Flyout;
using Microsoft.Phone.Maps.Services;
using System.Collections.ObjectModel;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Museum.Map
{
  public class RouteFlyoutViewModel : FlyoutViewModel
  {
    private readonly ObservableCollection<MuseumMapListItemViewModel> _items = new ObservableCollection<MuseumMapListItemViewModel>();

    public MuseumMapPartViewModel MapViewModel { get; private set; }

    public ObservableCollection<MuseumMapListItemViewModel> Items => this._items;

    public RouteFlyoutViewModel(MuseumMapPartViewModel mapViewModel)
    {
      this.MapViewModel = mapViewModel;
    }

    protected override bool CanExecuteOpenCommand(object parameter)
    {
      return this.MapViewModel != null && !this.MapViewModel.IsBusy;
    }

    public void RefreshList(Route route)
    {
      this.Items.Clear();
      if (route == null || route.Legs == null || route.Legs.Count <= 0)
        return;
      foreach (RouteManeuver maneuver in route.Legs[0].Maneuvers)
        this.Items.Add(new MuseumMapListItemViewModel(maneuver));
    }
  }
}
