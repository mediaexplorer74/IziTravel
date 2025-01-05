using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System;
using System.Diagnostics;
using System.Windows;
//using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Media.Views.Audio
{
    public sealed partial class AudioContentView : UserControl
    {
        public static readonly DependencyProperty TitleProperty 
            = DependencyProperty.Register(nameof(Title), typeof(string), typeof(AudioContentView), new PropertyMetadata((object)null));
        //internal UserControl PartAudioContentView;
        //private bool _contentLoaded;

        public string Title
        {
            get => (string)this.GetValue(AudioContentView.TitleProperty);
            set => this.SetValue(AudioContentView.TitleProperty, (object)value);
        }

        public AudioContentView()
        {
            this.InitializeComponent();
        }

       
    }
}

