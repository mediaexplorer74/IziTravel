// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Quiz.FlyoutQuizCommentViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Common.ViewModels.Flyout;
using Izi.Travel.Shell.Core.Extensions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Quiz
{
  public class FlyoutQuizCommentViewModel : FlyoutViewModel
  {
    private string _comment;
    private int _resetVerticalOffsetTrigger;

    public string Comment
    {
      get => this._comment;
      set => this.SetProperty<string>(ref this._comment, value, propertyName: nameof (Comment));
    }

    public int ResetVerticalOffsetTrigger
    {
      get => this._resetVerticalOffsetTrigger;
      set
      {
        this.SetProperty<int>(ref this._resetVerticalOffsetTrigger, value, propertyName: nameof (ResetVerticalOffsetTrigger));
      }
    }

    protected override void OnOpening() => ++this.ResetVerticalOffsetTrigger;
  }
}
