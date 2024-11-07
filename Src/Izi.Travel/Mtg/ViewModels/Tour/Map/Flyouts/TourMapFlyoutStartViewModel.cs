// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts.TourMapFlyoutStartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services;
using System;
using System.Linq.Expressions;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Tour.Map.Flyouts
{
  public class TourMapFlyoutStartViewModel : TourMapFlyoutViewModel
  {
    private bool _isEnabled;

    public bool IsEnabled
    {
      get => this._isEnabled;
      set
      {
        if (this._isEnabled == value)
          return;
        this._isEnabled = value;
        this.NotifyOfPropertyChange<bool>((Expression<Func<bool>>) (() => this.IsEnabled));
      }
    }

    public TourMapFlyoutStartViewModel(TourMapPartViewModel mapViewModel)
      : base(mapViewModel)
    {
      this.IsEnabled = ServiceFacade.SettingsService.GetAppSettings().TourStartPromptEnabled;
    }

    protected override void OnClosing()
    {
      AppSettings appSettings = ServiceFacade.SettingsService.GetAppSettings();
      appSettings.TourStartPromptEnabled = this.IsEnabled;
      ServiceFacade.SettingsService.SaveAppSettings(appSettings);
      if (this.MapViewModel == null)
        return;
      this.MapViewModel.StartTour();
    }
  }
}
