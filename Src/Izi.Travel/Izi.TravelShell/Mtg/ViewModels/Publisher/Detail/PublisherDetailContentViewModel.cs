// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail.PublisherDetailContentViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailContentViewModel : MtgObjectTabViewModel
  {
    private string _brandCoverUrl;
    private string _brandLogoUrl;
    private string _summary;
    private int _contentCount;

    public override string DisplayName
    {
      get => AppResources.LabelContent;
      set => throw new NotImplementedException();
    }

    public string BrandCoverUrl
    {
      get => this._brandCoverUrl;
      set
      {
        this.SetProperty<string>(ref this._brandCoverUrl, value, propertyName: nameof (BrandCoverUrl));
      }
    }

    public string BrandLogoUrl
    {
      get => this._brandLogoUrl;
      set
      {
        this.SetProperty<string>(ref this._brandLogoUrl, value, propertyName: nameof (BrandLogoUrl));
      }
    }

    public string Summary
    {
      get => this._summary;
      set => this.SetProperty<string>(ref this._summary, value, propertyName: nameof (Summary));
    }

    public int ContentCount
    {
      get => this._contentCount;
      set
      {
        this.SetProperty<int>(ref this._contentCount, value, propertyName: nameof (ContentCount));
      }
    }

    protected override void OnInitialize()
    {
      this.ActiveItem = (IScreen) IoC.Get<PublisherDetailContentListViewModel>();
      base.OnInitialize();
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      if (this.MtgObject == null || this.MtgObjectContent == null)
        return;
      this.Summary = !string.IsNullOrWhiteSpace(this.MtgObjectContent.Summary) ? this.MtgObjectContent.Summary : this.MtgObjectContent.Title;
      if (this.MtgObjectContent.Images != null)
      {
        Izi.Travel.Business.Entities.Data.Media media1 = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) this.MtgObjectContent.Images).FirstOrDefault<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.BrandCover));
        this.BrandCoverUrl = media1 == null ? "/Assets/Images/image.publisher.cover.jpg" : ServiceFacade.MediaService.GetImageUrl(media1.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.Undefined, ignoreLocal: true);
        Izi.Travel.Business.Entities.Data.Media media2 = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) this.MtgObjectContent.Images).FirstOrDefault<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.BrandLogo));
        this.BrandLogoUrl = media2 == null ? "/Assets/Images/image.publisher.logo.png" : ServiceFacade.MediaService.GetImageUrl(media2.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.Undefined, ImageExtension.Png, true);
      }
      this.ContentCount = this.MtgObject.ChildrenCount;
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[1]{ "NowPlaying" };
    }
  }
}
