// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.OpenQrCodeScannerCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.Components.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class OpenQrCodeScannerCommand : BaseCommand
  {
    private readonly IScreen _owner;
    private readonly string _parentUid;
    private readonly string _parentLanguage;
    private readonly MtgObjectType _parentType;

    public OpenQrCodeScannerCommand(
      IScreen owner,
      string parentUid,
      string parentLanguage,
      MtgObjectType parentType)
    {
      this._owner = owner;
      this._parentUid = parentUid;
      this._parentLanguage = parentLanguage;
      this._parentType = parentType;
    }

    public override bool CanExecute(object parameter) => true;

    public override void Execute(object parameter)
    {
      BarcodeScannerTask barcodeScannerTask = new BarcodeScannerTask();
      barcodeScannerTask.ParentUid = this._parentUid;
      barcodeScannerTask.ParentLanguage = this._parentLanguage;
      barcodeScannerTask.ParentType = this._parentType;
      barcodeScannerTask.ParentScreen = this._owner;
      barcodeScannerTask.ActivationMode = FlyoutSearchActivationMode.None;
      barcodeScannerTask.NavigationMode = FlyoutSearchNavigationMode.Player;
      barcodeScannerTask.CloseMode = FlyoutSearchCloseMode.Silent;
      barcodeScannerTask.Show();
    }
  }
}
