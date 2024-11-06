// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Controls.VideoPlayer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

#nullable disable
namespace Izi.Travel.Shell.Media.Controls
{
  [TemplateVisualState(Name = "Buffering", GroupName = "PlayerStates")]
  [TemplateVisualState(Name = "Ready", GroupName = "PlayerStates")]
  [TemplateVisualState(Name = "Playing", GroupName = "PlayerStates")]
  [TemplateVisualState(Name = "Paused", GroupName = "PlayerStates")]
  [TemplateVisualState(Name = "ControlPanelVisible", GroupName = "ControlPanelStates")]
  [TemplateVisualState(Name = "ControlPanelHidden", GroupName = "ControlPanelStates")]
  [TemplatePart(Name = "PartMediaElement", Type = typeof (MediaElement))]
  [TemplatePart(Name = "PartButtonControlPlay", Type = typeof (ActionRoundButton))]
  [TemplatePart(Name = "PartButtonControlPause", Type = typeof (ActionRoundButton))]
  [TemplatePart(Name = "PartSliderControl", Type = typeof (Slider))]
  public class VideoPlayer : Control
  {
    private const string VisualStateGroupPlayerStates = "PlayerStates";
    private const string VisualStateGroupControlPanelStates = "ControlPanelStates";
    private const string VisualStateBuffering = "Buffering";
    private const string VisualStateReady = "Ready";
    private const string VisualStatePlaying = "Playing";
    private const string VisualStatePaused = "Paused";
    private const string VisualStateControlPanelVisible = "ControlPanelVisible";
    private const string VisualStateControlPanelHidden = "ControlPanelHidden";
    private const string PartMediaElement = "PartMediaElement";
    private const string PartButtonControlPlay = "PartButtonControlPlay";
    private const string PartButtonControlPause = "PartButtonControlPause";
    private const string PartSliderControl = "PartSliderControl";
    private MediaElement _mediaElement;
    private ActionRoundButton _buttonPlay;
    private ActionRoundButton _buttonPause;
    private Slider _slider;
    private DispatcherTimer _positionTimer;
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (VideoPlayer), new PropertyMetadata((object) null));
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof (Source), typeof (Uri), typeof (VideoPlayer), new PropertyMetadata((object) null));

    public string Title
    {
      get => (string) this.GetValue(VideoPlayer.TitleProperty);
      set => this.SetValue(VideoPlayer.TitleProperty, (object) value);
    }

    public Uri Source
    {
      get => (Uri) this.GetValue(VideoPlayer.SourceProperty);
      set => this.SetValue(VideoPlayer.SourceProperty, (object) value);
    }

    public VideoPlayer() => this.DefaultStyleKey = (object) typeof (VideoPlayer);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._mediaElement = this.GetTemplateChild("PartMediaElement") as MediaElement;
      if (this._mediaElement != null)
      {
        this._mediaElement.Width = Application.Current.Host.Content.ActualHeight;
        this._mediaElement.Height = Application.Current.Host.Content.ActualWidth;
        this._mediaElement.AutoPlay = false;
        this._mediaElement.MediaOpened += new RoutedEventHandler(this.OnMediaElementMediaOpened);
        this._mediaElement.MediaEnded += new RoutedEventHandler(this.OnMediaElementMediaEnded);
        this._mediaElement.CurrentStateChanged += new RoutedEventHandler(this.OnMediaElementCurrentStateChanged);
      }
      this._buttonPlay = this.GetTemplateChild("PartButtonControlPlay") as ActionRoundButton;
      if (this._buttonPlay != null)
        this._buttonPlay.Click += new RoutedEventHandler(this.OnButtonPlayClick);
      this._buttonPause = this.GetTemplateChild("PartButtonControlPause") as ActionRoundButton;
      if (this._buttonPause != null)
        this._buttonPause.Click += new RoutedEventHandler(this.OnButtonPauseClick);
      this._slider = this.GetTemplateChild("PartSliderControl") as Slider;
      if (this._slider != null)
      {
        this._positionTimer = new DispatcherTimer()
        {
          Interval = TimeSpan.FromMilliseconds(500.0)
        };
        this._positionTimer.Tick += new EventHandler(this.OnPositionTimerTick);
        this._slider.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnSliderManipulationStarted);
        this._slider.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnSliderManipulationCompleted);
      }
      this.Tap += new EventHandler<GestureEventArgs>(this.OnTap);
      VisualStateManager.GoToState((Control) this, "Buffering", false);
      VisualStateManager.GoToState((Control) this, "ControlPanelVisible", false);
    }

    private void OnMediaElementMediaOpened(object sender, RoutedEventArgs e)
    {
      VisualStateManager.GoToState((Control) this, "Ready", true);
      if (this._slider == null)
        return;
      this._slider.Value = 0.0;
      this._slider.Maximum = this._mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
    }

    private void OnMediaElementMediaEnded(object sender, RoutedEventArgs e)
    {
      VisualStateManager.GoToState((Control) this, "Ready", true);
    }

    private void OnMediaElementCurrentStateChanged(object sender, RoutedEventArgs e)
    {
      switch (this._mediaElement.CurrentState)
      {
        case MediaElementState.Closed:
        case MediaElementState.Stopped:
          this._positionTimer.Stop();
          VisualStateManager.GoToState((Control) this, "Ready", true);
          break;
        case MediaElementState.Opening:
        case MediaElementState.Buffering:
          this._positionTimer.Stop();
          VisualStateManager.GoToState((Control) this, "Buffering", true);
          break;
        case MediaElementState.Playing:
          this._positionTimer.Start();
          VisualStateManager.GoToState((Control) this, "Playing", true);
          break;
        case MediaElementState.Paused:
          this._positionTimer.Stop();
          VisualStateManager.GoToState((Control) this, "Paused", true);
          break;
      }
    }

    private void OnButtonPlayClick(object sender, RoutedEventArgs e) => this._mediaElement.Play();

    private void OnButtonPauseClick(object sender, RoutedEventArgs e)
    {
      if (!this._mediaElement.CanPause)
        return;
      this._mediaElement.Pause();
    }

    private void OnSliderManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      if (!this._mediaElement.CanSeek)
        return;
      this._positionTimer.Stop();
    }

    private void OnSliderManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (!this._mediaElement.CanSeek)
        return;
      this._mediaElement.Position = TimeSpan.FromSeconds(this._slider.Value);
      this._positionTimer.Start();
    }

    private void OnTap(object sender, GestureEventArgs e)
    {
    }

    private void OnPositionTimerTick(object sender, EventArgs e)
    {
      this._slider.Value = this._mediaElement.Position.TotalSeconds;
    }
  }
}
