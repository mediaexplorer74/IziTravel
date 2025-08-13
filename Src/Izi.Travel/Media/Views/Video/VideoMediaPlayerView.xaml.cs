using System;
using Windows.Media.Playback;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Izi.Travel.Media.ViewModels.Video;

#nullable disable
namespace Izi.Travel.Media.Views.Video
{
    public sealed partial class VideoMediaPlayerView : UserControl
    {
        private bool _isMediaPlayerInitialized = false;

        public VideoMediaPlayerView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is VideoMediaPlayerItemViewModel viewModel)
            {
                if (!viewModel.IsExternal)
                {
                    InitializeMediaPlayer(viewModel.Url);
                }
            }
        }

        private void InitializeMediaPlayer(string mediaUrl)
        {
            if (_isMediaPlayerInitialized || string.IsNullOrEmpty(mediaUrl))
                return;

            try
            {
                var mediaSource = Windows.Media.Core.MediaSource.CreateFromUri(new Uri(mediaUrl));
                MediaPlayer.Source = mediaSource;
                _isMediaPlayerInitialized = true;
            }
            catch (Exception ex)
            {
                // Log error or handle as needed
                System.Diagnostics.Debug.WriteLine($"Failed to initialize media player: {ex.Message}");
            }
        }

        private async void PlayExternalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is VideoMediaPlayerItemViewModel viewModel && !string.IsNullOrEmpty(viewModel.Url))
            {
                try
                {
                    await Launcher.LaunchUriAsync(new Uri(viewModel.Url));
                }
                catch (Exception ex)
                {
                    // Log error or handle as needed
                    System.Diagnostics.Debug.WriteLine($"Failed to launch external player: {ex.Message}");
                }
            }
        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            // Clean up media resources when control is unloaded
            if (MediaPlayer != null)
            {
                MediaPlayer.Source = null;
                _isMediaPlayerInitialized = false;
            }
        }
    }
}

