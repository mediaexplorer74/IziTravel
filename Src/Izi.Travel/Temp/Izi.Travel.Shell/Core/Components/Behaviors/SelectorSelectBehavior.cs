// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.SelectorSelectBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class SelectorSelectBehavior : Behavior<Selector>
  {
    public static readonly DependencyProperty SelectCommandProperty = DependencyProperty.Register(nameof (SelectCommand), typeof (ICommand), typeof (SelectorSelectBehavior), new PropertyMetadata((object) null));

    public ICommand SelectCommand
    {
      get => (ICommand) this.GetValue(SelectorSelectBehavior.SelectCommandProperty);
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
