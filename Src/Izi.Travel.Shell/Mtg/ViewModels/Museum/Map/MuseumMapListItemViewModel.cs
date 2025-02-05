// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Museum.Map.MuseumMapListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Resources;
using Microsoft.Phone.Maps.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Museum.Map
{
  public class MuseumMapListItemViewModel : PropertyChangedBase
  {
    private readonly RouteManeuver _routeManeuver;
    private string _description;
    private string _distance;

    public string Description
    {
      get => this._description;
      set
      {
        if (!(this._description != value))
          return;
        this._description = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Description));
      }
    }

    public string Distance
    {
      get => this._distance;
      set
      {
        if (!(this._distance != value))
          return;
        this._distance = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Distance));
      }
    }

    public MuseumMapListItemViewModel(RouteManeuver routeManeuver)
    {
      this._routeManeuver = routeManeuver;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this._routeManeuver == null)
        return;
      int lengthInMeters = this._routeManeuver != null ? this._routeManeuver.LengthInMeters : 0;
      if ((double) lengthInMeters > double.Epsilon)
        this.Distance = lengthInMeters > 1000 ? string.Format("{0:0.0} {1}", (object) (lengthInMeters / 1000), (object) AppResources.StringDistanceKilometers) : string.Format("{0} {1}", (object) lengthInMeters, (object) AppResources.StringDistanceMeters);
      this.Description = this._routeManeuver.InstructionText;
    }
  }
}
