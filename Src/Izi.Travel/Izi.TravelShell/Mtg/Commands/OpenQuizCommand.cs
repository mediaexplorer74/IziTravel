// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.OpenQuizCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Quiz;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class OpenQuizCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;
    private readonly MtgObject _mtgObjectRoot;

    public bool HasQuiz { get; }

    public OpenQuizCommand(MtgObject mtgObject, MtgObject mtgObjectRoot)
    {
      this._mtgObject = mtgObject;
      this._mtgObjectRoot = mtgObjectRoot;
      this.HasQuiz = this._mtgObject?.MainContent?.Quiz != null;
    }

    public override bool CanExecute(object parameter) => this.HasQuiz;

    public override void Execute(object parameter)
    {
      if (!this.HasQuiz || !PurchaseFlyoutDialog.ConditionalShow(this._mtgObjectRoot))
        return;
      PhoneStateHelper.SetParameter<MtgObject>("MtgObjectFull", this._mtgObject);
      ShellServiceFacade.NavigationService.UriFor<QuizPartViewModel>().WithParam<string>((Expression<Func<QuizPartViewModel, string>>) (x => x.Uid), this._mtgObject.Uid).WithParam<string>((Expression<Func<QuizPartViewModel, string>>) (x => x.Language), this._mtgObject.Language).Navigate();
    }
  }
}
