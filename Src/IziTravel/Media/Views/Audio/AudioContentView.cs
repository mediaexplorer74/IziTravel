// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Views.Audio.AudioContentView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Media.Views.Audio
{
  public class AudioContentView : UserControl
  {
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (AudioContentView), new PropertyMetadata((object) null));
    internal UserControl PartAudioContentView;
    private bool _contentLoaded;

    public string Title
    {
      get => (string) this.GetValue(AudioContentView.TitleProperty);
      set => this.SetValue(AudioContentView.TitleProperty, (object) value);
    }

    public AudioContentView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Media/Views/Audio/AudioContentView.xaml", UriKind.Relative));
      this.PartAudioContentView = (UserControl) this.FindName("PartAudioContentView");
    }
  }
}
