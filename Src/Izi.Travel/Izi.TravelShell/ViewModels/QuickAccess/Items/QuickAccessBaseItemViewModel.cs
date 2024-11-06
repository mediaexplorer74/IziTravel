// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.QuickAccess.Items.QuickAccessBaseItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.QuickAccess.Items
{
  public abstract class QuickAccessBaseItemViewModel : PropertyChangedBase
  {
    private readonly QuickAccessViewModel _quickAccessViewModel;

    public QuickAccessViewModel QuickAccessViewModel => this._quickAccessViewModel;

    protected QuickAccessBaseItemViewModel(QuickAccessViewModel quickAccessViewModel)
    {
      this._quickAccessViewModel = quickAccessViewModel;
    }

    public void Activate() => this.OnActivate();

    public void Deactivate() => this.OnDeactivate();

    protected virtual void OnActivate()
    {
    }

    protected virtual void OnDeactivate()
    {
    }
  }
}
