// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Bootstrapper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using BugSense;
using BugSense.Core.Model;
using Caliburn.Micro;
using Caliburn.Micro.Extras;
using Izi.Travel.Business;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Geofencing;
using Izi.Travel.Geofencing.Geotracker;
using Izi.Travel.Shell.Core;
using Izi.Travel.Shell.Core.Attributes;
using Izi.Travel.Shell.Core.Context;
using Izi.Travel.Shell.Core.Helpers;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Contract;
using Izi.Travel.Shell.Core.Services.Implementation;
using Izi.Travel.Shell.Media.ViewModels;
using Izi.Travel.Shell.Media.ViewModels.Image;
using Izi.Travel.Shell.Media.ViewModels.Video;
using Izi.Travel.Shell.Mtg.Helpers;
using Izi.Travel.Shell.Mtg.ViewModels.Collection.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Collection.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player;
using Izi.Travel.Shell.Mtg.ViewModels.Exhibit.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Exhibit.List;
using Izi.Travel.Shell.Mtg.ViewModels.Museum.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Museum.Map;
using Izi.Travel.Shell.Mtg.ViewModels.Publisher.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Quiz;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.Detail;
using Izi.Travel.Shell.Mtg.ViewModels.TouristAttraction.List;
using Izi.Travel.Shell.Settings.ViewModels;
using Izi.Travel.Shell.Settings.ViewModels.Application;
using Izi.Travel.Shell.Settings.ViewModels.Internal;
using Izi.Travel.Shell.ViewModels;
using Izi.Travel.Shell.ViewModels.Explore;
using Izi.Travel.Shell.ViewModels.Featured;
using Izi.Travel.Shell.ViewModels.Profile;
using Izi.Travel.Shell.ViewModels.Profile.Bookmark;
using Izi.Travel.Shell.ViewModels.Profile.Download;
using Izi.Travel.Shell.ViewModels.Profile.History;
using Izi.Travel.Shell.ViewModels.Profile.Purchase;
using Izi.Travel.Shell.ViewModels.Profile.Quiz;
using Izi.Travel.Shell.ViewModels.QuickAccess;
using Izi.Travel.Utility;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Maps;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell
{
  public class Bootstrapper : PhoneBootstrapperBase
  {
    private PhoneContainer _container;
    private bool _reset;
    private static readonly ILog Logger;

    static Bootstrapper()
    {
      LogManager.GetLog = (Func<Type, ILog>) (type => (ILog) new CustomLogger(type));
      Bootstrapper.Logger = LogManager.GetLog(typeof (Bootstrapper));
    }

    public Bootstrapper() => this.Initialize();

    protected override PhoneApplicationFrame CreatePhoneApplicationFrame()
    {
      TransitionFrame applicationFrame = new TransitionFrame();
      applicationFrame.UriMapper = (UriMapperBase) new UriMapper();
      return (PhoneApplicationFrame) applicationFrame;
    }

    protected override void PrepareApplication()
    {
      base.PrepareApplication();
      AppResources.Culture = Thread.CurrentThread.CurrentUICulture;
      MapsSettings.ApplicationContext.ApplicationId = "d83847c4-012d-4d9b-91c1-c13dc807faa1";
      MapsSettings.ApplicationContext.AuthenticationToken = "V08O-j9c7o3QaW7gLBpvXQn";
      try
      {
        BugSenseHandler.Instance.InitAndStartSession((IExceptionManager) new ExceptionManager(System.Windows.Application.Current), this.RootFrame, "dc480d2d");
      }
      catch (Exception ex)
      {
        Bootstrapper.Logger.Error(ex);
      }
      this.PhoneService.UserIdleDetectionMode = IdleDetectionMode.Disabled;
      this.PhoneService.RunningInBackground += new EventHandler<RunningInBackgroundEventArgs>(this.OnRunningInBackground);
      this.PhoneService.Closing += (EventHandler<ClosingEventArgs>) ((s, e) => Bootstrapper.Logger.Info("Closing"));
      this.RootFrame.Navigating += new NavigatingCancelEventHandler(this.OnRootFrameNavigating);
      this.RootFrame.Navigated += new NavigatedEventHandler(this.OnRootFrameNavigated);
    }

    protected override void Configure()
    {
      if (Execute.InDesignMode)
        return;
      Bootstrapper.InitializeViewLocator();
      this._container = new PhoneContainer();
      this._container.RegisterPhoneServices((Frame) this.RootFrame);
      Module.ConfigureContainer((SimpleContainer) this._container);
      ModuleConventions.Install();
      this._container.RegisterSingleton(typeof (IDialogService), (string) null, typeof (DialogService));
      this._container.RegisterInstance(typeof (IFrameNavigationContext), (string) null, (object) FrameNavigationContext.Instance);
      this.InitializeViewModels();
      GeofenceMonitor.GetGeotracker = (Func<IGeotracker>) (() => (IGeotracker) Izi.Travel.Business.Managers.Geotracker.Instance);
      TourPlaybackManager.NotifyTouristAttractionReached = (Action<string>) (touristAttractionUid => ShellServiceFacade.DialogService.ShowToast(AppResources.ToastTouristAttractionReached, ShellServiceFacade.NavigationService.UriFor<TourMapPartViewModel>().WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Uid), TourPlaybackManager.Instance.TourPlaybackUid).WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Language), TourPlaybackManager.Instance.TourPlaybackLanguage).BuildUri(), (System.Action) (() => TourMapPartViewModel.Navigate(TourPlaybackManager.Instance.TourPlaybackUid, TourPlaybackManager.Instance.TourPlaybackLanguage, touristAttractionUid)), true));
      TourPlaybackManager.NotifyTourStopped = (System.Action) (() => ShellServiceFacade.DialogService.ShowToast(AppResources.ToastTourStopped, ShellServiceFacade.NavigationService.UriFor<TourMapPartViewModel>().WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Uid), TourPlaybackManager.Instance.TourPlaybackUid).WithParam<string>((Expression<Func<TourMapPartViewModel, string>>) (x => x.Language), TourPlaybackManager.Instance.TourPlaybackLanguage).BuildUri(), (System.Action) null, true));
      PurchaseManager.NotifyConnectionErrorOccurred = (System.Action) (() => ShellServiceFacade.DialogService.ShowToast(AppResources.ErrorPurchaseConnectionErrorMessage, (Uri) null, (System.Action) null, false));
    }

    protected override object GetInstance(Type service, string key)
    {
      if (service != null)
        ModuleConventions.InitializeAssembly(service.Assembly);
      return this._container.GetInstance(service, key);
    }

    protected override IEnumerable<object> GetAllInstances(Type service)
    {
      if (service != null)
        ModuleConventions.InitializeAssembly(service.Assembly);
      return this._container.GetAllInstances(service);
    }

    protected override void BuildUp(object instance) => this._container.BuildUp(instance);

    protected override void OnLaunch(object sender, LaunchingEventArgs e)
    {
      this.RootFrame.FlowDirection = FlowDirectionHelper.GetCurrentFlowDirection();
      IoC.Get<ISettingsService>().Initialize();
      IAnalyticsService analyticsService = IoC.Get<IAnalyticsService>();
      analyticsService.GoogleAnalyticsLaunchApplication();
      analyticsService.AdjustLaunchApplication();
      Module.ConfigureLocalDatabase();
      Bootstrapper.SetupLanguages();
      DownloadManager.Instance.Restore();
      // ISSUE: method pointer
      DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) null, __methodptr(OnDownloadProcessStateChanged));
      TourPlaybackManager.Instance.Restore();
      PurchaseManager.Instance.Initialize();
      RateHelper.Clear();
    }

    protected override void OnActivate(object sender, ActivatedEventArgs e)
    {
      if (!e.IsApplicationInstancePreserved)
      {
        IAnalyticsService analyticsService = IoC.Get<IAnalyticsService>();
        analyticsService.GoogleAnalyticsLaunchApplication();
        analyticsService.AdjustLaunchApplication();
        DownloadManager.Instance.Restore();
        // ISSUE: method pointer
        DownloadManager.Instance.DownloadProcessStateChanged += new TypedEventHandler<DownloadManager, DownloadProcess>((object) null, __methodptr(OnDownloadProcessStateChanged));
        TourPlaybackManager.Instance.Restore();
      }
      PurchaseManager.Instance.Initialize();
      if (ApplicationManager.RunningInBackground)
      {
        Bootstrapper.Logger.Info("Switch to foreground {0}", (object) e.IsApplicationInstancePreserved);
        ApplicationManager.RunningInBackground = false;
      }
      else
        Bootstrapper.Logger.Info("Activate {0}", (object) e.IsApplicationInstancePreserved);
    }

    protected override void OnDeactivate(object sender, DeactivatedEventArgs e)
    {
      Bootstrapper.Logger.Info("Deactivate");
      CustomLogger.Flush();
    }

    protected virtual void OnRunningInBackground(object sender, RunningInBackgroundEventArgs e)
    {
      Bootstrapper.Logger.Info("Switch to background");
      ApplicationManager.RunningInBackground = true;
    }

    protected override void OnUnhandledException(
      object sender,
      ApplicationUnhandledExceptionEventArgs e)
    {
      if (Debugger.IsAttached)
        Debugger.Break();
      Bootstrapper.Logger.Error(e.ExceptionObject);
      CustomLogger.Flush();
    }

    private static void InitializeViewLocator()
    {
      Func<Type, DependencyObject, object, Type> baseLocate = ViewLocator.LocateTypeForModelType;
      ViewLocator.LocateTypeForModelType = (Func<Type, DependencyObject, object, Type>) ((modelType, displayLocation, context) =>
      {
        ViewAttribute viewAttribute = modelType.GetCustomAttributes(typeof (ViewAttribute), false).OfType<ViewAttribute>().FirstOrDefault<ViewAttribute>((Func<ViewAttribute, bool>) (x => x.Context == context));
        return viewAttribute == null ? baseLocate(modelType, displayLocation, context) : viewAttribute.ViewType;
      });
    }

    private void InitializeViewModels()
    {
      this._container.PerRequest<MainViewModel>();
      this._container.PerRequest<FeaturedPartViewModel>();
      this._container.PerRequest<FeaturedListViewModel>();
      this._container.PerRequest<ExploreViewModel>();
      this._container.PerRequest<ProfileViewModel>();
      this._container.PerRequest<ProfileDetailPartViewModel>();
      this._container.PerRequest<ProfileDownloadTabViewModel>();
      this._container.PerRequest<ProfileDownloadListViewModel>();
      this._container.PerRequest<ProfileBookmarkTabViewModel>();
      this._container.PerRequest<ProfileBookmarkListViewModel>();
      this._container.PerRequest<ProfilePurchaseTabViewModel>();
      this._container.PerRequest<ProfilePurchaseListViewModel>();
      this._container.PerRequest<ProfileHistoryTabViewModel>();
      this._container.PerRequest<ProfileHistoryListViewModel>();
      this._container.PerRequest<ProfileQuizTabViewModel>();
      this._container.PerRequest<ProfileQuizListViewModel>();
      this._container.PerRequest<QuickAccessViewModel>();
      this._container.PerRequest<LogViewModel>();
      this._container.PerRequest<RedirectViewModel>();
      this._container.PerRequest<SettingsViewModel>();
      this._container.PerRequest<SettingsAppViewModel>();
      this._container.PerRequest<SettingsAppLocationViewModel>();
      this._container.PerRequest<SettingsAppLanguageViewModel>();
      this._container.PerRequest<SettingsAppLanguageSelectorViewModel>();
      this._container.PerRequest<SettingsAppLicenseViewModel>();
      this._container.PerRequest<SettingsAppAboutViewModel>();
      this._container.PerRequest<SettingsAppFeedbackViewModel>();
      this._container.PerRequest<SettingsAppFeedbackMessageViewModel>();
      this._container.PerRequest<SettingsInternalViewModel>();
      this._container.PerRequest<SettingsInternalServerViewModel>();
      this._container.PerRequest<SettingsInternalTourEmulationViewModel>();
      this._container.PerRequest<DetailPartViewModel>();
      this._container.PerRequest<DetailExhibitListViewModel>();
      this._container.PerRequest<DetailReferenceListViewModel>();
      this._container.PerRequest<DetailReviewListViewModel>();
      this._container.PerRequest<DetailSponsorListViewModel>();
      this._container.PerRequest<RatePartViewModel>();
      this._container.PerRequest<ReviewListPartViewModel>();
      this._container.PerRequest<ReferenceListViewModel>();
      this._container.PerRequest<ReviewListViewModel>();
      this._container.PerRequest<SponsorListViewModel>();
      this._container.PerRequest<InfoPartViewModel>();
      this._container.PerRequest<PlayerViewModel>();
      this._container.PerRequest<PlayerPartViewModel>();
      this._container.RegisterPerRequest(typeof (DetailViewModel), MtgObjectType.Tour.ToString(), typeof (TourDetailViewModel));
      this._container.PerRequest<TourDetailInfoViewModel>();
      this._container.PerRequest<TourDetailRouteViewModel>();
      this._container.PerRequest<TourMapPartViewModel>();
      this._container.RegisterPerRequest(typeof (DetailViewModel), MtgObjectType.TouristAttraction.ToString(), typeof (TouristAttractionDetailViewModel));
      this._container.PerRequest<TouristAttractionListViewModel>();
      this._container.PerRequest<TouristAttractionDetailInfoViewModel>();
      this._container.RegisterPerRequest(typeof (DetailViewModel), MtgObjectType.Museum.ToString(), typeof (MuseumDetailViewModel));
      this._container.PerRequest<MuseumDetailInfoViewModel>();
      this._container.PerRequest<MuseumDetailNewsViewModel>();
      this._container.PerRequest<MuseumDetailCollectionListViewModel>();
      this._container.PerRequest<MuseumMapPartViewModel>();
      this._container.PerRequest<CollectionListViewModel>();
      this._container.RegisterPerRequest(typeof (DetailViewModel), MtgObjectType.Collection.ToString(), typeof (CollectionDetailViewModel));
      this._container.PerRequest<CollectionDetailInfoViewModel>();
      this._container.PerRequest<ExhibitListViewModel>();
      this._container.RegisterPerRequest(typeof (DetailViewModel), MtgObjectType.Exhibit.ToString(), typeof (ExhibitDetailViewModel));
      this._container.PerRequest<ExhibitDetailInfoViewModel>();
      this._container.PerRequest<PublisherDetailPartViewModel>();
      this._container.PerRequest<PublisherDetailViewModel>();
      this._container.PerRequest<PublisherDetailInfoViewModel>();
      this._container.PerRequest<PublisherDetailContentViewModel>();
      this._container.PerRequest<PublisherDetailContentListViewModel>();
      this._container.PerRequest<QuizPartViewModel>();
      this._container.PerRequest<MediaPlayerPartViewModel>();
      this._container.RegisterPerRequest(typeof (MediaPlayerViewModel), MediaFormat.Image.ToString(), typeof (ImageMediaPlayerViewModel));
      this._container.RegisterPerRequest(typeof (MediaPlayerViewModel), MediaFormat.Video.ToString(), typeof (VideoMediaPlayerViewModel));
    }

    private static void SetupLanguages()
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      if (appSettings.Languages != null && appSettings.Languages.Length != 0)
        return;
      System.Collections.Generic.List<string> stringList = new System.Collections.Generic.List<string>();
      LanguageData languageByIsoCode = ServiceFacade.CultureService.GetLanguageByIsoCode(CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
      if (languageByIsoCode != null)
        stringList.Add(languageByIsoCode.Code.ToLower());
      if (!stringList.Contains("en"))
        stringList.Add("en");
      appSettings.Languages = stringList.ToArray();
      ServiceFacade.SettingsService.SaveAppSettings(appSettings);
    }

    private void OnRootFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
      if (this._reset && e.IsCancelable && e.Uri.OriginalString == "Views/MainView.xaml")
      {
        e.Cancel = true;
        this._reset = false;
      }
      else
      {
        try
        {
          GC.Collect();
          GC.WaitForPendingFinalizers();
        }
        catch (Exception ex)
        {
          Bootstrapper.Logger.Error(ex);
        }
      }
    }

    private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
    {
      this._reset = e.NavigationMode == NavigationMode.Reset;
      FrameNavigationContext.Instance.SetContext(e.Uri, e.Content, e.NavigationMode);
    }

    private static void OnDownloadProcessStateChanged(
      DownloadManager manager,
      DownloadProcess process)
    {
      ((System.Action) (() =>
      {
        switch (process.State)
        {
          case DownloadProcessState.Downloading:
            if (process.IsRestored)
              break;
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadStarted, (object) process.Title), (Uri) null, (System.Action) null, false);
            break;
          case DownloadProcessState.Downloaded:
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadCompleted, (object) process.Title), (Uri) null, (System.Action) null, false);
            break;
          case DownloadProcessState.Removing:
            Bootstrapper.StopProcessAudio(process);
            break;
          case DownloadProcessState.Removed:
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadRemoved, (object) process.Title), (Uri) null, (System.Action) null, false);
            break;
          case DownloadProcessState.Updating:
            Bootstrapper.StopProcessAudio(process);
            if (process.IsRestored)
              break;
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadUpdateStarted, (object) process.Title), (Uri) null, (System.Action) null, false);
            break;
          case DownloadProcessState.Updated:
            if (!string.IsNullOrWhiteSpace(process.Key))
            {
              System.Collections.Generic.List<string> stringList = PhoneStateHelper.GetParameter<System.Collections.Generic.List<string>>("UpdateSuspendList") ?? new System.Collections.Generic.List<string>();
              if (stringList.Contains(process.Key))
                stringList.Remove(process.Key);
            }
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadUpdateCompleted, (object) process.Title), (Uri) null, (System.Action) null, false);
            break;
          case DownloadProcessState.Error:
            if (process.IsRestored || process.Error == DownloadProcessError.ProcessCanceled)
              break;
            ShellServiceFacade.DialogService.ShowToast(string.Format(AppResources.ToastDownloadError, (object) process.Title), (Uri) null, (System.Action) null, false, "IziTravelVioletBrush");
            break;
        }
      })).OnUIThread();
    }

    private static void StopProcessAudio(DownloadProcess process)
    {
      AudioTrackInfo nowPlaying = ServiceFacade.AudioService.NowPlaying;
      if (nowPlaying == null || !(nowPlaying.MtgObjectUid == process.Uid) && !(nowPlaying.MtgParentUid == process.Uid))
        return;
      ServiceFacade.AudioService.Stop();
      ServiceFacade.AudioService.SetNowPlaying((AudioTrackInfo) null);
    }
  }
}
