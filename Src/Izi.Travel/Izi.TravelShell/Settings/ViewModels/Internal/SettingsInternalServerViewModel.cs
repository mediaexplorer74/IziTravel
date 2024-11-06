// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Internal.SettingsInternalServerViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services.Contract;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Internal
{
  public class SettingsInternalServerViewModel : Screen
  {
    private readonly ISettingsService _settingsService;
    private AppSettings _appSettings;
    private readonly ServerEnvironment[] _serverEnvironments;
    private ServerEnvironment _selectedServerEnvironment;

    public ServerEnvironment[] ServerEnvironments => this._serverEnvironments;

    public ServerEnvironment SelectedServerEnvironment
    {
      get => this._selectedServerEnvironment;
      set
      {
        if (this._selectedServerEnvironment == value)
          return;
        this._selectedServerEnvironment = value;
        this.NotifyOfPropertyChange<ServerEnvironment>((Expression<Func<ServerEnvironment>>) (() => this.SelectedServerEnvironment));
      }
    }

    public SettingsInternalServerViewModel(ISettingsService settingsService)
    {
      this._settingsService = settingsService;
      this._serverEnvironments = (ServerEnvironment[]) Enum.GetValues(typeof (ServerEnvironment));
    }

    protected override void OnActivate()
    {
      this._appSettings = this._settingsService.GetAppSettings();
      this.SelectedServerEnvironment = this._appSettings.ServerEnvironment;
    }

    protected override void OnDeactivate(bool close)
    {
      this._appSettings.ServerEnvironment = this.SelectedServerEnvironment;
      this._settingsService.SaveAppSettings(this._appSettings);
    }
  }
}
