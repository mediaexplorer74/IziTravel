// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.Behaviors.LongListSelectorLoadBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Components.Behaviors
{
  public class LongListSelectorLoadBehavior : Behavior<LongListSelector>
  {
    private readonly object _lockObject = new object();
    public static readonly DependencyProperty LoadCommandProperty = DependencyProperty.Register(nameof (LoadCommand), typeof (ICommand), typeof (LongListSelectorLoadBehavior), new PropertyMetadata((object) null));
    public static readonly DependencyProperty LoadItemCommandProperty = DependencyProperty.Register(nameof (LoadItemCommand), typeof (ICommand), typeof (LongListSelectorLoadBehavior), new PropertyMetadata((object) null));

    public ICommand LoadCommand
    {
      get => (ICommand) this.GetValue(LongListSelectorLoadBehavior.LoadCommandProperty);
      set => this.SetValue(LongListSelectorLoadBehavior.LoadCommandProperty, (object) value);
    }

    public ICommand LoadItemCommand
    {
      get => (ICommand) this.GetValue(LongListSelectorLoadBehavior.LoadItemCommandProperty);
      set => this.SetValue(LongListSelectorLoadBehavior.LoadItemCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.ItemRealized += new EventHandler<ItemRealizationEventArgs>(this.OnItemRealized);
    }

    protected override void OnDetaching()
    {
      base.OnDetaching();
      this.AssociatedObject.ItemRealized -= new EventHandler<ItemRealizationEventArgs>(this.OnItemRealized);
    }

    private void OnItemRealized(object sender, ItemRealizationEventArgs e)
    {
      this.TryExecuteLoadCommand(sender, e);
      this.TryExecuteLoadItemCommand(sender, e);
    }

    private void TryExecuteLoadCommand(object sender, ItemRealizationEventArgs e)
    {
      if (this.LoadCommand == null || !(sender is LongListSelector longListSelector) || longListSelector.ItemsSource == null)
        return;
      lock (this._lockObject)
      {
        if (e.ItemKind != LongListSelectorItemKind.Item || e.Container.Content == null || !e.Container.Content.Equals(longListSelector.ItemsSource[longListSelector.ItemsSource.Count - 1]) || !this.LoadCommand.CanExecute((object) null))
          return;
        this.LoadCommand.Execute((object) null);
      }
    }

    private void TryExecuteLoadItemCommand(object sender, ItemRealizationEventArgs e)
    {
      if (this.LoadItemCommand == null || !(sender is LongListSelector longListSelector) || longListSelector.ItemsSource == null)
        return;
      lock (this._lockObject)
      {
        if (e.ItemKind != LongListSelectorItemKind.Item || e.Container.Content == null || !this.LoadItemCommand.CanExecute(e.Container.Content))
          return;
        this.LoadItemCommand.Execute(e.Container.Content);
      }
    }
  }
}
