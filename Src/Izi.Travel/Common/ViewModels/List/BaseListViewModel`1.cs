// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.ViewModels.List.BaseListViewModel`1
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Common.ViewModels.List
{
  public abstract class BaseListViewModel<TItemViewModel> : 
    Screen,
    IListViewModel,
    IScreen,
    IHaveDisplayName,
    IActivate,
    IDeactivate,
    IGuardClose,
    IClose,
    INotifyPropertyChangedEx,
    INotifyPropertyChanged
    where TItemViewModel : class
  {
    protected const int DefaultLimit = 25;
    protected readonly ILog Logger;
    private bool _isDataLoading;
    private bool _isListEmpty;
    private ObservableCollection<TItemViewModel> _items;
    private TItemViewModel _selectedItem;
    private RelayCommand _loadDataCommand;
    private RelayCommand _refreshCommand;
    private RelayCommand _navigateCommand;
    private RelayCommand _deleteItemCommand;

    public bool IsDataLoading
    {
      get => this._isDataLoading;
      protected set
      {
        if (this._isDataLoading == value)
          return;
        this._isDataLoading = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsDataLoading));
      }
    }

    public bool IsListEmpty
    {
      get => this._isListEmpty;
      protected set
      {
        if (this._isListEmpty == value)
          return;
        this._isListEmpty = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsListEmpty));
      }
    }

    public ObservableCollection<TItemViewModel> Items
    {
      get => this._items;
      set
      {
        this.SetProperty<ObservableCollection<TItemViewModel>>(ref this._items, value, propertyName: nameof (Items));
      }
    }

    public TItemViewModel SelectedItem
    {
      get => this._selectedItem;
      set
      {
        if ((object) this._selectedItem == (object) value)
          return;
        this._selectedItem = value;
        this.NotifyOfPropertyChange<TItemViewModel>((Expression<Func<TItemViewModel>>) (() => this.SelectedItem));
      }
    }

    public int ItemsCount => this.Items.Count;

    protected BaseListViewModel()
    {
      this.Logger = LogManager.GetLog(this.GetType());
      this._items = new ObservableCollection<TItemViewModel>();
    }

    protected override void OnActivate() => this.LoadDataAsync();

    public RelayCommand LoadDataCommand
    {
      get
      {
        return this._loadDataCommand ?? (this._loadDataCommand = new RelayCommand(new Action<object>(this.ExecuteLoadDataCommand), new Func<object, bool>(this.CanExecuteLoadDataCommand)));
      }
    }

    protected virtual bool CanExecuteLoadDataCommand(object parameter) => !this.IsDataLoading;

    protected virtual void ExecuteLoadDataCommand(object parameter) => this.LoadDataAsync();

    public RelayCommand RefreshCommand
    {
      get
      {
        return this._refreshCommand ?? (this._refreshCommand = new RelayCommand(new Action<object>(this.ExecuteRefreshCommand), new Func<object, bool>(this.CanExecuteRefreshCommand)));
      }
    }

    protected virtual bool CanExecuteRefreshCommand(object parameter) => !this.IsDataLoading;

    protected virtual void ExecuteRefreshCommand(object parameter)
    {
      this.Items.Clear();
      this.LoadDataAsync();
    }

    public RelayCommand NavigateCommand
    {
      get
      {
        return this._navigateCommand ?? (this._navigateCommand = new RelayCommand(new Action<object>(this.ExecuteNavigateCommand), new Func<object, bool>(this.CanExecuteNavigateCommand)));
      }
    }

    protected virtual bool CanExecuteNavigateCommand(object parameter) => !this.IsDataLoading;

    protected virtual void ExecuteNavigateCommand(object parameter)
    {
    }

    public RelayCommand DeleteItemCommand
    {
      get
      {
        return this._deleteItemCommand ?? (this._deleteItemCommand = new RelayCommand(new Action<object>(this.ExecuteDeleteItemCommand), new Func<object, bool>(this.CanExecuteDeleteItemCommand)));
      }
    }

    protected virtual bool CanExecuteDeleteItemCommand(object parameter) => false;

    protected virtual void ExecuteDeleteItemCommand(object parameter)
    {
    }

    protected virtual Task<IEnumerable<TItemViewModel>> GetDataAsync()
    {
      throw new NotImplementedException();
    }

    protected virtual Task LoadAdditionalDataAsync() => Task.Factory.StartNew((System.Action) (() => { }));

    protected virtual void OnLoadDataBegin()
    {
      this.IsListEmpty = false;
      this.RefreshCommands();
    }

    protected virtual void OnLoadDataError(Exception ex)
    {
    }

    protected virtual void OnLoadDataCompleted()
    {
      this.NotifyOfPropertyChange<int>((Expression<Func<int>>) (() => this.ItemsCount));
      this.IsListEmpty = this.ItemsCount == 0;
      this.RefreshCommands();
    }

    protected virtual void RefreshCommands()
    {
      this.LoadDataCommand.RaiseCanExecuteChanged();
      this.RefreshCommand.RaiseCanExecuteChanged();
      this.NavigateCommand.RaiseCanExecuteChanged();
      this.DeleteItemCommand.RaiseCanExecuteChanged();
    }

    protected async void LoadDataAsync()
    {
      try
      {
        this.IsDataLoading = true;
        this.OnLoadDataBegin();
        await this.LoadAdditionalDataAsync();
        IEnumerable<TItemViewModel> dataAsync = await this.GetDataAsync();
        if (dataAsync == null)
          return;
        foreach (TItemViewModel itemViewModel in dataAsync)
          this.Items.Add(itemViewModel);
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.OnLoadDataError(ex);
      }
      finally
      {
        this.IsDataLoading = false;
        this.OnLoadDataCompleted();
      }
    }
  }
}
