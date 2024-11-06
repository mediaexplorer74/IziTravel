// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Media.AudioTrackInfo
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Utility;
using System.Runtime.Serialization;

#nullable disable
namespace Izi.Travel.Business.Entities.Media
{
  [DataContract]
  public class AudioTrackInfo
  {
    [IgnoreDataMember]
    public string Key
    {
      get
      {
        return string.IsNullOrWhiteSpace(this.MtgObjectUid) || string.IsNullOrWhiteSpace(this.Language) ? (string) null : string.Format("{0}_{1}", (object) this.MtgObjectUid, (object) this.Language).ToLower();
      }
    }

    [DataMember]
    public string MtgObjectUid { get; set; }

    [DataMember]
    public MtgObjectType MtgObjectType { get; set; }

    [DataMember]
    public string MtgParentUid { get; set; }

    [DataMember]
    public MtgObjectType MtgParentType { get; set; }

    [DataMember]
    public MtgObjectAccessType MtgObjectAccessType { get; set; }

    [DataMember]
    public string Language { get; set; }

    [DataMember]
    public string Title { get; set; }

    [DataMember]
    public string AudioUid { get; set; }

    [DataMember]
    public string AudioUrl { get; set; }

    [DataMember]
    public string ActivationType { get; set; }

    [DataMember]
    public string ImageUrl { get; set; }

    [DataMember]
    public int Duration { get; set; }

    public AudioTrackInfo()
    {
      this.MtgObjectType = MtgObjectType.Unknown;
      this.MtgParentType = MtgObjectType.Unknown;
      this.MtgObjectAccessType = MtgObjectAccessType.Online;
    }

    public string ToTag() => JsonSerializerHelper.Serialize<AudioTrackInfo>(this);

    public static AudioTrackInfo FromTag(string source)
    {
      if (string.IsNullOrWhiteSpace(source))
        return (AudioTrackInfo) null;
      try
      {
        return JsonSerializerHelper.Deserialize<AudioTrackInfo>(source);
      }
      catch
      {
        return (AudioTrackInfo) null;
      }
    }
  }
}
