//using IziTravel.AppServices.ViewModels.Forum;
using Izi.Travel.AppServices.ViewModels.Forum;
using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Core;
using Windows.UI.Xaml.Media.Animation;

namespace Izi.Travel.Views.Forum
{
public sealed partial class ForumPage : Page
{
    private ContentControl _titleControl;
    private string _currentTitle;

    public ForumPage()
    {
        this.InitializeComponent();
        this.Loaded += OnLoaded;
        this.Unloaded += OnUnloaded;
        this.DataContextChanged += OnDataContextChanged;
    }

    private void OnDataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
    {
        if (args.NewValue is ForumPageViewModel viewModel)
        {
            viewModel.PropertyChanged += ViewModel_PropertyChanged;
        }
    }

    private async void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(ForumPageViewModel.ForumName))
        {
            if (_titleControl != null && DataContext is ForumPageViewModel vm)
            {
                try
                {
                    // Update the content
                    _currentTitle = vm.ForumName;
                    
                    // Trigger the transition out animation
                    VisualStateManager.GoToState(_titleControl, "TransitionOut", true);
                    
                    // Wait for the transition out to complete
                    await Task.Delay(600); // Slightly longer than the animation duration
                    
                    // Trigger the transition in animation
                    VisualStateManager.GoToState(_titleControl, "TransitionIn", true);
                    
                    // Return to normal state after animation completes
                    await Task.Delay(600); // Slightly longer than the animation duration
                    VisualStateManager.GoToState(_titleControl, "Normal", false);
                }
                catch (Exception ex)
                {
                    // Log error or handle it appropriately
                    System.Diagnostics.Debug.WriteLine($"Error in ViewModel_PropertyChanged: {ex.Message}");
                }
            }
        }
    }

    public ForumPageViewModel ViewModel => (ForumPageViewModel)DataContext;

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        
        // Find the title control
        _titleControl = FindName("Title") as ContentControl;
        
        if (DataContext is ForumPageViewModel vm)
        {
            _currentTitle = vm.ForumName;
            
            // Set initial state
            if (_titleControl != null)
            {
                VisualStateManager.GoToState(_titleControl, "Normal", false);
            }
        }
        
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



