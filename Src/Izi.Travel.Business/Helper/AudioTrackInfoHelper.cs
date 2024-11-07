// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.AudioTrackInfoHelper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public static class AudioTrackInfoHelper
  {
    public static AudioTrackInfo FromMtgObject(
      MtgObject mtgObject,
      string mtgParentUid,
      MtgObjectType mtgParentType,
      ActivationTypeParameter activationType)
    {
      MtgObject mtgObject1 = mtgObject;
      MtgObject mtgObjectParent = new MtgObject();
      mtgObjectParent.Uid = mtgParentUid;
      mtgObjectParent.Type = mtgParentType;
      ActivationTypeParameter activationType1 = activationType;
      return AudioTrackInfoHelper.FromMtgObject(mtgObject1, mtgObjectParent, activationType1);
    }

    public static AudioTrackInfo FromMtgObject(
      MtgObject mtgObject,
      MtgObject mtgObjectParent,
      ActivationTypeParameter activationType)
    {
      if (mtgObject == null || mtgObject.MainAudioMedia == null)
        return (AudioTrackInfo) null;
      AudioTrackInfo audioTrackInfo = new AudioTrackInfo()
      {
        MtgObjectUid = mtgObject.Uid,
        MtgObjectType = mtgObject.Type,
        MtgObjectAccessType = mtgObject.AccessType,
        AudioUid = mtgObject.MainAudioMedia.Uid,
        AudioUrl = ServiceFacade.MediaService.GetAudioUrl(mtgObject.MainAudioMedia.Uid, mtgObject.ContentProvider.Uid),
        ImageUrl = ServiceFacade.MediaService.GetImageOrPlaceholderUrl(mtgObject, ImageFormat.Low480X360),
        Duration = mtgObject.MainAudioMedia.Duration,
        ActivationType = activationType.Value
      };
      if (mtgObjectParent != null)
      {
        audioTrackInfo.MtgParentUid = mtgObjectParent.Uid;
        audioTrackInfo.MtgParentType = mtgObjectParent.Type;
      }
      else
      {
        audioTrackInfo.MtgParentUid = mtgObject.ParentUid;
        audioTrackInfo.MtgParentType = MtgObjectType.Unknown;
      }
      if (mtgObject.MainContent != null)
      {
        audioTrackInfo.Title = mtgObject.MainContent.Title;
        audioTrackInfo.Language = mtgObject.MainContent.Language;
      }
      return audioTrackInfo;
    }
  }
}
