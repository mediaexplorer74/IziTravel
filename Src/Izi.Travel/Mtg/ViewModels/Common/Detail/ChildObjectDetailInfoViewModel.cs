// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Common.Detail.ChildObjectDetailInfoViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Context;
using Izi.Travel.Mtg.ViewModels.Common.Detail.Interfaces;
using Windows.UI.Xaml.Navigation;
using System.Windows.Input;
#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Common.Detail
{
  public class ChildObjectDetailInfoViewModel : DetailInfoViewModel
  {
    protected IDetailPartViewModel DetailPartViewModel
    {
      get
      {
        return this.DetailViewModel == null ? (IDetailPartViewModel) null : this.DetailViewModel.DetailPartViewModel;
      }
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      NavigationMode navigationMode = FrameNavigationContext.Instance.NavigationMode;
      if (this.DetailPartViewModel == null || !this.DetailPartViewModel.AutoPlay || navigationMode != NavigationMode.New || !this.AudioViewModel.PlayCommand.CanExecute((object) null))
        return;
      ((ICommand)this.AudioViewModel.PlayCommand).Execute((object) null);
    }
  }
}

