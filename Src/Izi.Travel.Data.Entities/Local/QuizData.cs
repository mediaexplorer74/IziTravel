// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.Entities.Local.QuizData
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
  public class QuizData : BaseEntity
  {
    private int _id;
    private string _uid;
    private string _parentUid;
    private string _language;
    private string _type;
    private string _title;
    private string _contentProviderUid;
    private string _imageUid;
    private int _answerIndex;
    private bool _answerCorrect;
    private DateTime _dateTime;
    private byte[] _data;
    [Column(IsVersion = true)]
    private Binary _version;

    [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false, AutoSync = AutoSync.OnInsert)]
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

    [Column]
    public string ParentUid
    {
      get => this._parentUid;
      set => this.SetProperty<string>(ref this._parentUid, value, nameof (ParentUid));
    }

    [Column(CanBeNull = false)]
    public string Language
    {
      get => this._language;
      set => this.SetProperty<string>(ref this._language, value, nameof (Language));
    }

    [Column(CanBeNull = false)]
    public string Type
    {
      get => this._type;
      set => this.SetProperty<string>(ref this._type, value, nameof (Type));
    }

    [Column(CanBeNull = false)]
    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, nameof (Title));
    }

    [Column(CanBeNull = false)]
    public string ContentProviderUid
    {
      get => this._contentProviderUid;
      set
      {
        this.SetProperty<string>(ref this._contentProviderUid, value, nameof (ContentProviderUid));
      }
    }

    [Column]
    public string ImageUid
    {
      get => this._imageUid;
      set => this.SetProperty<string>(ref this._imageUid, value, nameof (ImageUid));
    }

    [Column]
    public int AnswerIndex
    {
      get => this._answerIndex;
      set => this.SetProperty<int>(ref this._answerIndex, value, nameof (AnswerIndex));
    }

    [Column]
    public bool AnswerCorrect
    {
      get => this._answerCorrect;
      set => this.SetProperty<bool>(ref this._answerCorrect, value, nameof (AnswerCorrect));
    }

    [Column(CanBeNull = false)]
    public DateTime DateTime
    {
      get => this._dateTime;
      set => this.SetProperty<DateTime>(ref this._dateTime, value, nameof (DateTime));
    }

    [Column(CanBeNull = false, DbType = "image")]
    public byte[] Data
    {
      get => this._data;
      set => this.SetProperty<byte[]>(ref this._data, value, nameof (Data));
    }
  }
}
