// Decompiled with JetBrains decompiler
// Type: Coding4Fun.Toolkit.Controls.ButtonBase
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Coding4Fun.Toolkit.Controls
{
  public abstract class ButtonBase : Button, IButtonBase
  {
    public static readonly DependencyProperty LabelProperty = DependencyProperty.Register(nameof (Label), typeof (object), typeof (ButtonBase), new PropertyMetadata((object) string.Empty));

    public override void OnApplyTemplate() => base.OnApplyTemplate();

    public object Label
    {
      get => this.GetValue(ButtonBase.LabelProperty);
      set => this.SetValue(ButtonBase.LabelProperty, value);
    }
  }
}
