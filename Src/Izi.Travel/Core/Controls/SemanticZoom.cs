// ********************************************************************
// Type: Izi.Travel.Core.Controls.SemanticZoom
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace Izi.Travel.Core.Controls
{
  [TemplatePart(Name = "PartZoomedOutContent", Type = typeof (ContentControl))]
  [TemplatePart(Name = "PartZoomedInContent", Type = typeof (ContentControl))]
  public class SemanticZoom : Control
  {
    private const string PartZoomedOutContent = "PartZoomedOutContent";
    private const string PartZoomedInContent = "PartZoomedInContent";
    private ContentControl _zoomedOutContent;
    private ContentControl _zoomedInContent;
    public static readonly DependencyProperty ZoomedInViewProperty = DependencyProperty.Register(nameof (ZoomedInView), typeof (UIElement), typeof (SemanticZoom), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ZoomedOutViewProperty = DependencyProperty.Register(nameof (ZoomedOutView), typeof (UIElement), typeof (SemanticZoom), new PropertyMetadata((object) null));
    public static readonly DependencyProperty IsZoomedOutProperty = DependencyProperty.Register(nameof (IsZoomedOut), typeof (bool), typeof (SemanticZoom), new PropertyMetadata((object) false, new PropertyChangedCallback(SemanticZoom.OnIsZoomedOutPropertyChanged)));

    public int ScaleAnimationDuration { get; set; }

    public int OpacityAnimationDuration { get; set; }

    public UIElement ZoomedInView
    {
      get => (UIElement) this.GetValue(SemanticZoom.ZoomedInViewProperty);
      set => this.SetValue(SemanticZoom.ZoomedInViewProperty, (object) value);
    }

    public UIElement ZoomedOutView
    {
      get => (UIElement) this.GetValue(SemanticZoom.ZoomedOutViewProperty);
      set => this.SetValue(SemanticZoom.ZoomedOutViewProperty, (object) value);
    }

    public bool IsZoomedOut
    {
      get => (bool) this.GetValue(SemanticZoom.IsZoomedOutProperty);
      set => this.SetValue(SemanticZoom.IsZoomedOutProperty, (object) value);
    }

    public SemanticZoom()
    {
      this.DefaultStyleKey = (object) typeof (SemanticZoom);
      this.IsZoomedOut = false;
      this.ScaleAnimationDuration = 350;
      this.OpacityAnimationDuration = 350;
    }

    protected override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._zoomedOutContent = this.GetTemplateChild("PartZoomedOutContent") as ContentControl;
      if (this._zoomedOutContent != null)
        this._zoomedOutContent.ManipulationDelta += this.OnZoomedOutContentManipulationDelta;
      this._zoomedInContent = this.GetTemplateChild("PartZoomedInContent") as ContentControl;
      if (this._zoomedInContent == null)
        return;
      this._zoomedInContent.ManipulationDelta += this.OnZoomedInContentManipulationDelta;
    }

    private void ZoomIn()
    {
      Storyboard storyboard = new Storyboard();
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedInContent, 0.0, 1.0, 'X'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedInContent, 0.0, 1.0, 'Y'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedOutContent, 1.0, 2.0, 'X'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedOutContent, 1.0, 2.0, 'Y'));
      storyboard.Children.Add((Timeline) this.CreateOpacityAnimation((UIElement) this._zoomedInContent, true));
      storyboard.Children.Add((Timeline) this.CreateOpacityAnimation((UIElement) this._zoomedOutContent, false));
      this._zoomedInContent.Visibility = Visibility.Visible;
      storyboard.Begin();
    }

    private void ZoomOut()
    {
      Storyboard storyboard = new Storyboard();
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedInContent, 1.0, 0.0, 'X'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedInContent, 1.0, 0.0, 'Y'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedOutContent, 2.0, 1.0, 'X'));
      storyboard.Children.Add((Timeline) this.CreateScaleAnimation((UIElement) this._zoomedOutContent, 2.0, 1.0, 'Y'));
      storyboard.Children.Add((Timeline) this.CreateOpacityAnimation((UIElement) this._zoomedInContent, false));
      storyboard.Children.Add((Timeline) this.CreateOpacityAnimation((UIElement) this._zoomedOutContent, true));
      this._zoomedOutContent.Visibility = Visibility.Visible;
      storyboard.Begin();
    }

    private DoubleAnimation CreateScaleAnimation(
      UIElement target,
      double from,
      double to,
      char axis)
    {
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.Duration = (Duration) TimeSpan.FromMilliseconds((double) this.ScaleAnimationDuration);
      doubleAnimation.From = new double?(from);
      doubleAnimation.To = new double?(to);
      DoubleAnimation element = doubleAnimation;
      Storyboard.SetTargetProperty((Timeline)element, $"(UIElement.RenderTransform).(CompositeTransform.Scale{axis})");
      Storyboard.SetTarget((Timeline)element, (DependencyObject)target);
      return element;
    }

    private DoubleAnimation CreateOpacityAnimation(UIElement target, bool show)
    {
      DoubleAnimation element = new DoubleAnimation();
      element.Duration = (Duration) TimeSpan.FromMilliseconds((double) this.OpacityAnimationDuration);
      element.From = new double?(show ? 0.0 : 1.0);
      element.To = new double?(show ? 1.0 : 0.0);
      Storyboard.SetTargetProperty((Timeline)element, "Opacity");
      Storyboard.SetTarget((Timeline)element, (DependencyObject)target);
      element.Completed += (s, args) => target.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
      return element;
    }

    private static bool IsZoomIn(double scaleDelta) => scaleDelta > 1.0;

    private static void OnIsZoomedOutPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is SemanticZoom semanticZoom))
        return;
      if ((bool) e.NewValue)
        semanticZoom.ZoomOut();
      else
        semanticZoom.ZoomIn();
    }

    private void OnZoomedOutContentManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      if (!SemanticZoom.IsZoomIn(e.Delta.Scale))
        return;
      this.IsZoomedOut = false;
      e.Handled = true;
      e.Complete();
    }

    private void OnZoomedInContentManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
    {
      if (SemanticZoom.IsZoomIn(e.Delta.Scale))
        return;
      this.IsZoomedOut = true;
      e.Handled = true;
      e.Complete();
    }
  }
}

