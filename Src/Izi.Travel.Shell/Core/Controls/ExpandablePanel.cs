// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.ExpandablePanel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [System.Windows.Markup.ContentProperty("Content")]
  [TemplatePart(Name = "PartGrid", Type = typeof (Grid))]
  public class ExpandablePanel : Control
  {
    private const string PartGrid = "PartGrid";
    private const double PopupAnimationDuration = 250.0;
    private static readonly double ScreenWidth = Application.Current.Host.Content.ActualWidth;
    private static readonly double ScreenHeight = Application.Current.Host.Content.ActualHeight;
    private bool _ignoreIsExpandedCallback;
    private bool _popupExpanded;
    private Popup _popup;
    private Grid _popupGrid;
    private ContentControl _popupContent;
    private Image _popupImage;
    private Point _popupPosition;
    private Storyboard _storyboardPopup;
    private DoubleAnimation _animationPopupHorizontal;
    private DoubleAnimation _animationPopupVertical;
    private DoubleAnimation _animationPopupWidth;
    private DoubleAnimation _animationPopupHeight;
    private DoubleAnimation _animationPopupOpacity;
    private Grid _grid;
    private CompositeTransform _gridTransform;
    private PhoneApplicationPage _page;
    private bool _hasApplicationBar;
    public static readonly DependencyProperty IsExpandedProperty = DependencyProperty.Register(nameof (IsExpanded), typeof (bool), typeof (ExpandablePanel), new PropertyMetadata((object) false, new PropertyChangedCallback(ExpandablePanel.OnIsExpandedPropertyChanged)));
    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof (Content), typeof (UIElement), typeof (ExpandablePanel), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ExpandablePanel), new PropertyMetadata((object) null));
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(nameof (Command), typeof (ICommand), typeof (ExpandablePanel), new PropertyMetadata((object) null));

    public bool IsExpanded
    {
      get => (bool) this.GetValue(ExpandablePanel.IsExpandedProperty);
      set => this.SetValue(ExpandablePanel.IsExpandedProperty, (object) value);
    }

    public UIElement Content
    {
      get => (UIElement) this.GetValue(ExpandablePanel.ContentProperty);
      set => this.SetValue(ExpandablePanel.ContentProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ExpandablePanel.ImageSourceProperty);
      set => this.SetValue(ExpandablePanel.ImageSourceProperty, (object) value);
    }

    public ICommand Command
    {
      get => (ICommand) this.GetValue(ExpandablePanel.CommandProperty);
      set => this.SetValue(ExpandablePanel.CommandProperty, (object) value);
    }

    public ExpandablePanel() => this.DefaultStyleKey = (object) typeof (ExpandablePanel);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (Application.Current.RootVisual is Frame rootVisual)
      {
        this._page = rootVisual.Content as PhoneApplicationPage;
        if (this._page != null)
        {
          if (this._page.ApplicationBar != null)
            this._hasApplicationBar = this._page.ApplicationBar.IsVisible;
          this._page.BackKeyPress += new EventHandler<CancelEventArgs>(this.OnBackKeyPress);
        }
      }
      this._grid = this.GetTemplateChild("PartGrid") as Grid;
      if (this._grid != null)
      {
        this._gridTransform = new CompositeTransform();
        this._grid.RenderTransform = (Transform) this._gridTransform;
        this._grid.Tap += new EventHandler<System.Windows.Input.GestureEventArgs>(this.OnGridTap);
      }
      Grid grid = new Grid();
      grid.Background = this.Background;
      grid.VerticalAlignment = VerticalAlignment.Stretch;
      grid.HorizontalAlignment = HorizontalAlignment.Stretch;
      this._popupGrid = grid;
      Image image = new Image();
      image.Source = this.ImageSource;
      image.Stretch = Stretch.Uniform;
      image.VerticalAlignment = VerticalAlignment.Center;
      image.HorizontalAlignment = HorizontalAlignment.Center;
      this._popupImage = image;
      ContentControl contentControl = new ContentControl();
      contentControl.Content = (object) this.Content;
      contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
      contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
      contentControl.Visibility = Visibility.Collapsed;
      this._popupContent = contentControl;
      if (this.Parent is FrameworkElement parent)
        this._popupContent.DataContext = parent.DataContext;
      this._popupGrid.Children.Add((UIElement) this._popupImage);
      this._popupGrid.Children.Add((UIElement) this._popupContent);
      Popup popup = new Popup();
      popup.IsOpen = false;
      popup.Tag = (object) this;
      popup.Child = (UIElement) this._popupGrid;
      this._popup = popup;
      TimeSpan timeSpan = TimeSpan.FromMilliseconds(250.0);
      this._storyboardPopup = new Storyboard();
      CubicEase cubicEase1 = new CubicEase();
      cubicEase1.EasingMode = EasingMode.EaseInOut;
      CubicEase cubicEase2 = cubicEase1;
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.Duration = (Duration) timeSpan;
      doubleAnimation1.EasingFunction = (IEasingFunction) cubicEase2;
      this._animationPopupHorizontal = doubleAnimation1;
      this._storyboardPopup.Children.Add((Timeline) this._animationPopupHorizontal);
      Storyboard.SetTarget((Timeline) this._animationPopupHorizontal, (DependencyObject) this._popup);
      Storyboard.SetTargetProperty((Timeline) this._animationPopupHorizontal, new PropertyPath((object) Popup.HorizontalOffsetProperty));
      DoubleAnimation doubleAnimation2 = new DoubleAnimation();
      doubleAnimation2.Duration = (Duration) timeSpan;
      doubleAnimation2.EasingFunction = (IEasingFunction) cubicEase2;
      this._animationPopupVertical = doubleAnimation2;
      this._storyboardPopup.Children.Add((Timeline) this._animationPopupVertical);
      Storyboard.SetTarget((Timeline) this._animationPopupVertical, (DependencyObject) this._popup);
      Storyboard.SetTargetProperty((Timeline) this._animationPopupVertical, new PropertyPath((object) Popup.VerticalOffsetProperty));
      DoubleAnimation doubleAnimation3 = new DoubleAnimation();
      doubleAnimation3.Duration = (Duration) timeSpan;
      doubleAnimation3.EasingFunction = (IEasingFunction) cubicEase2;
      this._animationPopupWidth = doubleAnimation3;
      this._storyboardPopup.Children.Add((Timeline) this._animationPopupWidth);
      Storyboard.SetTarget((Timeline) this._animationPopupWidth, (DependencyObject) this._popupGrid);
      Storyboard.SetTargetProperty((Timeline) this._animationPopupWidth, new PropertyPath((object) FrameworkElement.WidthProperty));
      DoubleAnimation doubleAnimation4 = new DoubleAnimation();
      doubleAnimation4.Duration = (Duration) timeSpan;
      doubleAnimation4.EasingFunction = (IEasingFunction) cubicEase2;
      this._animationPopupHeight = doubleAnimation4;
      this._storyboardPopup.Children.Add((Timeline) this._animationPopupHeight);
      Storyboard.SetTarget((Timeline) this._animationPopupHeight, (DependencyObject) this._popupGrid);
      Storyboard.SetTargetProperty((Timeline) this._animationPopupHeight, new PropertyPath((object) FrameworkElement.HeightProperty));
      DoubleAnimation doubleAnimation5 = new DoubleAnimation();
      doubleAnimation5.Duration = (Duration) timeSpan;
      this._animationPopupOpacity = doubleAnimation5;
      this._storyboardPopup.Children.Add((Timeline) this._animationPopupOpacity);
      Storyboard.SetTarget((Timeline) this._animationPopupOpacity, (DependencyObject) this._popupImage);
      Storyboard.SetTargetProperty((Timeline) this._animationPopupOpacity, new PropertyPath((object) UIElement.OpacityProperty));
      this._storyboardPopup.Completed += new EventHandler(this.OnPopupStoryboardCompleted);
    }

    private void ShowPopup()
    {
      this._popupExpanded = true;
      this._popupPosition = this.TransformToVisual(Application.Current.RootVisual).Transform(new Point(0.0, 0.0));
      this._popup.IsOpen = true;
      this.Visibility = Visibility.Collapsed;
      if (this._hasApplicationBar && this._page.ApplicationBar != null)
        this._page.ApplicationBar.IsVisible = false;
      this.AnimatePopup(true);
    }

    private void HidePopup()
    {
      this._popupImage.Visibility = Visibility.Visible;
      this._popupContent.Visibility = Visibility.Collapsed;
      this._popupExpanded = false;
      this.AnimatePopup(false);
    }

    private void AnimatePopup(bool open)
    {
      if (open)
      {
        this._animationPopupHorizontal.From = new double?(this._popupPosition.X);
        this._animationPopupHorizontal.To = new double?(0.0);
        this._animationPopupVertical.From = new double?(this._popupPosition.Y);
        this._animationPopupVertical.To = new double?(0.0);
        this._animationPopupWidth.From = new double?(this.Width);
        this._animationPopupWidth.To = new double?(ExpandablePanel.ScreenWidth);
        this._animationPopupHeight.From = new double?(this.Height);
        this._animationPopupHeight.To = new double?(ExpandablePanel.ScreenHeight);
        this._animationPopupOpacity.From = new double?(1.0);
        this._animationPopupOpacity.To = new double?(0.0);
      }
      else
      {
        this._animationPopupHorizontal.From = new double?(0.0);
        this._animationPopupHorizontal.To = new double?(this._popupPosition.X);
        this._animationPopupVertical.From = new double?(0.0);
        this._animationPopupVertical.To = new double?(this._popupPosition.Y);
        this._animationPopupWidth.From = new double?(ExpandablePanel.ScreenWidth);
        this._animationPopupWidth.To = new double?(this.Width);
        this._animationPopupHeight.From = new double?(ExpandablePanel.ScreenHeight);
        this._animationPopupHeight.To = new double?(this.Height);
        this._animationPopupOpacity.From = new double?(0.0);
        this._animationPopupOpacity.To = new double?(1.0);
      }
      this._storyboardPopup.Stop();
      this._storyboardPopup.Begin();
      this._storyboardPopup.SeekAlignedToLastTick(TimeSpan.Zero);
    }

    private void OnPopupStoryboardCompleted(object sender, EventArgs eventArgs)
    {
      if (!this._popupExpanded)
      {
        this._popup.IsOpen = false;
        this.Visibility = Visibility.Visible;
        if (this._hasApplicationBar && this._page.ApplicationBar != null)
          this._page.ApplicationBar.IsVisible = true;
      }
      else
      {
        this._popupImage.Visibility = Visibility.Collapsed;
        this._popupContent.Visibility = Visibility.Visible;
      }
      this._ignoreIsExpandedCallback = true;
      this.IsExpanded = this._popupExpanded;
      this._ignoreIsExpandedCallback = false;
    }

    private void OnGridTap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      if (this.Command != null)
      {
        if (!this.Command.CanExecute((object) null))
          return;
        this.Command.Execute((object) null);
      }
      this.ShowPopup();
    }

    private void OnBackKeyPress(object sender, CancelEventArgs e)
    {
      if (!this._popup.IsOpen || VisualTreeHelper.GetOpenPopups().Count<Popup>((Func<Popup, bool>) (x => !(x.Tag is ExpandablePanel))) > 0)
        return;
      this.HidePopup();
      e.Cancel = true;
    }

    private static void OnIsExpandedPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ExpandablePanel expandablePanel) || expandablePanel._ignoreIsExpandedCallback)
        return;
      if ((bool) e.NewValue)
        expandablePanel.ShowPopup();
      else
        expandablePanel.HidePopup();
    }
  }
}
