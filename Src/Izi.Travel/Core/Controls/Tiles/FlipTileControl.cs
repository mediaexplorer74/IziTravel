// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Tiles.FlipTileControl
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Tiles
{
  public class FlipTileControl : BaseTileControl
  {
    public static readonly DependencyProperty FrontContentProperty = DependencyProperty.Register(nameof (FrontContent), typeof (object), typeof (FlipTileControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty FrontContentTemplateProperty = DependencyProperty.Register(nameof (FrontContentTemplate), typeof (DataTemplate), typeof (FlipTileControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty BackContentProperty = DependencyProperty.Register(nameof (BackContent), typeof (object), typeof (FlipTileControl), new PropertyMetadata((object) null));
    public static readonly DependencyProperty BackContentTemplateProperty = DependencyProperty.Register(nameof (BackContentTemplate), typeof (DataTemplate), typeof (FlipTileControl), new PropertyMetadata((object) null));
    private static readonly DependencyProperty StateProperty = DependencyProperty.Register(nameof (State), typeof (FlipTileState), typeof (FlipTileControl), new PropertyMetadata((object) FlipTileState.Front, new PropertyChangedCallback(FlipTileControl.OnStatePropertyChanged)));

    public object FrontContent
    {
      get => this.GetValue(FlipTileControl.FrontContentProperty);
      set => this.SetValue(FlipTileControl.FrontContentProperty, value);
    }

    public DataTemplate FrontContentTemplate
    {
      get => (DataTemplate) this.GetValue(FlipTileControl.FrontContentTemplateProperty);
      set => this.SetValue(FlipTileControl.FrontContentTemplateProperty, (object) value);
    }

    public object BackContent
    {
      get => this.GetValue(FlipTileControl.BackContentProperty);
      set => this.SetValue(FlipTileControl.BackContentProperty, value);
    }

    public DataTemplate BackContentTemplate
    {
      get => (DataTemplate) this.GetValue(FlipTileControl.BackContentTemplateProperty);
      set => this.SetValue(FlipTileControl.BackContentTemplateProperty, (object) value);
    }

    internal FlipTileState State
    {
      get => (FlipTileState) this.GetValue(FlipTileControl.StateProperty);
      set => this.SetValue(FlipTileControl.StateProperty, (object) value);
    }

    public event TypedEventHandler<FlipTileControl, FlipTileState> StateChanged
    {
      add
      {
        TypedEventHandler<FlipTileControl, FlipTileState> typedEventHandler1 = this.StateChanged;
        TypedEventHandler<FlipTileControl, FlipTileState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<FlipTileControl, FlipTileState>>(ref this.StateChanged, (TypedEventHandler<FlipTileControl, FlipTileState>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<FlipTileControl, FlipTileState> typedEventHandler1 = this.StateChanged;
        TypedEventHandler<FlipTileControl, FlipTileState> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<FlipTileControl, FlipTileState>>(ref this.StateChanged, (TypedEventHandler<FlipTileControl, FlipTileState>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public FlipTileControl() => this.DefaultStyleKey = (object) typeof (FlipTileControl);

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdateVisualState();
    }

    protected override ITileService CreateTileService() => (ITileService) new FlipTileService();

    private void UpdateVisualState()
    {
      VisualStateManager.GoToState((Control) this, this.State == FlipTileState.Front ? "Front" : "Back", true);
    }

    private void OnStateChanged() => this.StateChanged?.Invoke(this, this.State);

    private static void OnStatePropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      if (!(d is FlipTileControl flipTileControl))
        return;
      flipTileControl.UpdateVisualState();
      flipTileControl.OnStateChanged();
    }
  }
}
