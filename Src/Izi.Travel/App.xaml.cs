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

using System.Diagnostics;
using System.Globalization;

using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using Caliburn.Micro;
using Newtonsoft.Json;

namespace Izi.Travel.Shell
{
    sealed partial class App : Application
    {
        private WinRTContainer container;
        private bool _reset;
        private static readonly ILog Logger;

        static App()
        {
            LogManager.GetLog = (Func<Type, ILog>)(type => (ILog)new CustomLogger(type));
            App.Logger = LogManager.GetLog(typeof(App));
        }

        public App()
        {
            InitializeComponent();
            Suspending += OnSuspending;
            Resuming += OnResuming;
            ThemeHelper.OverrideSystemColors();
        }

        protected override void Configure()
        {
            container = new WinRTContainer();
            container.RegisterWinRTServices();
            container.PerRequest<MainPageViewModel>();
            
            // Configure Caliburn.Micro for UWP
            MessageBinder.SpecialValues.Add("$clickeditem", 
                context => ((ItemClickEventArgs)context.EventArgs).ClickedItem);
            
            // Register frame navigation service with suspension handling
            var navigationService = new FrameAdapter(container.GetNavigationService());
            container.Instance<INavigationService>(navigationService);
        }

        protected override void PrepareViewFirst(Frame rootFrame)
        {
            container.RegisterNavigationService(rootFrame);
            DisplayRootView<MainPageView>();
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
            var processes = ServiceFacade.DownloadManager.GetProcesses().Select(p => new
            {
                p.Uid,
                p.Key,
                p.Title,
                State = p.State.ToString(),
                p.IsRestored,
                Error = p.Error?.ToString(),
                Content = p.Content?.Select(c => c.Uid).ToList()
            }).ToList();
            
            localSettings.Values["DownloadProcesses"] = JsonConvert.SerializeObject(processes);
            
            // Save app settings
            var appSettings = ServiceFacade.SettingsService.GetAppSettings();
            localSettings.Values["AppSettings"] = JsonConvert.SerializeObject(appSettings);
            
            // Save navigation state
            var navigationService = (FrameAdapter)container.GetInstance<INavigationService>();
            localSettings.Values["NavigationState"] = JsonConvert.SerializeObject(
                navigationService.GetNavigationState());
        }

        private void RestoreAppStateAsync()
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            
            // Restore navigation state
            if (localSettings.Values.TryGetValue("NavigationState", out object navigationState))
            {
                var navigationService = (FrameAdapter)container.GetInstance<INavigationService>();
                navigationService.RestoreNavigationState((string)navigationState);
            }
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
            _reset = e.NavigationMode == NavigationMode.Reset;
        }

        private static void OnDownloadProcessStateChanged(
            DownloadManager manager,
            DownloadProcess process)
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

    /// <summary>
    /// Custom navigation service adapter for UWP suspension management
    /// </summary>
    public class FrameAdapter : INavigationService
    {
        private readonly Frame _frame;
        private readonly Stack<string> _backStack = new Stack<string>();
        private string _currentViewKey;

        public string CurrentSource { get; private set; }

        public FrameAdapter(Frame frame)
        {
            _frame = frame;
            _frame.Navigated += OnFrameNavigated;
        }

        private void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var viewModel = e.Content?.GetType().Name.Replace("View", "ViewModel");
            if (!string.IsNullOrEmpty(viewModel))
            {
                _backStack.Push(_currentViewKey);
                _currentViewKey = viewModel;
            }
        }

        public void GoBack()
        {
            if (_frame.CanGoBack)
                _frame.GoBack();
        }

        public string GetNavigationState() => JsonConvert.SerializeObject(
            new { Current = _currentViewKey, Stack = _backStack.ToList() });

        public void RestoreNavigationState(string state)
        {
            var navState = JsonConvert.DeserializeObject<dynamic>(state);
            _currentViewKey = navState.Current;
            
            foreach (var item in navState.Stack)
                _backStack.Push(item);
        }

        // Other INavigationService members...
        public void NavigateToViewModel<T>(object parameter = null) where T : class
        {
            var viewName = typeof(T).Name.Replace("Model", "");
            var viewType = Type.GetType($"Izi.Travel.Shell.Views.{viewName}, Izi.Travel.Shell");
            if (viewType != null)
                _frame.Navigate(viewType, parameter);
        }

        public bool CanGoBack => _frame.CanGoBack;
        public void GoForward() => _frame.GoForward();
        public void Refresh() => _frame.Navigate(_frame.SourcePageType, _frame.Tag);
    }
}
