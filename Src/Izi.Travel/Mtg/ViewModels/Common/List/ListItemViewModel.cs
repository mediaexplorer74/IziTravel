// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.ListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public abstract class ListItemViewModel : PropertyChangedBase
  {
    private readonly IListViewModel _listViewModel;
    private MtgObject _mtgObject;
    private bool _isHidden;

    public IListViewModel ListViewModel => this._listViewModel;

    public MtgObject MtgObject => this._mtgObject;

    protected Content MtgObjectContent
    {
      get => this._mtgObject == null ? (Content) null : this._mtgObject.MainContent;
    }

    public string Uid => this.MtgObject == null ? (string) null : this.MtgObject.Uid;

    public string Title
    {
      get
      {
        return this.MtgObjectContent == null || string.IsNullOrWhiteSpace(this.MtgObjectContent.Title) ? (string) null : this.MtgObjectContent.Title.Trim();
      }
    }

    public MtgObjectType Type
    {
      get => this.MtgObject == null ? MtgObjectType.Unknown : this.MtgObject.Type;
    }

    public string Language
    {
      get => this.MtgObjectContent == null ? (string) null : this.MtgObjectContent.Language;
    }

    protected virtual ImageFormat ImageFormat => ImageFormat.Low120X90;

    public string ImageUrl { get; protected set; }

    public bool IsHidden
    {
      get => this._isHidden;
      set => this.SetProperty<bool>(ref this._isHidden, value, propertyName: nameof (IsHidden));
    }

    public bool IsRated
    {
      get
      {
        return this.MtgObject != null && this.MtgObject.Rating != null && this.MtgObject.Rating.Average > 0.0;
      }
    }

    public double Rating
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Rating == null ? 0.0 : this.MtgObject.Rating.Average / 2.0;
      }
    }

    public string RatingLabel
    {
      get
      {
        return string.Format(AppResources.LabelShortRatingCount, (object) (this.MtgObject == null || this.MtgObject.Rating == null ? 0 : this.MtgObject.Rating.Count));
      }
    }

    protected ListItemViewModel(IListViewModel listViewModel, MtgObject mtgObject)
    {
      this._listViewModel = listViewModel;
      this._mtgObject = mtgObject;
      this.Initialize();
    }

    private void Initialize()
    {
      this.ImageUrl = ServiceFacade.MediaService.GetImageOrPlaceholderUrl(this.MtgObject, this.ImageFormat);
    }

    public virtual void RefreshData(MtgObject mtgObject)
    {
      if (this._mtgObject == null || mtgObject == null || this._mtgObject.Key != mtgObject.Key)
        return;
      this._mtgObject = mtgObject;
      this.Initialize();
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.Title));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.ImageUrl));
    }
  }
}
