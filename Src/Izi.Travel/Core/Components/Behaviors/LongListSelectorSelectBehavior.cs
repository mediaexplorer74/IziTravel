// ********************************************************************
// Type: Izi.Travel.Core.Components.Behaviors.LongListSelectorSelectBehavior
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Core.Components.Behaviors
{
  public class LongListSelectorSelectBehavior : Behavior<ListView>
  {
    public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof (SelectCommand), typeof (System.Windows.Input.ICommand), typeof (LongListSelectorSelectBehavior), new PropertyMetadata((object) null));

    public System.Windows.Input.ICommand SelectCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(LongListSelectorSelectBehavior.SelectCommandProperty);
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
      if (!(sender is ListView listView) || listView.SelectedItem == null || this.SelectCommand == null || !this.SelectCommand.CanExecute(listView.SelectedItem))
        return;
      this.SelectCommand.Execute(listView.SelectedItem);
      listView.SelectedItem = (object) null;
    }
  }
}

