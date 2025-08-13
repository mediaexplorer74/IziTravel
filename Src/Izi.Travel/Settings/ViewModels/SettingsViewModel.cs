// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.SettingsViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Resources;
using Izi.Travel.Settings.ViewModels.Application;
using System;

#nullable disable
namespace Izi.Travel.Settings.ViewModels
{
  public class SettingsViewModel : Conductor<IScreen>.Collection.OneActive
  {
    public override string DisplayName
    {
      get => AppResources.LabelSettings.ToUpper();
      set => throw new NotImplementedException();
    }

    public PasscodeFlyoutViewModel PasscodeFlyoutViewModel { get; private set; }

    public SettingsViewModel() => this.PasscodeFlyoutViewModel = new PasscodeFlyoutViewModel();

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.Items.Add((IScreen) IoC.Get<SettingsAppViewModel>());
      this.ActivateItem(this.Items[0]);
    }
  }
}
