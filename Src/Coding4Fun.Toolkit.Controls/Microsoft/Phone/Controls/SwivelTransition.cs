// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.SwivelTransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

#nullable disable
namespace Microsoft.Phone.Controls
{
  internal class SwivelTransition : TransitionElement
  {
    public static readonly DependencyProperty ModeProperty = DependencyProperty.Register(nameof (Mode), typeof (SwivelTransitionMode), typeof (SwivelTransition), (PropertyMetadata) null);

    public SwivelTransitionMode Mode
    {
      get => (SwivelTransitionMode) this.GetValue(SwivelTransition.ModeProperty);
      set => this.SetValue(SwivelTransition.ModeProperty, (object) value);
    }

    public override ITransition GetTransition(UIElement element)
    {
      return Transitions.Swivel(element, this.Mode);
    }
  }
}
