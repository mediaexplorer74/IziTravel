// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Application.SettingsAppLocationViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Application
{
  public class SettingsAppLocationViewModel : Screen
  {
    private bool _locationEnabled;

    public bool LocationEnabled
    {
      get => this._locationEnabled;
      set
      {
        if (this._locationEnabled == value)
          return;
        this._locationEnabled = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.LocationEnabled));
      }
    }

    protected override void OnActivate()
    {
      this.LocationEnabled = ServiceFacade.SettingsService.GetAppSettings().LocationEnabled;
    }

    protected override void OnDeactivate(bool close)
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      appSettings.LocationEnabled = this.LocationEnabled;
      ServiceFacade.SettingsService.SaveAppSettings(appSettings);
    }
  }
}
