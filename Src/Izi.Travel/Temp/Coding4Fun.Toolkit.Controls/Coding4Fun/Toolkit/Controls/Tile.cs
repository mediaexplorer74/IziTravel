// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.Tile
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public class Tile : ButtonBase
  {
    public static readonly DependencyProperty TextWrappingProperty = DependencyProperty.Register(nameof (TextWrapping), typeof (TextWrapping), typeof (Tile), new PropertyMetadata((object) TextWrapping.NoWrap));

    public Tile() => this.DefaultStyleKey = (object) typeof (Tile);

    public TextWrapping TextWrapping
    {
      get => (TextWrapping) this.GetValue(Tile.TextWrappingProperty);
      set => this.SetValue(Tile.TextWrappingProperty, (object) value);
    }
  }
}
