// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Museum.Detail.MuseumDetailCollectionListViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Resources;
using Izi.Travel.Mtg.ViewModels.Collection.List;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Mtg.Views.Common.Detail;
using System;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Museum.Detail
{
  [View(typeof (DetailTabView))]
  public class MuseumDetailCollectionListViewModel : DetailTabViewModel
  {
    public override string DisplayName
    {
      get => AppResources.LabelCollections.ToLower();
      set => throw new NotImplementedException();
    }

    protected override void OnInitialize()
    {
      this.ActiveItem = (IScreen) IoC.Get<CollectionListViewModel>();
      base.OnInitialize();
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[2]{ "NowPlaying", "QrCode" };
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[2]{ "GetDirections", "Share" };
    }
  }
}
