// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.ISettingsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Settings;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface ISettingsService
  {
    void Initialize();

    AppSettings GetAppSettings();

    void SaveAppSettings(AppSettings appSettings);

    string[] GetAppSettingsChangeKeys(AppSettings settings);
  }
}
