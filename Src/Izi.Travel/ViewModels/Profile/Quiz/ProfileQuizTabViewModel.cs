// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Quiz.ProfileQuizTabViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using Izi.Travel.Shell.Views.Profile;
using System;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Quiz
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
