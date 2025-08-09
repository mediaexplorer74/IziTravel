//using IziTravel.AppServices.ViewModels.Forum;
using Izi.Travel.Shell.AppServices.ViewModels.Forum;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;

namespace Izi.Travel.Shell.Views.Forum
{
public sealed partial class ForumPage : Page
{
    public ForumPage()
    {
        //this.InitializeComponent();
        this.Loaded += OnLoaded;
        this.Unloaded += OnUnloaded;
    }

    public ForumPageViewModel ViewModel => (ForumPageViewModel)DataContext;

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        // Handle any navigation parameters if needed
        if (e.Parameter != null)
        {
            // Process navigation parameters
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        // Subscribe to back button press events
        SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        // Unsubscribe from back button press events
        SystemNavigationManager.GetForCurrentView().BackRequested -= OnBackRequested;
    }

    private void OnBackRequested(object sender, Windows.UI.Core.BackRequestedEventArgs e)
    {
        if (!ViewModel.CanReturnBack) 
            return;
                
        e.Handled = true;
        ViewModel.GoBack();
    }
        //this.Title.IsBackTransition = false;
    }
}



