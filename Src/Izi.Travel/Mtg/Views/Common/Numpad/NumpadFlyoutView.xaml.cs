using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Izi.Travel.Mtg.ViewModels.Common.Numpad;

namespace Izi.Travel.Mtg.Views.Common.Numpad
{
    public sealed partial class NumpadFlyoutView : UserControl
    {
        public NumpadFlyoutView()
        {
            this.InitializeComponent();
            this.DataContextChanged += NumpadFlyoutView_DataContextChanged;
        }

        private void NumpadFlyoutView_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (args.NewValue is NumpadFlyoutViewModel viewModel)
            {
                viewModel.PropertyChanged += (s, e) =>
                {
                    if (e.PropertyName == nameof(NumpadFlyoutViewModel.IsOpen))
                    {
                        if (viewModel.IsOpen)
                        {
                            NumpadFlyout.ShowAt(this);
                        }
                        else
                        {
                            NumpadFlyout.Hide();
                        }
                    }
                };
            }
        }

        private void NumpadFlyout_Opening(object sender, object e)
        {
            if (NumpadContent.Content == null && DataContext is NumpadFlyoutViewModel viewModel)
            {
                // Move the content template to the flyout
                if (this.Resources["NumpadContentTemplate"] is FrameworkElement contentTemplate)
                {
                    contentTemplate.Visibility = Visibility.Visible;
                    NumpadContent.Content = contentTemplate;
                }
            }
        }

        private void NumpadFlyout_Closed(object sender, object e)
        {
            if (DataContext is NumpadFlyoutViewModel viewModel)
            {
                viewModel.IsOpen = false;
            }
        }
    }
}
