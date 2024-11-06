// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Quiz.QuizPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Helpers;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Quiz
{
  public class QuizPartViewModel : Conductor<IScreen>
  {
    public string Uid { get; set; }

    public string Language { get; set; }

    protected override void OnInitialize()
    {
      MtgObject parameter = PhoneStateHelper.GetParameter<MtgObject>("MtgObjectFull");
      if (parameter != null)
        this.ActiveItem = (IScreen) new QuizViewModel(parameter);
      PhoneStateHelper.SetParameter<MtgObject>("MtgObjectFull", (MtgObject) null);
      base.OnInitialize();
    }
  }
}
