// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Profile.ProfileTabViewModel`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Commands;
using Izi.Travel.Shell.Common.Model;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Model.Profile;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Profile
{
  public abstract class ProfileTabViewModel<TItemViewModel> : 
    Conductor<IScreen>,
    IProfileTabViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
    where TItemViewModel : IScreen
  {
    private IEnumerable<ButtonInfo> _appBarButtons;
    private IEnumerable<MenuItemInfo> _appBarMenuItems;
    private RateApplicationCommand _rateApplicationCommand;
    private FeedbackCommand _feedbackCommand;

    protected TItemViewModel ItemViewModel { get; private set; }

    public abstract ProfileType Type { get; }

    public IEnumerable<ButtonInfo> AppBarButtons
    {
      get => this._appBarButtons;
      private set
      {
        this.SetProperty<IEnumerable<ButtonInfo>>(ref this._appBarButtons, value, propertyName: nameof (AppBarButtons));
      }
    }

    public IEnumerable<MenuItemInfo> AppBarMenuItems
    {
      get => this._appBarMenuItems;
      private set
      {
        this.SetProperty<IEnumerable<MenuItemInfo>>(ref this._appBarMenuItems, value, propertyName: nameof (AppBarMenuItems));
      }
    }

    protected ProfileTabViewModel() => this.ItemViewModel = IoC.Get<TItemViewModel>();

    public RateApplicationCommand RateApplicationCommand
    {
      get
      {
        return this._rateApplicationCommand ?? (this._rateApplicationCommand = new RateApplicationCommand());
      }
    }

    public FeedbackCommand FeedbackCommand
    {
      get => this._feedbackCommand ?? (this._feedbackCommand = new FeedbackCommand());
    }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.ActiveItem = (IScreen) this.ItemViewModel;
    }

    protected override void OnActivate()
    {
      base.OnActivate();
      this.AppBarButtons = this.GetAppBarButtonList();
      this.AppBarMenuItems = this.GetAppBarMenuItemList();
    }

    protected virtual IEnumerable<ButtonInfo> GetAppBarButtonList()
    {
      return (IEnumerable<ButtonInfo>) null;
    }

    protected virtual IEnumerable<MenuItemInfo> GetAppBarMenuItemList()
    {
      return (IEnumerable<MenuItemInfo>) new MenuItemInfo[2]
      {
        new MenuItemInfo()
        {
          Text = AppResources.CommandRateAndReview.ToLower(),
          Command = (ICommand) this.RateApplicationCommand
        },
        new MenuItemInfo()
        {
          Text = AppResources.LabelFeedback.ToLower(),
          Command = (ICommand) this.FeedbackCommand
        }
      };
    }
  }
}
