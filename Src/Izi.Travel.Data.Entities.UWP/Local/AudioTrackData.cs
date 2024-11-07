// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Local.AudioTrackData
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
  public class AudioTrackData : BaseEntity
  {
    private int _id;
    private string _uid;
    private string _url;
    private string _title;
    private string _tag;
    private DateTime _dateTime;
    private AudioTrackState _state;
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
    public string Uid
    {
      get => this._uid;
      set
      {
        if (!(this._uid != value))
          return;
        this.NotifyPropertyChanging(nameof (Uid));
        this._uid = value;
        this.NotifyPropertyChanged(nameof (Uid));
      }
    }

    [Column(CanBeNull = false)]
    public string Url
    {
      get => this._url;
      set
      {
        if (!(this._url != value))
          return;
        this.NotifyPropertyChanging(nameof (Url));
        this._url = value;
        this.NotifyPropertyChanged(nameof (Url));
      }
    }

    [Column(CanBeNull = false)]
    public string Title
    {
      get => this._title;
      set
      {
        if (!(this._title != value))
          return;
        this.NotifyPropertyChanging(nameof (Title));
        this._title = value;
        this.NotifyPropertyChanged(nameof (Title));
      }
    }

    [Column]
    public string Tag
    {
      get => this._tag;
      set
      {
        if (!(this._tag != value))
          return;
        this.NotifyPropertyChanging(nameof (Tag));
        this._tag = value;
        this.NotifyPropertyChanged(nameof (Tag));
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
    public AudioTrackState State
    {
      get => this._state;
      set
      {
        if (this._state == value)
          return;
        this.NotifyPropertyChanging(nameof (State));
        this._state = value;
        this.NotifyPropertyChanged(nameof (State));
      }
    }
  }
}
