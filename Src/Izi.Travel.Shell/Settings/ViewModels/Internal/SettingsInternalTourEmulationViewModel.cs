// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.ViewModels.Internal.SettingsInternalTourEmulationViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Settings.ViewModels.Internal
{
  public class SettingsInternalTourEmulationViewModel : Screen
  {
    private AppSettings _appSettings;
    private bool _enabled;
    private double _speed;

    public bool Enabled
    {
      get => this._enabled;
      set
      {
        if (this._enabled == value)
          return;
        this._enabled = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.Enabled));
      }
    }

    public double Speed
    {
      get => this._speed;
      set
      {
        if (Math.Abs(this._speed - value) <= double.Epsilon)
          return;
        this._speed = value;
        this.NotifyOfPropertyChange<double>((Expression<Func<double>>) (() => this.Speed));
      }
    }

    protected override void OnActivate()
    {
      this._appSettings = ServiceFacade.SettingsService.GetAppSettings();
      this.Enabled = this._appSettings.TourEmulationEnabled;
      this.Speed = this._appSettings.TourEmulationSpeed;
    }

    protected override void OnDeactivate(bool close)
    {
      this._appSettings.TourEmulationEnabled = this.Enabled;
      this._appSettings.TourEmulationSpeed = this.Speed;
      ServiceFacade.SettingsService.SaveAppSettings(this._appSettings);
    }
  }
}
