using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Automation.Peers;
using Windows.Foundation;

namespace Izi.Travel.Controls
{
    public sealed partial class PhoneTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(PhoneTextBox), 
                new PropertyMetadata(string.Empty, OnTextChanged));

        public static readonly DependencyProperty PlaceholderTextProperty =
            DependencyProperty.Register("PlaceholderText", typeof(string), typeof(PhoneTextBox), 
                new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty InputScopeProperty =
            DependencyProperty.Register("InputScope", typeof(InputScope), typeof(PhoneTextBox), 
                new PropertyMetadata(CreateDefaultInputScope()));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public string PlaceholderText
        {
            get { return (string)GetValue(PlaceholderTextProperty); }
            set { SetValue(PlaceholderTextProperty, value); }
        }

        public InputScope InputScope
        {
            get { return (InputScope)GetValue(InputScopeProperty); }
            set { SetValue(InputScopeProperty, value); }
        }

        public PhoneTextBox()
        {
            this.InitializeComponent();
            this.Loaded += PhoneTextBox_Loaded;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Clear the text when delete button is clicked
            Text = string.Empty;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Update the Text property when the text changes
            Text = ((TextBox)sender).Text;
        }

        private void ContentElement_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            // Handle scroll viewer view changed if needed
        }

        private void PhoneTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            UpdatePlaceholderTextVisibility();
            UpdateDeleteButtonVisibility();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdatePlaceholderTextVisibility();
            UpdateDeleteButtonVisibility();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Text = string.Empty;
            TextBox.Focus(FocusState.Programmatic);
        }

        private void UpdatePlaceholderTextVisibility()
        {
            if (TextBox != null)
            {
                var placeholder = TextBox.FindName("PlaceholderTextContentPresenter") as ContentPresenter;
                if (placeholder != null)
                {
                    placeholder.Visibility = string.IsNullOrEmpty(Text) ? 
                        Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private void UpdateDeleteButtonVisibility()
        {
            if (TextBox != null)
            {
                var deleteButton = TextBox.FindName("DeleteButton") as Button;
                if (deleteButton != null)
                {
                    deleteButton.Visibility = !string.IsNullOrEmpty(Text) ? 
                        Visibility.Visible : Visibility.Collapsed;
                }
            }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as PhoneTextBox;
            if (control != null)
            {
                control.UpdatePlaceholderTextVisibility();
                control.UpdateDeleteButtonVisibility();
            }
        }

        private void ContentElement_ViewChanged(object sender, Windows.UI.Xaml.Controls.ScrollViewerViewChangedEventArgs e)
        {
            // Empty implementation to satisfy the XAML event handler
            // No specific action needed for this event in the MVP
        }

        private static InputScope CreateDefaultInputScope()
        {
            var inputScope = new InputScope();
            inputScope.Names.Add(new InputScopeName(InputScopeNameValue.Text));
            return inputScope;
        }
    }
}
