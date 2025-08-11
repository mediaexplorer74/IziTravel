// ********************************************************************
// Type: Izi.Travel.Shell.Core.Components.Behaviors.SelectorSelectBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Microsoft.Xaml.Interactivity;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class SelectorSelectBehavior : Behavior<Selector>
  {
    public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof (SelectCommand), typeof (System.Windows.Input.ICommand), typeof (SelectorSelectBehavior), new PropertyMetadata((object) null));

    public System.Windows.Input.ICommand SelectCommand
    {
      get => (System.Windows.Input.ICommand) this.GetValue(SelectorSelectBehavior.SelectCommandProperty);
      set => this.SetValue(SelectorSelectBehavior.SelectCommandProperty, (object) value);
    }

    public bool SuppressReset { get; set; }

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

    private void OnSelectorSelectionChanged(object sender, SelectionChangedEventArgs args)
    {
      if (!(sender is Selector selector) || selector.SelectedItem == null || this.SelectCommand == null || !this.SelectCommand.CanExecute(selector.SelectedItem))
        return;
      this.SelectCommand.Execute(selector.SelectedItem);
      if (this.SuppressReset)
        return;
      selector.SelectedItem = (object) null;
    }
  }
}

