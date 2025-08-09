using Caliburn.Micro;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.ViewModels;
using Izi.Travel.Utility;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace Izi.Travel.Shell.Views
{
    public sealed partial class MainView : Page
    {
        public MainView()
        {
            this.InitializeComponent();
            this.Loaded += MainView_Loaded;
        }

        private async void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            await FillAsync();
        }

        private async Task FillAsync()
        {
            try
            {
                string path = Path.Combine("Content", "data");
                var localFolder = ApplicationData.Current.LocalFolder;

                var folder = await localFolder.TryGetItemAsync(path) as StorageFolder;
                if (folder != null)
                {
                    // do something with the folder
                }
                else
                {
                    string directoryName = Path.GetDirectoryName(path);
                    var directoryItem = await ApplicationData.Current.LocalFolder.TryGetItemAsync(directoryName);
                    if (directoryItem != null)
                    {
                        await ApplicationData.Current.LocalFolder.CreateFolderAsync(directoryName, CreationCollisionOption.OpenIfExists);
                    }
                    var fileItem = await ApplicationData.Current.LocalFolder.TryGetItemAsync(path);
                    if (fileItem != null)
                    {
                        return;
                    }

                    //using (var file = await ApplicationData.Current.LocalFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting))
                    //    await file.SetLengthAsync(ApplicationData.Current.LocalFolder.AvailableFreeSpace - 62914560L);

                    StorageFile file = await ApplicationData.Current.LocalFolder.CreateFileAsync(path, CreationCollisionOption.ReplaceExisting);
                    //await file.SetLengthAsync(ApplicationData.Current.LocalFolder.AvailableFreeSpace - 62914560L);
                }
            }
            catch { }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (VisualTreeHelper.GetOpenPopups().Any<Popup>() || !(this.DataContext is MainViewModel dataContext))
            //    return;
            /*if (dataContext.ActiveItem != dataContext.ExploreViewModel)
            {
                MainViewModel mainViewModel = dataContext;
                mainViewModel.ActiveItem = (IScreen)mainViewModel.ExploreViewModel;
            }
            else
            {
                if (TourPlaybackManager.Instance.TourPlaybackState != TourPlaybackState.Started)
                    return;
                ShellServiceFacade.DialogService.Show(ManifestResources.ApplicationTitle,
                    AppResources.PromptAppExitWhenTourStartedInfo, MessageBoxButtonContent.OkCancel, (Action<FlyoutDialog>)null, (Action<FlyoutDialog, MessageBoxResult>)((d, x) =>
                {
                    //if (x != MessageBoxResult.OK)
                    //    return;
                    Application.Current.Exit();
                }));
            }*/
        }
    }
}
