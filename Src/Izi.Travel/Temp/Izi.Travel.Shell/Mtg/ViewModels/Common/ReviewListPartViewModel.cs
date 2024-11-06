// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.ReviewListPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using System.Linq;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public class ReviewListPartViewModel : Conductor<Screen>.Collection.OneActive, IMtgObjectProvider
  {
    private static MtgObject _sharedMtgObject;

    public MtgObject MtgObject { get; private set; }

    public MtgObject MtgObjectParent { get; private set; }

    public MtgObject MtgObjectRoot { get; private set; }

    public ReviewListViewModel ReviewListViewModel { get; set; }

    public ReviewListPartViewModel(ReviewListViewModel reviewListViewModel)
    {
      this.ReviewListViewModel = reviewListViewModel;
      this.MtgObject = ReviewListPartViewModel._sharedMtgObject;
      if (this.MtgObject != null)
        return;
      ShellServiceFacade.NavigationService.GoBack();
    }

    protected override void OnInitialize()
    {
      this.Items.Add((Screen) this.ReviewListViewModel);
      this.ActivateItem(this.Items.First<Screen>());
    }

    public static void Navigate(MtgObject mtgObject)
    {
      ReviewListPartViewModel._sharedMtgObject = mtgObject;
      ShellServiceFacade.NavigationService.UriFor<ReviewListPartViewModel>().Navigate();
    }
  }
}
