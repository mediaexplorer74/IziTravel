// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Download.DownloadObjectLink
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
  public class DownloadObjectLink : BaseEntity
  {
    private int _objectId;
    private int _parentId;
    private EntityRef<DownloadObject> _object;
    private EntityRef<DownloadObject> _parent;

    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public int ObjectId
    {
      get => this._objectId;
      set
      {
        if (this._objectId == value)
          return;
        this.NotifyPropertyChanging(nameof (ObjectId));
        this._objectId = value;
        this.NotifyPropertyChanged(nameof (ObjectId));
      }
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

    [Column(IsPrimaryKey = true, CanBeNull = false)]
    public int ParentId
    {
      get => this._parentId;
      set
      {
        if (this._parentId == value)
          return;
        this.NotifyPropertyChanging(nameof (ParentId));
        this._parentId = value;
        this.NotifyPropertyChanged(nameof (ParentId));
      }
    }

    [Association(IsForeignKey = true, ThisKey = "ParentId", OtherKey = "Id", Storage = "_parent")]
    public DownloadObject Parent
    {
      get => this._parent.Entity;
      set
      {
        this.NotifyPropertyChanging(nameof (Parent));
        this._parent.Entity = value;
        if (value != null)
          this._parentId = value.Id;
        this.NotifyPropertyChanged(nameof (Parent));
      }
    }

    public DownloadObjectLink()
    {
      this._object = new EntityRef<DownloadObject>();
      this._parent = new EntityRef<DownloadObject>();
    }
  }
}
