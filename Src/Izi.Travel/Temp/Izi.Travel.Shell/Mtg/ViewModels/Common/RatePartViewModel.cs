// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.RatePartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Mtg.Helpers;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public class RatePartViewModel : Screen
  {
    private readonly ILog _log = LogManager.GetLog(typeof (RatePartViewModel));
    private double _rating;
    private string _reviewerName;
    private string _text;
    private bool _isBusy;
    private RelayCommand _submitCommand;
    private RelayCommand _cancelCommand;

    public string Uid { get; set; }

    public string Hash { get; set; }

    public string Language { get; set; }

    public string Title { get; set; }

    public MtgObjectType Type { get; set; }

    public double Rating
    {
      get => this._rating;
      set
      {
        this.SetProperty<double, string>(ref this._rating, value, (Expression<Func<string>>) (() => this.Hint), new System.Action(((BaseCommand) this.SubmitCommand).RaiseCanExecuteChanged), nameof (Rating));
      }
    }

    public string ReviewerName
    {
      get => this._reviewerName;
      set
      {
        this.SetProperty<string>(ref this._reviewerName, value, propertyName: nameof (ReviewerName));
      }
    }

    public string Text
    {
      get => this._text;
      set => this.SetProperty<string>(ref this._text, value, propertyName: nameof (Text));
    }

    public bool IsBusy
    {
      get => this._isBusy;
      set => this.SetProperty<bool>(ref this._isBusy, value, propertyName: nameof (IsBusy));
    }

    public string Hint
    {
      get
      {
        switch (this.Rating)
        {
          case 1.0:
            return AppResources.HintRating1;
          case 2.0:
            return AppResources.HintRating2;
          case 3.0:
            return AppResources.HintRating3;
          case 4.0:
            return AppResources.HintRating4;
          case 5.0:
            return AppResources.HintRating5;
          default:
            return AppResources.HintRating0;
        }
      }
    }

    public RatePartViewModel()
    {
      this.ReviewerName = ServiceFacade.SettingsService.GetAppSettings().ReviewerName;
    }

    public RelayCommand SubmitCommand
    {
      get
      {
        return this._submitCommand ?? (this._submitCommand = new RelayCommand(new Action<object>(this.Submit), new Func<object, bool>(this.CanSubmit)));
      }
    }

    private bool CanSubmit(object o) => this.Rating > 0.0;

    private async void Submit(object x)
    {
      if (this.IsBusy)
        return;
      this.IsBusy = true;
      try
      {
        PostReviewFilter reviewFilter = new PostReviewFilter()
        {
          Uid = this.Uid,
          Hash = this.Hash,
          Language = this.Language,
          Rating = (int) this.Rating * 2,
          ReviewerName = !string.IsNullOrWhiteSpace(this.ReviewerName) ? this.ReviewerName : (string) null,
          Text = !string.IsNullOrWhiteSpace(this.Text) ? this.Text : (string) null
        };
        await ServiceFacade.MtgObjectService.PostReviewAsync(reviewFilter);
        RateHelper.Rate(this.Uid, this.Hash);
        AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
        appSettings.ReviewerName = reviewFilter.ReviewerName;
        ServiceFacade.SettingsService.SaveAppSettings(appSettings);
        MtgObject mtgObject = new MtgObject();
        mtgObject.Uid = this.Uid;
        mtgObject.Hash = this.Hash;
        mtgObject.Languages = new string[1]{ this.Language };
        mtgObject.Type = this.Type;
        mtgObject.AccessType = MtgObjectAccessType.Online;
        mtgObject.Content = new Content[1]
        {
          new Content()
          {
            Title = this.Title,
            Language = this.Language
          }
        };
        AnalyticsHelper.SendReview(mtgObject, reviewFilter.Text, (long) reviewFilter.Rating);
        ShellServiceFacade.DialogService.ShowToast(AppResources.RateSuccess, (Uri) null, (System.Action) null, false);
        ShellServiceFacade.NavigationService.GoBack();
        reviewFilter = (PostReviewFilter) null;
      }
      catch (Exception ex)
      {
        ShellServiceFacade.DialogService.ShowToast(AppResources.RateFailure, (Uri) null, (System.Action) null, false, "IziTravelVioletBrush");
        this._log.Error(ex);
      }
      this.IsBusy = false;
    }

    public RelayCommand CancelCommand
    {
      get
      {
        return this._cancelCommand ?? (this._cancelCommand = new RelayCommand(new Action<object>(this.Cancel)));
      }
    }

    private void Cancel(object x) => ShellServiceFacade.NavigationService.GoBack();
  }
}
