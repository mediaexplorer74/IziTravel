// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.BaseSearchFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Analytics.Parameters;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.Flyout;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Mtg.Components.Enums;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.Model;
using System;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public abstract class BaseSearchFlyoutViewModel : FlyoutViewModel
  {
    private readonly ILog _logger;
    private readonly IScreen _parentScreen;
    private bool _isBusy;
    private SearchFlyoutResult _result;
    private RelayCommand _searchCommand;

    protected ILog Logger => this._logger;

    protected IScreen ParentScreen => this._parentScreen;

    public string ParentUid { get; set; }

    public MtgObjectType ParentType { get; set; }

    public string ParentLanguage { get; set; }

    public FlyoutSearchActivationMode ActivationMode { get; set; }

    public FlyoutSearchNavigationMode NavigationMode { get; set; }

    public FlyoutSearchCloseMode CloseMode { get; set; }

    public virtual bool IsBusy
    {
      get => this._isBusy;
      set
      {
        this.SetProperty<bool>(ref this._isBusy, value, new System.Action(this.RefreshCommands), nameof (IsBusy));
      }
    }

    protected BaseSearchFlyoutViewModel(IScreen parentScreen)
    {
      this._parentScreen = parentScreen;
      this._logger = LogManager.GetLog(this.GetType());
    }

    public RelayCommand SearchCommand
    {
      get
      {
        return this._searchCommand ?? (this._searchCommand = new RelayCommand(new Action<object>(this.ExecuteSearchCommand), new Func<object, bool>(this.CanExecuteSearchCommand)));
      }
    }

    protected virtual bool CanExecuteSearchCommand(object parameter) => !this.IsBusy;

    private void ExecuteSearchCommand(object parameter) => this.SearchAsync(parameter);

    protected override void OnOpening()
    {
      base.OnOpening();
      this.IsBusy = false;
    }

    protected override void OnClosed()
    {
      if (this.ParentScreen == null)
        return;
      if (this.CloseMode == FlyoutSearchCloseMode.ReactivateParent)
      {
        ScreenExtensions.TryDeactivate((object) this.ParentScreen, false);
        ScreenExtensions.TryActivate((object) this.ParentScreen);
      }
      else
      {
        if (this.CloseMode != FlyoutSearchCloseMode.Handler || !(this.ParentScreen is IFlyoutSearchResultHandler parentScreen))
          return;
        parentScreen.HandleFlyoutSearchResult(this, this._result);
      }
    }

    protected override bool CanExecuteCloseCommand(object parameter) => !this.IsBusy;

    protected virtual Task<SearchFlyoutResult> SearchTask(object parameter)
    {
      return (Task<SearchFlyoutResult>) null;
    }

    protected virtual void OnSearchError(Exception ex)
    {
    }

    protected virtual void RefreshCommands() => this.SearchCommand.RaiseCanExecuteChanged();

    protected void ActivateInternal(
      MtgObject mtgObjectParent,
      MtgObject mtgObject,
      ActivationTypeParameter activationType)
    {
      if (mtgObject == null || this.ActivationMode != FlyoutSearchActivationMode.Play)
        return;
      AudioTrackInfo audioTrackInfo = ServiceFacade.AudioService.NowPlaying;
      if (audioTrackInfo == null || !string.Equals(audioTrackInfo.MtgObjectUid, mtgObject.Uid, StringComparison.CurrentCultureIgnoreCase) || audioTrackInfo.Language != mtgObject.Language)
        audioTrackInfo = AudioTrackInfoHelper.FromMtgObject(mtgObject, mtgObjectParent?.Uid, mtgObjectParent != null ? mtgObjectParent.Type : MtgObjectType.Unknown, ActivationTypeParameter.Numpad);
      ServiceFacade.AudioService.Play(audioTrackInfo);
    }

    protected void NavigateInternal(MtgObject mtgObject, string parentUid)
    {
      if (mtgObject == null)
        return;
      if (this.NavigationMode == FlyoutSearchNavigationMode.Player)
      {
        NavigationHelper.NavigateToAudio(mtgObject.Type, mtgObject.Uid, mtgObject.Language, parentUid, true);
      }
      else
      {
        if (this.NavigationMode != FlyoutSearchNavigationMode.None)
          return;
        this.IsOpen = false;
      }
    }

    private async void SearchAsync(object parameter)
    {
      this.IsBusy = true;
      try
      {
        this._result = SearchFlyoutResult.Empty;
        Task<SearchFlyoutResult> task = this.SearchTask(parameter);
        if (task == null)
          return;
        BaseSearchFlyoutViewModel searchFlyoutViewModel = this;
        SearchFlyoutResult result = searchFlyoutViewModel._result;
        SearchFlyoutResult searchFlyoutResult = await task;
        searchFlyoutViewModel._result = searchFlyoutResult;
        searchFlyoutViewModel = (BaseSearchFlyoutViewModel) null;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.OnSearchError(ex);
      }
      finally
      {
        this.IsBusy = false;
      }
    }
  }
}
