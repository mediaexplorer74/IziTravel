// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.LongListSelectorSelectBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class LongListSelectorSelectBehavior : Behavior<LongListSelector>
  {
    public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof (SelectCommand), typeof (ICommand), typeof (LongListSelectorSelectBehavior), new PropertyMetadata((object) null));

    public ICommand SelectCommand
    {
      get => (ICommand) this.GetValue(LongListSelectorSelectBehavior.SelectCommandProperty);
      set => this.SetValue(LongListSelectorSelectBehavior.SelectCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.SelectionChanged += new SelectionChangedEventHandler(this.OnSelectorSelectionChanged);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.SelectionChanged -= new SelectionChangedEventHandler(this.OnSelectorSelectionChanged);
      base.OnDetaching();
    }

    private void OnSelectorSelectionChanged(
      object sender,
      SelectionChangedEventArgs selectionChangedEventArgs)
    {
      if (!(sender is LongListSelector longListSelector) || longListSelector.SelectedItem == null || this.SelectCommand == null || !this.SelectCommand.CanExecute(longListSelector.SelectedItem))
        return;
      this.SelectCommand.Execute(longListSelector.SelectedItem);
      longListSelector.SelectedItem = (object) null;
    }
  }
}
