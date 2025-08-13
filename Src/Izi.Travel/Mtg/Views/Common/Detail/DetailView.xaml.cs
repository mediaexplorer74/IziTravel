// ********************************************************************
// Type: Izi.Travel.Mtg.Views.Common.Detail.DetailView
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Utility;
using System;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Reflection;

#nullable disable
namespace Izi.Travel.Mtg.Views.Common.Detail
{
    public partial class DetailView : UserControl
    {
        private Flyout _languageFlyout;
        private Button _languageButton;
        
        public DetailView()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Get references to the controls after they're loaded
            _languageButton = this.FindName("ShowLanguageFlyout") as Button;
            _languageFlyout = this.Resources["LanguageFlyout"] as Flyout;
            
            if (_languageFlyout != null && _languageButton != null)
            {
                _languageFlyout.Closed += OnLanguageFlyoutClosed;
            }
        }

        private async void ShowLanguageFlyout_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_languageFlyout != null && _languageButton != null)
                {
                    // Show the flyout
                    _languageFlyout.ShowAt(_languageButton);
                    
                    // Set focus to the first item in the list when the flyout opens
                    if (_languageFlyout.Content is FrameworkElement content && 
                        content.FindName("LanguageListBox") is ListBox listBox && 
                        listBox.Items.Count > 0)
                    {
                        listBox.SelectedIndex = 0;
                        listBox.Focus(FocusState.Programmatic);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error showing language flyout: {ex.Message}");
            }
        }

        private void OnLanguageFlyoutClosed(object sender, object e)
        {
            try
            {
                // Get the ViewModel and call the closed command if it exists
                var viewModel = DataContext;
                if (viewModel != null)
                {
                    // Use reflection to safely access the FlyoutLanguageViewModel property
                    var propertyInfo = viewModel.GetType().GetProperty("FlyoutLanguageViewModel");
                    if (propertyInfo != null)
                    {
                        var flyoutViewModel = propertyInfo.GetValue(viewModel);
                        if (flyoutViewModel is IDisposable disposableViewModel)
                        {
                            // If the ViewModel implements IDisposable, we can dispose it
                            disposableViewModel.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in language flyout closed handler: {ex.Message}");
            }
        }

        ~DetailView()
        {
            // Clean up event handlers
            if (_languageFlyout != null)
            {
                _languageFlyout.Closed -= OnLanguageFlyoutClosed;
            }
            
            this.Loaded -= OnLoaded;
        }
    }
}

