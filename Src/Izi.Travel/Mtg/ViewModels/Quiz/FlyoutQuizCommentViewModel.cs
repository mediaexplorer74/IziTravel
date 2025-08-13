// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Quiz.FlyoutQuizCommentViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Common.ViewModels.Flyout;
using Izi.Travel.Core.Extensions;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Quiz
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
