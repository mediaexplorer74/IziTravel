// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Tour.Detail.TourDetailInfoViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Command;
using Izi.Travel.Core.Converters;
using Izi.Travel.Mtg.Converters;
using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Mtg.Views.Tour.Detail;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Tour.Detail
{
  [View(typeof (TourDetailInfoView))]
  public class TourDetailInfoViewModel : ParentObjectDetailInfoViewModel
  {
    private int _distance;
    private int _duration;

    public string InfoString
    {
      get
      {
        List<string> values = new List<string>();
        if (this._duration > 0)
          values.Add(RoundedTimeSpanToStringConverter.RoundedTimeSpanToString(TimeSpan.FromSeconds((double) this._duration)));
        if (this._distance > 0)
          values.Add(DistanceToStringConverter.DistanceToString((double) this._distance));
        return values.Count <= 0 ? (string) null : string.Join(" / ", (IEnumerable<string>) values);
      }
    }

    public string CategoryString
    {
      get
      {
        return this.MtgObject == null ? (string) null : TourHelper.GetCategoryName(this.MtgObject.Category);
      }
    }

    public RelayCommand OpenMapCommand
    {
      get
      {
        return this.DetailViewModel == null ? (RelayCommand) null : this.DetailViewModel.OpenMapCommand;
      }
    }

    protected override void OnActivate()
    {
      this._distance = this.MtgObject.Distance;
      if (this._distance == 0 && this.MtgObject.Map != null && this.MtgObject.Map.Route != null)
      {
        double num = 0.0;
                List<GeoCoordinate> list = default;//((IEnumerable<GeoLocation>) this.MtgObject.Map.Route).Select<GeoLocation, GeoCoordinate>((Func<GeoLocation, GeoCoordinate>) (x => x.ToGeoCoordinate())).ToList<GeoCoordinate>();
        
                
                
                
       for (int index = 0; index < list.Count - 1; ++index)
          num += list[index].GetDistanceTo(list[index + 1]);
        this._distance = (int) num;
      }
      this._duration = this.MtgObject.Duration;
      if (this._duration == 0 && this._distance > 0)
        this._duration = (int) ((double) this._distance / (TourHelper.GetAvarageSpeedByCategory(this.MtgObject.Category) * 1000.0 / 3600.0));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.InfoString));
      base.OnActivate();
    }
  }
}
