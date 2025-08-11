// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.DetailInfoViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Quiz;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.Controls;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Media.Provider;
using Izi.Travel.Shell.Media.ViewModels;
using Izi.Travel.Shell.Media.ViewModels.Audio;
using Izi.Travel.Shell.Media.ViewModels.Common;
using Izi.Travel.Shell.Mtg.Commands;
using Izi.Travel.Shell.Mtg.Model;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Windows.UI.Xaml;
using Windows.System;
using Windows.ApplicationModel.Calls;
using Windows.Foundation;
using Izi.Travel.Utility.Extensions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public abstract class DetailInfoViewModel : DetailTabViewModel
  {
    private MediaItemViewModel[] _images;
    private MediaItemViewModel _selectedImage;
    private string _title;
    private string _description;
    private AudioContentViewModel _audioViewModel;
    private DetailInfoViewModel.QuizInfoViewModel _quizViewModel;
    private MediaInfo _videoMedia;
    private WorkingHoursInfo[] _workingHours;
    private string _address;
    private string[] _sites;
    private string _phone;
    private string _contentProviderName;
    private string _publisherName;
    private string _publisherImageUrl;
    protected readonly ILog Logger;
    private RelayCommand _openPublisherCommand;
    private RelayCommand _purchaseCommand;
    private RelayCommand _openImageMediaPlayerCommand;
    private OpenVideoCommand _openVideoMediaPlayerCommand;
    private RelayCommand _openSiteCommand;
    private RelayCommand _callPhoneCommand;
    private BaseCommand _openQuizCommand;

    public override string DisplayName
    {
      get => AppResources.LabelInfo.ToLower();
      set => throw new NotImplementedException();
    }

    public MediaItemViewModel[] Images
    {
      get => this._images;
      set
      {
        this.SetProperty<MediaItemViewModel[], MediaItemViewModel[], bool>(ref this._images, value, (Expression<Func<MediaItemViewModel[]>>) (() => this.PromoImages), (Expression<Func<bool>>) (() => this.IsImageIndexerVisible), propertyName: nameof (Images));
      }
    }

    public bool IsImageIndexerVisible => this.Images != null && this.Images.Length > 1;

    public MediaItemViewModel[] PromoImages
    {
      get
      {
        return this.Images == null ? (MediaItemViewModel[]) null : ((IEnumerable<MediaItemViewModel>) this.Images).Take<MediaItemViewModel>(5).ToArray<MediaItemViewModel>();
      }
    }

    public MediaItemViewModel SelectedImage
    {
      get => this._selectedImage;
      set
      {
        if (this._selectedImage == value)
          return;
        if (this.Images != null)
        {
          foreach (var x in this.Images)
          {
            x.IsSelected = false;
          }
        }
        this._selectedImage = value;
        if (this._selectedImage != null)
          this._selectedImage.IsSelected = true;
        this.NotifyOfPropertyChange<MediaItemViewModel>((Expression<Func<MediaItemViewModel>>) (() => this.SelectedImage));
      }
    }

    public string Title
    {
      get => this._title;
      set => this.SetProperty<string>(ref this._title, value, propertyName: nameof (Title));
    }

    public string Description
    {
      get => this._description;
      set
      {
        this.SetProperty<string>(ref this._description, value, propertyName: nameof (Description));
      }
    }

    public AudioContentViewModel AudioViewModel
    {
      get => this._audioViewModel ?? (this._audioViewModel = new AudioContentViewModel());
    }

    public bool IsPurchased => PurchaseManager.Instance.IsPurchased(this.MtgObjectRoot);

    public string PurchaseLabel
    {
      get
      {
        return this.MtgObject == null || this.MtgObject.Purchase == null ? (string) null : string.Format(AppResources.CommandBuyFor, (object) this.MtgObject.Purchase.PriceString);
      }
    }

    public string AudioLabel
    {
      get => this.Type != MtgObjectType.Museum ? AppResources.LabelAudio : AppResources.LabelIntro;
    }

    public MediaInfo VideoMedia
    {
      get => this._videoMedia;
      set
      {
        this.SetProperty<MediaInfo>(ref this._videoMedia, value, propertyName: nameof (VideoMedia));
      }
    }

    public WorkingHoursInfo[] WorkingHours
    {
      get => this._workingHours;
      set
      {
        this.SetProperty<WorkingHoursInfo[]>(ref this._workingHours, value, propertyName: nameof (WorkingHours));
      }
    }

    public string Address
    {
      get => this._address;
      set => this.SetProperty<string>(ref this._address, value, propertyName: nameof (Address));
    }

    public string[] Sites
    {
      get => this._sites;
      set => this.SetProperty<string[]>(ref this._sites, value, propertyName: nameof (Sites));
    }

    public string Phone
    {
      get => this._phone;
      set => this.SetProperty<string>(ref this._phone, value, propertyName: nameof (Phone));
    }

    public bool HasContacts => this.Sites != null && this.Sites.Length != 0 || this.Phone != null;

    public string ContentProviderName
    {
      get => this._contentProviderName;
      set
      {
        this.SetProperty<string>(ref this._contentProviderName, value, propertyName: nameof (ContentProviderName));
      }
    }

    public bool HasPublisherName => this.PublisherName != null;

    public string PublisherName
    {
      get => this._publisherName;
      set
      {
        this.SetProperty<string, bool>(ref this._publisherName, value, (Expression<Func<bool>>) (() => this.HasPublisherName), propertyName: nameof (PublisherName));
      }
    }

    public string PublisherImage
    {
      get => this._publisherImageUrl;
      set
      {
        this.SetProperty<string>(ref this._publisherImageUrl, value, propertyName: nameof (PublisherImage));
      }
    }

    public MtgObjectType Type
    {
      get => this.MtgObject == null ? MtgObjectType.Unknown : this.MtgObject.Type;
    }

    public DetailInfoViewModel.QuizInfoViewModel QuizViewModel
    {
      get => this._quizViewModel;
      set
      {
        this.SetProperty<DetailInfoViewModel.QuizInfoViewModel>(ref this._quizViewModel, value, (System.Action) (() => this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.HasQuiz))), nameof (QuizViewModel));
      }
    }

    public bool HasQuiz => this.QuizViewModel != null;

    public bool HasPlan
    {
      get
      {
        return this.MtgObject != null && this.MtgObject.MainContent != null && this.MtgObject.MainContent.Images != null && ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) this.MtgObject.MainContent.Images).Any<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.Map));
      }
    }

    protected DetailInfoViewModel() => this.Logger = LogManager.GetLog(this.GetType());

    public RelayCommand OpenPublisherCommand
    {
      get
      {
        return this._openPublisherCommand ?? (this._openPublisherCommand = new RelayCommand(new Action<object>(this.ExecuteOpenPublisherCommand), new Func<object, bool>(this.CanExecuteOpenPublisherCommand)));
      }
    }

    private bool CanExecuteOpenPublisherCommand(object parameter)
    {
      return this.MtgObject != null && this.MtgObject.Publisher != null;
    }

    private void ExecuteOpenPublisherCommand(object parameter)
    {
      ShellServiceFacade.NavigationService.UriFor<PublisherDetailPartViewModel>().WithParam<string>((Expression<Func<PublisherDetailPartViewModel, string>>) (x => x.Uid), this.MtgObject.Publisher.Uid).WithParam<string>((Expression<Func<PublisherDetailPartViewModel, string>>) (x => x.Language), this.MtgObject.Publisher.Language).Navigate();
    }

    public RelayCommand PurchaseCommand
    {
      get
      {
        return this._purchaseCommand ?? (this._purchaseCommand = new RelayCommand(new Action<object>(this.Purchase)));
      }
    }

    private void Purchase(object parameter)
    {
      PurchaseFlyoutDialog.ConditionalShow(this.MtgObjectParent ?? this.MtgObject);
    }

    public RelayCommand OpenImageMediaPlayerCommand
    {
      get
      {
        return this._openImageMediaPlayerCommand ?? (this._openImageMediaPlayerCommand = new RelayCommand(new Action<object>(this.ExecuteOpenImageMediaPlayerCommand)));
      }
    }

    protected virtual void ExecuteOpenImageMediaPlayerCommand(object parameter)
    {
      if (this.Images == null || this.Images.Length == 0 || string.IsNullOrWhiteSpace(this.Images[0].ImageUrl))
        return;
      MediaPlayerDataProvider.Instance.MediaData = this.Images != null ? ((IEnumerable<MediaItemViewModel>) this.Images).Select<MediaItemViewModel, MediaInfo>((Func<MediaItemViewModel, MediaInfo>) (x => x.MediaInfo)).ToArray<MediaInfo>() : (MediaInfo[]) null;
      MediaPlayerDataProvider.Instance.MediaDataUid = this.SelectedImage != null ? this.SelectedImage.Uid : (string) null;
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Image).Navigate();
    }

    public OpenVideoCommand OpenVideoMediaPlayerCommand
    {
      get
      {
        return this._openVideoMediaPlayerCommand ?? (this._openVideoMediaPlayerCommand = new OpenVideoCommand(this.MtgObject, this.MtgObjectRoot));
      }
    }

    public RelayCommand OpenSiteCommand
    {
      get
      {
        return this._openSiteCommand ?? (this._openSiteCommand = new RelayCommand(new Action<object>(this.ExecuteOpenSiteCommand)));
      }
    }

    private void ExecuteOpenSiteCommand(object parameter)
    {
      try
      {
        string uriString = parameter as string;
        if (string.IsNullOrWhiteSpace(uriString))
          return;
        if (!uriString.StartsWith("http://", StringComparison.OrdinalIgnoreCase))
          uriString = "http://" + uriString;
        var _ = Launcher.LaunchUriAsync(new Uri(uriString, UriKind.RelativeOrAbsolute));
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        ShellServiceFacade.DialogService.Show(AppResources.ErrorInvalidExternalLinkTitle, AppResources.ErrorInvalidExternalLinkInfo, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) null);
      }
    }

    public RelayCommand CallPhoneCommand
    {
      get
      {
        return this._callPhoneCommand ?? (this._callPhoneCommand = new RelayCommand(new Action<object>(this.ExecuteCallPhoneCommand)));
      }
    }

    private void ExecuteCallPhoneCommand(object parameter)
    {
      string str = parameter as string;
      if (string.IsNullOrWhiteSpace(str))
        return;
      var _ = Launcher.LaunchUriAsync(new Uri("tel:" + str));
    }

    protected override async void OnActivate()
    {
      PurchaseManager.Instance.IsPurchasedChanged += PurchaseManagerIsPurchasedChanged;
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPurchased));
      await System.Threading.Tasks.Task.CompletedTask;
      base.OnActivate();
    }

    protected override void OnDeactivate(bool close)
    {
      PurchaseManager.Instance.IsPurchasedChanged -= PurchaseManagerIsPurchasedChanged;
      this.AudioViewModel.Deactivate();
      base.OnDeactivate(close);
    }

    private void PurchaseManagerIsPurchasedChanged(string sender, bool args)
    {
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsPurchased));
    }

    protected override string[] GetAppBarButtonKeys()
    {
      return new string[3]
      {
        "NowPlaying",
        "Bookmark",
        "Language"
      };
    }

    protected override string[] GetAppBarMenuItemKeys()
    {
      List<string> list = new List<string>();
      list.Add<string>("QrCode", this.MtgObject.IsMuseumOrCollection());
      list.Add<string>("ShowPlan", this.HasPlan);
      list.Add("Share");
      list.Add("GetDirections");
      return list.ToArray();
    }

    private void OpenVideoMediaPlayer()
    {
      TourPlaybackManager.Instance.Pause();
      ServiceFacade.AudioService.Stop();
      MediaPlayerDataProvider.Instance.MediaData = new MediaInfo[1]
      {
        this.VideoMedia
      };
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Video).Navigate();
    }

    public class QuizInfoViewModel : PropertyChangedBase
    {
      private bool _completed;
      private bool _correct;

      public bool Completed
      {
        get => this._completed;
        set
        {
          this.SetProperty<bool, string, string>(ref this._completed, value, (Expression<Func<string>>) (() => this.Title), (Expression<Func<string>>) (() => this.Subtitle), propertyName: nameof (Completed));
        }
      }

      public bool Correct
      {
        get => this._correct;
        set
        {
          this.SetProperty<bool, string, string>(ref this._correct, value, (Expression<Func<string>>) (() => this.Title), (Expression<Func<string>>) (() => this.Subtitle), propertyName: nameof (Correct));
        }
      }

      public string Title
      {
        get
        {
          return !this.Completed ? AppResources.MessageQuizDetailsTitleIncompleted : AppResources.MessageQuizDetailsTitleCompleted;
        }
      }

      public string Subtitle
      {
        get
        {
          if (!this.Completed)
            return AppResources.MessageQuizDetailsSubtitleIncompleted;
          return !this.Correct ? AppResources.MessageQuizDetailsSubtitleIncorrect : AppResources.MessageQuizDetailsSubtitleCorrect;
        }
      }
    }
  }
}

