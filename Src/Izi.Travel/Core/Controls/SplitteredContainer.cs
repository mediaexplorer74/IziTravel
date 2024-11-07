// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.SplitteredContainer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Extensions;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "PartStackPanel", Type = typeof (StackPanel))]
  [TemplatePart(Name = "PartSplitterContentControl", Type = typeof (ContentControl))]
  [TemplatePart(Name = "PartMainContentControl", Type = typeof (ContentControl))]
  [TemplatePart(Name = "PartAdditionalContentControl", Type = typeof (ContentControl))]
  public sealed class SplitteredContainer : Control
  {
    private const string PartStackPanel = "PartStackPanel";
    private const string PartSplitterContentControl = "PartSplitterContentControl";
    private const string PartMainContentControl = "PartMainContentControl";
    private const string PartAdditionalContentControl = "PartAdditionalContentControl";
    private StackPanel _stackPanel;
    private TranslateTransform _transform;
    private ContentControl _splitterContentControl;
    private ContentControl _mainContentControl;
    private ContentControl _additionalContentControl;
    private DoubleAnimation _animation;
    private Storyboard _storyboard;
    private double _bottom;
    private bool _needToScroll;
    private bool _ignoreManipulation;
    public static readonly DependencyProperty SplitterTemplateProperty = DependencyProperty.Register(nameof (SplitterTemplate), typeof (DataTemplate), typeof (SplitteredContainer), new PropertyMetadata((object) null));
    public static readonly DependencyProperty MainContentTemplateProperty = DependencyProperty.Register(nameof (MainContentTemplate), typeof (DataTemplate), typeof (SplitteredContainer), new PropertyMetadata((object) null));
    public static readonly DependencyProperty AdditionalContentTemplateProperty = DependencyProperty.Register(nameof (AdditionalContentTemplate), typeof (DataTemplate), typeof (SplitteredContainer), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsMainContentSelectedProperty = DependencyProperty.Register(nameof (IsMainContentSelected), typeof (bool), typeof (SplitteredContainer), new PropertyMetadata((object) false, new PropertyChangedCallback(SplitteredContainer.OnIsMainContentSelectedPropertyChanged)));
    public static readonly DependencyProperty MinContentHeightProperty = DependencyProperty.Register(nameof (MinContentHeight), typeof (double), typeof (SplitteredContainer), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(SplitteredContainer.OnMinContentHeightPropertyChanged)));
    public static readonly DependencyProperty IgnoreManipulationProperty = DependencyProperty.RegisterAttached("IgnoreManipulation", typeof (bool), typeof (SplitteredContainer), new PropertyMetadata((object) false));

    public static bool GetIgnoreManipulation(DependencyObject dp)
    {
      return (bool) dp.GetValue(SplitteredContainer.IgnoreManipulationProperty);
    }

    public static void SetIgnoreManipulation(DependencyObject dp, bool value)
    {
      dp.SetValue(SplitteredContainer.IgnoreManipulationProperty, (object) value);
    }

    public DataTemplate SplitterTemplate
    {
      get => (DataTemplate) this.GetValue(SplitteredContainer.SplitterTemplateProperty);
      set => this.SetValue(SplitteredContainer.SplitterTemplateProperty, (object) value);
    }

    public DataTemplate MainContentTemplate
    {
      get => (DataTemplate) this.GetValue(SplitteredContainer.MainContentTemplateProperty);
      set => this.SetValue(SplitteredContainer.MainContentTemplateProperty, (object) value);
    }

    public DataTemplate AdditionalContentTemplate
    {
      get => (DataTemplate) this.GetValue(SplitteredContainer.AdditionalContentTemplateProperty);
      set => this.SetValue(SplitteredContainer.AdditionalContentTemplateProperty, (object) value);
    }

    public bool IsMainContentSelected
    {
      get => (bool) this.GetValue(SplitteredContainer.IsMainContentSelectedProperty);
      set => this.SetValue(SplitteredContainer.IsMainContentSelectedProperty, (object) value);
    }

    public double MinContentHeight
    {
      get => (double) this.GetValue(SplitteredContainer.MinContentHeightProperty);
      set => this.SetValue(SplitteredContainer.MinContentHeightProperty, (object) value);
    }

    public SplitteredContainer()
    {
      this.DefaultStyleKey = (object) typeof (SplitteredContainer);
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._stackPanel = this.GetTemplateChild("PartStackPanel") as StackPanel;
      if (this._stackPanel == null)
        return;
      this._transform = this._stackPanel.RenderTransform as TranslateTransform;
      if (this._transform == null)
      {
        this._transform = new TranslateTransform();
        this._stackPanel.RenderTransform = (Transform) this._transform;
      }
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.Duration = (Duration) TimeSpan.FromMilliseconds(200.0);
      this._animation = doubleAnimation;
      this._storyboard = new Storyboard();
      Storyboard.SetTarget((Timeline) this._animation, (DependencyObject) this._transform);
      Storyboard.SetTargetProperty((Timeline) this._animation, new PropertyPath((object) TranslateTransform.YProperty));
      this._storyboard.Children.Add((Timeline) this._animation);
      this._splitterContentControl = this.GetTemplateChild("PartSplitterContentControl") as ContentControl;
      if (this._splitterContentControl != null)
      {
        this._splitterContentControl.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
        this._splitterContentControl.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnSplitterManipulationStarted);
        this._splitterContentControl.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnSplitterManipulationDelta);
        this._splitterContentControl.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnSplitterManipulationCompleted);
      }
      this._mainContentControl = this.GetTemplateChild("PartMainContentControl") as ContentControl;
      this._additionalContentControl = this.GetTemplateChild("PartAdditionalContentControl") as ContentControl;
    }

    private void SetupContentSize()
    {
      if (this._mainContentControl == null || this._additionalContentControl == null || this._splitterContentControl == null || this._transform == null)
        return;
      double num = this.ActualHeight - this.MinContentHeight - this._splitterContentControl.ActualHeight;
      if (num < 0.0)
        num = 0.0;
      if (num > this.ActualHeight)
        num = this.ActualHeight;
      this._bottom = this.ActualHeight - 2.0 * (this.MinContentHeight + this._splitterContentControl.ActualHeight);
      if (this._transform.Y > 0.0)
        this._transform.Y = 0.0;
      if (this._transform.Y < -this._bottom)
        this._transform.Y = -this._bottom;
      this._mainContentControl.Height = num;
      this._additionalContentControl.Height = num;
    }

    private void AnimateScrollToOffset(double offset)
    {
      this._animation.From = new double?(this._transform.Y);
      this._animation.To = new double?(offset);
      this._storyboard.Begin();
    }

    private void SetIsMainContentSelected(bool value)
    {
      this._needToScroll = false;
      this.IsMainContentSelected = value;
      this._needToScroll = true;
    }

    private static void OnMinContentHeightPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(d is SplitteredContainer splitteredContainer))
        return;
      splitteredContainer.SetupContentSize();
    }

    private static void OnIsMainContentSelectedPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs args)
    {
      if (!(d is SplitteredContainer splitteredContainer) || !splitteredContainer._needToScroll)
        return;
      splitteredContainer.AnimateScrollToOffset((bool) args.NewValue ? 0.0 : -splitteredContainer._bottom);
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      this.SetIsMainContentSelected(true);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) => this.SetupContentSize();

    private void OnSplitterManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this._ignoreManipulation = e.ManipulationContainer.FindParent(new Func<DependencyObject, bool>(SplitteredContainer.GetIgnoreManipulation)) != null;
    }

    private void OnSplitterManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (this._ignoreManipulation)
        return;
      this._transform.Y += e.DeltaManipulation.Translation.Y;
      if (this._transform.Y > 0.0)
        this._transform.Y = 0.0;
      if (this._transform.Y < -this._bottom)
        this._transform.Y = -this._bottom;
      e.Handled = true;
    }

    private void OnSplitterManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      if (this._ignoreManipulation)
        return;
      double y = e.TotalManipulation.Translation.Y;
      if (Math.Abs(y) > double.Epsilon)
        this.SetIsMainContentSelected(y > 0.0);
      else
        this.SetIsMainContentSelected(Math.Abs(this._transform.Y) > double.Epsilon);
      this.AnimateScrollToOffset(this.IsMainContentSelected ? 0.0 : -this._bottom);
      e.Handled = true;
    }
  }
}
