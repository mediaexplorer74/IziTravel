// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Featured.FeaturedPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using Izi.Travel.Shell.Core.Services;
using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Featured
{
  public class FeaturedPartViewModel : Conductor<IScreen>.Collection.AllActive
  {
    private readonly FeaturedListViewModel _featuredListViewModel;
    private int _selectedIndex;
    private RelayCommand _startCommand;

    public FeaturedListViewModel List => this._featuredListViewModel;

    public int SelectedIndex
    {
      get => this._selectedIndex;
      set
      {
        this.SetProperty<int>(ref this._selectedIndex, value, propertyName: nameof (SelectedIndex));
      }
    }

    public bool IsLocked => this.List.IsDataLoading || this.List.IsListEmpty;

    public FeaturedPartViewModel(FeaturedListViewModel featuredListViewModel)
    {
      this._featuredListViewModel = featuredListViewModel;
      this._featuredListViewModel.IsHeaderVisible = true;
      this._featuredListViewModel.IsFooterVisible = true;
      this._featuredListViewModel.ExploreCommand = this.StartCommand;
    }

    public RelayCommand StartCommand
    {
      get
      {
        return this._startCommand ?? (this._startCommand = new RelayCommand(new Action<object>(this.ExecuteStartCommand)));
      }
    }

    private async void ExecuteStartCommand(object parameter)
    {
      if (this.SelectedIndex == 0 && !this.List.IsListEmpty)
      {
        this.SelectedIndex = 1;
      }
      else
      {
        AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
        appSettings.FirstLaunch = false;
        ServiceFacade.SettingsService.SaveAppSettings(appSettings);
        ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();
        await Task.Factory.StartNew((System.Action) (() => Deployment.Current.Dispatcher.BeginInvoke((System.Action) (() => ShellServiceFacade.NavigationService.RemoveBackEntry()))));
      }
    }

    protected override void OnInitialize() => this.Items.Add((IScreen) this._featuredListViewModel);

    protected override void OnActivate()
    {
      base.OnActivate();
      this.List.PropertyChanged += new PropertyChangedEventHandler(this.OnListPropertyChanged);
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      this.List.PropertyChanged -= new PropertyChangedEventHandler(this.OnListPropertyChanged);
    }

    private void OnListPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "IsDataLoading") && !(e.PropertyName == "IsListEmpty"))
        return;
      this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsLocked));
    }
  }
}
