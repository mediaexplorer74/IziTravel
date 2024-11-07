// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Quiz.QuizViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Quiz;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Quiz
{
  public class QuizViewModel : Screen
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (QuizViewModel));
    private readonly MtgObject _mtgObject;
    private string _question;
    private bool _isAnswered;
    private bool _isCorrect;
    private QuizAnswerViewModelExtended[] _answers;
    private QuizAnswerViewModelExtended _expandedAnswer;
    private bool _isQuestionVisible;
    private bool _isAnswersVisible;
    private bool _isResultVisible;
    private bool _isExpandedAnswerVisible;
    private bool _isAnswerExpanded;
    private readonly FlyoutQuizCommentViewModel _flyoutQuizCommentViewModel;
    private QuizData _quizData;
    private int _resetVerticalOffsetTrigger;
    private RelayCommand _animationStartupCompletedCommand;
    private RelayCommand _animationAnswerHighlightCompleted;
    private RelayCommand _selectAnswerCommand;
    private RelayCommand _resetAnswerCommand;
    private RelayCommand _expandAnswerCommand;
    private RelayCommand _collapseAnswerCommand;
    private RelayCommand _animationExpandAsnwerCompletedCommand;

    public FlyoutQuizCommentViewModel FlyoutQuizCommentViewModel
    {
      get => this._flyoutQuizCommentViewModel;
    }

    public string Question
    {
      get => this._question;
      set => this.SetProperty<string>(ref this._question, value, propertyName: nameof (Question));
    }

    public bool IsAnswered
    {
      get => this._isAnswered;
      set
      {
        this.SetProperty<bool>(ref this._isAnswered, value, (System.Action) (() => this.ResetAnswerCommand.RaiseCanExecuteChanged()), nameof (IsAnswered));
      }
    }

    public bool IsCorrect
    {
      get => this._isCorrect;
      set => this.SetProperty<bool>(ref this._isCorrect, value, propertyName: nameof (IsCorrect));
    }

    public bool IsQuestionVisible
    {
      get => this._isQuestionVisible;
      set
      {
        this.SetProperty<bool>(ref this._isQuestionVisible, value, propertyName: nameof (IsQuestionVisible));
      }
    }

    public QuizAnswerViewModelExtended[] Answers
    {
      get => this._answers;
      set
      {
        this.SetProperty<QuizAnswerViewModelExtended[]>(ref this._answers, value, propertyName: nameof (Answers));
      }
    }

    public bool IsAnswersVisible
    {
      get => this._isAnswersVisible;
      set
      {
        this.SetProperty<bool>(ref this._isAnswersVisible, value, propertyName: nameof (IsAnswersVisible));
      }
    }

    public bool IsResultVisible
    {
      get => this._isResultVisible;
      set
      {
        this.SetProperty<bool>(ref this._isResultVisible, value, (System.Action) (() => this.ExpandAnswerCommand.RaiseCanExecuteChanged()), nameof (IsResultVisible));
      }
    }

    public int ResetVerticalOffsetTrigger
    {
      get => this._resetVerticalOffsetTrigger;
      set
      {
        this.SetProperty<int>(ref this._resetVerticalOffsetTrigger, value, propertyName: nameof (ResetVerticalOffsetTrigger));
      }
    }

    public QuizAnswerViewModelExtended ExpandedAnswer
    {
      get => this._expandedAnswer;
      set
      {
        this.SetProperty<QuizAnswerViewModelExtended>(ref this._expandedAnswer, value, propertyName: nameof (ExpandedAnswer));
      }
    }

    public bool IsAnswerExpanded
    {
      get => this._isAnswerExpanded;
      set
      {
        this.SetProperty<bool>(ref this._isAnswerExpanded, value, propertyName: nameof (IsAnswerExpanded));
      }
    }

    public bool IsExpandedAnswerVisible
    {
      get => this._isExpandedAnswerVisible;
      set
      {
        this.SetProperty<bool>(ref this._isExpandedAnswerVisible, value, propertyName: nameof (IsExpandedAnswerVisible));
      }
    }

    public QuizViewModel(MtgObject mtgObject)
    {
      this._mtgObject = mtgObject;
      this._flyoutQuizCommentViewModel = new FlyoutQuizCommentViewModel();
    }

    public RelayCommand AnimationStartupCompletedCommand
    {
      get
      {
        return this._animationStartupCompletedCommand ?? (this._animationStartupCompletedCommand = new RelayCommand(new Action<object>(this.ExecuteAnimationStartupCompletedCommand)));
      }
    }

    private void ExecuteAnimationStartupCompletedCommand(object parameter)
    {
    }

    public RelayCommand AnimationAnswerHighlightCompleted
    {
      get
      {
        return this._animationAnswerHighlightCompleted ?? (this._animationAnswerHighlightCompleted = new RelayCommand(new Action<object>(this.ExecuteAnimationAnswerHighlightCompleted)));
      }
    }

    private void ExecuteAnimationAnswerHighlightCompleted(object parameter)
    {
      if (!(parameter is QuizAnswerViewModelExtended viewModelExtended) || !viewModelExtended.IsCorrect)
        return;
      this.ShowResultAsync();
    }

    public RelayCommand SelectAnswerCommand
    {
      get
      {
        return this._selectAnswerCommand ?? (this._selectAnswerCommand = new RelayCommand(new Action<object>(this.ExecuteSelectAnswerCommand)));
      }
    }

    private async void ExecuteSelectAnswerCommand(object parameter)
    {
      if (!(parameter is QuizAnswerViewModelExtended currentAnswerViewModel))
        return;
      this.IsResultVisible = true;
      foreach (QuizAnswerViewModelExtended answer in this.Answers)
        answer.IsEnabled = false;
      currentAnswerViewModel.IsChecked = true;
      this.IsCorrect = currentAnswerViewModel.IsCorrect;
      this._quizData.AnswerIndex = currentAnswerViewModel.Index;
      this._quizData.AnswerCorrect = currentAnswerViewModel.IsCorrect;
      await ServiceFacade.QuizService.CreateOrUpdateQuizDataAsync(this._quizData);
      if (currentAnswerViewModel.IsCorrect)
        return;
      this.SelectCorrectAnswerAsync();
    }

    public RelayCommand ResetAnswerCommand
    {
      get
      {
        return this._resetAnswerCommand ?? (this._resetAnswerCommand = new RelayCommand(new Action<object>(this.ExecuteResetAnswerCommand), new Func<object, bool>(this.CanExecuteResetAnswerCommand)));
      }
    }

    private bool CanExecuteResetAnswerCommand(object parameter) => this.IsAnswered;

    private void ExecuteResetAnswerCommand(object parameter)
    {
      ShellServiceFacade.DialogService.Show(AppResources.PromptQuizResetTitle, AppResources.PromptQuizResetInfo, MessageBoxButtonContent.OkCancel, (Action<FlyoutDialog>) (x =>
      {
        x.PartFlyout.OverlayBrush = (Brush) new SolidColorBrush(Colors.Transparent);
        x.LeftButtonContent = (object) AppResources.CommandReset;
      }), (Action<FlyoutDialog, MessageBoxResult>) (async (x, e) =>
      {
        if (e != MessageBoxResult.OK)
          return;
        await ServiceFacade.QuizService.DeleteQuizDataAsync(this._mtgObject.Uid, this._mtgObject.Language);
        this.InitializeDataAsync();
      }));
    }

    public RelayCommand ExpandAnswerCommand
    {
      get
      {
        return this._expandAnswerCommand ?? (this._expandAnswerCommand = new RelayCommand(new Action<object>(this.ExecuteExpandAnswerCommand), new Func<object, bool>(this.CanExecuteExpandAnswerCommand)));
      }
    }

    private bool CanExecuteExpandAnswerCommand(object parameter) => !this.IsResultVisible;

    private void ExecuteExpandAnswerCommand(object parameter)
    {
      if (!(parameter is QuizAnswerViewModelExtended viewModelExtended))
        return;
      this.ExpandedAnswer = viewModelExtended;
      this.IsAnswerExpanded = true;
      this.IsExpandedAnswerVisible = true;
    }

    public RelayCommand CollapseAnswerCommand
    {
      get
      {
        return this._collapseAnswerCommand ?? (this._collapseAnswerCommand = new RelayCommand(new Action<object>(this.ExecuteCollapseAnswerCommand)));
      }
    }

    private void ExecuteCollapseAnswerCommand(object parameter)
    {
      this.IsAnswerExpanded = false;
      this.IsQuestionVisible = true;
      this.IsAnswersVisible = true;
    }

    public RelayCommand AnimationExpandAnswerCompletedCommand
    {
      get
      {
        return this._animationExpandAsnwerCompletedCommand ?? (this._animationExpandAsnwerCompletedCommand = new RelayCommand(new Action<object>(this.ExecuteAnimationExpandAnswerCompletedCommand)));
      }
    }

    private void ExecuteAnimationExpandAnswerCompletedCommand(object parameter)
    {
      if (this.IsAnswerExpanded)
      {
        this.IsQuestionVisible = false;
        this.IsAnswersVisible = false;
      }
      else
      {
        this.ExpandedAnswer = (QuizAnswerViewModelExtended) null;
        this.IsExpandedAnswerVisible = false;
      }
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.InitializeDataAsync();
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      this.IsAnswerExpanded = false;
      this.IsQuestionVisible = true;
      this.IsAnswersVisible = true;
      this.IsResultVisible = false;
      this.IsAnswered = false;
      this.Answers = (QuizAnswerViewModelExtended[]) null;
    }

    private async void InitializeDataAsync()
    {
      try
      {
        QuizViewModel quizViewModel = this;
        QuizData quizData = quizViewModel._quizData;
        QuizData quizDataAsync = await ServiceFacade.QuizService.GetQuizDataAsync(new QuizDataFilter(this._mtgObject.Uid, this._mtgObject.Language));
        quizViewModel._quizData = quizDataAsync;
        quizViewModel = (QuizViewModel) null;
      }
      catch (Exception ex)
      {
        QuizViewModel.Logger.Error(ex);
      }
      if (this._quizData == null)
        this._quizData = new QuizData()
        {
          ParentUid = (string) null,
          Uid = this._mtgObject.Uid,
          Language = this._mtgObject.Language,
          Type = this._mtgObject.Type,
          Title = this._mtgObject.MainContent.Title,
          ContentProviderUid = this._mtgObject.GetContentProviderUid(),
          ImageUid = this._mtgObject.GetImageUid(),
          Quiz = this._mtgObject.MainContent.Quiz
        };
      this.Question = this._quizData.Quiz.Question;
      this.IsQuestionVisible = true;
      this.FlyoutQuizCommentViewModel.Comment = this._quizData.Quiz.Comment;
      if (this._quizData.Quiz.Answers != null && this._quizData.Quiz.Answers.Length != 0)
        this.Answers = ((IEnumerable<QuizAnswer>) this._quizData.Quiz.Answers).Select<QuizAnswer, QuizAnswerViewModelExtended>((Func<QuizAnswer, int, QuizAnswerViewModelExtended>) ((x, i) => new QuizAnswerViewModelExtended(this)
        {
          Index = i + 1,
          Title = x.Content,
          IsCorrect = x.Correct
        })).ToArray<QuizAnswerViewModelExtended>();
      this.IsCorrect = this._quizData.AnswerCorrect;
      this.IsAnswered = this._quizData.AnswerIndex > 0;
      this.IsAnswersVisible = !this.IsAnswered;
      this.IsResultVisible = this.IsAnswered;
      await Task.Delay(300);
      if (this.IsAnswersVisible)
      {
        foreach (QuizAnswerViewModelExtended answer in this.Answers)
          answer.AnimationEnabled = true;
      }
      ++this.ResetVerticalOffsetTrigger;
    }

    private async void SelectCorrectAnswerAsync()
    {
      await Task.Delay(1000);
      if (this.Answers == null)
        return;
      QuizAnswerViewModelExtended viewModelExtended = ((IEnumerable<QuizAnswerViewModelExtended>) this.Answers).FirstOrDefault<QuizAnswerViewModelExtended>((Func<QuizAnswerViewModelExtended, bool>) (x => x.IsCorrect));
      if (viewModelExtended == null)
        return;
      viewModelExtended.IsChecked = true;
    }

    private async void ShowResultAsync()
    {
      await Task.Delay(1000);
      this.IsAnswered = true;
    }
  }
}
