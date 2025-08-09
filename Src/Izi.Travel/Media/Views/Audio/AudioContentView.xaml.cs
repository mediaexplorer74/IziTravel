using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Shell.Media.Views.Audio
{
    public sealed partial class AudioContentView : UserControl
    {
        public static readonly DependencyProperty TitleProperty 
            = DependencyProperty.Register(nameof(Title), typeof(string), typeof(AudioContentView), new PropertyMetadata((object)null));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public AudioContentView()
        {
            InitializeComponent();
        }

       
    }
}

