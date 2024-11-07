// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.PasscodeFlyoutViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Common.ViewModels.Flyout;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Extensions;
using System;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels
{
  public class PasscodeFlyoutViewModel : FlyoutViewModel
  {
    private string _passcode;
    private RelayCommand _saveCommand;

    public string Passcode
    {
      get => this._passcode;
      set => this.SetProperty<string>(ref this._passcode, value, propertyName: nameof (Passcode));
    }

    public RelayCommand SaveCommand
    {
      get
      {
        return this._saveCommand ?? (this._saveCommand = new RelayCommand(new Action<object>(this.ExecuteSaveCommand)));
      }
    }

    private void ExecuteSaveCommand(object parameter)
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      appSettings.CodeName = this.Passcode;
      ServiceFacade.SettingsService.SaveAppSettings(appSettings);
      this.IsOpen = false;
    }

    protected override void OnOpening()
    {
      this.Passcode = ServiceFacade.SettingsService.GetAppSettings().CodeName;
    }
  }
}
