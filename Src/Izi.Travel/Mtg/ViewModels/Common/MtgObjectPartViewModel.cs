// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.MtgObjectPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Helper;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Context;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.Messages;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common
{
  public abstract class MtgObjectPartViewModel : 
    Conductor<IScreen>,
    IMtgObjectPartViewModel,
    IMtgObjectProvider
  {
    private ILog _logger;
    private IEventAggregator _eventAggregator;
    private IFrameNavigationContext _frameNavigationContext;
    private bool _isDataLoading;
    private RelayCommand _refreshCommand;

    protected ILog Logger => this._logger ?? (this._logger = LogManager.GetLog(this.GetType()));

    protected IEventAggregator EventAggregator
    {
      get => this._eventAggregator ?? (this._eventAggregator = IoC.Get<IEventAggregator>());
    }

    protected IFrameNavigationContext FrameNavigationContext
    {
      get
      {
        return this._frameNavigationContext ?? (this._frameNavigationContext = IoC.Get<IFrameNavigationContext>());
      }
    }

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

    public string Uid { get; set; }

    public string Language { get; set; }

    public string ParentUid { get; set; }

    public string RootUid { get; set; }

    public MtgObject MtgObject { get; private set; }

    public MtgObject MtgObjectParent { get; private set; }

    public MtgObject MtgObjectRoot { get; private set; }

    public RelayCommand RefreshCommand
    {
      get
      {
        return this._refreshCommand ?? (this._refreshCommand = new RelayCommand(new Action<object>(this.ExecuteRefreshCommand), new Func<object, bool>(this.CanExecuteRefreshCommand)));
      }
    }

    protected virtual bool CanExecuteRefreshCommand(object parameter) => !this.IsDataLoading;

    protected virtual void ExecuteRefreshCommand(object parameter) => this.LoadDataAsync();

    protected override void OnInitialize() => this.LoadDataAsync();

    protected override void OnActivate()
    {
      if (this.IsDataLoading || this.ActiveItem == null)
        return;
      this.ActiveItem.Activate();
    }

    protected override void OnDeactivate(bool close)
    {
      base.OnDeactivate(close);
      if (this.ActiveItem == null)
        return;
      this.ActiveItem.Deactivate(close);
    }

    protected abstract Task<MtgObject> LoadMtgObjectAsync();

    protected abstract IScreen CreateScreenItem();

    protected virtual void OnLoadedFirst()
    {
    }

    protected virtual void OnLoadedNoData()
    {
      ShellServiceFacade.DialogService.Show(AppResources.ErrorTitleDataLoading, AppResources.ErrorMessageDataLoading, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) => NavigationHelper.TryGoBack()));
    }

    protected virtual void OnLoadError(Exception ex)
    {
      ShellServiceFacade.DialogService.Show(AppResources.ErrorTitleDataLoading, AppResources.ErrorMessageDataLoading, MessageBoxButtonContent.Ok, (Action<FlyoutDialog, MessageBoxResult>) ((d, e) => NavigationHelper.TryGoBack()));
    }

    protected virtual void OnLoadCompleted()
    {
    }

    private async void LoadDataAsync()
    {
      try
      {
        this.IsDataLoading = true;
        this.EventAggregator.PublishOnUIThread((object) new DataLoadingMessage());
        this.MtgObject = await this.LoadMtgObjectAsync();
        if (this.MtgObject != null && this.MtgObject.MainContent != null && this.MtgObject.MainContent.Language != null)
        {
          this.Language = this.MtgObject.MainContent.Language;
          await this.LoadParentDataAsync();
          if (this.MtgObjectRoot != null && (this.MtgObject.Type == MtgObjectType.Collection || this.MtgObject.Type == MtgObjectType.Exhibit))
            this.MtgObject.Location = this.MtgObjectRoot.Location;
          if (this.FrameNavigationContext.NavigationMode == NavigationMode.New)
            this.OnLoadedFirst();
          this.ActiveItem = this.CreateScreenItem();
          this.ActiveItem.Activate();
        }
        else
          this.OnLoadedNoData();
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        this.OnLoadError(ex);
      }
      finally
      {
        this.IsDataLoading = false;
        this.OnLoadCompleted();
        this.EventAggregator.PublishOnUIThread((object) new DataLoadedMessage());
      }
    }

    private async Task LoadParentDataAsync()
    {
      if (this.MtgObject.Type == MtgObjectType.Tour || this.MtgObject.Type == MtgObjectType.Museum)
      {
        this.MtgObjectParent = (MtgObject) null;
        this.MtgObjectRoot = this.MtgObject;
      }
      else
      {
        string str = !string.IsNullOrWhiteSpace(this.ParentUid) ? this.ParentUid : this.MtgObject.ParentUid;
        if (!string.IsNullOrWhiteSpace(str))
        {
          MtgObjectFilter filter = new MtgObjectFilter();
          filter.Uid = str;
          filter.Languages = new string[1]
          {
            this.MtgObject.MainContent.Language
          };
          filter.Includes = ContentSection.None;
          filter.Form = MtgObjectForm.Compact;
          this.MtgObjectParent = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
        }
        if (string.IsNullOrWhiteSpace(this.RootUid))
        {
          this.MtgObjectRoot = this.MtgObjectParent;
        }
        else
        {
          MtgObjectFilter filter = new MtgObjectFilter();
          filter.Uid = this.RootUid;
          filter.Languages = new string[1]
          {
            this.MtgObject.MainContent.Language
          };
          filter.Includes = ContentSection.None;
          filter.Form = MtgObjectForm.Compact;
          this.MtgObjectRoot = await MtgObjectServiceHelper.GetMtgObjectAsync(filter);
        }
      }
    }
  }
}
