// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ToastPrompt
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using Clarity.Phone.Extensions;
using Coding4Fun.Toolkit.Controls.Binding;
using Coding4Fun.Toolkit.Controls.Common;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class ToastPrompt : PopUp<string, PopUpResult>, IDisposable, IImageSourceFull, IImageSource
  {
    private const string ToastImageName = "ToastImage";
    protected Image ToastImage;
    private Timer _timer;
    private TranslateTransform _translate;
    public static readonly DependencyProperty MillisecondsUntilHiddenProperty = DependencyProperty.Register(nameof (MillisecondsUntilHidden), typeof (int), typeof (ToastPrompt), new PropertyMetadata((object) 4000));
    public static readonly DependencyProperty IsTimerEnabledProperty = DependencyProperty.Register(nameof (IsTimerEnabled), typeof (bool), typeof (ToastPrompt), new PropertyMetadata((object) true));
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (ToastPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(nameof (Message), typeof (string), typeof (ToastPrompt), new PropertyMetadata((object) ""));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ToastPrompt), new PropertyMetadata(new PropertyChangedCallback(ToastPrompt.OnImageSource)));
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (ToastPrompt), new PropertyMetadata((object) Stretch.None));
    public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof (ImageWidth), typeof (double), typeof (ToastPrompt), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof (ImageHeight), typeof (double), typeof (ToastPrompt), new PropertyMetadata((object) double.NaN));
    public static readonly DependencyProperty TextOrientationProperty = DependencyProperty.Register(nameof (TextOrientation), typeof (Orientation), typeof (ToastPrompt), new PropertyMetadata((object) Orientation.Horizontal));
    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof (TextWrapping), typeof (TextWrapping), typeof (ToastPrompt), new PropertyMetadata((object) TextWrapping.NoWrap, new PropertyChangedCallback(ToastPrompt.OnTextWrapping)));

    public ToastPrompt()
    {
      this.DefaultStyleKey = (object) typeof (ToastPrompt);
      this.IsAppBarVisible = true;
      this.IsBackKeyOverride = true;
      this.IsCalculateFrameVerticalOffset = true;
      this.IsOverlayApplied = false;
      this.AnimationType = DialogService.AnimationTypes.SlideHorizontal;
      this.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.ToastPromptManipulationStarted);
      this.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.ToastPromptManipulationDelta);
      this.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.ToastPromptManipulationCompleted);
      this.Opened += new EventHandler(this.ToastPromptOpened);
    }

    private void ToastPromptManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this.PauseTimer();
    }

    private void ToastPromptManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      this._translate.X += e.DeltaManipulation.Translation.X;
      if (this._translate.X >= 0.0)
        return;
      this._translate.X = 0.0;
    }

    private void ToastPromptManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (e.TotalManipulation.Translation.X > 200.0 || e.FinalVelocities.LinearVelocity.X > 1000.0)
        this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
        {
          PopUpResult = PopUpResult.UserDismissed
        });
      else if (e.TotalManipulation.Translation.X < 20.0)
      {
        this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
        {
          PopUpResult = PopUpResult.Ok
        });
      }
      else
      {
        this._translate.X = 0.0;
        this.StartTimer();
      }
    }

    private void StartTimer()
    {
      if (this._timer == null)
        this._timer = new Timer(new TimerCallback(this.TimerTick));
      this._timer.Change(TimeSpan.FromMilliseconds((double) this.MillisecondsUntilHidden), TimeSpan.FromMilliseconds(-1.0));
    }

    private void PauseTimer()
    {
      if (this._timer == null)
        return;
      this._timer.Change(TimeSpan.FromMilliseconds(-1.0), TimeSpan.FromMilliseconds(-1.0));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetRenderTransform();
      this.ToastImage = this.GetTemplateChild("ToastImage") as Image;
      if (this.ToastImage != null && this.ImageSource != null)
      {
        this.ToastImage.Source = this.ImageSource;
        this.SetImageVisibility(this.ImageSource);
      }
      this.SetTextOrientation(this.TextWrapping);
    }

    private void SetRenderTransform()
    {
      this._translate = new TranslateTransform();
      this.RenderTransform = (Transform) this._translate;
    }

    public override void Show()
    {
      if (!this.IsTimerEnabled)
        return;
      base.Show();
      this.SetRenderTransform();
      PreventScrollBinding.SetIsEnabled((DependencyObject) this, true);
    }

    private void ToastPromptOpened(object sender, EventArgs e) => this.StartTimer();

    private void TimerTick(object state)
    {
      this.Dispatcher.BeginInvoke((Action) (() => this.OnCompleted(new PopUpEventArgs<string, PopUpResult>()
      {
        PopUpResult = PopUpResult.NoResponse
      })));
    }

    public override void OnCompleted(PopUpEventArgs<string, PopUpResult> result)
    {
      PreventScrollBinding.SetIsEnabled((DependencyObject) this, false);
      this.PauseTimer();
      this.Dispose();
      base.OnCompleted(result);
    }

    public void Dispose()
    {
      if (this._timer == null)
        return;
      this._timer.Dispose();
      this._timer = (Timer) null;
    }

    public int MillisecondsUntilHidden
    {
      get => (int) this.GetValue(ToastPrompt.MillisecondsUntilHiddenProperty);
      set => this.SetValue(ToastPrompt.MillisecondsUntilHiddenProperty, (object) value);
    }

    public bool IsTimerEnabled
    {
      get => (bool) this.GetValue(ToastPrompt.IsTimerEnabledProperty);
      set => this.SetValue(ToastPrompt.IsTimerEnabledProperty, (object) value);
    }

    public string Title
    {
      get => (string) this.GetValue(ToastPrompt.TitleProperty);
      set => this.SetValue(ToastPrompt.TitleProperty, (object) value);
    }

    public string Message
    {
      get => (string) this.GetValue(ToastPrompt.MessageProperty);
      set => this.SetValue(ToastPrompt.MessageProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ToastPrompt.ImageSourceProperty);
      set => this.SetValue(ToastPrompt.ImageSourceProperty, (object) value);
    }

    public Stretch Stretch
    {
      get => (Stretch) this.GetValue(ToastPrompt.StretchProperty);
      set => this.SetValue(ToastPrompt.StretchProperty, (object) value);
    }

    public double ImageWidth
    {
      get => (double) this.GetValue(ToastPrompt.ImageWidthProperty);
      set => this.SetValue(ToastPrompt.ImageWidthProperty, (object) value);
    }

    public double ImageHeight
    {
      get => (double) this.GetValue(ToastPrompt.ImageHeightProperty);
      set => this.SetValue(ToastPrompt.ImageHeightProperty, (object) value);
    }

    public Orientation TextOrientation
    {
      get => (Orientation) this.GetValue(ToastPrompt.TextOrientationProperty);
      set => this.SetValue(ToastPrompt.TextOrientationProperty, (object) value);
    }

    public TextWrapping TextWrapping
    {
      get => (TextWrapping) this.GetValue(ToastPrompt.TextWrappingProperty);
      set => this.SetValue(ToastPrompt.TextWrappingProperty, (object) value);
    }

    private static void OnTextWrapping(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is ToastPrompt toastPrompt) || toastPrompt.ToastImage == null)
        return;
      toastPrompt.SetTextOrientation((TextWrapping) e.NewValue);
    }

    private static void OnImageSource(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      if (!(o is ToastPrompt toastPrompt) || toastPrompt.ToastImage == null)
        return;
      toastPrompt.SetImageVisibility(e.NewValue as ImageSource);
    }

    private void SetImageVisibility(ImageSource source)
    {
      this.ToastImage.Visibility = source == null ? Visibility.Collapsed : Visibility.Visible;
    }

    private void SetTextOrientation(TextWrapping value)
    {
      if (value != TextWrapping.Wrap)
        return;
      this.TextOrientation = Orientation.Vertical;
    }
  }
}
