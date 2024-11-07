// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.DownloadObject
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using Izi.Travel.Data.Entities.Local;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Linq;
using System.Data.Linq.Mapping;

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
    private readonly EntitySet<DownloadObjectLink> _childLinks;
    private readonly EntitySet<DownloadObjectLink> _parentLinks;
    private readonly EntitySet<DownloadMedia> _downloadMediaItems;
    [Column]
    private Binary _version;

    [Column]
    public int Id
    {
      get => this._id;
      set => this.SetProperty<int>(ref this._id, value, nameof (Id));
    }

    [Column]
    public string Uid
    {
      get => this._uid;
      set => this.SetProperty<string>(ref this._uid, value, nameof (Uid));
    }

    [Column]
    public string Language
    {
      get => this._language;
      set => this.SetProperty<string>(ref this._language, value, nameof (Language));
    }

    [Column]
    public DownloadObjectType Type
    {
      get => this._type;
      set => this.SetProperty<DownloadObjectType>(ref this._type, value, nameof (Type));
    }

    [Column]
    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    [Column]
    public double Latitude
    {
      get => this._latitude;
      set => this.SetProperty<double>(ref this._latitude, value, nameof (Latitude));
    }

    [Column]
    public double Longitude
    {
      get => this._longitude;
      set => this.SetProperty<double>(ref this._longitude, value, nameof (Longitude));
    }

    [Column]
    public string RegionUid
    {
      get => this._regionUid;
      set => this.SetProperty<string>(ref this._regionUid, value, nameof (RegionUid));
    }

    [Column]
    public string Number
    {
      get => this._number;
      set => this.SetProperty<string>(ref this._number, value, nameof (Number));
    }

    [Column]
    public byte[] Data
    {
      get => this._data;
      set => this.SetProperty<byte[]>(ref this._data, value, nameof (Data));
    }

    [Column]
    public string Hash
    {
      get => this._hash;
      set => this.SetProperty<string>(ref this._hash, value, nameof (Hash));
    }

    [Column]
    public DownloadStatus Status
    {
      get => this._status;
      set => this.SetProperty<DownloadStatus>(ref this._status, value, nameof (Status));
    }

    [Association]
    public EntitySet<DownloadObjectLink> ChildLinks
    {
      get => this._childLinks;
      set => this._childLinks.Assign((IEnumerable<DownloadObjectLink>) value);
    }

    [Association]
    public EntitySet<DownloadObjectLink> ParentLinks
    {
      get => this._parentLinks;
      set => this._parentLinks.Assign((IEnumerable<DownloadObjectLink>) value);
    }

    [Association]
    public EntitySet<DownloadMedia> DownloadMediaItems
    {
      get => this._downloadMediaItems;
      set => this._downloadMediaItems.Assign((IEnumerable<DownloadMedia>) value);
    }

    public DownloadObject()
    {
      this._childLinks = new EntitySet<DownloadObjectLink>();
      this._parentLinks = new EntitySet<DownloadObjectLink>();
      this._downloadMediaItems = new EntitySet<DownloadMedia>();
    }
  }
}
