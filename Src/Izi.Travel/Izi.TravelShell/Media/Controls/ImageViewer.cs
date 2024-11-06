// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.Controls.ImageViewer
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#nullable disable
namespace Izi.Travel.Shell.Media.Controls
{
  [TemplatePart(Name = "PartViewport", Type = typeof (ViewportControl))]
  [TemplatePart(Name = "PartCanvas", Type = typeof (Canvas))]
  [TemplatePart(Name = "PartImage", Type = typeof (Image))]
  public class ImageViewer : Control
  {
    private const string PartViewport = "PartViewport";
    private const string PartCanvas = "PartCanvas";
    private const string PartImage = "PartImage";
    private const double MaxScale = 10.0;
    private ViewportControl _viewport;
    private Canvas _canvas;
    private Image _image;
    private ScaleTransform _scaleTransform;
    private double _scale = 1.0;
    private double _minScale;
    private double _coercedScale;
    private double _originalScale;
    private Size _viewportSize;
    private bool _pinching;
    private Point _screenMidpoint;
    private Point _relativeMidpoint;
    private BitmapImage _bitmap;
    public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(nameof (Source), typeof (ImageSource), typeof (ImageViewer), new PropertyMetadata((object) null));

    public ImageSource Source
    {
      get => (ImageSource) this.GetValue(ImageViewer.SourceProperty);
      set => this.SetValue(ImageViewer.SourceProperty, (object) value);
    }

    public ImageViewer() => this.DefaultStyleKey = (object) typeof (ImageViewer);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._viewport = this.GetTemplateChild("PartViewport") as ViewportControl;
      if (this._viewport != null)
      {
        this._viewport.ViewportChanged += new EventHandler<ViewportChangedEventArgs>(this.OnViewportChanged);
        this._viewport.ManipulationStarted += new EventHandler<ManipulationStartedEventArgs>(this.OnManipulationStarted);
        this._viewport.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.OnManipulationDelta);
        this._viewport.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.OnManipulationCompleted);
      }
      this._canvas = this.GetTemplateChild("PartCanvas") as Canvas;
      this._image = this.GetTemplateChild("PartImage") as Image;
      if (this._image == null)
        return;
      this._scaleTransform = new ScaleTransform();
      this._image.RenderTransform = (Transform) this._scaleTransform;
      this._image.ImageOpened += new EventHandler<RoutedEventArgs>(this.OnImageOpened);
    }

    private void CoerceScale(bool recompute)
    {
      if (recompute && this._bitmap != null && this._viewport != null)
        this._minScale = Math.Min(this._viewport.ActualWidth / (double) this._bitmap.PixelWidth, this._viewport.ActualHeight / (double) this._bitmap.PixelHeight);
      this._coercedScale = Math.Min(10.0, Math.Max(this._scale, this._minScale));
    }

    private void ResizeImage(bool center)
    {
      if (Math.Abs(this._coercedScale) <= double.Epsilon || this._bitmap == null)
        return;
      double width = this._canvas.Width = Math.Round((double) this._bitmap.PixelWidth * this._coercedScale);
      double height = this._canvas.Height = Math.Round((double) this._bitmap.PixelHeight * this._coercedScale);
      this._scaleTransform.ScaleX = this._scaleTransform.ScaleY = this._coercedScale;
      this._viewport.Bounds = new Rect(0.0, 0.0, width, height);
      if (center)
      {
        this._viewport.SetViewportOrigin(new Point(Math.Round((width - this._viewport.ActualWidth) / 2.0), Math.Round((height - this._viewport.ActualHeight) / 2.0)));
      }
      else
      {
        Point point = new Point(width * this._relativeMidpoint.X, height * this._relativeMidpoint.Y);
        this._viewport.SetViewportOrigin(new Point(point.X - this._screenMidpoint.X, point.Y - this._screenMidpoint.Y));
      }
    }

    private void OnViewportChanged(object sender, ViewportChangedEventArgs e)
    {
      Size size;
      ref Size local = ref size;
      Rect viewport = this._viewport.Viewport;
      double width = viewport.Width;
      viewport = this._viewport.Viewport;
      double height = viewport.Height;
      local = new Size(width, height);
      if (size == this._viewportSize)
        return;
      this._viewportSize = size;
      this.CoerceScale(true);
      this.ResizeImage(false);
    }

    private void OnImageOpened(object sender, RoutedEventArgs e)
    {
      this._bitmap = (BitmapImage) this._image.Source;
      this._scale = 0.0;
      this.CoerceScale(true);
      this._scale = this._coercedScale;
      this.ResizeImage(true);
    }

    private void OnManipulationStarted(object sender, ManipulationStartedEventArgs e)
    {
      this._pinching = false;
      this._originalScale = this._scale;
    }

    private void OnManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      if (e.PinchManipulation != null)
      {
        e.Handled = true;
        if (!this._pinching)
        {
          this._pinching = true;
          Point center = e.PinchManipulation.Original.Center;
          this._relativeMidpoint = new Point(center.X / this._image.ActualWidth, center.Y / this._image.ActualHeight);
          this._screenMidpoint = this._image.TransformToVisual((UIElement) this._viewport).Transform(center);
        }
        this._scale = this._originalScale * e.PinchManipulation.CumulativeScale;
        this.CoerceScale(false);
        this.ResizeImage(false);
      }
      else
      {
        if (!this._pinching)
          return;
        this._pinching = false;
        this._originalScale = this._scale = this._coercedScale;
      }
    }

    private void OnManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
    {
      this._pinching = false;
      this._scale = this._coercedScale;
    }
  }
}
