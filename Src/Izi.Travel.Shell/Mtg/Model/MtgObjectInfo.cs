// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Model.MtgObjectInfo
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Model
{
  public class MtgObjectInfo
  {
    public string Uid { get; set; }

    public string Language { get; set; }

    public string Title { get; set; }

    public string Number { get; set; }

    public string ImageUrl { get; set; }

    public static MtgObjectInfo FromMtgObject(MtgObject mtgObject, ImageFormat imageFormat)
    {
      MtgObjectInfo mtgObjectInfo = new MtgObjectInfo()
      {
        Uid = mtgObject.Uid,
        Number = mtgObject.Location != null ? mtgObject.Location.Number : (string) null,
        ImageUrl = ServiceFacade.MediaService.GetImageOrPlaceholderUrl(mtgObject, imageFormat)
      };
      if (mtgObject.MainContent != null)
      {
        mtgObjectInfo.Language = mtgObject.MainContent.Language;
        mtgObjectInfo.Title = mtgObject.MainContent.Title;
      }
      return mtgObjectInfo;
    }
  }
}
