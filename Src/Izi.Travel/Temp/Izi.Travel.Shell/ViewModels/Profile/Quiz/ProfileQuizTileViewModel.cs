// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Quiz.ProfileQuizTileViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Quiz
{
  public class ProfileQuizTileViewModel : ProfileTileViewModel
  {
    public override string Title => AppResources.LabelQuizes;

    public override ProfileType Type => ProfileType.Quiz;
  }
}
