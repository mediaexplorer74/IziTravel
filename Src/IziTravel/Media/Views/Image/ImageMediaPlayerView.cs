// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Views.Image.ImageMediaPlayerView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Media.Views.Image
{
  public class ImageMediaPlayerView : UserControl
  {
    private bool _contentLoaded;

    public ImageMediaPlayerView() => this.InitializeComponent();

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Media/Views/Image/ImageMediaPlayerView.xaml", UriKind.Relative));
    }
  }
}
