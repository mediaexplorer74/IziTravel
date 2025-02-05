// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items.PlayerStartItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Resources;
using System;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items
{
  public class PlayerStartItemViewModel : PlayerItemViewModel
  {
    public int ChildrenCount { get; set; }

    public int AudioDuration { get; set; }

    public string StartLabel
    {
      get
      {
        return string.Format(AppResources.PlayerStartLabel, (object) this.ChildrenCount, (object) Math.Max(this.AudioDuration / 60, 1));
      }
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

    public PlayerStartItemViewModel(
      PlayerViewModel playerViewModel,
      MtgObject mtgObjectRoot,
      MtgObject mtgObject)
      : base(playerViewModel, -1, mtgObjectRoot, (MtgObject) null, mtgObject)
    {
      if (mtgObject == null || mtgObject.MainContent == null)
        return;
      this.ChildrenCount = mtgObject.MainContent.ChildrenCount;
      this.AudioDuration = mtgObject.MainContent.AudioDuration;
    }

    protected override string GetImageUrl()
    {
      return this.MtgObject == null || this.MtgObject.MainImageMedia == null || this.MtgObject.ContentProvider == null ? (string) null : ServiceFacade.MediaService.GetImageUrl(this.MtgObject.MainImageMedia.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.High800X600);
    }
  }
}
