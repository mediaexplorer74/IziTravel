// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Toolkit.Controls.FlipViewItem
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

#nullable disable
namespace Izi.Travel.Shell.Toolkit.Controls
{
  public class FlipViewItem : ContentControl
  {
    public static readonly DependencyProperty IsSelectedProperty = FlipView.IsSelectedProperty;

    public FlipViewItem() => this.DefaultStyleKey = (object) typeof (FlipViewItem);

    public bool IsSelected
    {
      get => (bool) this.GetValue(FlipViewItem.IsSelectedProperty);
      set => this.SetValue(FlipViewItem.IsSelectedProperty, (object) value);
    }

    internal FlipView ParentFlipView { get; set; }

    internal object Item { get; set; }

    internal void OnIsSelectedChanged(bool newValue)
    {
      if (this.ParentFlipView == null)
        return;
      this.ParentFlipView.NotifyItemSelected(this, newValue);
    }

    protected override void OnManipulationStarted(ManipulationStartedEventArgs e)
    {
      base.OnManipulationStarted(e);
      if (this.ParentFlipView != null)
        this.ParentFlipView.OnManipulationStarted((object) this, e);
      e.Handled = true;
    }

    protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
    {
      base.OnManipulationDelta(e);
      if (this.ParentFlipView != null)
        this.ParentFlipView.OnManipulationDelta((object) this, e);
      e.Handled = true;
    }

    protected override void OnManipulationCompleted(ManipulationCompletedEventArgs e)
    {
      base.OnManipulationCompleted(e);
      if (this.ParentFlipView != null)
        this.ParentFlipView.OnManipulationCompleted((object) this, e);
      e.Handled = true;
    }
  }
}
