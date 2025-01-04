// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.QuickAccess.QuickAccessViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.Components.Tasks;
using Izi.Travel.Shell.Mtg.Model;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Numpad;
using Izi.Travel.Shell.ViewModels.QuickAccess.Items;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.QuickAccess
{
  public class QuickAccessViewModel : 
    Screen,
    IMainTabViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged,
    IFlyoutSearchResultHandler
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (QuickAccessViewModel));
    private bool _isDataLoading;
    private CancellationTokenSource _tokenSource;
    private Task<QuickAccessBaseItemViewModel> _task;
    private QuickAccessBaseItemViewModel _item;
    private QuickAccessInfoItemViewModel _itemInfo;
    private QuickAccessPlayerItemViewModel _playItem;
    private RelayCommand _qrCodeScannerCommand;
    private RelayCommand _showNumpadCommand;
    private RelayCommand _forwardCommand;
    private RelayCommand _backwardCommand;
    private RelayCommand _nowPlayingCommand;

    public string ImageUrl => "/Assets/Icons/tab.nowplaying.png";

    public string SelectedImageUrl => "/Assets/Icons/tab.nowplaying.selected.png";

    public bool IsBusy => false;

    public string BusyTitle => (string) null;

    public ScreenProperties Properties { get; set; }

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      set
      {
        this.SetProperty<bool>(ref this._isDataLoading, value, propertyName: nameof (IsDataLoading));
      }
    }

    public QuickAccessBaseItemViewModel Item
    {
      get => this._item;
      set
      {
        if (this._item != null)
          this._item.Deactivate();
        if (this._item != value)
        {
          this._item = value;
          this.NotifyOfPropertyChange<QuickAccessBaseItemViewModel>((Expression<Func<QuickAccessBaseItemViewModel>>) (() => this.Item));
        }
        if (this._item == null)
          return;
        this._item.Activate();
      }
    }

    public QuickAccessInfoItemViewModel ItemInfo
    {
      get => this._itemInfo ?? (this._itemInfo = new QuickAccessInfoItemViewModel(this));
    }

    public QuickAccessViewModel()
    {
      ScreenProperties screenProperties = new ScreenProperties();
      screenProperties.AppBarButtons = (IEnumerable<ButtonInfo>) new ButtonInfo[3]
      {
        new ButtonInfo()
        {
          Key = "QrCode",
          Text = AppResources.LabelQrScanner,
          ImageUrl = "/Assets/Icons/appbar.qrcode.png",
          Command = (ICommand) this.QrCodeScannerCommand
        },
        new ButtonInfo()
        {
          Key = "NowPlaying",
          Text = AppResources.CommandNavigateNowPlaying,
          ImageUrl = "/Assets/Icons/appbar.nowplaying.png",
          Command = (ICommand) this.NowPlayingCommand
        },
        new ButtonInfo()
        {
          Key = "Numpad",
          Text = AppResources.LabelNumpad,
          ImageUrl = "/Assets/Icons/appbar.numpad.png",
          Command = (ICommand) this.ShowNumpadCommand
        }
      };
      this.Properties = screenProperties;
    }

    public RelayCommand QrCodeScannerCommand
    {
      get
      {
        return this._qrCodeScannerCommand ?? (this._qrCodeScannerCommand = new RelayCommand(new Action<object>(this.ExecuteQrCodeScannerCommand)));
      }
    }

    private void ExecuteQrCodeScannerCommand(object parameter)
    {
      BarcodeScannerTask barcodeScannerTask = new BarcodeScannerTask();
      barcodeScannerTask.ParentScreen = (IScreen) this;
      barcodeScannerTask.ActivationMode = FlyoutSearchActivationMode.None;
      barcodeScannerTask.NavigationMode = FlyoutSearchNavigationMode.None;
      barcodeScannerTask.CloseMode = FlyoutSearchCloseMode.Handler;
      barcodeScannerTask.Show();
    }

    public RelayCommand ShowNumpadCommand
    {
      get
      {
        return this._showNumpadCommand ?? (this._showNumpadCommand = new RelayCommand(new Action<object>(this.ExecuteShowNumpadCommand), new Func<object, bool>(this.CanExecutedShowNumpadCommand)));
      }
    }

    private bool CanExecutedShowNumpadCommand(object parameter)
    {
      if (this.IsDataLoading || !(this.Item is QuickAccessPlayerItemViewModel playerItemViewModel) || playerItemViewModel.MtgObjectParent == null)
        return false;
      return playerItemViewModel.MtgObjectParent.Type == MtgObjectType.Museum || playerItemViewModel.MtgObjectParent.Type == MtgObjectType.Collection;
    }

    private void ExecuteShowNumpadCommand(object parameter)
    {
      if (!(this.Item is QuickAccessPlayerItemViewModel playerItemViewModel) || playerItemViewModel.MtgObjectParent == null)
        return;
      NumpadSearchTask numpadSearchTask = new NumpadSearchTask();
      numpadSearchTask.ParentScreen = (IScreen) this;
      numpadSearchTask.ParentUid = playerItemViewModel.MtgObjectParent.Uid;
      numpadSearchTask.ParentType = playerItemViewModel.MtgObjectParent.Type;
      numpadSearchTask.ParentLanguage = playerItemViewModel.Language;
      numpadSearchTask.ActivationMode = FlyoutSearchActivationMode.None;
      numpadSearchTask.NavigationMode = FlyoutSearchNavigationMode.None;
      numpadSearchTask.CloseMode = FlyoutSearchCloseMode.Handler;
      numpadSearchTask.Show();
    }

    public RelayCommand ForwardCommand
    {
      get
      {
        return this._forwardCommand ?? (this._forwardCommand = new RelayCommand(new Action<object>(this.ExecuteForwardCommand), new Func<object, bool>(this.CanExecuteForwardCommand)));
      }
    }

    private bool CanExecuteForwardCommand(object parameter)
    {
      return !this.IsDataLoading && this.Item is QuickAccessPlayerItemViewModel playerItemViewModel && playerItemViewModel.HasNext;
    }

    private void ExecuteForwardCommand(object parameter)
    {
      this.LoadDataAsync(QuickAccessViewModel.LoadDataQuery.FromPlayerItem(this.Item as QuickAccessPlayerItemViewModel, new bool?(true), false));
    }

    public RelayCommand BackwardCommand
    {
      get
      {
        return this._backwardCommand ?? (this._backwardCommand = new RelayCommand(new Action<object>(this.ExecuteBackwardCommand), new Func<object, bool>(this.CanExecuteBackwardCommand)));
      }
    }

    private bool CanExecuteBackwardCommand(object parameter)
    {
      return !this.IsDataLoading && this.Item is QuickAccessPlayerItemViewModel playerItemViewModel && playerItemViewModel.HasPrevious;
    }

    private void ExecuteBackwardCommand(object parameter)
    {
      this.LoadDataAsync(QuickAccessViewModel.LoadDataQuery.FromPlayerItem(this.Item as QuickAccessPlayerItemViewModel, new bool?(false), false));
    }

    public RelayCommand NowPlayingCommand
    {
      get
      {
        return this._nowPlayingCommand ?? (this._nowPlayingCommand = new RelayCommand(new Action<object>(this.ExecuteNowPlayingCommand), new Func<object, bool>(this.CanExecuteNowPlayingCommand)));
      }
    }

    private bool CanExecuteNowPlayingCommand(object parameter)
    {
      if (this.IsDataLoading)
        return false;
      AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
      return currentTrackInfo != null && (!(this.Item is QuickAccessPlayerItemViewModel playerItemViewModel) || !(playerItemViewModel.Key == currentTrackInfo.Key));
    }

    private void ExecuteNowPlayingCommand(object parameter)
    {
      if (this.Item is QuickAccessPlayerItemViewModel playerItemViewModel && ServiceFacade.AudioService.IsNowPlaying(playerItemViewModel.Uid, playerItemViewModel.Language))
        return;
      this.LoadDataAsync(QuickAccessViewModel.LoadDataQuery.FromTrackInfoCurrent(new bool?(), false));
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.LoadDataAsync(QuickAccessViewModel.LoadDataQuery.FromTrackInfoCurrent(new bool?(), false));
      // ISSUE: method pointer
      ServiceFacade.AudioService.StateChanged += new TypedEventHandler<IAudioService, AudioServiceState>((object) this, __methodptr(OnAudioStateChanged));
    }

    protected override void OnDeactivate(bool close)
    {
      // ISSUE: method pointer
      ServiceFacade.AudioService.StateChanged -= new TypedEventHandler<IAudioService, AudioServiceState>((object) this, __methodptr(OnAudioStateChanged));
      this.Item = (QuickAccessBaseItemViewModel) null;
      base.OnDeactivate(close);
    }

    private async void LoadDataAsync(QuickAccessViewModel.LoadDataQuery query)
    {
      this.IsDataLoading = true;
      this.RefreshCommands();
      try
      {
        if (this._tokenSource != null && !this._tokenSource.IsCancellationRequested)
          this._tokenSource.Cancel();
        if (this._task != null)
        {
          QuickAccessBaseItemViewModel task = await this._task;
        }
        if (!query.HasData)
          this.Item = (QuickAccessBaseItemViewModel) this.ItemInfo;
        else if (this._playItem != null && !query.Direction.HasValue && query.Key == this._playItem.Key)
        {
          this.Item = (QuickAccessBaseItemViewModel) this._playItem;
        }
        else
        {
          this._tokenSource = new CancellationTokenSource();
          this._task = this.LoadDataTask(query, this._tokenSource.Token);
          QuickAccessBaseItemViewModel item;
          QuickAccessBaseItemViewModel baseItemViewModel = item;
          item = await this._task;
          this._task = (Task<QuickAccessBaseItemViewModel>) null;
          this._tokenSource = (CancellationTokenSource) null;
          if (item != null)
            ((System.Action) (() => this.Item = item)).OnUIThread();
          else
            this.Item = (QuickAccessBaseItemViewModel) this.ItemInfo;
        }
      }
      catch (Exception ex)
      {
        this.Item = (QuickAccessBaseItemViewModel) this.ItemInfo;
        QuickAccessViewModel.Logger.Error(ex);
      }
      finally
      {
        this.IsDataLoading = false;
        this.RefreshCommands();
        QuickAccessPlayerItemViewModel playerItemViewModel = this.Item as QuickAccessPlayerItemViewModel;
        if (query.AutoPlay && playerItemViewModel != null && playerItemViewModel.AudioViewModel.HasAudio && playerItemViewModel.AudioViewModel.PlayCommand.CanExecute((object) null))
        {
          this._playItem = playerItemViewModel;
          playerItemViewModel.AudioViewModel.PlayCommand.Execute((object) null);
        }
      }
    }

    private async Task<QuickAccessBaseItemViewModel> LoadDataTask(
      QuickAccessViewModel.LoadDataQuery query,
      CancellationToken token)
    {
      if (token.IsCancellationRequested)
        return (QuickAccessBaseItemViewModel) null;
      QuickAccessPlayerItemViewModel currentItem = this.Item as QuickAccessPlayerItemViewModel;
      bool? direction = query.Direction;
      if (!direction.HasValue && currentItem != null && currentItem.Key == query.Key)
        return (QuickAccessBaseItemViewModel) null;
      MtgObject mtgObject = (MtgObject) null;
      direction = query.Direction;
      MtgObject mtgObjectParent = !direction.HasValue || currentItem == null ? query.MtgObjectParent : currentItem.MtgObjectParent;
      int index = -1;
      bool hasNext = false;
      bool hasPrevious = false;
      if (!string.IsNullOrWhiteSpace(query.ParentUid) && (mtgObjectParent == null || mtgObjectParent.Uid != query.ParentUid || mtgObjectParent.Language != query.Language))
      {
        MtgObjectFilter filter = new MtgObjectFilter(query.ParentUid, query.Language);
        filter.Includes = ContentSection.None;
        filter.Excludes = ContentSection.All;
        filter.Form = MtgObjectForm.Compact;
        mtgObjectParent = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
      }
      List<MtgObjectType> mtgObjectTypeList = new List<MtgObjectType>()
      {
        MtgObjectType.Exhibit,
        MtgObjectType.TouristAttraction
      };
      if (mtgObjectParent != null && mtgObjectParent.IsMuseumOrCollection())
        mtgObjectTypeList.Add(MtgObjectType.StoryNavigation);
      if (mtgObjectParent != null && mtgObjectTypeList.Contains(query.Type))
      {
        if (token.IsCancellationRequested)
          return (QuickAccessBaseItemViewModel) null;
        MtgObjectChildrenExtendedFilter childrenExtendedFilter1 = new MtgObjectChildrenExtendedFilter();
        childrenExtendedFilter1.Limit = new int?(1);
        childrenExtendedFilter1.Uid = query.ParentUid;
        childrenExtendedFilter1.Languages = new string[1]
        {
          query.Language
        };
        childrenExtendedFilter1.ShowHidden = false;
        childrenExtendedFilter1.Types = mtgObjectTypeList.ToArray();
        childrenExtendedFilter1.Form = MtgObjectForm.Full;
        childrenExtendedFilter1.Includes = ContentSection.None;
        childrenExtendedFilter1.Excludes = ContentSection.All;
        MtgObjectChildrenExtendedFilter filter = childrenExtendedFilter1;
        if (mtgObjectParent.Type == MtgObjectType.Museum)
          filter.SortExhibits = "number";
        direction = query.Direction;
        if (direction.HasValue && currentItem != null && currentItem.Index != -1)
        {
          MtgObjectChildrenExtendedFilter childrenExtendedFilter2 = filter;
          direction = query.Direction;
          int? nullable = new int?(direction.Value ? currentItem.Index + 1 : currentItem.Index - 1);
          childrenExtendedFilter2.Offset = nullable;
        }
        else
          filter.PageUid = query.Uid;
        MtgChildrenListResult childrenExtendedAsync = await MtgObjectServiceHelper.GetMtgObjectChildrenExtendedAsync(filter);
        if (childrenExtendedAsync != null && childrenExtendedAsync.Metadata != null && childrenExtendedAsync.Data != null && childrenExtendedAsync.Data.Length != 0)
        {
          mtgObject = childrenExtendedAsync.Data[0];
          index = childrenExtendedAsync.Metadata.Offset;
          hasNext = childrenExtendedAsync.Metadata.PageRight;
          hasPrevious = childrenExtendedAsync.Metadata.PageLeft;
        }
      }
      else
        mtgObject = await MtgObjectServiceHelper.GetMtgObjectAsync(new MtgObjectFilter(query.Uid, query.Language));
      return mtgObject != null ? (QuickAccessBaseItemViewModel) new QuickAccessPlayerItemViewModel(this, mtgObjectParent, mtgObject, index, hasNext, hasPrevious) : (QuickAccessBaseItemViewModel) null;
    }

    private void RefreshCommands()
    {
      this.ShowNumpadCommand.RaiseCanExecuteChanged();
      this.BackwardCommand.RaiseCanExecuteChanged();
      this.ForwardCommand.RaiseCanExecuteChanged();
      this.NowPlayingCommand.RaiseCanExecuteChanged();
    }

    public void HandleFlyoutSearchResult(
      BaseSearchFlyoutViewModel flyout,
      SearchFlyoutResult result)
    {
      if (result == null || !result.Success || result.MtgObject == null || flyout is NumpadFlyoutViewModel && this.Item is QuickAccessPlayerItemViewModel playerItemViewModel && result.MtgObject.Key == playerItemViewModel.Key)
        return;
      if (result.MtgObject.MainAudioMedia != null)
        this._playItem = (QuickAccessPlayerItemViewModel) null;
      this.LoadDataAsync(QuickAccessViewModel.LoadDataQuery.FromMtgObject(result.MtgObject, result.MtgObjectParent, new bool?(), true));
    }

    private void OnAudioStateChanged(IAudioService sender, AudioServiceState args)
    {
      this.ShowNumpadCommand.RaiseCanExecuteChanged();
      this.NowPlayingCommand.RaiseCanExecuteChanged();
    }

    private class LoadDataQuery
    {
      public bool? Direction { get; private set; }

      public bool AutoPlay { get; private set; }

      private AudioTrackInfo TrackInfo { get; set; }

      private MtgObject MtgObject { get; set; }

      public MtgObject MtgObjectParent { get; private set; }

      public bool HasData => this.TrackInfo != null || this.MtgObject != null;

      public string Uid
      {
        get
        {
          if (this.TrackInfo != null)
            return this.TrackInfo.MtgObjectUid;
          return this.MtgObject == null ? (string) null : this.MtgObject.Uid;
        }
      }

      public string Key
      {
        get
        {
          if (this.TrackInfo != null)
            return this.TrackInfo.Key;
          return this.MtgObject == null ? (string) null : this.MtgObject.Key;
        }
      }

      public string Language
      {
        get
        {
          if (this.TrackInfo != null)
            return this.TrackInfo.Language;
          return this.MtgObject == null ? (string) null : this.MtgObject.Language;
        }
      }

      public MtgObjectType Type
      {
        get
        {
          if (this.TrackInfo != null)
            return this.TrackInfo.MtgObjectType;
          return this.MtgObject == null ? MtgObjectType.Unknown : this.MtgObject.Type;
        }
      }

      public string ParentUid
      {
        get
        {
          if (this.TrackInfo != null)
            return this.TrackInfo.MtgParentUid;
          return this.MtgObjectParent == null ? (string) null : this.MtgObjectParent.Uid;
        }
      }

      private LoadDataQuery()
      {
      }

      public static QuickAccessViewModel.LoadDataQuery FromTrackInfoCurrent(
        bool? direction,
        bool autoPlay)
      {
        AudioTrackInfo currentTrackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
        return new QuickAccessViewModel.LoadDataQuery()
        {
          AutoPlay = autoPlay,
          Direction = direction,
          TrackInfo = currentTrackInfo
        };
      }

      public static QuickAccessViewModel.LoadDataQuery FromMtgObject(
        MtgObject mtgObject,
        MtgObject mtgObjectParent,
        bool? direction,
        bool autoPlay)
      {
        return new QuickAccessViewModel.LoadDataQuery()
        {
          AutoPlay = autoPlay,
          Direction = direction,
          MtgObject = mtgObject,
          MtgObjectParent = mtgObjectParent
        };
      }

      public static QuickAccessViewModel.LoadDataQuery FromPlayerItem(
        QuickAccessPlayerItemViewModel item,
        bool? direction,
        bool autoPlay)
      {
        if (item == null)
          return QuickAccessViewModel.LoadDataQuery.FromTrackInfoCurrent(direction, autoPlay);
        return new QuickAccessViewModel.LoadDataQuery()
        {
          AutoPlay = autoPlay,
          Direction = direction,
          MtgObject = item.MtgObject,
          MtgObjectParent = item.MtgObjectParent
        };
      }
    }
  }
}
