// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Quiz.ProfileQuizListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Quiz;
using Izi.Travel.Business.Services;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Quiz
{
  public class ProfileQuizListItemViewModel : PropertyChangedBase
  {
    private readonly ProfileQuizListViewModel _listViewModel;
    private readonly QuizData _quizData;

    public ProfileQuizListViewModel ListViewModel => this._listViewModel;

    public QuizData Data => this._quizData;

    public string Uid { get; private set; }

    public string Language { get; private set; }

    public string Title { get; private set; }

    public MtgObjectType Type { get; private set; }

    public string ImageUrl { get; private set; }

    public bool Correct { get; private set; }

    public ProfileQuizListItemViewModel(ProfileQuizListViewModel listViewModel, QuizData quizData)
    {
      this._listViewModel = listViewModel;
      this._quizData = quizData;
      this.Initialize();
    }

    private void Initialize()
    {
      if (this._quizData == null)
        return;
      this.Uid = this._quizData.Uid;
      this.Language = this._quizData.Language;
      this.Type = this._quizData.Type;
      this.Title = this._quizData.Title;
      this.ImageUrl = ServiceFacade.MediaService.GetImageUrl(this._quizData.ImageUid, this._quizData.ContentProviderUid, ImageFormat.Low120X90);
      this.Correct = this._quizData.AnswerCorrect;
    }
  }
}
