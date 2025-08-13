// ********************************************************************
// Type: Izi.Travel.ViewModels.Profile.Quiz.ProfileQuizTabViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Resources;
using Izi.Travel.Model.Profile;
using Izi.Travel.Views.Profile;
using System;

#nullable disable
namespace Izi.Travel.ViewModels.Profile.Quiz
{
  [View(typeof (ProfileTabView))]
  public class ProfileQuizTabViewModel : ProfileTabViewModel<ProfileQuizListViewModel>
  {
    public override string DisplayName
    {
      get => AppResources.LabelQuizes;
      set => throw new NotImplementedException();
    }

    public override ProfileType Type => ProfileType.Quiz;
  }
}
