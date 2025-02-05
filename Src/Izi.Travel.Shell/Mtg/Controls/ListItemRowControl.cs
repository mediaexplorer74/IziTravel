// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.ListItemRowControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
{
  public class ListItemRowControl : ListItemControl
  {
    public static readonly DependencyProperty ShowNumberProperty = DependencyProperty.Register(nameof (ShowNumber), typeof (bool), typeof (ListItemRowControl), new PropertyMetadata((object) false));
    public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof (Number), typeof (string), typeof (ListItemRowControl), new PropertyMetadata((object) null));

    public bool ShowNumber
    {
      get => (bool) this.GetValue(ListItemRowControl.ShowNumberProperty);
      set => this.SetValue(ListItemRowControl.ShowNumberProperty, (object) value);
    }

    public string Number
    {
      get => (string) this.GetValue(ListItemRowControl.NumberProperty);
      set => this.SetValue(ListItemRowControl.NumberProperty, (object) value);
    }

    public ListItemRowControl() => this.DefaultStyleKey = (object) typeof (ListItemRowControl);
  }
}
