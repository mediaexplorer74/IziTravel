// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.OpenReviewListCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Mtg.ViewModels.Common;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class OpenReviewListCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;

    public OpenReviewListCommand(MtgObject mtgObject) => this._mtgObject = mtgObject;

    public override bool CanExecute(object parameter) => true;

    public override void Execute(object parameter)
    {
      ReviewListPartViewModel.Navigate(this._mtgObject);
    }
  }
}
