// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Tiles.FlipTileControlBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Tiles
{
  public class FlipTileControlBehavior : Behavior<FlipTileControl>
  {
    public static readonly DependencyProperty StateChangeCommandProperty = DependencyProperty.Register(nameof (StateChangeCommand), typeof (ICommand), typeof (FlipTileControlBehavior), new PropertyMetadata((object) null));

    public ICommand StateChangeCommand
    {
      get => (ICommand) this.GetValue(FlipTileControlBehavior.StateChangeCommandProperty);
      set => this.SetValue(FlipTileControlBehavior.StateChangeCommandProperty, (object) value);
    }

    protected override void OnAttached()
    {
      base.OnAttached();
      // ISSUE: method pointer
      this.AssociatedObject.StateChanged += new TypedEventHandler<FlipTileControl, FlipTileState>((object) this, __methodptr(OnStateChanged));
    }

    protected override void OnDetaching()
    {
      // ISSUE: method pointer
      this.AssociatedObject.StateChanged -= new TypedEventHandler<FlipTileControl, FlipTileState>((object) this, __methodptr(OnStateChanged));
      base.OnDetaching();
    }

    private void OnStateChanged(FlipTileControl control, FlipTileState state)
    {
      if (this.StateChangeCommand == null || !this.StateChangeCommand.CanExecute((object) state))
        return;
      this.StateChangeCommand.Execute((object) state);
    }
  }
}
