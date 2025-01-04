// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.Flyout
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Helpers;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class Flyout : FlyoutBase
  {
    private static readonly double ScreenWidth = Application.Current.Host.Content.ActualWidth;
    private static readonly double ScreenHeight = Application.Current.Host.Content.ActualHeight;
    private PhoneApplicationFrame _frame;
    private PhoneApplicationPage _page;
    private bool _isPopupReady;
    private Popup _popup;
    private Grid _container;
    private Border _border;
    private ContentControl _contentControl;
    private Rectangle _rectangle;
    private bool _hasApplicationBar;
    private Color _systemTrayColor;
    private Uri _frameUri;
    public static readonly DependencyProperty ContentProperty = DependencyProperty.Register(nameof (Content), typeof (UIElement), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null));
    public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register(nameof (OverlayBrush), typeof (Brush), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OnOverlayBrushPropertyChanged)));
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(nameof (Background), typeof (Brush), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) null, new PropertyChangedCallback(Izi.Travel.Shell.Core.Controls.Flyout.Flyout.OnBackgroundPropertyChanged)));
    public static readonly DependencyProperty IsFullScreenProperty = DependencyProperty.Register(nameof (IsFullScreen), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) false));
    public static readonly DependencyProperty CloseOnNavigationProperty = DependencyProperty.Register(nameof (CloseOnNavigation), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) true));
    public static readonly DependencyProperty HideApplicationBarProperty = DependencyProperty.Register(nameof (HideApplicationBar), typeof (bool), typeof (Izi.Travel.Shell.Core.Controls.Flyout.Flyout), new PropertyMetadata((object) true));

    private PageOrientation PageOrientation
    {
      get => this._page == null ? PageOrientation.None : this._page.Orientation;
    }

    private bool IsPageOrientationLandscape
    {
      get
      {
        return this.PageOrientation == PageOrientation.Landscape || this.PageOrientation == PageOrientation.LandscapeLeft || this.PageOrientation == PageOrientation.LandscapeRight;
      }
    }

    private bool IsSystemTrayVisible => SystemTray.IsVisible && SystemTray.Opacity > double.Epsilon;

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
      ITransition transition = new SlideTransition()
      {
        Mode = SlideTransitionMode.SlideUpFadeOut
      }.GetTransition((UIElement) this._container);
      transition.Completed += (EventHandler) ((s, e) =>
      {
        transition.Stop();
        this.ClosePopup();
        this.OnClosed();
      });
      transition.Begin();
    }

    private void ShowPopup()
    {
      if (this._popup != null && this._popup.IsOpen || VisualTreeHelper.GetOpenPopups().Any<Popup>((Func<Popup, bool>) (x => x.Tag is FlyoutBase)))
        return;
      this._isPopupReady = false;
      this._frame = Application.Current.RootVisual as PhoneApplicationFrame;
      this._page = this._frame != null ? this._frame.Content as PhoneApplicationPage : (PhoneApplicationPage) null;
      if (this._page != null && this._page.ApplicationBar != null)
      {
        this._hasApplicationBar = this._page.ApplicationBar.IsVisible;
        if (this._hasApplicationBar)
          this._page.ApplicationBar.IsVisible = !this.HideApplicationBar;
      }
      else
        this._hasApplicationBar = false;
      if (this.IsSystemTrayVisible)
      {
        this._systemTrayColor = SystemTray.BackgroundColor;
        if (this.Background is SolidColorBrush background)
          SystemTray.BackgroundColor = background.Color;
      }
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
        this._container.LayoutUpdated += new EventHandler(this.OnContainerLayoutUpdated);
      this.ArrangePopupSize();
      this._popup.IsOpen = true;
      if (this._frame != null)
      {
        this._frameUri = this._frame.CurrentSource;
        this._frame.Navigating += new NavigatingCancelEventHandler(this.OnFrameNavigating);
      }
      if (this._page == null)
        return;
      this._page.BackKeyPress += new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
      this._page.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.OnPageOrientationChanged);
    }

    private void ClosePopup()
    {
      if (this.IsSystemTrayVisible)
        SystemTray.BackgroundColor = this._systemTrayColor;
      if (this._popup != null)
        this._popup.IsOpen = false;
      if (this._frame != null)
        this._frame.Navigating -= new NavigatingCancelEventHandler(this.OnFrameNavigating);
      if (this._page != null)
      {
        this._page.BackKeyPress -= new EventHandler<CancelEventArgs>(this.OnPageBackKeyPress);
        this._page.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.OnPageOrientationChanged);
        if (this._hasApplicationBar && this._page.ApplicationBar != null && this.HideApplicationBar)
          this._page.ApplicationBar.IsVisible = true;
      }
      this._frame = (PhoneApplicationFrame) null;
      this._page = (PhoneApplicationPage) null;
    }

    private void ArrangePopupSize()
    {
      if (this._popup == null)
        return;
      Rect rect = new Rect(0.0, 0.0, this.IsPageOrientationLandscape ? Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenHeight : Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenWidth, this.IsPageOrientationLandscape ? Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenWidth : Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenHeight);
      if (this._container != null)
      {
        this._container.RenderTransform = this.GetTransform();
        this._container.Width = rect.Width;
        this._container.Height = rect.Height;
      }
      if (!this.IsSystemTrayVisible || this._popup == null)
        return;
      switch (this.PageOrientation)
      {
        case PageOrientation.PortraitUp:
          this._popup.HorizontalOffset = 0.0;
          this._popup.VerticalOffset = 32.0;
          if (this._container == null)
            break;
          this._container.Height -= 32.0;
          break;
        case PageOrientation.LandscapeLeft:
          this._popup.HorizontalOffset = 0.0;
          this._popup.VerticalOffset = 72.0;
          break;
        case PageOrientation.LandscapeRight:
          this._popup.HorizontalOffset = 0.0;
          this._popup.VerticalOffset = 0.0;
          break;
      }
    }

    private Transform GetTransform()
    {
      switch (this.PageOrientation)
      {
        case PageOrientation.Landscape:
        case PageOrientation.LandscapeLeft:
          return (Transform) new CompositeTransform()
          {
            Rotation = 90.0,
            TranslateX = Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenWidth
          };
        case PageOrientation.LandscapeRight:
          return (Transform) new CompositeTransform()
          {
            Rotation = -90.0,
            TranslateY = Izi.Travel.Shell.Core.Controls.Flyout.Flyout.ScreenHeight
          };
        default:
          return (Transform) null;
      }
    }

    private void OnContainerLayoutUpdated(object sender, EventArgs eventArgs)
    {
      ITransition transition = new SlideTransition()
      {
        Mode = SlideTransitionMode.SlideDownFadeIn
      }.GetTransition((UIElement) this._container);
      transition.Completed += (EventHandler) ((obj, e) =>
      {
        transition.Stop();
        this._isPopupReady = true;
      });
      transition.Begin();
      this._container.LayoutUpdated -= new EventHandler(this.OnContainerLayoutUpdated);
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

    private void OnPageOrientationChanged(object sender, OrientationChangedEventArgs e)
    {
      this.ArrangePopupSize();
    }

    private void OnPageBackKeyPress(object sender, CancelEventArgs e)
    {
      e.Cancel = true;
      if (!this._isPopupReady)
        return;
      this.Hide();
    }

    private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
      if (!this.CloseOnNavigation)
        this._popup.IsOpen = e.NavigationMode == NavigationMode.Back && this._frameUri == e.Uri;
      else
        this.Hide();
    }
  }
}
