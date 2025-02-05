// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Extensions.UIElementExtensions
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Extensions
{
  public class UIElementExtensions
  {
    public static readonly DependencyProperty TransitionInElementProperty = DependencyProperty.RegisterAttached("TransitionInElement", typeof (TransitionElement), typeof (UIElementExtensions), new PropertyMetadata((object) null));
    public static readonly DependencyProperty TransitionOutElementProperty = DependencyProperty.RegisterAttached("TransitionOutElement", typeof (TransitionElement), typeof (UIElementExtensions), new PropertyMetadata((object) null));
    public static readonly DependencyProperty TransitionIsVisibleProperty = DependencyProperty.RegisterAttached("TransitionIsVisible", typeof (bool), typeof (UIElementExtensions), new PropertyMetadata((object) true, new PropertyChangedCallback(UIElementExtensions.OnTransitionIsVisiblePropertyChanged)));

    public static void SetTransitionInElement(DependencyObject element, TransitionElement value)
    {
      element.SetValue(UIElementExtensions.TransitionInElementProperty, (object) value);
    }

    public static TransitionElement GetTransitionInElement(DependencyObject element)
    {
      return (TransitionElement) element.GetValue(UIElementExtensions.TransitionInElementProperty);
    }

    public static void SetTransitionOutElement(DependencyObject element, TransitionElement value)
    {
      element.SetValue(UIElementExtensions.TransitionOutElementProperty, (object) value);
    }

    public static TransitionElement GetTransitionOutElement(DependencyObject element)
    {
      return (TransitionElement) element.GetValue(UIElementExtensions.TransitionOutElementProperty);
    }

    public static void SetTransitionIsVisible(DependencyObject element, bool value)
    {
      element.SetValue(UIElementExtensions.TransitionIsVisibleProperty, (object) value);
    }

    public static bool GetTransitionIsVisible(DependencyObject element)
    {
      return (bool) element.GetValue(UIElementExtensions.TransitionIsVisibleProperty);
    }

    private static void OnTransitionIsVisiblePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      UIElement element = d as UIElement;
      if (element == null || !(e.NewValue is bool))
        return;
      if (!(bool) e.NewValue)
      {
        TransitionElement transitionOutElement = UIElementExtensions.GetTransitionOutElement((DependencyObject) element);
        if (transitionOutElement != null)
        {
          ITransition transition = transitionOutElement.GetTransition(element);
          transition.Completed += (EventHandler) ((obj, args) =>
          {
            transition.Stop();
            element.Visibility = Visibility.Collapsed;
          });
          transition.Begin();
        }
        else
          element.Visibility = Visibility.Collapsed;
      }
      else
      {
        element.Visibility = Visibility.Visible;
        TransitionElement transitionInElement = UIElementExtensions.GetTransitionInElement((DependencyObject) element);
        if (transitionInElement == null)
          return;
        ITransition transition = transitionInElement.GetTransition(element);
        transition.Completed += (EventHandler) ((obj, args) => transition.Stop());
        transition.Begin();
      }
    }
  }
}
