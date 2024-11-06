// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Map.BaseMapItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using System;
using System.Device.Location;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Map
{
  public abstract class BaseMapItemViewModel : PropertyChangedBase
  {
    private readonly MtgObject _mtgObject;
    private bool _isSelected;
    private string _uid;
    private string _title;
    private GeoCoordinate _location = GeoCoordinate.Unknown;
    private double _distance;

    public MtgObject MtgObject => this._mtgObject;

    public virtual bool IsSelected
    {
      get => this._isSelected;
      set
      {
        if (this._isSelected == value)
          return;
        this._isSelected = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsSelected));
      }
    }

    public virtual string Uid
    {
      get => this._uid;
      set
      {
        if (!(this._uid != value))
          return;
        this._uid = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Uid));
      }
    }

    public MtgObjectType Type
    {
      get => this.MtgObject == null ? MtgObjectType.Unknown : this.MtgObject.Type;
    }

    public virtual string Title
    {
      get => this._title;
      set
      {
        if (!(this._title != value))
          return;
        this._title = value;
        this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Title));
      }
    }

    public string Language
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.MainContent == null ? string.Empty : this.MtgObject.MainContent.Language;
      }
    }

    public virtual GeoCoordinate Location
    {
      get => this._location;
      set
      {
        if (!(this._location != value))
          return;
        this._location = value;
        this.NotifyOfPropertyChange<GeoCoordinate>((Expression<Func<GeoCoordinate>>) (() => this.Location));
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasLocation));
      }
    }

    public double Distance
    {
      get => this._distance;
      set
      {
        if (Math.Abs(this._distance - value) <= double.Epsilon)
          return;
        this._distance = value;
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Distance));
      }
    }

    public bool HasLocation
    {
      get => this.Location != (GeoCoordinate) null && this.Location != GeoCoordinate.Unknown;
    }

    protected BaseMapItemViewModel(MtgObject mtgObject)
    {
      this._mtgObject = mtgObject;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this.MtgObject == null)
        return;
      this.Uid = this.MtgObject.Uid;
      if (this.MtgObject.MainContent != null)
        this.Title = this.MtgObject.MainContent.Title;
      if (this.MtgObject.Map != null && this.MtgObject.Map.Route != null && this.MtgObject.Map.Route.Length != 0)
      {
        this.Location = this.MtgObject.Map.Route[0].ToGeoCoordinate();
      }
      else
      {
        if (this.MtgObject.Location == null)
          return;
        this.Location = this.MtgObject.Location.ToGeoCoordinate();
      }
    }
  }
}
