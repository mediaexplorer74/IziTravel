// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.CircleSegment
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls
{
  [TemplatePart(Name = "PartPath", Type = typeof (Path))]
  [TemplatePart(Name = "PartFigure", Type = typeof (PathFigure))]
  [TemplatePart(Name = "PartSegment", Type = typeof (ArcSegment))]
  public class CircleSegment : Control
  {
    private const string PartPath = "PartPath";
    private const string PartFigure = "PartFigure";
    private const string PartSegment = "PartSegment";
    private Path _path;
    private PathFigure _figure;
    private ArcSegment _segment;
    public static readonly DependencyProperty StrokeProperty = DependencyProperty.Register(nameof (Stroke), typeof (Brush), typeof (CircleSegment), new PropertyMetadata((object) null, new PropertyChangedCallback(CircleSegment.OnPropertyChanged)));
    public static readonly DependencyProperty StrokeThicknessProperty = DependencyProperty.Register(nameof (StrokeThickness), typeof (double), typeof (CircleSegment), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register(nameof (MinValue), typeof (double), typeof (CircleSegment), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(CircleSegment.OnPropertyChanged)));
    public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register(nameof (MaxValue), typeof (double), typeof (CircleSegment), new PropertyMetadata((object) 0.0, new PropertyChangedCallback(CircleSegment.OnPropertyChanged)));

    public Brush Stroke
    {
      get => (Brush) this.GetValue(CircleSegment.StrokeProperty);
      set => this.SetValue(CircleSegment.StrokeProperty, (object) value);
    }

    public double StrokeThickness
    {
      get => (double) this.GetValue(CircleSegment.StrokeThicknessProperty);
      set => this.SetValue(CircleSegment.StrokeThicknessProperty, (object) value);
    }

    public double MinValue
    {
      get => (double) this.GetValue(CircleSegment.MinValueProperty);
      set => this.SetValue(CircleSegment.MinValueProperty, (object) value);
    }

    public double MaxValue
    {
      get => (double) this.GetValue(CircleSegment.MaxValueProperty);
      set => this.SetValue(CircleSegment.MaxValueProperty, (object) value);
    }

    public CircleSegment() => this.DefaultStyleKey = (object) typeof (CircleSegment);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this._path = this.GetTemplateChild("PartPath") as Path;
      this._figure = this.GetTemplateChild("PartFigure") as PathFigure;
      this._segment = this.GetTemplateChild("PartSegment") as ArcSegment;
      this.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
    {
      this.Render();
    }

    private void Render()
    {
      if (this._path == null || this._figure == null || this._segment == null || double.IsNaN(this.ActualWidth) || this.ActualWidth < double.Epsilon || double.IsNaN(this.ActualHeight) || this.ActualHeight < double.Epsilon || this.MinValue > this.MaxValue)
        return;
      double num1 = Math.Min(this.ActualWidth, this.ActualHeight);
      double num2 = num1 / 2.0;
      double num3 = this.StrokeThickness / 2.0;
      double radius = num1 / 2.0 - num3;
      double angle1 = this.MinValue * 360.0 / 100.0;
      double angle2 = this.MaxValue * 360.0 / 100.0;
      Point cartesianCoordinate1 = CircleSegment.ComputeCartesianCoordinate(angle1, radius);
      cartesianCoordinate1.X += num2;
      cartesianCoordinate1.Y += num2;
      Point cartesianCoordinate2 = CircleSegment.ComputeCartesianCoordinate(angle2, radius);
      cartesianCoordinate2.X += num2;
      cartesianCoordinate2.Y += num2;
      this._path.Width = num1;
      this._path.Height = num1;
      bool flag = angle2 - angle1 > 180.0;
      Size size;
      ref Size local = ref size;
      double num4 = radius;
      local = new Size(num4, num4);
      this._figure.StartPoint = cartesianCoordinate1;
      if (cartesianCoordinate1.X == Math.Round(cartesianCoordinate2.X) && cartesianCoordinate1.Y == Math.Round(cartesianCoordinate2.Y))
        cartesianCoordinate2.X -= 0.01;
      this._segment.Point = cartesianCoordinate2;
      this._segment.Size = size;
      this._segment.IsLargeArc = flag;
    }

    private static Point ComputeCartesianCoordinate(double angle, double radius)
    {
      double num = Math.PI / 180.0 * (angle - 90.0);
      return new Point(radius * Math.Cos(num), radius * Math.Sin(num));
    }

    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is CircleSegment circleSegment))
        return;
      circleSegment.Render();
    }
  }
}
