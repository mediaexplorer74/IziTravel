// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Transitions.CustomTransition
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Core.Transitions
{
  public class CustomTransition : TransitionElement
  {
    public static readonly DependencyProperty StoryboardProperty = DependencyProperty.Register(nameof (Storyboard), typeof (Storyboard), typeof (CustomTransition), new PropertyMetadata((object) null));

    public Storyboard Storyboard
    {
      get => (Storyboard) this.GetValue(CustomTransition.StoryboardProperty);
      set => this.SetValue(CustomTransition.StoryboardProperty, (object) value);
    }

    public override ITransition GetTransition(UIElement element)
    {
      if (this.Storyboard != null)
        this.Storyboard.Stop();
      Storyboard.SetTarget((Timeline) this.Storyboard, (DependencyObject) element);
      return (ITransition) new Transition(element, this.Storyboard);
    }
  }
}
