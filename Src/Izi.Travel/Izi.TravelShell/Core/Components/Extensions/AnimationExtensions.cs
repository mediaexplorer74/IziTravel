// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.AnimationExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public static class AnimationExtensions
  {
    public static readonly DependencyProperty StoryboardProperty = DependencyProperty.RegisterAttached("Storyboard", typeof (Storyboard), typeof (AnimationExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(AnimationExtensions.OnStoryboardPropertyChanged)));
    public static readonly DependencyProperty IsPlayingProperty = DependencyProperty.RegisterAttached("IsPlaying", typeof (bool), typeof (AnimationExtensions), new PropertyMetadata((object) false, new PropertyChangedCallback(AnimationExtensions.OnIsPlayingPropertyChanged)));
    public static readonly DependencyProperty CompletedCommandProperty = DependencyProperty.RegisterAttached("CompletedCommand", typeof (ICommand), typeof (AnimationExtensions), new PropertyMetadata((object) null, new PropertyChangedCallback(AnimationExtensions.OnCompletedCommandPropertyChanged)));
    public static readonly DependencyProperty CompletedCommandParameterProperty = DependencyProperty.RegisterAttached("CompletedCommandParameter", typeof (object), typeof (AnimationExtensions), new PropertyMetadata((object) null));

    public static void SetStoryboard(DependencyObject element, Storyboard value)
    {
      element.SetValue(AnimationExtensions.StoryboardProperty, (object) value);
    }

    public static Storyboard GetStoryboard(DependencyObject element)
    {
      return (Storyboard) element.GetValue(AnimationExtensions.StoryboardProperty);
    }

    private static void OnStoryboardPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      Storyboard oldValue = (Storyboard) e.OldValue;
      Storyboard storyboard = (Storyboard) d.GetValue(AnimationExtensions.StoryboardProperty);
      oldValue?.Stop();
      if (storyboard == null || !AnimationExtensions.GetIsPlaying(d))
        return;
      storyboard.Begin();
    }

    public static bool GetIsPlaying(DependencyObject d)
    {
      return (bool) d.GetValue(AnimationExtensions.IsPlayingProperty);
    }

    public static void SetIsPlaying(DependencyObject d, bool value)
    {
      d.SetValue(AnimationExtensions.IsPlayingProperty, (object) value);
    }

    private static void OnIsPlayingPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      bool flag = (bool) d.GetValue(AnimationExtensions.IsPlayingProperty);
      Storyboard storyboard = AnimationExtensions.GetStoryboard(d);
      if (storyboard == null)
        return;
      if (!flag)
        storyboard.Stop();
      if (!flag)
        return;
      storyboard.Begin();
    }

    public static void SetCompletedCommand(Storyboard element, ICommand value)
    {
      element.SetValue(AnimationExtensions.CompletedCommandProperty, (object) value);
    }

    public static ICommand GetCompletedCommand(Storyboard element)
    {
      return (ICommand) element.GetValue(AnimationExtensions.CompletedCommandProperty);
    }

    private static void OnCompletedCommandPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is Storyboard storyboard))
        return;
      ICommand oldValue = (ICommand) e.OldValue;
      ICommand command = (ICommand) d.GetValue(AnimationExtensions.CompletedCommandProperty);
      if (oldValue != null)
        storyboard.Completed -= new EventHandler(AnimationExtensions.OnStoryboardCompleted);
      if (command == null)
        return;
      storyboard.Completed += new EventHandler(AnimationExtensions.OnStoryboardCompleted);
    }

    private static void OnStoryboardCompleted(object sender, EventArgs eventArgs)
    {
      if (!(sender is Storyboard storyboard))
        return;
      ICommand command = (ICommand) storyboard.GetValue(AnimationExtensions.CompletedCommandProperty);
      if (command == null)
        return;
      object parameter = storyboard.GetValue(AnimationExtensions.CompletedCommandParameterProperty);
      if (!command.CanExecute(parameter))
        return;
      command.Execute(parameter);
    }

    public static void SetCompletedCommandParameter(DependencyObject element, object value)
    {
      element.SetValue(AnimationExtensions.CompletedCommandParameterProperty, value);
    }

    public static object GetCompletedCommandParameter(DependencyObject element)
    {
      return element.GetValue(AnimationExtensions.CompletedCommandParameterProperty);
    }
  }
}
