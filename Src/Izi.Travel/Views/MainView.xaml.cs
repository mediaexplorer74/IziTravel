
using Caliburn.Micro;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Managers;
using Izi.Travel.Core.Controls.Flyout;
using Izi.Travel.Core.Resources;
using Izi.Travel.Core.Services;
using Izi.Travel.Core.Services.Entities;
using Izi.Travel.ViewModels;
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
using System.ComponentModel;




namespace Izi.Travel.Views
{
    public sealed partial class MainView : Page, Caliburn.Micro.IScreen
    {
        public string DisplayName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsActive => throw new NotImplementedException();

        public bool IsNotifying { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public MainView()
        {
            this.InitializeComponent();
            this.Loaded += MainView_Loaded;
        }

        public event EventHandler<ActivationEventArgs> Activated;
        public event EventHandler<DeactivationEventArgs> AttemptingDeactivation;
        public event EventHandler<DeactivationEventArgs> Deactivated;
        public event PropertyChangedEventHandler PropertyChanged;

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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //if (VisualTreeHelper.GetOpenPopups().Any<Popup>() || !(this.DataContext is MainViewModel dataContext))
            //    return;

            var dataContext = this.DataContext as MainViewModel;

            if (dataContext.ActiveItem != dataContext.ExploreViewModel)
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
            }
        }

        public void Activate()
        {
            throw new NotImplementedException();
        }

        public void Deactivate(bool close)
        {
            throw new NotImplementedException();
        }

        public void CanClose(Action<bool> callback)
        {
            throw new NotImplementedException();
        }

        public void TryClose(bool? dialogResult = null)
        {
            throw new NotImplementedException();
        }

        public void NotifyOfPropertyChange(string propertyName)
        {
            throw new NotImplementedException();
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}

