// Decompiled with JetBrains decompiler
// Type: Microsoft.Phone.Controls.NavigationTransition
// Assembly: Coding4Fun.Toolkit.Controls, Version=2.0.7.0, Culture=neutral, PublicKeyToken=c5fd7b72b1a17ce4
// MVID: FA82EF8B-B083-4BA3-8FA6-4342AD0FAB1C
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Coding4Fun.Toolkit.Controls.dll

using System.Windows;

#nullable disable
namespace Microsoft.Phone.Controls
{
  internal class NavigationTransition : DependencyObject
  {
    public static readonly DependencyProperty BackwardProperty = DependencyProperty.Register(nameof (Backward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);
    public static readonly DependencyProperty ForwardProperty = DependencyProperty.Register(nameof (Forward), typeof (TransitionElement), typeof (NavigationTransition), (PropertyMetadata) null);

    public event RoutedEventHandler BeginTransition;

    public event RoutedEventHandler EndTransition;

    public TransitionElement Backward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.BackwardProperty);
      set => this.SetValue(NavigationTransition.BackwardProperty, (object) value);
    }

    public TransitionElement Forward
    {
      get => (TransitionElement) this.GetValue(NavigationTransition.ForwardProperty);
      set => this.SetValue(NavigationTransition.ForwardProperty, (object) value);
    }

    internal void OnBeginTransition()
    {
      if (this.BeginTransition == null)
        return;
      this.BeginTransition((object) this, new RoutedEventArgs());
    }

    internal void OnEndTransition()
    {
      if (this.EndTransition == null)
        return;
      this.EndTransition((object) this, new RoutedEventArgs());
    }
  }
}
