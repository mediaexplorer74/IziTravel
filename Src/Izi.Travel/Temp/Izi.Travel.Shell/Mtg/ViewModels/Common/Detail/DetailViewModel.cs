// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.DetailViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.Helpers;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Media.Provider;
using Izi.Travel.Shell.Media.ViewModels;
using Izi.Travel.Shell.Mtg.Messages;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Flyouts;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.Interfaces;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;
using System.Windows.Media.Imaging;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public abstract class DetailViewModel : 
    Conductor<IScreen>.Collection.OneActive,
    IDetailViewModel,
    IMtgObjectViewModel,
    IMtgObjectProvider,
    IHandle<DataLoadingMessage>,
    IHandle,
    IHandle<DataLoadedMessage>
  {
    private readonly ILog _logger;
    private static readonly IEventAggregator EventAggregator = IoC.Get<IEventAggregator>();
    private readonly FlyoutLanguageViewModel _flyoutLanguageViewModel;
    private bool _hasBookmark;
    private GeoCoordinate _location = GeoCoordinate.Unknown;
    private IEnumerable<ButtonInfo> _availableAppBarButtons;
    private IEnumerable<MenuItemInfo> _availableAppBarMenuItems;
    private BitmapImage _mapImage;
    private RelayCommand _getDirectionsCommand;
    private RelayCommand _openMapCommand;
    private BaseCommand _nowPlayingCommand;
    private RelayCommand _toggleBookmarkCommand;
    private RelayCommand _selectLanguageCommand;
    private BaseCommand _shareCommand;

    protected ILog Logger => this._logger;

    public IDetailPartViewModel DetailPartViewModel => this.Parent as IDetailPartViewModel;

    public DetailTabViewModel ActiveTabViewModel => this.ActiveItem as DetailTabViewModel;

    public FlyoutLanguageViewModel FlyoutLanguageViewModel => this._flyoutLanguageViewModel;

    public MtgObject MtgObject { get; private set; }

    public MtgObject MtgObjectParent { get; private set; }

    public MtgObject MtgObjectRoot { get; private set; }

    public Content MtgObjectContent
    {
      get => this.MtgObject == null ? (Content) null : this.MtgObject.MainContent;
    }

    public bool HasBookmark
    {
      get => this._hasBookmark;
      set
      {
        this.SetProperty<bool>(ref this._hasBookmark, value, propertyName: nameof (HasBookmark));
      }
    }

    public GeoCoordinate Location
    {
      get => this._location;
      set
      {
        this.SetProperty<GeoCoordinate, bool>(ref this._location, value, (Expression<Func<bool>>) (() => this.HasLocation), propertyName: nameof (Location));
      }
    }

    public bool HasLocation
    {
      get => this.Location != (GeoCoordinate) null && this.Location != GeoCoordinate.Unknown;
    }

    public string MapImageUrl
    {
      get
      {
        return this.MtgObject == null ? (string) null : MtgObjectHelper.GetMapImageUrl(this.MtgObject.Type, false);
      }
    }

    public IEnumerable<ButtonInfo> AvailableAppBarButtons => this._availableAppBarButtons;

    public IEnumerable<MenuItemInfo> AvailableAppBarMenuItems => this._availableAppBarMenuItems;

    public BitmapImage MapImage
    {
      get => this._mapImage;
      set
      {
        this.SetProperty<BitmapImage>(ref this._mapImage, value, propertyName: nameof (MapImage));
      }
    }

    protected DetailViewModel()
    {
      this._logger = LogManager.GetLog(this.GetType());
      this._flyoutLanguageViewModel = new FlyoutLanguageViewModel(this);
    }

    public RelayCommand GetDirectionsCommand
    {
      get
      {
        return this._getDirectionsCommand ?? (this._getDirectionsCommand = new RelayCommand(new Action<object>(this.GetDirections)));
      }
    }

    private void GetDirections(object o)
    {
      if (this.DetailPartViewModel == null || this.DetailPartViewModel.MtgObject == null)
        return;
      MtgObject mtgObject = this.DetailPartViewModel.MtgObject.IsCollectionOrExhibit() ? this.DetailPartViewModel.MtgObjectParent : this.DetailPartViewModel.MtgObject;
      if (mtgObject == null)
        return;
      mtgObject.ShowMapDirectionsTask();
    }

    public RelayCommand OpenMapCommand
    {
      get
      {
        return this._openMapCommand ?? (this._openMapCommand = new RelayCommand(new Action<object>(this.ExecuteOpenMapCommand)));
      }
    }

    protected virtual void ExecuteOpenMapCommand(object parameter)
    {
    }

    public BaseCommand NowPlayingCommand
    {
      get
      {
        return this._nowPlayingCommand ?? (this._nowPlayingCommand = (BaseCommand) new Izi.Travel.Shell.Mtg.Commands.NowPlayingCommand((IScreen) this));
      }
    }

    public RelayCommand ToggleBookmarkCommand
    {
      get
      {
        return this._toggleBookmarkCommand ?? (this._toggleBookmarkCommand = new RelayCommand(new Action<object>(this.ExecuteToggleBookmarkCommand), new Func<object, bool>(this.CanExecuteToggleBookmarkCommand)));
      }
    }

    protected virtual bool CanExecuteToggleBookmarkCommand(object parameter)
    {
      return this.DetailPartViewModel != null && !this.DetailPartViewModel.IsDataLoading && this.MtgObject != null;
    }

    protected virtual async void ExecuteToggleBookmarkCommand(object parameter)
    {
      if (!this.HasBookmark)
      {
        await ServiceFacade.MtgObjectService.CreateBookmarkAsync(this.MtgObject, this.DetailPartViewModel.ParentUid);
        ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkAdded, (Uri) null, (System.Action) null, false);
      }
      else if (this.MtgObjectContent != null)
      {
        await ServiceFacade.MtgObjectService.RemoveBookmarkAsync(new MtgObjectFilter(this.MtgObject.Uid, new string[1]
        {
          this.MtgObjectContent.Language
        }));
        ShellServiceFacade.DialogService.ShowToast(AppResources.ToastBookmarkRemoved, (Uri) null, (System.Action) null, false);
      }
      this.RefreshBookmarkStatusAsync();
    }

    public RelayCommand SelectLanguageCommand
    {
      get
      {
        return this._selectLanguageCommand ?? (this._selectLanguageCommand = new RelayCommand(new Action<object>(this.ExecuteSelectLanguageCommand), new Func<object, bool>(this.CanExecuteSelectLanguageCommand)));
      }
    }

    protected virtual bool CanExecuteSelectLanguageCommand(object parameter)
    {
      return this.DetailPartViewModel != null && !this.DetailPartViewModel.IsDataLoading && this.MtgObjectContent != null;
    }

    protected virtual void ExecuteSelectLanguageCommand(object parameter)
    {
      this.FlyoutLanguageViewModel.Initialize(this.MtgObject);
      this.FlyoutLanguageViewModel.IsOpen = true;
    }

    public BaseCommand ShareCommand
    {
      get
      {
        return this._shareCommand ?? (this._shareCommand = (BaseCommand) new Izi.Travel.Shell.Mtg.Commands.ShareCommand(this.MtgObject, this.MtgObjectParent));
      }
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      DetailViewModel.EventAggregator.Subscribe((object) this);
    }

    protected override void OnActivate()
    {
      if (this.DetailPartViewModel == null)
        return;
      this.MtgObject = this.DetailPartViewModel.MtgObject;
      this.MtgObjectParent = this.DetailPartViewModel.MtgObjectParent;
      this.MtgObjectRoot = this.DetailPartViewModel.MtgObjectRoot;
      if (this.MtgObject == null)
        return;
      if (this._availableAppBarButtons == null)
        this._availableAppBarButtons = this.GetAvailableAppBarButtons();
      if (this._availableAppBarMenuItems == null)
        this._availableAppBarMenuItems = this.GetAvailableAppBarMenuItems();
      this.DisplayName = this.MtgObjectContent != null ? this.MtgObjectContent.Title : string.Empty;
      if (this.MtgObject.Location != null)
      {
        this.Location = this.MtgObject.Location.ToGeoCoordinate();
        this.MapImage = ImageHelper.ImageFromUri(new Uri(ServiceFacade.MediaService.GetBingMapImageUrl(this.Location, 12.0, 480, 125)));
      }
      this.RefreshBookmarkStatusAsync();
      if (this.MtgObjectContent != null && (this.MtgObjectContent.References == null || this.MtgObjectContent.References.Length == 0))
      {
        IScreen screen = this.Items.FirstOrDefault<IScreen>((Func<IScreen, bool>) (x => x is DetailReferenceListViewModel));
        if (screen != null)
          this.Items.Remove(screen);
      }
      if (this.MtgObject.Sponsors == null || this.MtgObject.Sponsors.Length == 0)
      {
        IScreen screen = this.Items.FirstOrDefault<IScreen>((Func<IScreen, bool>) (x => x is DetailSponsorListViewModel));
        if (screen != null)
          this.Items.Remove(screen);
      }
      base.OnActivate();
    }

    protected virtual void RefreshCommands()
    {
      this.NowPlayingCommand.RaiseCanExecuteChanged();
      this.ShareCommand.RaiseCanExecuteChanged();
      this.ToggleBookmarkCommand.RaiseCanExecuteChanged();
      this.SelectLanguageCommand.RaiseCanExecuteChanged();
      this.OpenMapCommand.RaiseCanExecuteChanged();
    }

    protected virtual IEnumerable<ButtonInfo> GetAvailableAppBarButtons()
    {
      return (IEnumerable<ButtonInfo>) new ButtonInfo[4]
      {
        new ButtonInfo()
        {
          Order = 5,
          Key = "NowPlaying",
          Text = AppResources.CommandNavigateNowPlaying,
          ImageUrl = "/Assets/Icons/appbar.nowplaying.png",
          Command = (ICommand) this.NowPlayingCommand
        },
        new ButtonInfo()
        {
          Order = 10,
          Key = "Bookmark",
          Text = AppResources.CommandBookmark,
          AlternativeText = AppResources.CommandBookmark,
          ImageUrl = "/Assets/Icons/appbar.favorite.png",
          AlternativeImageUrl = "/Assets/Icons/appbar.unfavorite.png",
          Command = (ICommand) this.ToggleBookmarkCommand
        },
        new ButtonInfo()
        {
          Order = 15,
          Key = "Language",
          Text = AppResources.LabelLanguage,
          ImageUrl = "/Assets/Icons/appbar.language.png",
          Command = (ICommand) this.SelectLanguageCommand
        },
        new ButtonInfo()
        {
          Order = 50,
          Key = "GetDirections",
          Text = AppResources.LabelDirections,
          ImageUrl = "/Assets/Icons/appbar.getdirections.png",
          Command = (ICommand) this.GetDirectionsCommand
        }
      };
    }

    protected virtual IEnumerable<MenuItemInfo> GetAvailableAppBarMenuItems()
    {
      List<MenuItemInfo> availableAppBarMenuItems = new List<MenuItemInfo>()
      {
        new MenuItemInfo()
        {
          Order = 1,
          Key = "GetDirections",
          Text = AppResources.CommandGetDirections,
          Command = (ICommand) this.GetDirectionsCommand
        },
        new MenuItemInfo()
        {
          Order = 10,
          Key = "ShowPlan",
          Text = AppResources.LabelPlan,
          Command = (ICommand) new RelayCommand(new Action<object>(this.ExecuteShowPlanCommand))
        }
      };
      if (this.MtgObject != null && this.MtgObject.Status != MtgObjectStatus.Limited)
        availableAppBarMenuItems.Add(new MenuItemInfo()
        {
          Order = 15,
          Key = "Share",
          Text = AppResources.CommandShare,
          Command = (ICommand) this.ShareCommand
        });
      return (IEnumerable<MenuItemInfo>) availableAppBarMenuItems;
    }

    private void ExecuteShowPlanCommand(object o)
    {
      if (this.MtgObject == null || this.MtgObject.MainContent == null || this.MtgObject.MainContent.Images == null)
        return;
      MediaPlayerDataProvider.Instance.MediaData = ((IEnumerable<Izi.Travel.Business.Entities.Data.Media>) this.MtgObject.MainContent.Images).Where<Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Business.Entities.Data.Media, bool>) (x => x.Type == MediaType.Map)).Select<Izi.Travel.Business.Entities.Data.Media, MediaInfo>((Func<Izi.Travel.Business.Entities.Data.Media, MediaInfo>) (x => new MediaInfo()
      {
        MediaFormat = MediaFormat.Image,
        MediaUid = x.Uid,
        ContentProviderUid = this.MtgObject.ContentProvider.Uid,
        Title = x.Title,
        ImageUrl = ServiceFacade.MediaService.GetImageUrl(x.Uid, this.MtgObject.ContentProvider.Uid, ImageFormat.Undefined)
      })).ToArray<MediaInfo>();
      ShellServiceFacade.NavigationService.UriFor<MediaPlayerPartViewModel>().WithParam<MediaFormat>((Expression<Func<MediaPlayerPartViewModel, MediaFormat>>) (x => x.MediaFormat), MediaFormat.Image).Navigate();
    }

    public void Handle(DataLoadingMessage message) => this.RefreshCommands();

    public void Handle(DataLoadedMessage message) => this.RefreshCommands();

    private async void RefreshBookmarkStatusAsync()
    {
      if (this.MtgObjectContent == null)
        return;
      this.HasBookmark = await ServiceFacade.MtgObjectService.IsBookmarkExistsForMtgObjectAsync(new MtgObjectFilter(this.MtgObject.Uid, new string[1]
      {
        this.MtgObjectContent.Language
      }));
      if (this.AvailableAppBarButtons == null)
        return;
      ButtonInfo buttonInfo = this.AvailableAppBarButtons.FirstOrDefault<ButtonInfo>((Func<ButtonInfo, bool>) (x => x.Key == "Bookmark"));
      if (buttonInfo == null)
        return;
      buttonInfo.ShowAlternative = this.HasBookmark;
    }
  }
}
