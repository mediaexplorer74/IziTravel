// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.Quiz.ProfileQuizListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Quiz;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.ViewModels.Quiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile.Quiz
{
  public class ProfileQuizListViewModel : BaseListViewModel<ProfileQuizListItemViewModel>
  {
    private int _correctCount;
    private int _incorrectCount;

    public int CorrectCount
    {
      get => this._correctCount;
      set
      {
        this.SetProperty<int>(ref this._correctCount, value, propertyName: nameof (CorrectCount));
      }
    }

    public int IncorrectCount
    {
      get => this._incorrectCount;
      set
      {
        this.SetProperty<int>(ref this._incorrectCount, value, propertyName: nameof (IncorrectCount));
      }
    }

    public int TotalCount => this.CorrectCount + this.IncorrectCount;

    public double Ratio
    {
      get => this.TotalCount <= 0 ? 0.0 : (double) this.CorrectCount / (double) this.TotalCount;
    }

    public string AnswersLabel
    {
      get => string.Format(AppResources.QuizTotalAnswersCount, (object) this.TotalCount);
    }

    public string CorrectAnswersLabel
    {
      get => string.Format(AppResources.QuizCorrectAnswersCount, (object) this.CorrectCount);
    }

    public string IncorrectAnswersLabel
    {
      get => string.Format(AppResources.QuizIncorrectAnswersCount, (object) this.IncorrectCount);
    }

    public bool IsStatisticsVisible => this.TotalCount > 0;

    protected override void ExecuteNavigateCommand(object parameter)
    {
      if (!(parameter is ProfileQuizListItemViewModel listItemViewModel) || listItemViewModel.Data == null)
        return;
      MtgObject mtgObject1 = new MtgObject();
      mtgObject1.Uid = listItemViewModel.Uid;
      mtgObject1.Type = listItemViewModel.Type;
      MtgObject mtgObject2 = mtgObject1;
      ContentProvider contentProvider = new ContentProvider();
      contentProvider.Uid = listItemViewModel.Data.ContentProviderUid;
      mtgObject2.ContentProvider = contentProvider;
      MtgObject mtgObject3 = mtgObject1;
      Content[] contentArray = new Content[1];
      Content content1 = new Content();
      content1.Title = listItemViewModel.Title;
      content1.Language = listItemViewModel.Language;
      Content content2 = content1;
      Izi.Travel.Business.Entities.Data.Media[] mediaArray = new Izi.Travel.Business.Entities.Data.Media[1];
      Izi.Travel.Business.Entities.Data.Media media = new Izi.Travel.Business.Entities.Data.Media();
      media.Uid = listItemViewModel.Data.ImageUid;
      media.Format = MediaFormat.Image;
      mediaArray[0] = media;
      content2.Images = mediaArray;
      content1.Quiz = listItemViewModel.Data.Quiz;
      contentArray[0] = content1;
      mtgObject3.Content = contentArray;
      PhoneStateHelper.SetParameter<MtgObject>("MtgObjectFull", mtgObject1);
      ShellServiceFacade.NavigationService.UriFor<QuizPartViewModel>().WithParam<string>((Expression<Func<QuizPartViewModel, string>>) (x => x.Uid), listItemViewModel.Uid).WithParam<string>((Expression<Func<QuizPartViewModel, string>>) (x => x.Language), listItemViewModel.Language).Navigate();
    }

    protected override bool CanExecuteDeleteItemCommand(object parameter) => !this.IsDataLoading;

    protected override async void ExecuteDeleteItemCommand(object parameter)
    {
      if (!(parameter is FrameworkElement frameworkElement) || !(frameworkElement.DataContext is ProfileQuizListItemViewModel listItemViewModel))
        return;
      await ServiceFacade.QuizService.DeleteQuizDataAsync(listItemViewModel.Uid, listItemViewModel.Language);
      this.Items.Remove(listItemViewModel);
      await this.LoadAdditionalDataAsync();
      this.OnLoadDataCompleted();
    }

    protected override void OnActivate()
    {
      this.Items.Clear();
      base.OnActivate();
    }

    protected override async Task LoadAdditionalDataAsync()
    {
      QuizDataStatistics dataStatisticsAsync = await ServiceFacade.QuizService.GetQuizDataStatisticsAsync(new QuizDataStatisticsFilter());
      if (dataStatisticsAsync == null)
        return;
      this.CorrectCount = dataStatisticsAsync.CorrectAnswerCount;
      this.IncorrectCount = dataStatisticsAsync.IncorrectAnswerCount;
      this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Ratio));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.AnswersLabel));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.CorrectAnswersLabel));
      this.NotifyOfPropertyChange<string>((Expression<Func<string>>) (() => this.IncorrectAnswersLabel));
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsStatisticsVisible));
    }

    protected override async Task<IEnumerable<ProfileQuizListItemViewModel>> GetDataAsync()
    {
      QuizData[] quizDataListAsync = await ServiceFacade.QuizService.GetQuizDataListAsync(new QuizDataListFilter()
      {
        Offset = new int?(this.Items.Count),
        Limit = new int?(25)
      });
      return quizDataListAsync == null || quizDataListAsync.Length == 0 ? (IEnumerable<ProfileQuizListItemViewModel>) null : ((IEnumerable<QuizData>) quizDataListAsync).Select<QuizData, ProfileQuizListItemViewModel>((Func<QuizData, ProfileQuizListItemViewModel>) (x => new ProfileQuizListItemViewModel(this, x)));
    }
  }
}
