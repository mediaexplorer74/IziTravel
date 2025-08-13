// ********************************************************************
// Type: Izi.Travel.Mtg.Components.Tasks.NumpadSearchTask
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Mtg.ViewModels.Common;
using Izi.Travel.Mtg.ViewModels.Common.Numpad;
using Izi.Travel.Mtg.Views.Common.Numpad;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Mtg.Components.Tasks
{
  public class NumpadSearchTask : BaseSearchTask
  {
    protected override BaseSearchFlyoutViewModel CreateFlyoutViewModel()
    {
      return (BaseSearchFlyoutViewModel) new NumpadFlyoutViewModel(this.ParentScreen);
    }

    protected override Control CreateFlyoutView() => (Control) new NumpadFlyoutView();
  }
}
