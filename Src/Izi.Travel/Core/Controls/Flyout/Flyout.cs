// ********************************************************************
// Type: Izi.Travel.Shell.Core.Controls.Flyout.Flyout
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Helpers;
using Windows.UI;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.Foundation.Metadata;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class Flyout : FlyoutBase
  {
    private static Size CurrentBounds => new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height);
    private Frame _frame;
    private bool _isPopupReady;
    private Popup _popup;
    private Grid _container;
    private Border _border;
    private ContentControl _contentControl;
    private Rectangle _rectangle;
    private Color _systemTrayColor;
        internal FlyoutPlacementMode Placement;
        public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof (Content), typeof (UIElement), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null));
    public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register(nameof (OverlayBrush), typeof (Brush), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OnOverlayBrushPropertyChanged)));
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof (Background), typeof (Brush), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OnBackgroundPropertyChanged)));
    public static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.Register(nameof (IsFullScreen), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) false));
    public static readonly DependencyProperty CloseOnNavigationProperty = DependencyProperty.Register(nameof (CloseOnNavigation), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) true));
    public static readonly DependencyProperty HideApplicationBarProperty = DependencyProperty.Register(nameof (HideApplicationBar), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) true));

    private bool IsSystemTrayVisible
    {
        get
        {
            // In UWP, we'll always return false as there's no StatusBar in the same way as Windows Phone 8.x
            return false;
        }
    }

    public UIElement Content
    {
      get => (UIElement) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ContentProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ContentProperty, (object) value);
    }

    public Brush OverlayBrush
    {
      get => (Brush) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OverlayBrushProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OverlayBrushProperty, (object) value);
    }

    public Brush Background
    {
      get => (Brush) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.BackgroundProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.BackgroundProperty, (object) value);
    }

    public bool IsFullScreen
    {
      get => (bool) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.IsFullScreenProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.IsFullScreenProperty, (object) value);
    }

    public bool CloseOnNavigation
    {
      get => (bool) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.CloseOnNavigationProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.CloseOnNavigationProperty, (object) value);
    }

    public bool HideApplicationBar
    {
      get => (bool) this.GetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.HideApplicationBarProperty);
      set => this.SetValue(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.HideApplicationBarProperty, (object) value);
    }

    protected override void ShowImpl()
    {
      this.OnOpening();
      this.ShowPopup();
      this.OnOpened();
    }

    protected override void HideImpl()
    {
      this.OnClosing();
      // WP8 SlideTransition removed for UWP; close immediately
      this.ClosePopup();
      this.OnClosed();
    }

    private void ShowPopup()
    {
      if (this._isPopupReady)
        return;
            
      // In UWP, we don't modify the status bar
      this._systemTrayColor = Colors.Transparent;
      if (this._popup != null && this._popup.IsOpen || VisualTreeHelper.GetOpenPopups(Window.Current).Any<Popup>((Func<Popup, bool>) (x => x.Tag is FlyoutBase)))
        return;
      this._isPopupReady = false;
      this._frame = Window.Current.Content as Frame;
      if (this._popup == null)
      {
        Rectangle rectangle = new Rectangle();
        rectangle.Fill = this.OverlayBrush;
        this._rectangle = rectangle;
        Border border = new Border();
        border.Background = this.Background;
        border.VerticalAlignment = this.IsFullScreen ? VerticalAlignment.Stretch : VerticalAlignment.Top;
        border.HorizontalAlignment = HorizontalAlignment.Stretch;
        this._border = border;
        ContentControl contentControl = new ContentControl();
        contentControl.FlowDirection = FlowDirectionHelper.GetCurrentFlowDirection();
        contentControl.DataContext = this.Owner != null ? this.Owner.DataContext : (object) null;
        contentControl.Content = (object) this.Content;
        contentControl.VerticalContentAlignment = VerticalAlignment.Stretch;
        contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        this._contentControl = contentControl;
        this._border.Child = (UIElement) this._contentControl;
        this._container = new Grid();
        this._container.Children.Add((UIElement) this._rectangle);
        this._container.Children.Add((UIElement) this._border);
        Popup popup = new Popup();
        popup.Child = (UIElement) this._container;
        popup.Tag = (object) this;
        this._popup = popup;
      }
      if (this._container != null)
        this._container.LayoutUpdated += this.OnContainerLayoutUpdated;
      this.ArrangePopupSize();
      this._popup.IsOpen = true;
      if (this._frame != null)
      {
        this._frame.Navigating += this.OnFrameNavigating;
      }
    }

    private void ClosePopup()
    {
      // No-op in UWP
      if (this._popup != null)
        this._popup.IsOpen = false;
      if (this._frame != null)
        this._frame.Navigating -= this.OnFrameNavigating;
      this._frame = (Frame) null;
    }

    private void ShowStatusBar()
    {
        // No-op in UWP
    }

    private void HideStatusBar()
    {
        // No-op in UWP
    }

    private void ArrangePopupSize()
    {
      if (this._popup == null)
        return;
      var bounds = CurrentBounds;
      Rect rect = new Rect(0.0, 0.0, bounds.Width, bounds.Height);
      if (this._container != null)
      {
        this._container.RenderTransform = null;
        this._container.Width = rect.Width;
        this._container.Height = rect.Height;
      }
      // No orientation-based offsetting in UWP; StatusBar overlays content on Mobile.
    }

    // Orientation transforms are not applied in UWP; content is laid out using current window bounds.

    private void OnContainerLayoutUpdated(object sender, object eventArgs)
    {
      // WP8 transition removed; mark popup as ready
      this._isPopupReady = true;
      this._container.LayoutUpdated -= this.OnContainerLayoutUpdated;
    }

    private static void OnBackgroundPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Izi.Travel.Shell.Core.Controls.Flyout.Flyout flyout1 = d as Izi.Travel.Shell.Core.Controls.Flyout.Flyout;
      SolidColorBrush oldValue = e.OldValue as SolidColorBrush;
      if (e.NewValue is SolidColorBrush newValue && (oldValue == null || newValue.Color != oldValue.Color || newValue.Opacity != oldValue.Opacity))
      {
        Izi.Travel.Shell.Core.Controls.Flyout.Flyout flyout2 = flyout1;
        SolidColorBrush solidColorBrush = new SolidColorBrush(newValue.Color);
        solidColorBrush.Opacity = newValue.Opacity;
        flyout2.Background = (Brush) solidColorBrush;
      }
      if (flyout1._border == null)
        return;
      flyout1._border.Background = e.NewValue as Brush;
    }

    private static void OnOverlayBrushPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Izi.Travel.Shell.Core.Controls.Flyout.Flyout flyout1 = d as Izi.Travel.Shell.Core.Controls.Flyout.Flyout;
      SolidColorBrush oldValue = e.OldValue as SolidColorBrush;
      if (e.NewValue is SolidColorBrush newValue && (oldValue == null || newValue.Color != oldValue.Color || newValue.Opacity != oldValue.Opacity))
      {
        Izi.Travel.Shell.Core.Controls.Flyout.Flyout flyout2 = flyout1;
        SolidColorBrush solidColorBrush = new SolidColorBrush(newValue.Color);
        solidColorBrush.Opacity = newValue.Opacity;
        flyout2.OverlayBrush = (Brush) solidColorBrush;
      }
      if (flyout1._rectangle == null)
        return;
      flyout1._rectangle.Fill = e.NewValue as Brush;
    }

    private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
      if (this.CloseOnNavigation)
        this.Hide();
    }
  }
}

