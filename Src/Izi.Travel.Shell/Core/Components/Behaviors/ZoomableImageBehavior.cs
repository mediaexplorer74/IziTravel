// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.ZoomableImageBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class ZoomableImageBehavior : Behavior<FrameworkElement>
  {
    public static readonly DependencyProperty ScaleProperty = DependencyProperty.Register(nameof (ScaleProperty), typeof (double), typeof (ZoomableImageBehavior), new PropertyMetadata(new PropertyChangedCallback(ZoomableImageBehavior.OnScaleChanged)));
    private const double MaxImageZoom = 5.0;
    private Point _imagePosition = new Point(0.0, 0.0);
    private Point _oldFinger1;
    private Point _oldFinger2;
    private double _oldScaleFactor;
    private bool _pinch;

    private static void OnScaleChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
      if (!(obj is ZoomableImageBehavior zoomableImageBehavior))
        return;
      if (zoomableImageBehavior.AssociatedObject != null && zoomableImageBehavior.Scale == 1.0)
        zoomableImageBehavior.Reset();
      if (zoomableImageBehavior.ScaleChanged == null)
        return;
      zoomableImageBehavior.ScaleChanged((object) obj, new EventArgs());
    }

    public event EventHandler ScaleChanged;

    public double Scale
    {
      get => (double) this.GetValue(ZoomableImageBehavior.ScaleProperty);
      set => this.SetValue(ZoomableImageBehavior.ScaleProperty, (object) value);
    }

    public ZoomableImageBehavior() => this.Scale = 1.0;

    private void OnPinchStarted(Point position1, Point position2)
    {
      this._oldFinger1 = position1;
      this._oldFinger2 = position2;
      this._oldScaleFactor = 1.0;
    }

    private void OnPinchDelta(double distanceRatio, Point position1, Point position2)
    {
      double num = Math.Max(distanceRatio / this._oldScaleFactor, 1.0 / this.Scale);
      if (!this.IsScaleValid(num))
        return;
      Point currentFinger1 = position1;
      Point currentFinger2 = position2;
      Point translationDelta = this.GetTranslationDelta(currentFinger1, currentFinger2, this._oldFinger1, this._oldFinger2, this._imagePosition, num);
      this._oldFinger1 = currentFinger1;
      this._oldFinger2 = currentFinger2;
      this._oldScaleFactor = distanceRatio;
      this.UpdateImageScale(num);
      this.UpdateImagePosition(translationDelta);
    }

    private void OnDragDelta(double horizontalChange, double verticalChange)
    {
      Point point = new Point(horizontalChange * this.Scale, verticalChange * this.Scale);
      if (!this.IsDragValid(1.0, point))
        return;
      this.UpdateImagePosition(point);
    }

    private Point GetTranslationDelta(
      Point currentFinger1,
      Point currentFinger2,
      Point oldFinger1,
      Point oldFinger2,
      Point currentPosition,
      double scaleFactor)
    {
      Point point1 = new Point(currentFinger1.X + (currentPosition.X - oldFinger1.X) * scaleFactor, currentFinger1.Y + (currentPosition.Y - oldFinger1.Y) * scaleFactor);
      Point point2 = new Point(currentFinger2.X + (currentPosition.X - oldFinger2.X) * scaleFactor, currentFinger2.Y + (currentPosition.Y - oldFinger2.Y) * scaleFactor);
      Point point3 = new Point((point1.X + point2.X) / 2.0, (point1.Y + point2.Y) / 2.0);
      return new Point(point3.X - currentPosition.X, point3.Y - currentPosition.Y);
    }

    private void UpdateImageScale(double scaleFactor)
    {
      this.Scale *= scaleFactor;
      this.ApplyScale();
    }

    private void ApplyScale()
    {
      ((CompositeTransform) this.AssociatedObject.RenderTransform).ScaleX = this.Scale;
      ((CompositeTransform) this.AssociatedObject.RenderTransform).ScaleY = this.Scale;
    }

    private void UpdateImagePosition(Point delta)
    {
      Point point = new Point(this._imagePosition.X + delta.X, this._imagePosition.Y + delta.Y);
      if (point.X > 0.0)
        point.X = 0.0;
      if (point.Y > 0.0)
        point.Y = 0.0;
      if (this.AssociatedObject.ActualWidth * this.Scale + point.X < this.AssociatedObject.ActualWidth)
        point.X = this.AssociatedObject.ActualWidth - this.AssociatedObject.ActualWidth * this.Scale;
      if (this.AssociatedObject.ActualHeight * this.Scale + point.Y < this.AssociatedObject.ActualHeight)
        point.Y = this.AssociatedObject.ActualHeight - this.AssociatedObject.ActualHeight * this.Scale;
      this._imagePosition = point;
      this.ApplyPosition();
    }

    private void ApplyPosition()
    {
      ((CompositeTransform) this.AssociatedObject.RenderTransform).TranslateX = this._imagePosition.X;
      ((CompositeTransform) this.AssociatedObject.RenderTransform).TranslateY = this._imagePosition.Y;
    }

    private bool IsDragValid(double scaleDelta, Point translateDelta)
    {
      return this._imagePosition.X + translateDelta.X <= 0.0 && this._imagePosition.Y + translateDelta.Y <= 0.0 && this.AssociatedObject.ActualWidth * this.Scale * scaleDelta + (this._imagePosition.X + translateDelta.X) >= this.AssociatedObject.ActualWidth && this.AssociatedObject.ActualHeight * this.Scale * scaleDelta + (this._imagePosition.Y + translateDelta.Y) >= this.AssociatedObject.ActualHeight;
    }

    private bool IsScaleValid(double scaleDelta)
    {
      return this.Scale * scaleDelta >= 1.0 && this.Scale * scaleDelta <= 5.0;
    }

    private void Reset()
    {
      this._imagePosition = new Point(0.0, 0.0);
      this.Scale = 1.0;
      this.ApplyScale();
      this.ApplyPosition();
    }

    protected override void OnAttached()
    {
      this.AssociatedObject.RenderTransform = (Transform) new CompositeTransform();
      this.AssociatedObject.CacheMode = (CacheMode) new BitmapCache();
      this.AssociatedObject.DoubleTap += new EventHandler<GestureEventArgs>(this.AssociatedObject_DoubleTap);
      this.AssociatedObject.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(this.AssociatedObject_ManipulationDelta);
      this.AssociatedObject.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(this.AssociatedObject_ManipulationCompleted);
      base.OnAttached();
    }

    private void AssociatedObject_DoubleTap(object sender, GestureEventArgs e) => this.Reset();

    private void AssociatedObject_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
    {
      bool flag = e.PinchManipulation != null;
      if (flag)
      {
        if (!this._pinch)
          this.OnPinchStarted(e.PinchManipulation.Current.PrimaryContact, e.PinchManipulation.Current.SecondaryContact);
        else
          this.OnPinchDelta(e.PinchManipulation.CumulativeScale, e.PinchManipulation.Current.PrimaryContact, e.PinchManipulation.Current.SecondaryContact);
      }
      else
        this.OnDragDelta(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
      this._pinch = flag;
    }

    private void AssociatedObject_ManipulationCompleted(
      object sender,
      ManipulationCompletedEventArgs e)
    {
      this._pinch = false;
    }
  }
}
