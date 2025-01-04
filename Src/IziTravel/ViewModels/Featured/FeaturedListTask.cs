// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Featured.FeaturedListTask
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Views.Featured;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Featured
{
  public sealed class FeaturedListTask
  {
    public void Show()
    {
      FeaturedListFlyoutViewModel listFlyoutViewModel = new FeaturedListFlyoutViewModel();
      new FeaturedListFlyoutView().DataContext = (object) listFlyoutViewModel;
      if (!listFlyoutViewModel.OpenCommand.CanExecute((object) null))
        return;
      listFlyoutViewModel.OpenCommand.Execute((object) null);
    }
  }
}
