﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.DownloadObject
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using Izi.Travel.Data.Entities.Local;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using System.Collections;
//using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace Izi.Travel.Data.Entities.Download
{
  [Table]
  public class DownloadObject : BaseEntity
  {
    private int _id;
    private string _uid;
    private string _language;
    private DownloadObjectType _type;
    private string _title;
    private double _latitude;
    private double _longitude;
    private string _regionUid;
    private string _number;
    private byte[] _data;
    private string _hash;
    private DownloadStatus _status;
    private /*readonly*/ EntitySet<DownloadObjectLink> _childLinks;
    private /*readonly*/ EntitySet<DownloadObjectLink> _parentLinks;
    private /*readonly*/ EntitySet<DownloadMedia> _downloadMediaItems;
    [Column(IsVersion = true)]
    private Binary _version;

    [Column(IsPrimaryKey = true, IsDbGenerated = true, 
            CanBeNull = false, AutoSync = AutoSync.OnInsert)]
    public int Id
    {
      get => this._id;
      set => this.SetProperty<int>(ref this._id, value, nameof (Id));
    }

    [Column(CanBeNull = false)]
    public string Uid
    {
      get => this._uid;
      set => this.SetProperty<string>(ref this._uid, value, nameof (Uid));
    }

    [Column(CanBeNull = false, DbType = "nvarchar(2)")]
    public string Language
    {
      get => this._language;
      set => this.SetProperty<string>(ref this._language, value, nameof (Language));
    }

    [Column(CanBeNull = false)]
    public DownloadObjectType Type
    {
      get => this._type;
      set => this.SetProperty<DownloadObjectType>(ref this._type, value, nameof (Type));
    }

    [Column(CanBeNull = false)]
    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    [Column(CanBeNull = false)]
    public double Latitude
    {
      get => this._latitude;
      set => this.SetProperty<double>(ref this._latitude, value, nameof (Latitude));
    }

    [Column(CanBeNull = false)]
    public double Longitude
    {
      get => this._longitude;
      set => this.SetProperty<double>(ref this._longitude, value, nameof (Longitude));
    }

    [Column(CanBeNull = true)]
    public string RegionUid
    {
      get => this._regionUid;
      set => this.SetProperty<string>(ref this._regionUid, value, nameof (RegionUid));
    }

    [Column(CanBeNull = true)]
    public string Number
    {
      get => this._number;
      set => this.SetProperty<string>(ref this._number, value, nameof (Number));
    }

    [Column(CanBeNull = false, DbType = "image")]
    public byte[] Data
    {
      get => this._data;
      set => this.SetProperty<byte[]>(ref this._data, value, nameof (Data));
    }

    [Column(CanBeNull = false)]
    public string Hash
    {
      get => this._hash;
      set => this.SetProperty<string>(ref this._hash, value, nameof (Hash));
    }

    [Column(CanBeNull = false)]
    public DownloadStatus Status
    {
      get => this._status;
      set => this.SetProperty<DownloadStatus>(ref this._status, value, nameof (Status));
    }

    [Association(Storage = "_childLinks", OtherKey = "ParentId")]
    public EntitySet<DownloadObjectLink> ChildLinks
        {
            get => this._childLinks;
            set
            {
                //this._parentLinks.Assign((IEnumerable<DownloadObjectLink>)value);
                this._childLinks = value;
            }
        }

        [Association(Storage = "_parentLinks", OtherKey = "ObjectId")]
    public EntitySet<DownloadObjectLink> ParentLinks
        {
            get => this._parentLinks;
            set
            {
                //this._parentLinks.Assign((IEnumerable<DownloadObjectLink>)value);
                this._parentLinks = value;
            }
        }

        [Association(Storage = "_downloadMediaItems", OtherKey = "ObjectId")]
    public EntitySet<DownloadMedia> DownloadMediaItems
        {
            get => this._downloadMediaItems;
            set
            {
                //this._downloadMediaItems.Assign(value);
                this._downloadMediaItems = value;
            }
        }

        public DownloadObject()
    {
      this._childLinks = new EntitySet<DownloadObjectLink>();
      this._parentLinks = new EntitySet<DownloadObjectLink>();
      this._downloadMediaItems = new EntitySet<DownloadMedia>();
    }
  }
}
