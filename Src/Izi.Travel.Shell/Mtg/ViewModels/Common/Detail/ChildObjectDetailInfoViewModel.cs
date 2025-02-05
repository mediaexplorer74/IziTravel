// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.ChildObjectDetailInfoViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Context;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
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
      this.AudioViewModel.PlayCommand.Execute((object) null);
    }
  }
}
