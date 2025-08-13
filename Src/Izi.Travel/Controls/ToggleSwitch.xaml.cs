using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace Izi.Travel.Controls
{
    [ContentProperty(Name = "Header")]
    public sealed partial class ToggleSwitch : UserControl
    {
        public static readonly DependencyProperty IsCheckedProperty =
            DependencyProperty.Register(
                nameof(IsChecked),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(false, OnIsCheckedChanged));

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register(
                nameof(Header),
                typeof(object),
                typeof(ToggleSwitch),
                new PropertyMetadata(null));

        public static readonly DependencyProperty ShowHeaderProperty =
            DependencyProperty.Register(
                nameof(ShowHeader),
                typeof(bool),
                typeof(ToggleSwitch),
                new PropertyMetadata(true));

        public event RoutedEventHandler Toggled;

        public bool IsChecked
        {
            get => (bool)GetValue(IsCheckedProperty);
            set => SetValue(IsCheckedProperty, value);
        }

        public object Header
        {
            get => GetValue(HeaderProperty);
            set => SetValue(HeaderProperty, value);
        }

        public bool ShowHeader
        {
            get => (bool)GetValue(ShowHeaderProperty);
            set => SetValue(ShowHeaderProperty, value);
        }

        public ToggleSwitch()
        {
            this.InitializeComponent();
        }

        private static void OnIsCheckedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ToggleSwitch toggleSwitch && e.NewValue is bool isChecked)
            {
                VisualStateManager.GoToState(toggleSwitch, isChecked ? "Checked" : "Unchecked", true);
            }
        }

        private void ToggleSwitchControl_Toggled(object sender, RoutedEventArgs e)
        {
            if (sender is Windows.UI.Xaml.Controls.ToggleSwitch toggleSwitch)
            {
                IsChecked = toggleSwitch.IsOn;
                Toggled?.Invoke(this, e);
            }
        }
    }
}
