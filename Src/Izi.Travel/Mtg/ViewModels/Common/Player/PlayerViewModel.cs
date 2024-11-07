// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Player.PlayerViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.Components.Tasks;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.Model;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Numpad;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items;
using Izi.Travel.Utility.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Player
{
  public class PlayerViewModel : Screen, IFlyoutSearchResultHandler
  {
    private const int DefaultLimit = 15;
    private static readonly ILog Logger = LogManager.GetLog(typeof (PlayerViewModel));
    private readonly ObservableCollection<PlayerItemViewModel> _items;
    private readonly ObservableCollection<PlayerItemViewModel> _jumpListItems;
    private readonly HashSet<int> _offsets;
    private JumpListFlyoutViewModel _jumpListFlyoutViewModel;
    private PlayerItemViewModel _selectedItem;
    private MtgChildrenListResultMetadata _metadata;
    private bool _isDataLoading;
    private bool _autoPlay;
    private string _uid;
    private string _language;
    private string _parentUid;
    private MtgObject _mtgObjectParent;
    private MtgObject _mtgObjectRoot;
    private bool _isQuizVisible;
    private RelayCommand _jumpListClosedCommand;
    private RelayCommand _jumpCommand;
    private RelayCommand _loadItemCommand;
    private RelayCommand _nowPlayingCommand;
    private RelayCommand _forwardCommand;
    private RelayCommand _backwardCommand;
    private RelayCommand _navigateCommand;
    private RelayCommand _showNumpadCommand;

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      set
      {
        this.SetProperty<bool>(ref this._isDataLoading, value, new System.Action(this.RefreshCommands), nameof (IsDataLoading));
      }
    }

    public ObservableCollection<PlayerItemViewModel> Items => this._items;

    public ObservableCollection<PlayerItemViewModel> JumpListItems => this._jumpListItems;

    public PlayerItemViewModel SelectedItem
    {
      get => this._selectedItem;
      set
      {
        if (this._selectedItem != null)
          this._selectedItem.Deactivate();
        if (this._selectedItem != value)
        {
          this._selectedItem = value;
          this.NotifyOfPropertyChange<PlayerItemViewModel>((Expression<Func<PlayerItemViewModel>>) (() => this.SelectedItem));
          this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsRegularItem));
        }
        if (this._selectedItem != null)
          this._selectedItem.Activate();
        this.RefreshQuizVisibilityAsync();
        this.ForwardCommand.RaiseCanExecuteChanged();
        this.BackwardCommand.RaiseCanExecuteChanged();
        this.NowPlayingCommand.RaiseCanExecuteChanged();
        this.Invalidate();
      }
    }

    public bool IsRegularItem => this.SelectedItem is PlayerRegularItemViewModel;

    public JumpListFlyoutViewModel JumpListFlyoutViewModel
    {
      get
      {
        return this._jumpListFlyoutViewModel ?? (this._jumpListFlyoutViewModel = new JumpListFlyoutViewModel());
      }
    }

    public MtgObject MtgObject => this._mtgObjectParent;

    public bool IsQuizVisible
    {
      get => this._isQuizVisible;
      set
      {
        this.SetProperty<bool>(ref this._isQuizVisible, value, propertyName: nameof (IsQuizVisible));
      }
    }

    public PlayerViewModel()
    {
      this._offsets = new HashSet<int>();
      this._items = new ObservableCollection<PlayerItemViewModel>();
      this._jumpListItems = new ObservableCollection<PlayerItemViewModel>();
    }

    public RelayCommand JumpListClosedCommand
    {
      get
      {
        return this._jumpListClosedCommand ?? (this._jumpListClosedCommand = new RelayCommand((Action<object>) (x => this.Items.ForEach<PlayerItemViewModel>((Action<PlayerItemViewModel, int>) ((y, z) => y.PreviewVisible = false)))));
      }
    }

    public RelayCommand JumpCommand
    {
      get
      {
        return this._jumpCommand ?? (this._jumpCommand = new RelayCommand(new Action<object>(this.ExecutenJumpCommand)));
      }
    }

    private void ExecutenJumpCommand(object parameter)
    {
      this.SelectedItem = (PlayerItemViewModel) parameter;
      this.JumpListFlyoutViewModel.IsOpen = false;
    }

    public RelayCommand LoadItemCommand
    {
      get
      {
        return this._loadItemCommand ?? (this._loadItemCommand = new RelayCommand(new Action<object>(this.ExecuteLoadItemCommand)));
      }
    }

    private void ExecuteLoadItemCommand(object parameter)
    {
      if (!(parameter is PlayerItemViewModel playerItemViewModel))
        return;
      playerItemViewModel.PreviewVisible = true;
      if (!(playerItemViewModel is PlayerRegularItemViewModel))
        return;
      this.LoadPageAsync(playerItemViewModel);
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
      return currentTrackInfo != null && (!(this.SelectedItem is PlayerRegularItemViewModel selectedItem) || selectedItem.MtgObject == null || !(selectedItem.MtgObject.Key == currentTrackInfo.Key));
    }

    private void ExecuteNowPlayingCommand(object parameter)
    {
      AudioTrackInfo trackInfo = ServiceFacade.AudioService.GetCurrentTrackInfo();
      if (trackInfo == null)
        return;
      if (trackInfo.Language == this._language)
      {
        PlayerRegularItemViewModel regularItemViewModel = this.Items.OfType<PlayerRegularItemViewModel>().FirstOrDefault<PlayerRegularItemViewModel>((Func<PlayerRegularItemViewModel, bool>) (x => string.Equals(x.Uid, trackInfo.MtgObjectUid, StringComparison.InvariantCultureIgnoreCase)));
        if (regularItemViewModel != null)
        {
          this.SelectedItem = (PlayerItemViewModel) regularItemViewModel;
          return;
        }
      }
      if (trackInfo.Language == this._language && trackInfo.MtgParentUid == this._parentUid)
        this.LoadInitialDataAsync();
      else
        NavigationHelper.NavigateToAudio(trackInfo.MtgObjectType, trackInfo.MtgObjectUid, trackInfo.Language, trackInfo.MtgParentUid);
    }

    public RelayCommand ForwardCommand
    {
      get
      {
        return this._forwardCommand ?? (this._forwardCommand = new RelayCommand((Action<object>) (x => this.SelectedItem = this.Items[(this.Items.IndexOf(this.SelectedItem) + 1).Clamp(0, this.Items.Count)])));
      }
    }

    public RelayCommand BackwardCommand
    {
      get
      {
        return this._backwardCommand ?? (this._backwardCommand = new RelayCommand((Action<object>) (x => this.SelectedItem = this.Items[(this.Items.IndexOf(this.SelectedItem) - 1).Clamp(0, this.Items.Count)])));
      }
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand), new Func<object, bool>(this.CanExecuteNavigateCommand)));
      }
    }

    private bool CanExecuteNavigateCommand(object parameter) => !this.IsDataLoading;

    private void ExecuteNavigateCommand(object parameter)
    {
      if (this.SelectedItem == null || this.SelectedItem.MtgObject == null)
        return;
      ShellServiceFacade.NavigationService.UriFor<DetailPartViewModel>().WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Uid), this.SelectedItem.MtgObject.Uid).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.Language), this.SelectedItem.MtgObject.Language).WithParam<string>((Expression<Func<DetailPartViewModel, string>>) (x => x.ParentUid), this.SelectedItem.MtgObjectParent != null ? this.SelectedItem.MtgObjectParent.Uid : (string) null).Navigate();
    }

    public RelayCommand ShowNumpadCommand
    {
      get
      {
        return this._showNumpadCommand ?? (this._showNumpadCommand = new RelayCommand(new Action<object>(this.ExecuteShowNumpadCommand)));
      }
    }

    private void ExecuteShowNumpadCommand(object parameter)
    {
      if (this._mtgObjectParent == null || this._mtgObjectParent.MainContent == null)
        return;
      NumpadSearchTask numpadSearchTask = new NumpadSearchTask();
      numpadSearchTask.ParentScreen = (IScreen) this;
      numpadSearchTask.ParentUid = this._mtgObjectParent.Uid;
      numpadSearchTask.ParentType = this._mtgObjectParent.Type;
      numpadSearchTask.ParentLanguage = this._mtgObjectParent.MainContent.Language;
      numpadSearchTask.ActivationMode = FlyoutSearchActivationMode.None;
      numpadSearchTask.NavigationMode = FlyoutSearchNavigationMode.None;
      numpadSearchTask.CloseMode = FlyoutSearchCloseMode.Handler;
      numpadSearchTask.Show();
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      if (this.Parent is IPlayerParentViewModel parent)
      {
        this._autoPlay = parent.AutoPlay;
        this._uid = parent.Uid;
        this._language = parent.Language;
        this._parentUid = parent.ParentUid;
      }
      this.LoadInitialDataAsync();
    }

    protected override void OnActivate()
    {
      if (this.SelectedItem != null)
      {
        this.SelectedItem.Activate();
        this.SelectedItem.RateCommand.RaiseCanExecuteChanged();
      }
      // ISSUE: method pointer
      ServiceFacade.AudioService.NowPlayingChanged += new TypedEventHandler<IAudioService, AudioTrackInfo>((object) this, __methodptr(OnAudioServiceNowPlayingChanged));
    }

    protected override void OnDeactivate(bool close)
    {
      // ISSUE: method pointer
      ServiceFacade.AudioService.NowPlayingChanged -= new TypedEventHandler<IAudioService, AudioTrackInfo>((object) this, __methodptr(OnAudioServiceNowPlayingChanged));
    }

    private void Invalidate()
    {
      if (this._selectedItem == null)
        return;
      int num = this.Items.IndexOf(this._selectedItem);
      if (num < 0)
        return;
      for (int index = 0; index < this.Items.Count; ++index)
      {
        PlayerItemViewModel playerItemViewModel = this.Items[index];
        if (Math.Abs(num - index) < 2)
        {
          playerItemViewModel.ImageVisible = true;
          this.LoadPageAsync(playerItemViewModel);
        }
        else
          playerItemViewModel.ImageVisible = false;
      }
    }

    private async void LoadInitialDataAsync()
    {
      this.IsDataLoading = true;
      try
      {
        this.Items.Clear();
        if (string.IsNullOrWhiteSpace(this._parentUid))
          throw new Exception("ParentUid can't be empty");
        this._mtgObjectParent = (MtgObject) null;
        this._mtgObjectRoot = (MtgObject) null;
        MtgObjectFilter filter1 = new MtgObjectFilter(this._parentUid, this._language);
        filter1.Form = MtgObjectForm.Full;
        filter1.Includes = ContentSection.None;
        filter1.Excludes = ContentSection.All;
        filter1.IncludeChildrenCountInFullForm = true;
        filter1.IncludeAudioDuration = true;
        PlayerViewModel playerViewModel = this;
        MtgObject mtgObjectParent = playerViewModel._mtgObjectParent;
        MtgObject mtgObjectAsync1 = await MtgObjectServiceHelper.GetMtgObjectAsync(filter1);
        playerViewModel._mtgObjectParent = mtgObjectAsync1;
        playerViewModel = (PlayerViewModel) null;
        if (this._mtgObjectParent == null)
          throw new Exception("MtgObjectParent can't be null");
        switch (this._mtgObjectParent.Type)
        {
          case MtgObjectType.Museum:
            this._mtgObjectRoot = this._mtgObjectParent;
            break;
          case MtgObjectType.Collection:
            MtgObjectFilter filter2 = new MtgObjectFilter(this._mtgObjectParent.ParentUid, this._language);
            filter2.Form = MtgObjectForm.Compact;
            filter2.Includes = ContentSection.None;
            filter2.Excludes = ContentSection.All;
            playerViewModel = this;
            MtgObject mtgObjectRoot = playerViewModel._mtgObjectRoot;
            MtgObject mtgObjectAsync2 = await MtgObjectServiceHelper.GetMtgObjectAsync(filter2);
            playerViewModel._mtgObjectRoot = mtgObjectAsync2;
            playerViewModel = (PlayerViewModel) null;
            break;
        }
        if (this._mtgObjectRoot == null)
          throw new Exception("MtgObjectRoot can't be null");
        await ServiceFacade.MtgObjectService.CreateOrUpdateHistoryAsync(this._mtgObjectParent);
        this.Items.Add((PlayerItemViewModel) new PlayerStartItemViewModel(this, this._mtgObjectRoot, this._mtgObjectParent));
        if (this._mtgObjectParent.MainAudioMedia != null)
        {
          ObservableCollection<PlayerItemViewModel> items = this.Items;
          PlayerRegularItemViewModel regularItemViewModel = new PlayerRegularItemViewModel(this, -1, this._mtgObjectRoot, (MtgObject) null, this._mtgObjectParent);
          regularItemViewModel.ForcedTitle = AppResources.IntroductionTitle;
          items.Add((PlayerItemViewModel) regularItemViewModel);
        }
        MtgChildrenListResult children;
        MtgChildrenListResult childrenListResult = children;
        children = await this.GetChildren(this._uid != this._parentUid ? this._uid : (string) null, new int?());
        if (children != null && children.Data != null && children.Data.Length != 0)
        {
          this._metadata = children.Metadata;
          \u003C\u003Ef__AnonymousType0<int, MtgObject>[] array = ((IEnumerable<MtgObject>) children.Data).Select((x, i) => new
          {
            Index = children.Metadata.Offset + i,
            Data = x
          }).ToArray();
          for (int i = 0; i < this._metadata.TotalCount; i++)
          {
            var data = array.FirstOrDefault(x => x.Index == i);
            this.Items.Add((PlayerItemViewModel) new PlayerRegularItemViewModel(this, i, this._mtgObjectRoot, this._mtgObjectParent, data?.Data));
          }
        }
        this.Items.Add((PlayerItemViewModel) new PlayerEndItemViewModel(this, this._mtgObjectRoot, this._mtgObjectParent));
        this.SelectedItem = this.Items.FirstOrDefault<PlayerItemViewModel>((Func<PlayerItemViewModel, bool>) (x => x is PlayerRegularItemViewModel && string.Equals(x.Uid, this._uid, StringComparison.InvariantCultureIgnoreCase))) ?? this.Items.FirstOrDefault<PlayerItemViewModel>();
        if (this._autoPlay && this.SelectedItem != null && this.SelectedItem.AudioViewModel.PlayCommand.CanExecute((object) null))
        {
          this._autoPlay = false;
          this.SelectedItem.AudioViewModel.PlayCommand.Execute((object) null);
        }
        this.JumpListItems.Clear();
        this.JumpListItems.AddRange<PlayerItemViewModel>((IEnumerable<PlayerItemViewModel>) this.Items.OfType<PlayerRegularItemViewModel>());
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsRegularItem));
      }
      catch (Exception ex)
      {
        PlayerViewModel.Logger.Error(ex);
        ShellServiceFacade.DialogService.Show(AppResources.ErrorTitleDataLoading, AppResources.ErrorMessageDataLoading, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) => NavigationHelper.TryGoBack()));
      }
      finally
      {
        this.IsDataLoading = false;
      }
    }

    private async void LoadPageAsync(MtgObject mtgObject)
    {
      if (mtgObject == null || mtgObject.Location == null)
        return;
      this.IsDataLoading = true;
      try
      {
        this.InitializeChildren(await this.GetChildren(mtgObject.Uid, new int?()));
        PlayerRegularItemViewModel regularItemViewModel = this.Items.OfType<PlayerRegularItemViewModel>().FirstOrDefault<PlayerRegularItemViewModel>((Func<PlayerRegularItemViewModel, bool>) (x => x.Uid == mtgObject.Uid));
        if (regularItemViewModel == null)
          return;
        this.SelectedItem = (PlayerItemViewModel) regularItemViewModel;
        if (!this.SelectedItem.AudioViewModel.PlayCommand.CanExecute((object) null))
          return;
        this.SelectedItem.AudioViewModel.PlayCommand.Execute((object) null);
      }
      catch (Exception ex)
      {
        PlayerViewModel.Logger.Error(ex);
      }
      finally
      {
        this.IsDataLoading = false;
      }
    }

    private async void LoadPageAsync(PlayerItemViewModel playerItemViewModel)
    {
      if (this._metadata == null || playerItemViewModel == null || playerItemViewModel.IsLoaded)
        return;
      int offset = playerItemViewModel.Index / 15 * 15;
      if (this._offsets.Contains(offset))
        return;
      this.IsDataLoading = true;
      this._offsets.Add(offset);
      try
      {
        this.InitializeChildren(await this.GetChildren((string) null, new int?(offset)));
      }
      catch (Exception ex)
      {
        PlayerViewModel.Logger.Error(ex);
      }
      this._offsets.Remove(offset);
      this.IsDataLoading = false;
    }

    private void InitializeChildren(MtgChildrenListResult children)
    {
      if (children == null || children.Data == null || children.Data.Length == 0)
        return;
      this._metadata = children.Metadata;
      foreach (var data in ((IEnumerable<MtgObject>) children.Data).Select((x, i) => new
      {
        Index = children.Metadata.Offset + i,
        Data = x
      }).ToArray())
      {
        var indexedChild = data;
        this.Items.FirstOrDefault<PlayerItemViewModel>((Func<PlayerItemViewModel, bool>) (x => x.Index == indexedChild.Index))?.SetMtgObject(indexedChild.Data);
      }
    }

    private async Task<MtgChildrenListResult> GetChildren(string pageUid, int? offset)
    {
      MtgObjectChildrenExtendedFilter filter = new MtgObjectChildrenExtendedFilter();
      filter.PageUid = pageUid;
      filter.Offset = offset;
      filter.Limit = new int?(15);
      filter.Uid = this._parentUid;
      filter.Languages = new string[1]{ this._language };
      filter.ShowHidden = false;
      MtgObjectChildrenExtendedFilter childrenExtendedFilter = filter;
      MtgObjectType[] mtgObjectTypeArray;
      if (this._mtgObjectParent.Type != MtgObjectType.Collection)
        mtgObjectTypeArray = new MtgObjectType[1]
        {
          MtgObjectType.Exhibit
        };
      else
        mtgObjectTypeArray = new MtgObjectType[2]
        {
          MtgObjectType.Exhibit,
          MtgObjectType.StoryNavigation
        };
      childrenExtendedFilter.Types = mtgObjectTypeArray;
      filter.SortExhibits = this._mtgObjectParent.Type == MtgObjectType.Museum ? "number" : (string) null;
      filter.Form = MtgObjectForm.Full;
      filter.Includes = ContentSection.References;
      return await MtgObjectServiceHelper.GetMtgObjectChildrenExtendedAsync(filter);
    }

    private void RefreshCommands()
    {
      this.NowPlayingCommand.RaiseCanExecuteChanged();
      this.NavigateCommand.RaiseCanExecuteChanged();
      this.ForwardCommand.RaiseCanExecuteChanged();
      this.BackwardCommand.RaiseCanExecuteChanged();
    }

    private async void RefreshQuizVisibilityAsync()
    {
      this.IsQuizVisible = false;
      await Task.Delay(150);
      this.IsQuizVisible = this.SelectedItem != null && this.SelectedItem.MtgObject != null && this.SelectedItem.MtgObject.MainContent != null && this.SelectedItem.MtgObject.MainContent.Quiz != null;
    }

    public void HandleFlyoutSearchResult(
      BaseSearchFlyoutViewModel flyout,
      SearchFlyoutResult result)
    {
      if (result == null || !result.Success || result.MtgObject == null || !(flyout is NumpadFlyoutViewModel))
        return;
      string number = result.MtgObject.Location != null ? result.MtgObject.Location.Number : (string) null;
      if (string.IsNullOrWhiteSpace(number))
        return;
      PlayerRegularItemViewModel regularItemViewModel = this.Items.OfType<PlayerRegularItemViewModel>().Where<PlayerRegularItemViewModel>((Func<PlayerRegularItemViewModel, bool>) (x => x.MtgObject != null && x.MtgObject.Location != null)).FirstOrDefault<PlayerRegularItemViewModel>((Func<PlayerRegularItemViewModel, bool>) (x => string.Equals(x.MtgObject.Location.Number, number, StringComparison.InvariantCultureIgnoreCase)));
      if (regularItemViewModel != null)
      {
        this.SelectedItem = (PlayerItemViewModel) regularItemViewModel;
        if (!this.SelectedItem.AudioViewModel.PlayCommand.CanExecute((object) null))
          return;
        this.SelectedItem.AudioViewModel.PlayCommand.Execute((object) null);
      }
      else
        this.LoadPageAsync(result.MtgObject);
    }

    private void OnAudioServiceNowPlayingChanged(
      IAudioService audioService,
      AudioTrackInfo audioTrackInfo)
    {
      this.NowPlayingCommand.RaiseCanExecuteChanged();
      if (audioTrackInfo == null)
        return;
      foreach (PlayerItemViewModel playerItemViewModel in (Collection<PlayerItemViewModel>) this.Items)
        playerItemViewModel.IsNowPlaying = string.Equals(playerItemViewModel.Uid, audioTrackInfo.MtgObjectUid, StringComparison.InvariantCultureIgnoreCase) && string.Equals(playerItemViewModel.Language, audioTrackInfo.Language, StringComparison.InvariantCultureIgnoreCase);
    }
  }
}
