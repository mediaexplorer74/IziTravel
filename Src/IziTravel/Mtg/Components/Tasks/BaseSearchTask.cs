// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Components.Tasks.BaseSearchTask
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Components.Tasks
{
  public abstract class BaseSearchTask
  {
    public IScreen ParentScreen { get; set; }

    public string ParentUid { get; set; }

    public MtgObjectType ParentType { get; set; }

    public string ParentLanguage { get; set; }

    public FlyoutSearchActivationMode ActivationMode { get; set; }

    public FlyoutSearchNavigationMode NavigationMode { get; set; }

    public FlyoutSearchCloseMode CloseMode { get; set; }

    protected BaseSearchTask()
    {
      this.ParentType = MtgObjectType.Unknown;
      this.ActivationMode = FlyoutSearchActivationMode.None;
      this.NavigationMode = FlyoutSearchNavigationMode.None;
      this.CloseMode = FlyoutSearchCloseMode.ReactivateParent;
    }

    public void Show()
    {
      BaseSearchFlyoutViewModel flyoutViewModel = this.CreateFlyoutViewModel();
      if (flyoutViewModel == null)
        return;
      Control flyoutView = this.CreateFlyoutView();
      if (flyoutView == null)
        return;
      flyoutViewModel.ParentUid = this.ParentUid;
      flyoutViewModel.ParentType = this.ParentType;
      flyoutViewModel.ParentLanguage = this.ParentLanguage;
      flyoutViewModel.NavigationMode = this.NavigationMode;
      flyoutViewModel.ActivationMode = this.ActivationMode;
      flyoutViewModel.CloseMode = this.CloseMode;
      flyoutView.DataContext = (object) flyoutViewModel;
      if (!flyoutViewModel.OpenCommand.CanExecute((object) null))
        return;
      flyoutViewModel.OpenCommand.Execute((object) null);
    }

    protected abstract BaseSearchFlyoutViewModel CreateFlyoutViewModel();

    protected abstract Control CreateFlyoutView();
  }
}
