// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.ListItemControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
{
  public abstract class ListItemControl : Control
  {
    public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(nameof (Title), typeof (string), typeof (ListItemControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(nameof (ImageSource), typeof (ImageSource), typeof (ListItemControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty ImageWidthProperty = DependencyProperty.Register(nameof (ImageWidth), typeof (double), typeof (ListItemControl), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty ImageHeightProperty = DependencyProperty.Register(nameof (ImageHeight), typeof (double), typeof (ListItemControl), new PropertyMetadata((object) 0.0));

    public string Title
    {
      get => (string) this.GetValue(ListItemControl.TitleProperty);
      set => this.SetValue(ListItemControl.TitleProperty, (object) value);
    }

    public ImageSource ImageSource
    {
      get => (ImageSource) this.GetValue(ListItemControl.ImageSourceProperty);
      set => this.SetValue(ListItemControl.ImageSourceProperty, (object) value);
    }

    public double ImageWidth
    {
      get => (double) this.GetValue(ListItemControl.ImageWidthProperty);
      set => this.SetValue(ListItemControl.ImageWidthProperty, (object) value);
    }

    public double ImageHeight
    {
      get => (double) this.GetValue(ListItemControl.ImageHeightProperty);
      set => this.SetValue(ListItemControl.ImageHeightProperty, (object) value);
    }
  }
}
