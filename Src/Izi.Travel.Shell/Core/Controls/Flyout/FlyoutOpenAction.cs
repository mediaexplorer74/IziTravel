// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Controls.Flyout.FlyoutOpenAction
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Core.Controls.Flyout
{
  public class FlyoutOpenAction : TriggerAction<FrameworkElement>
  {
    public string FlyoutKey { get; set; }

    protected override void Invoke(object parameter)
    {
      FlyoutBase.ShowAttachedFlyout(this.AssociatedObject, this.FlyoutKey);
    }
  }
}
