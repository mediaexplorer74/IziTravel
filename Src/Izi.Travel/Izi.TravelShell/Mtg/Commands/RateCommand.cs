// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.RateCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class RateCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;

    public RateCommand(MtgObject mtgObject) => this._mtgObject = mtgObject;

    public override bool CanExecute(object parameter)
    {
      return this._mtgObject != null && RateHelper.CanRate(this._mtgObject.Uid, this._mtgObject.Hash);
    }

    public override void Execute(object parameter)
    {
      if (this._mtgObject == null || this._mtgObject.MainContent == null)
        return;
      ShellServiceFacade.NavigationService.UriFor<RatePartViewModel>().WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Uid), this._mtgObject.Uid).WithParam<MtgObjectType>((Expression<Func<RatePartViewModel, MtgObjectType>>) (x => x.Type), this._mtgObject.Type).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Hash), this._mtgObject.Hash).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Language), this._mtgObject.MainContent.Language).WithParam<string>((Expression<Func<RatePartViewModel, string>>) (x => x.Title), this._mtgObject.MainContent.Title).Navigate();
    }
  }
}
