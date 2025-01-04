// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Components.Tasks.BarcodeScannerTask
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.ViewModels.Common;
using Izi.Travel.Shell.Mtg.ViewModels.Common.QrCode;
using Izi.Travel.Shell.Mtg.Views.Common.QrCode;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Components.Tasks
{
  public class BarcodeScannerTask : BaseSearchTask
  {
    protected override BaseSearchFlyoutViewModel CreateFlyoutViewModel()
    {
      return (BaseSearchFlyoutViewModel) new QrCodeScannerFlyoutViewModel(this.ParentScreen);
    }

    protected override Control CreateFlyoutView() => (Control) new QrCodeScannerFlyoutView();
  }
}
