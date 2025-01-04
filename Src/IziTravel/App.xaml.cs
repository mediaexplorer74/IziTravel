// IziTravel.App

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using IziTravel.ViewModels;
using Izi.Travel.Shell.Views; //using IziTravel.Views;
using Izi.Travel.Shell.Core.Themes;

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
//using Microsoft.Phone.Controls;
//using Microsoft.Phone.Maps;
//using Microsoft.Phone.Shell;

using System.Diagnostics;
using System.Globalization;

using System.Linq.Expressions;
using System.Threading;
using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Navigation;

using Caliburn.Micro; //using Caliburn.Micro.Extras;



namespace IziTravel
{
    sealed partial class App //: Application
    {
        private WinRTContainer _container;

        private bool _reset;
        private static readonly ILog Logger;

        static App()
        {
            LogManager.GetLog = (Func<Type, ILog>)(type => (ILog)new CustomLogger(type));
            App.Logger = LogManager.GetLog(typeof(App));
        }


        public App()
        {
            this.InitializeComponent();

            ThemeHelper.OverrideSystemColors();

            this.Suspending += OnSuspending;
        }

        protected override void Configure()
        {
            _container = new WinRTContainer();

            _container.RegisterWinRTServices();

            MessageBinder.SpecialValues.Add("$clickeditem", 
                c => ((ItemClickEventArgs)c.EventArgs).ClickedItem);

            _container.PerRequest<MainPageViewModel>();
        }


     

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            _container.RegisterNavigationService(rootFrame);
        }



        private static void SetupLanguages()
        {
            //RnD
            /*
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
            */
        }

        private void OnRootFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (this._reset /*&& e.IsCancelable && e.Uri.OriginalString == "Views/MainView.xaml"*/)
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
                    App.Logger.Error(ex);
                }
            }
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            this._reset = e.NavigationMode == default;//NavigationMode.Reset;
            //FrameNavigationContext.Instance.SetContext(e.Uri, e.Content, e.NavigationMode);
        }


        private static void OnDownloadProcessStateChanged(
         DownloadManager manager,
         DownloadProcess process)
        {
            ((System.Action)(() =>
            {
                switch (process.State)
                {
                    case DownloadProcessState.Downloading:
                        if (process.IsRestored)
                            break;
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadStarted,
                        //    (object)process.Title), (Uri)null, (System.Action)null, false);
                        break;
                    case DownloadProcessState.Downloaded:
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadCompleted,
                        //    (object)process.Title), (Uri)null, (System.Action)null, false);
                        break;
                    case DownloadProcessState.Removing:
                        App.StopProcessAudio(process);
                        break;
                    case DownloadProcessState.Removed:
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadRemoved,
                        //    (object)process.Title), (Uri)null, (System.Action)null, false);
                        break;
                    case DownloadProcessState.Updating:
                        App.StopProcessAudio(process);
                        if (process.IsRestored)
                            break;
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadUpdateStarted,
                        //    (object)process.Title), (Uri)null, (System.Action)null, false);
                        break;
                    case DownloadProcessState.Updated:
                        if (!string.IsNullOrWhiteSpace(process.Key))
                        {
                            //System.Collections.Generic.List<string> stringList
                            //= PhoneStateHelper.GetParameter<System.Collections.Generic.List<string>>
                            //("UpdateSuspendList") ?? new System.Collections.Generic.List<string>();

                            //if (stringList.Contains(process.Key))
                            //    stringList.Remove(process.Key);
                        }
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadUpdateCompleted,
                        //    (object)process.Title), (Uri)null, (System.Action)null, false);
                        break;
                    case DownloadProcessState.Error:
                        if (process.IsRestored || process.Error == DownloadProcessError.ProcessCanceled)
                            break;
                        //ShellServiceFacade.DialogService.ShowToast(
                        //    string.Format(AppResources.ToastDownloadError, (object)process.Title),
                        //    (Uri)null, (System.Action)null, false, "IziTravelVioletBrush");
                        break;
                }
            })).OnUIThread();
        }


        private static void StopProcessAudio(DownloadProcess process)
        {
            AudioTrackInfo nowPlaying = ServiceFacade.AudioService.NowPlaying;
            if (nowPlaying == null || !(nowPlaying.MtgObjectUid == process.Uid)
                && !(nowPlaying.MtgParentUid == process.Uid))
                return;
            ServiceFacade.AudioService.Stop();
            ServiceFacade.AudioService.SetNowPlaying((AudioTrackInfo)null);
        }



        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            DisplayRootView<MainPageView>();
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }


        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }


    }
}


