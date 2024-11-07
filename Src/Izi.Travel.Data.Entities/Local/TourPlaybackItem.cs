// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Local.TourPlaybackItem
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;

#nullable disable
namespace Izi.Travel.Data.Entities.Local
{
  [Table]
  public class TourPlaybackItem : BaseEntity
  {
    private int _id;
    private string _tourUid;
    private string _childUid;
    private string _language;
    private DateTime _dateTime;
    private TourPlaybackAction _action;
    [Column(IsVersion = true)]
    private Binary _version;

    [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
    public int Id
    {
      get => this._id;
      set
      {
        if (this._id == value)
          return;
        this.NotifyPropertyChanging(nameof (Id));
        this._id = value;
        this.NotifyPropertyChanged(nameof (Id));
      }
    }

    [Column(CanBeNull = false)]
    public string TourUid
    {
      get => this._tourUid;
      set
      {
        if (!(this._tourUid != value))
          return;
        this.NotifyPropertyChanging(nameof (TourUid));
        this._tourUid = value;
        this.NotifyPropertyChanged(nameof (TourUid));
      }
    }

    [Column(CanBeNull = true)]
    public string ChildUid
    {
      get => this._childUid;
      set
      {
        if (!(this._childUid != value))
          return;
        this.NotifyPropertyChanging(nameof (ChildUid));
        this._childUid = value;
        this.NotifyPropertyChanged(nameof (ChildUid));
      }
    }

    [Column(CanBeNull = false)]
    public string Language
    {
      get => this._language;
      set
      {
        if (!(this._language != value))
          return;
        this.NotifyPropertyChanging(nameof (Language));
        this._language = value;
        this.NotifyPropertyChanged(nameof (Language));
      }
    }

    [Column(CanBeNull = false)]
    public DateTime DateTime
    {
      get => this._dateTime;
      set
      {
        if (!(this._dateTime != value))
          return;
        this.NotifyPropertyChanging(nameof (DateTime));
        this._dateTime = value;
        this.NotifyPropertyChanged(nameof (DateTime));
      }
    }

    [Column(CanBeNull = false)]
    public TourPlaybackAction Action
    {
      get => this._action;
      set
      {
        if (this._action == value)
          return;
        this.NotifyPropertyChanging(nameof (Action));
        this._action = value;
        this.NotifyPropertyChanged(nameof (Action));
      }
    }
  }
}
