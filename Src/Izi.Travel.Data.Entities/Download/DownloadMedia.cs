// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.DownloadMedia
// Assembly: Izi.Travel.Data.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C2535A39-73A9-477D-A740-0ABDD93ED172
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.Entities.dll

using Izi.Travel.Data.Entities.Local;
using System.Data.Linq;
using System.Data.Linq.Mapping;

#nullable disable
namespace Izi.Travel.Data.Entities.Download
{
  [Table]
  public class DownloadMedia : BaseEntity
  {
    private int _id;
    private int _objectId;
    private EntityRef<DownloadObject> _object;
    private string _path;
    private DownloadStatus _status;
    [Column(IsVersion = true)]
    private Binary _version;

    [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
    public int Id
    {
      get => this._id;
      set => this.SetProperty<int>(ref this._id, value, nameof (Id));
    }

    [Column(CanBeNull = false)]
    public int ObjectId
    {
      get => this._objectId;
      set => this.SetProperty<int>(ref this._objectId, value, nameof (ObjectId));
    }

    
    [Association(IsForeignKey = true, ThisKey = "ObjectId", OtherKey = "Id", Storage = "_object")]
    public DownloadObject Object
    {
      get => this._object.Entity;
      set
      {
        this.NotifyPropertyChanging(nameof (Object));
        this._object.Entity = value;
        if (value != null)
          this._objectId = value.Id;
        this.NotifyPropertyChanged(nameof (Object));
      }
    }

    
    [Column(CanBeNull = true)]
    public string Path
    {
      get => this._path;
      set => this.SetProperty<string>(ref this._path, value, nameof (Path));
    }

    //RnD
    [Column(CanBeNull = false)]
    public DownloadStatus Status
    {
      get => this._status;
      set => this.SetProperty<DownloadStatus>(ref this._status, value, nameof (Status));
    }

        public DownloadMedia()
        {
            //RnD
            this._object = new EntityRef<DownloadObject>();
        }
    }
}
