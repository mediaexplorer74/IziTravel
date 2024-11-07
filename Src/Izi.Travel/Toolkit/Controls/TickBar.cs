// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.TickBar
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  public sealed class TickBar : Control
  {
    private const double ReservedSpace = 12.0;
    private const double TickLength = 2.0;
    private const string MinimumPropertyName = "Minimum";
    private const string MaximumPropertyName = "Maximum";
    private const string TickFrequencyPropertyName = "TickFrequency";
    private const string IsDirectionReversedPropertyName = "IsDirectionReversed";
    private const string OrientationPropertyName = "Orientation";
    private Canvas _container;
    private bool _ingorePropertyChanges;
    public static readonly DependencyProperty FillProperty = DependencyProperty.Register(nameof (Fill), typeof (Brush), typeof (TickBar), new PropertyMetadata((PropertyChangedCallback) ((d, e) => ((TickBar) d).OnFillChanged(e))));
    private static readonly DependencyProperty MinimumProperty = DependencyProperty.Register(nameof (Minimum), typeof (double), typeof (TickBar), new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((TickBar) d).UpdateTicks())));
    private static readonly DependencyProperty MaximumProperty = DependencyProperty.Register(nameof (Maximum), typeof (double), typeof (TickBar), new PropertyMetadata((object) 1.0, (PropertyChangedCallback) ((d, e) => ((TickBar) d).UpdateTicks())));
    private static readonly DependencyProperty TickFrequencyProperty = DependencyProperty.Register(nameof (TickFrequency), typeof (double), typeof (TickBar), new PropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((TickBar) d).UpdateTicks())));
    private static readonly DependencyProperty IsDirectionReversedProperty = DependencyProperty.Register(nameof (IsDirectionReversed), typeof (bool), typeof (TickBar), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TickBar) d).UpdateTicks())));
    private static readonly DependencyProperty OrientationProperty = DependencyProperty.Register(nameof (Orientation), typeof (Orientation), typeof (TickBar), new PropertyMetadata((object) Orientation.Horizontal));

    public TickBar()
    {
      this.DefaultStyleKey = (object) typeof (TickBar);
      this.SizeChanged += new SizeChangedEventHandler(this.OnSizeChanged);
    }

    public Brush Fill
    {
      get => (Brush) this.GetValue(TickBar.FillProperty);
      set => this.SetValue(TickBar.FillProperty, (object) value);
    }

    private void OnFillChanged(DependencyPropertyChangedEventArgs e)
    {
      if (this._container == null)
        return;
      foreach (Shape shape in this._container.Children.OfType<Rectangle>())
        shape.Fill = (Brush) e.NewValue;
    }

    private double Minimum
    {
      get => (double) this.GetValue(TickBar.MinimumProperty);
      set => this.SetValue(TickBar.MinimumProperty, (object) value);
    }

    private double Maximum
    {
      get => (double) this.GetValue(TickBar.MaximumProperty);
      set => this.SetValue(TickBar.MaximumProperty, (object) value);
    }

    private double TickFrequency
    {
      get => (double) this.GetValue(TickBar.TickFrequencyProperty);
      set => this.SetValue(TickBar.TickFrequencyProperty, (object) value);
    }

    private bool IsDirectionReversed
    {
      get => (bool) this.GetValue(TickBar.IsDirectionReversedProperty);
      set => this.SetValue(TickBar.IsDirectionReversedProperty, (object) value);
    }

    private Orientation Orientation
    {
      get => (Orientation) this.GetValue(TickBar.OrientationProperty);
      set => this.SetValue(TickBar.OrientationProperty, (object) value);
    }

    public override void OnApplyTemplate()
    {
      this._ingorePropertyChanges = true;
      base.OnApplyTemplate();
      this._container = this.GetFirstLogicalChildByType<Canvas>(false);
      if (this.GetParentByType<PhoneSlider>() != null)
      {
        this.BindToTemplatedParent(TickBar.TickFrequencyProperty, "TickFrequency");
        this.BindToTemplatedParent(TickBar.MinimumProperty, "Minimum");
        this.BindToTemplatedParent(TickBar.MaximumProperty, "Maximum");
        this.BindToTemplatedParent(TickBar.IsDirectionReversedProperty, "IsDirectionReversed");
        this.BindToTemplatedParent(TickBar.OrientationProperty, "Orientation");
      }
      this._ingorePropertyChanges = false;
      this.UpdateTicks();
    }

    private void UpdateTicks()
    {
      if (this._ingorePropertyChanges || this._container == null)
        return;
      if (this._container.Children.Count > 0)
        this._container.Children.Clear();
      if (this.TickFrequency <= 0.0)
        return;
      double num1 = this.Maximum - this.Minimum;
      if (num1 <= 0.0)
        return;
      Size size = new Size(this.ActualWidth, this.ActualHeight);
      double num2;
      if (this.Orientation == Orientation.Horizontal)
      {
        if (NumericExtensions.IsGreaterThan(12.0, size.Width))
          return;
        size.Width -= 12.0;
        num2 = size.Width / num1;
      }
      else
      {
        if (NumericExtensions.IsGreaterThan(12.0, size.Height))
          return;
        size.Height -= 12.0;
        num2 = size.Height / num1 * -1.0;
      }
      if (this.IsDirectionReversed)
        num2 *= -1.0;
      if (this.TickFrequency <= 0.0 || size.Width <= 0.0 || size.Height <= 0.0)
        return;
      double tickFrequency = this.TickFrequency;
      double num3 = 6.0;
      for (double num4 = tickFrequency; num4 < num1; num4 += tickFrequency)
      {
        Rectangle element;
        if (this.Orientation == Orientation.Horizontal)
        {
          double num5 = num4 * num2;
          if (this.IsDirectionReversed)
            num5 += size.Width;
          double num6 = num5 + num3;
          if (this.IsDirectionReversed)
          {
            if (NumericExtensions.IsGreaterThan(12.0, num6))
              break;
          }
          else if (NumericExtensions.IsGreaterThan(num6, size.Width - 12.0))
            break;
          Rectangle rectangle = new Rectangle();
          rectangle.Width = 2.0;
          rectangle.Height = size.Height;
          element = rectangle;
          Canvas.SetLeft((UIElement) element, num6);
        }
        else
        {
          double num7 = num4 * num2;
          if (!this.IsDirectionReversed)
            num7 += size.Height;
          double num8 = num7 + num3;
          if (this.IsDirectionReversed)
          {
            if (NumericExtensions.IsGreaterThan(num8, size.Height - 12.0))
              break;
          }
          else if (NumericExtensions.IsGreaterThan(12.0, num8))
            break;
          Rectangle rectangle = new Rectangle();
          rectangle.Width = size.Width;
          rectangle.Height = 2.0;
          element = rectangle;
          Canvas.SetTop((UIElement) element, num8);
        }
        element.Fill = this.Fill;
        this._container.Children.Add((UIElement) element);
      }
    }

    private void OnSizeChanged(object sender, SizeChangedEventArgs e) => this.UpdateTicks();

    private void BindToTemplatedParent(DependencyProperty target, string source)
    {
      this.SetBinding(target, new Binding()
      {
        RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent),
        Path = new PropertyPath(source, new object[0])
      });
    }
  }
}
