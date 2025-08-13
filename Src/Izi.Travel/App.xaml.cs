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
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Izi.Travel.Core.Themes;
using Izi.Travel.Business;
using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Media;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Core.Services;
using Izi.Travel.Geofencing;
using Izi.Travel.Geofencing.Geotracker;
using Izi.Travel.Core;
using Izi.Travel.Core.Attributes;
using Izi.Travel.Core.Context;
using Izi.Travel.Core.Helpers;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services.Contract;
using Izi.Travel.Core.Services.Implementation;
using Izi.Travel.Media.ViewModels;
using Izi.Travel.Media.ViewModels.Image;
using Izi.Travel.Media.ViewModels.Video;
using Izi.Travel.Mtg.Helpers;
using Izi.Travel.Mtg.ViewModels.Collection.Detail;
using Izi.Travel.Mtg.ViewModels.Collection.List;
using Izi.Travel.Mtg.ViewModels.Common;
using Izi.Travel.Mtg.ViewModels.Common.Detail;
using Izi.Travel.Mtg.ViewModels.Common.List;
using Izi.Travel.Mtg.ViewModels.Common.Player;
using Izi.Travel.Mtg.ViewModels.Exhibit.Detail;
using Izi.Travel.Mtg.ViewModels.Exhibit.List;
using Izi.Travel.Mtg.ViewModels.Museum.Detail;
using Izi.Travel.Mtg.ViewModels.Museum.Map;
using Izi.Travel.Mtg.ViewModels.Publisher.Detail;
using Izi.Travel.Mtg.ViewModels.Quiz;
using Izi.Travel.Mtg.ViewModels.Tour.Detail;
using Izi.Travel.Mtg.ViewModels.Tour.Map;
using Izi.Travel.Mtg.ViewModels.TouristAttraction.Detail;
using Izi.Travel.Mtg.ViewModels.TouristAttraction.List;
using Izi.Travel.Settings.ViewModels;
using Izi.Travel.Settings.ViewModels.Application;
using Izi.Travel.Settings.ViewModels.Internal;
using Izi.Travel.ViewModels;
using Izi.Travel.ViewModels.Explore;
using Izi.Travel.ViewModels.Featured;
using Izi.Travel.ViewModels.Profile;
using Izi.Travel.ViewModels.Profile.Bookmark;
using Izi.Travel.ViewModels.Profile.Download;
using Izi.Travel.ViewModels.Profile.History;
using Izi.Travel.ViewModels.Profile.Purchase;
using Izi.Travel.ViewModels.Profile.Quiz;
using Izi.Travel.ViewModels.QuickAccess;
using Izi.Travel.Utility;

using System.Diagnostics;
using System.Globalization;

using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;
using Newtonsoft.Json;
using Izi.Travel.Mtg.Interfaces;
using Windows.Services.Maps;
using Izi.Travel.Views;

