// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Museum.Detail.MuseumDetailInfoViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Services;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Mtg.ViewModels.Museum.Map;
using Izi.Travel.Mtg.Views.Common.Detail;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Museum.Detail
{
  [View(typeof (DetailInfoView))]
  public class MuseumDetailInfoViewModel : ParentObjectDetailInfoViewModel
  {
    private RelayCommand _showRouteMapCommand;

    public RelayCommand ShowRouteMapCommand
    {
      get
      {
        return this._showRouteMapCommand ?? (this._showRouteMapCommand = new RelayCommand(new Action<object>(this.ExecuteShowRouteMapCommand)));
      }
    }

    private void ExecuteShowRouteMapCommand(object parameter)
    {
      if (this.MtgObject == null)
        return;
      ShellServiceFacade.NavigationService.UriFor<MuseumMapPartViewModel>().WithParam<string>((Expression<Func<MuseumMapPartViewModel, string>>) (x => x.TargetUid), this.MtgObject.Uid).Navigate();
    }
  }
}
