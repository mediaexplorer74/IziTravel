// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Commands;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.ViewModels.Profile.Bookmark;
using Izi.Travel.Shell.ViewModels.Profile.Download;
using Izi.Travel.Shell.ViewModels.Profile.History;
using Izi.Travel.Shell.ViewModels.Profile.Purchase;
using Izi.Travel.Shell.ViewModels.Profile.Quiz;
using Izi.Travel.Shell.ViewModels.Profile.Settings;
using Microsoft.Phone.Shell;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public class ProfileViewModel : 
    Screen,
    IMainTabViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
  {
    private bool _isBusy;
    private string _busyTitle;
    private ProfileTileViewModel _tileDownloadViewModel;
    private ProfileBookmarkTileViewModel _tileBookmarkViewModel;
    private ProfilePurchaseTileViewModel _tilePurchaseViewModel;
    private ProfileHistoryTileViewModel _tileHistoryViewModel;
    private ProfileQuizTileViewModel _tileQuizViewModel;
    private ProfileSettingsTileViewModel _tileSettingsViewModel;

    public ScreenProperties Properties { get; set; }

    public string ImageUrl => "/Assets/Icons/tab.profile.png";

    public string SelectedImageUrl => "/Assets/Icons/tab.profile.selected.png";

    public bool IsBusy
    {
      get => this._isBusy;
      set => this.SetProperty<bool>(ref this._isBusy, value, propertyName: nameof (IsBusy));
    }

    public string BusyTitle
    {
      get => this._busyTitle;
      set => this.SetProperty<string>(ref this._busyTitle, value, propertyName: nameof (BusyTitle));
    }

    public ProfileTileViewModel TileDownloadViewModel
    {
      get
      {
        return this._tileDownloadViewModel ?? (this._tileDownloadViewModel = (ProfileTileViewModel) new ProfileDownloadTileViewModel());
      }
    }

    public ProfileBookmarkTileViewModel TileBookmarkViewModel
    {
      get
      {
        return this._tileBookmarkViewModel ?? (this._tileBookmarkViewModel = new ProfileBookmarkTileViewModel());
      }
    }

    public ProfilePurchaseTileViewModel TilePurchaseViewModel
    {
      get
      {
        return this._tilePurchaseViewModel ?? (this._tilePurchaseViewModel = new ProfilePurchaseTileViewModel());
      }
    }

    public ProfileHistoryTileViewModel TileHistoryViewModel
    {
      get
      {
        return this._tileHistoryViewModel ?? (this._tileHistoryViewModel = new ProfileHistoryTileViewModel());
      }
    }

    public ProfileQuizTileViewModel TileQuizViewModel
    {
      get => this._tileQuizViewModel ?? (this._tileQuizViewModel = new ProfileQuizTileViewModel());
    }

    public ProfileSettingsTileViewModel TileSettingsViewModel
    {
      get
      {
        return this._tileSettingsViewModel ?? (this._tileSettingsViewModel = new ProfileSettingsTileViewModel());
      }
    }

    public ProfileViewModel()
    {
      ScreenProperties screenProperties = new ScreenProperties();
      screenProperties.AppBarMode = ApplicationBarMode.Default;
      screenProperties.AppBarButtons = (IEnumerable<ButtonInfo>) new ButtonInfo[2]
      {
        new ButtonInfo()
        {
          Key = "Settings",
          Text = AppResources.LabelSettings,
          ImageUrl = "/Assets/Icons/appbar.settings.png",
          Command = (ICommand) this.TileSettingsViewModel.NavigateCommand
        },
        new ButtonInfo()
        {
          Key = "Feedback",
          Text = AppResources.LabelFeedback.ToLower(),
          ImageUrl = "/Assets/Icons/appbar.feedback.png",
          Command = (ICommand) new FeedbackCommand()
        }
      };
      screenProperties.AppBarMenuItems = (IEnumerable<MenuItemInfo>) new MenuItemInfo[1]
      {
        new MenuItemInfo()
        {
          Text = AppResources.CommandRateTheApp.ToLower(),
          Command = (ICommand) new RateApplicationCommand()
        }
      };
      this.Properties = screenProperties;
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      ScreenExtensions.TryActivate((object) this.TileDownloadViewModel);
      ScreenExtensions.TryActivate((object) this.TileQuizViewModel);
      ScreenExtensions.TryActivate((object) this.TileBookmarkViewModel);
      ScreenExtensions.TryActivate((object) this.TilePurchaseViewModel);
      ScreenExtensions.TryActivate((object) this.TileHistoryViewModel);
      ScreenExtensions.TryActivate((object) this.TileSettingsViewModel);
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      ScreenExtensions.TryDeactivate((object) this.TileDownloadViewModel, true);
      ScreenExtensions.TryDeactivate((object) this.TileQuizViewModel, true);
      ScreenExtensions.TryDeactivate((object) this.TileBookmarkViewModel, true);
      ScreenExtensions.TryDeactivate((object) this.TilePurchaseViewModel, true);
      ScreenExtensions.TryDeactivate((object) this.TileHistoryViewModel, true);
      ScreenExtensions.TryDeactivate((object) this.TileSettingsViewModel, true);
    }
  }
}
