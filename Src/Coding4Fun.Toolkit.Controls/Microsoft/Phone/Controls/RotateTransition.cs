// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.RotateTransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

#nullable disable
namespace Microsoft.Phone.Controls
{
  internal class RotateTransition : TransitionElement
  {
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (RotateTransitionMode), typeof (RotateTransition), (PropertyMetadata) null);

    public RotateTransitionMode Mode
    {
      get => (RotateTransitionMode) this.GetValue(RotateTransition.ModeProperty);
      set => this.SetValue(RotateTransition.ModeProperty, (object) value);
    }

    public override ITransition GetTransition(UIElement element)
    {
      return Transitions.Rotate(element, this.Mode);
    }
  }
}