namespace Izi.Travel
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App //: CaliburnApplication
    {
        private WinRTContainer container;
        private bool _reset;
        private static readonly ILog Logger;

        static App()
        {
            //LogManager.GetLog = (Func<Type, ILog>)(type => (ILog)new CustomLogger(type));
            //App.Logger = LogManager.GetLog(typeof(App));
        }

        public App()
        {
            InitializeComponent();
            //Suspending += OnSuspending;
            //Resuming += OnResuming;

            //ThemeHelper.OverrideSystemColors();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();

            container.RegisterWinRTServices();
            
            container.PerRequest<MainPageViewModel>();
            
            // Register the UWP phone service
            container.Singleton<IPhoneService, UwpPhoneService>();
            
            // Register map services
            //container.Singleton<IMapService, MapService>();
            
            // Register view models (old place)
            container.PerRequest<DetailPartViewModel>();
            container.PerRequest<TourMapPartViewModel>();
            
            // Configure Caliburn.Micro for UWP
            //MessageBinder.SpecialValues.Add("$clickeditem", 
            //    context => ((ItemClickEventArgs)context.EventArgs).ClickedItem);

            // Register view models (experimental - new place)
            //container.PerRequest<DetailPartViewModel>();
            //container.PerRequest<TourMapPartViewModel>();
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            // Register the navigation service with Caliburn.Micro's container
            container.RegisterNavigationService(rootFrame);
            
            // Initialize our ShellServiceFacade with the root frame
            ShellServiceFacade.Initialize(rootFrame);

            // Set up navigation events if needed (-)
            //rootFrame.Navigating += OnRootFrameNavigating;
            //rootFrame.Navigated += OnRootFrameNavigated;

            // Display the root view
            //DisplayRootView<MainPageView>();
            //DisplayRootView<MainView>();
        }

        // !
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            //    DisplayRootView<MainPageView>();
            DisplayRootView<MainView>();
        }

        // !
        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        // !
        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }


        // !
        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }


        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);

            if (args.Kind == ActivationKind.Protocol)
            {
                var protocolArgs = (ProtocolActivatedEventArgs)args;
                var uri = protocolArgs.Uri;

                if (uri.IsWellFormedOriginalString())
                {
                    string str = System.Net.WebUtility.UrlDecode(uri.OriginalString);
                    if (str.StartsWith("izi-travel:")) // Assuming "izi-travel" is your protocol scheme
                    {
                        // Remove the protocol scheme to get the path similar to WP8 UriMapper
                        string path = str.Substring("izi-travel:".Length);

                        // Re-use the logic from the old UriMapper
                        MtgLinkInfo mtgLinkInfo = MtgLinkHelper.Parse(path);

                        if (mtgLinkInfo == null)
                        {
                            ShellServiceFacade.NavigationService.UriFor<MainViewModel>().Navigate();
                        }
                        else
                        {
                            if (string.Equals(mtgLinkInfo.Language, "any", StringComparison.CurrentCultureIgnoreCase))
                                mtgLinkInfo.Language = null;

                            ShellServiceFacade.NavigationService.UriFor<RedirectViewModel>()
                                .WithParam<string>(x => x.Uid, mtgLinkInfo.Uid)
                                .WithParam<string>(x => x.Language, mtgLinkInfo.Language)
                                .Navigate();
                        }
                    }
                }
            }

            // Ensure the current window is active
            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            try
            {
                await SaveAppStateAsync();
            }
            finally
            {
                deferral.Complete();
            }
        }

        private void OnResuming(object sender, object e)
        {
            RestoreAppStateAsync();
        }

        private async Task SaveAppStateAsync()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            // Save download processes

            localSettings.Values["DownloadProcesses"] = default;

            try
            {
                localSettings.Values["DownloadProcesses"] = JsonConvert.SerializeObject(ServiceFacade.DownloadManager.GetProcesses().Select(p => new
                {
                    p.Uid,
                    p.Key,
                    p.Title,
                    State = p.State.ToString(),
                    p.IsRestored,
                    Error = p.Error?.ToString(),
                    Content = p.Content?.Select(c => c.Uid).ToList()
                }).ToList());
            }
            catch (Exception ex)
            {
                Debug.WriteLine("set localSettings.Values[DownloadProcesses] error: " + ex.Message);
            }

            // Save app settings
            try
            {
                AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
                localSettings.Values["AppSettings"] = JsonConvert.SerializeObject(appSettings);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to save app settings: " + ex.Message);
            }

            // Navigation state saving skipped in UWP port for now
        }

        private void RestoreAppStateAsync()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            // Navigation state restore skipped in UWP port for now
        }

        private static void SetupLanguages()
        {
            var appSettings = ServiceFacade.SettingsService.GetAppSettings();
            if (appSettings.Languages != null && appSettings.Languages.Length > 0)
                return;
                
            var stringList = new List<string>();
            var languageByIsoCode = ServiceFacade.CultureService.GetLanguageByIsoCode(
                CultureInfo.CurrentUICulture.TwoLetterISOLanguageName);
            
            if (languageByIsoCode != null)
                stringList.Add(languageByIsoCode.Code.ToLower());
                
            if (!stringList.Contains("en"))
                stringList.Add("en");
                
            appSettings.Languages = stringList.ToArray();
            ServiceFacade.SettingsService.SaveAppSettings(appSettings);
        }

        private void OnRootFrameNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (_reset)
            {
                e.Cancel = true;
                _reset = false;
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
                    Logger.Error(ex);
                }
            }
        }

        private void OnRootFrameNavigated(object sender, NavigationEventArgs e)
        {
            //_reset = e.NavigationMode == NavigationMode.Reset;
        }

        private static void OnDownloadProcessStateChanged( DownloadManager manager,   DownloadProcess process)
        {
            Execute.OnUIThread(() =>
            {
                switch (process.State)
                {
                    case DownloadProcessState.Downloading:
                        if (process.IsRestored) break;
                        break;
                    case DownloadProcessState.Downloaded:
                        break;
                    case DownloadProcessState.Removing:
                        StopProcessAudio(process);
                        break;
                    case DownloadProcessState.Removed:
                        break;
                    case DownloadProcessState.Updating:
                        StopProcessAudio(process);
                        if (process.IsRestored) break;
                        break;
                    case DownloadProcessState.Updated:
                        if (!string.IsNullOrWhiteSpace(process.Key))
                        {
                            var stringList = new List<string>();
                            // Здесь нужно восстановить логику из PhoneStateHelper если нужно
                        }
                        break;
                    case DownloadProcessState.Error:
                        if (process.IsRestored || process.Error == DownloadProcessError.ProcessCanceled)
                            break;
                        break;
                }
            });
        }

      
        private static void StopProcessAudio(DownloadProcess process)
        {
            var nowPlaying = ServiceFacade.AudioService.NowPlaying;
            if (nowPlaying == null || 
                (nowPlaying.MtgObjectUid != process.Uid && 
                 nowPlaying.MtgParentUid != process.Uid))
                return;
                
            ServiceFacade.AudioService.Stop();
            ServiceFacade.AudioService.SetNowPlaying(null);
        }
    }

   
}
