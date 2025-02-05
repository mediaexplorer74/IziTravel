// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Featured.FeaturedListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Extensions;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Featured
{
  public sealed class FeaturedListItemViewModel : PropertyChangedBase
  {
    private readonly IListViewModel _listViewModel;
    private readonly MtgObject _mtgObject;
    private string _title;
    private string _subTitle;
    private string _imageUrl;

    public IListViewModel ListViewModel => this._listViewModel;

    public MtgObject MtgObject => this._mtgObject;

    public string Title
    {
      get => this._title;
      private set => this.SetProperty<string>(ref this._title, value, propertyName: nameof (Title));
    }

    public string SubTitle
    {
      get => this._subTitle;
      private set
      {
        this.SetProperty<string>(ref this._subTitle, value, propertyName: nameof (SubTitle));
      }
    }

    public string ImageUrl
    {
      get => this._imageUrl;
      private set
      {
        this.SetProperty<string>(ref this._imageUrl, value, propertyName: nameof (ImageUrl));
      }
    }

    public FeaturedListItemViewModel(IListViewModel listViewModel, MtgObject mtgObject)
    {
      this._listViewModel = listViewModel;
      this._mtgObject = mtgObject;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this._mtgObject == null)
        return;
      if (this._mtgObject.MainContent != null)
      {
        this.Title = this._mtgObject.MainContent.Title;
        this.SubTitle = this._mtgObject.MainContent.Summary;
      }
      if (this._mtgObject.MainImageMedia == null)
        return;
      this.ImageUrl = ServiceFacade.MediaService.GetFeaturedImageUrl(this._mtgObject.MainImageMedia.Uid);
    }
  }
}
