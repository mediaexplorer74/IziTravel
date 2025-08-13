// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.DetailExhibitListViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Resources;
using Izi.Travel.Mtg.ViewModels.Exhibit.List;
using Izi.Travel.Mtg.Views.Common.Detail;
using System;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail
{
  [View(typeof (DetailTabView))]
  public class DetailExhibitListViewModel : DetailTabViewModel
  {
    public override string DisplayName
    {
      get => AppResources.LabelExhibits.ToLower();
      set => throw new NotImplementedException();
    }

    public ExhibitListViewModel ExhibitListViewModel => this.ActiveItem as ExhibitListViewModel;

    protected override void OnInitialize()
    {
      this.ActiveItem = (IScreen) IoC.Get<ExhibitListViewModel>();
      base.OnInitialize();
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[3]
      {
        "NowPlaying",
        "Numpad",
        "QrCode"
      };
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      return new string[2]{ "GetDirections", "Share" };
    }
  }
}
