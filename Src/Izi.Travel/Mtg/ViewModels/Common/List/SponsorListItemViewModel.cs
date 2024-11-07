// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.SponsorListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public class SponsorListItemViewModel
  {
    public string Name { get; set; }

    public string ImageUrl { get; set; }

    public string Url { get; set; }

    public SponsorListItemViewModel(MtgObject mtgObject, Sponsor sponsor)
    {
      if (mtgObject == null || sponsor == null)
        return;
      this.Name = sponsor.Name;
      this.Url = sponsor.Website;
      if (mtgObject.ContentProvider == null || sponsor.Images == null || sponsor.Images.Length == 0)
        return;
      this.ImageUrl = ServiceFacade.MediaService.GetImageUrl(sponsor.Images[0].Uid, mtgObject.ContentProvider.Uid, ImageFormat.Undefined, ImageExtension.Png);
    }
  }
}
