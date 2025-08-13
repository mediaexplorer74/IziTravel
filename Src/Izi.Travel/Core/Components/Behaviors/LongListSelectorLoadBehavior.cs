// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.LongListSelectorLoadBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll


using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using System.Windows.Input;
using Izi.Travel.Controls;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class LongListSelectorLoadBehavior : Behavior<LongListSelector>
  {
    private readonly object _lockObject = new object();
    public static readonly DependencyProperty LoadCommandProperty = DependencyProperty.Register(nameof (LoadCommand), typeof (System.Windows.Input.ICommand), typeof (LongListSelectorLoadBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty LoadItemCommandProperty = DependencyProperty.Register(nameof (LoadItemCommand), typeof (System.Windows.Input.ICommand), typeof (LongListSelectorLoadBehavior), new PropertyMetadata((object) null));

    public System.Windows.Input.ICommand LoadCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(LongListSelectorLoadBehavior.LoadCommandProperty);
      set => this.SetValue(LongListSelectorLoadBehavior.LoadCommandProperty, (object) value);
    }

    public System.Windows.Input.ICommand LoadItemCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(LongListSelectorLoadBehavior.LoadItemCommandProperty);
      set => this.SetValue(LongListSelectorLoadBehavior.LoadItemCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      // Behavior kept for XAML compatibility; actual load/selection is handled in control itself.
    }

    protected override void OnDetaching()
    {
      base.OnDetaching();
    }

    // No-op methods retained for legacy code compatibility
  }
}

