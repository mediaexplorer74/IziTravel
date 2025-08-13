// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Museum.Map.MuseumMapPartViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Mtg.Commands;
using Izi.Travel.Mtg.ViewModels.Common.Map;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Museum.Map
{
  public class MuseumMapPartViewModel : BaseMapViewModel
  {
    private MuseumMapItemViewModel _targetItem;
    private NowPlayingCommand _nowPlayingCommand;
    private RelayCommand _getDirectionsCommand;
        internal List<MapItem> MapItems;
        public BaseCommand MapItemClickCommand;
        public BaseCommand MapTappedCommand;

        public string TargetUid { get; set; }

    public MuseumMapItemViewModel TargetItem
    {
      get => this._targetItem;
      protected set
      {
        this.SetProperty<MuseumMapItemViewModel>(ref this._targetItem, value, propertyName: nameof (TargetItem));
      }
    }

        public Geopoint Center;
        internal double ZoomLevel;

        public NowPlayingCommand NowPlayingCommand
    {
      get
      {
        return this._nowPlayingCommand ?? (this._nowPlayingCommand = new NowPlayingCommand((IScreen) this));
      }
    }

    public RelayCommand GetDirectionsCommand
    {
      get
      {
        return this._getDirectionsCommand ?? (this._getDirectionsCommand = new RelayCommand(new Action<object>(this.GetDirections)));
      }
    }

    private void GetDirections(object o)
    {
      if (this.TargetItem == null || this.TargetItem.MtgObject == null)
        return;
      this.TargetItem.MtgObject.ShowMapDirectionsTask();
    }

    protected override async Task OnInitializeProcessAsync()
    {
      if (string.IsNullOrWhiteSpace(this.TargetUid))
        return;
      MtgObject mtgObjectAsync = await MtgObjectServiceHelper.GetMtgObjectAsync(new MtgObjectFilter(this.TargetUid, ServiceFacade.SettingsService.GetAppSettings().Languages));
      if (mtgObjectAsync == null)
        return;
      this.TargetItem = new MuseumMapItemViewModel(mtgObjectAsync);
      this.Center = this.TargetItem.Location;
    }
  }
}
