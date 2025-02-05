// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Controls.VideoExternalPlayer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Media.Controls
{
  [TemplatePart(Name = "PartWebBrowser", Type = typeof (WebBrowser))]
  public class VideoExternalPlayer : Control
  {
    private const string PartWebBrowser = "PartWebBrowser";
    private WebBrowser _webBrowser;
    public static readonly DependencyProperty UrlProperty = DependencyProperty.Register(nameof (Url), typeof (string), typeof (VideoExternalPlayer), new PropertyMetadata((object) null, new PropertyChangedCallback(VideoExternalPlayer.OnUrlPropertyChanged)));

    public string Url
    {
      get => (string) this.GetValue(VideoExternalPlayer.UrlProperty);
      set => this.SetValue(VideoExternalPlayer.UrlProperty, (object) value);
    }

    public VideoExternalPlayer() => this.DefaultStyleKey = (object) typeof (VideoExternalPlayer);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.Unloaded += new RoutedEventHandler(this.OnUnloaded);
      this._webBrowser = this.GetTemplateChild("PartWebBrowser") as WebBrowser;
      this.Play(this.Url);
    }

    private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      if (this._webBrowser == null)
        return;
      this._webBrowser.Navigate(new Uri("about:blank"));
    }

    private void Play(string url)
    {
      if (this._webBrowser == null || string.IsNullOrWhiteSpace(url))
        return;
      this._webBrowser.IsScriptEnabled = true;
      this._webBrowser.NavigateToString(VideoExternalPlayer.GetNavigateHtml(this.Url));
    }

    private static string GetNavigateHtml(string url)
    {
      return "<!doctype html><html><head><meta name=\"viewport\" content=\"width=480\" /><title></title></head><body>" + string.Format("<iframe style=\"background:#000000; position:fixed; top:0px; left:0px; bottom:0px; right:0px; width:100%; height:100%; border:none; margin:0; padding:0; overflow:hidden; z-index:999999;\" src=\"{0}\"></iframe>", (object) VideoExternalPlayer.GetVideoLink(url)) + "</body></html>";
    }

    private static string GetVideoLink(string url)
    {
      string videoId = YoutubeHelper.GetVideoId(url);
      return !string.IsNullOrWhiteSpace(videoId) ? string.Format("http://www.youtube.com/embed/{0}", (object) videoId) : url;
    }

    private static void OnUrlPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is VideoExternalPlayer videoExternalPlayer))
        return;
      videoExternalPlayer.Play((string) e.NewValue);
    }
  }
}
