// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ColorBaseControl
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public abstract class ColorBaseControl : Control
  {
    public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(nameof (Color), typeof (Color), typeof (ColorBaseControl), new PropertyMetadata(new PropertyChangedCallback(ColorBaseControl.OnColorChanged)));
    public static readonly DependencyProperty SolidColorBrushProperty = DependencyProperty.Register(nameof (SolidColorBrush), typeof (SolidColorBrush), typeof (ColorBaseControl), new PropertyMetadata((object) null));

    public event ColorBaseControl.ColorChangedHandler ColorChanged;

    public Color Color
    {
      get => (Color) this.GetValue(ColorBaseControl.ColorProperty);
      set => this.SetValue(ColorBaseControl.ColorProperty, (object) value);
    }

    public SolidColorBrush SolidColorBrush
    {
      get => (SolidColorBrush) this.GetValue(ColorBaseControl.SolidColorBrushProperty);
      private set => this.SetValue(ColorBaseControl.SolidColorBrushProperty, (object) value);
    }

    private static void OnColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (!(d is ColorBaseControl colorBaseControl))
        return;
      colorBaseControl.UpdateLayoutBasedOnColor();
      colorBaseControl.SolidColorBrush = new SolidColorBrush((Color) e.NewValue);
    }

    protected internal virtual void UpdateLayoutBasedOnColor()
    {
    }

    protected internal void ColorChanging(Color color)
    {
      this.Color = color;
      this.SolidColorBrush = new SolidColorBrush(this.Color);
      if (this.ColorChanged == null)
        return;
      this.ColorChanged((object) this, this.Color);
    }

    public delegate void ColorChangedHandler(object sender, Color color);
  }
}
