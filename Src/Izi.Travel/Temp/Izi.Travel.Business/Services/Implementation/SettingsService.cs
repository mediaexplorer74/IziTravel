// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.SettingsService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Settings;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  public class SettingsService : ISettingsService
  {
    private readonly IziTravelClient _iziTravelClient;

    public SettingsService(IziTravelClient iziTravelClient)
    {
      this._iziTravelClient = iziTravelClient;
    }

    public void Initialize()
    {
      AppSettings appSettings = this.GetAppSettings();
      string[] languages = appSettings.Languages;
      if (languages == null)
        return;
      for (int index = 0; index < languages.Length; ++index)
      {
        string str = languages[index];
        if (str != null)
        {
          if (str.Contains<char>('-'))
          {
            try
            {
              CultureInfo cultureInfo = new CultureInfo(str);
              languages[index] = cultureInfo.TwoLetterISOLanguageName;
            }
            catch
            {
            }
          }
        }
      }
      appSettings.Languages = languages;
      this.SetIziTravelClientValues(appSettings);
      SettingsService.SetUserIdValue(appSettings);
      this.SaveAppSettings(appSettings);
    }

    public AppSettings GetAppSettings()
    {
      return new AppSettings()
      {
        ServerEnvironment = SettingsService.GetIsolatedSettingsValue<ServerEnvironment>("AppSettingsServerEnvironment", ServerEnvironment.Production),
        LocationEnabled = SettingsService.GetIsolatedSettingsValue<bool>("AppSettingsLocationEnabled", true),
        Languages = SettingsService.GetIsolatedSettingsValue<string[]>("AppSettingsLanguages", (string[]) null),
        CodeNames = SettingsService.GetIsolatedSettingsValue<string[]>("AppSettingsCodeNames", (string[]) null),
        TourEmulationEnabled = SettingsService.GetIsolatedSettingsValue<bool>("AppSettingsTourEmulationEnabled", false),
        TourEmulationSpeed = SettingsService.GetIsolatedSettingsValue<double>("AppSettingsTourEmulationSpeed", 4.0),
        TourStartPromptEnabled = SettingsService.GetIsolatedSettingsValue<bool>("AppSettingsTourStartPromptEnabled", true),
        TourPauseOnVideoPromptEnabled = SettingsService.GetIsolatedSettingsValue<bool>("AppSettingsTourPauseOnVideoPromptEnabled", true),
        NowPlayingString = SettingsService.GetIsolatedSettingsValue<string>("AppSettingsNowPlayingString", (string) null),
        ReviewerName = SettingsService.GetIsolatedSettingsValue<string>("AppSettingsReviewerName", (string) null),
        FirstLaunch = SettingsService.GetIsolatedSettingsValue<bool>("AppSettingsFirstLaunch", true),
        UserUid = SettingsService.GetIsolatedSettingsValue<string>("AppSettingsUserUid", (string) null)
      };
    }

    public void SaveAppSettings(AppSettings appSettings)
    {
      SettingsService.SetIsolatedSettingsValue("AppSettingsServerEnvironment", (object) appSettings.ServerEnvironment);
      SettingsService.SetIsolatedSettingsValue("AppSettingsLocationEnabled", (object) appSettings.LocationEnabled);
      SettingsService.SetIsolatedSettingsValue("AppSettingsLanguages", (object) appSettings.Languages);
      SettingsService.SetIsolatedSettingsValue("AppSettingsCodeNames", (object) appSettings.CodeNames);
      SettingsService.SetIsolatedSettingsValue("AppSettingsTourEmulationEnabled", (object) appSettings.TourEmulationEnabled);
      SettingsService.SetIsolatedSettingsValue("AppSettingsTourEmulationSpeed", (object) appSettings.TourEmulationSpeed);
      SettingsService.SetIsolatedSettingsValue("AppSettingsTourStartPromptEnabled", (object) appSettings.TourStartPromptEnabled);
      SettingsService.SetIsolatedSettingsValue("AppSettingsTourPauseOnVideoPromptEnabled", (object) appSettings.TourPauseOnVideoPromptEnabled);
      SettingsService.SetIsolatedSettingsValue("AppSettingsNowPlayingString", (object) appSettings.NowPlayingString);
      SettingsService.SetIsolatedSettingsValue("AppSettingsReviewerName", (object) appSettings.ReviewerName);
      SettingsService.SetIsolatedSettingsValue("AppSettingsFirstLaunch", (object) appSettings.FirstLaunch);
      SettingsService.SetIsolatedSettingsValue("AppSettingsUserUid", (object) appSettings.UserUid);
      this.SetIziTravelClientValues(appSettings);
      IsolatedStorageSettings.ApplicationSettings.Save();
    }

    public string[] GetAppSettingsChangeKeys(AppSettings settings)
    {
      if (settings == null)
        return (string[]) null;
      AppSettings appSettings = this.GetAppSettings();
      if (appSettings == null)
        return (string[]) null;
      List<string> stringList = new List<string>();
      if (appSettings.ServerEnvironment != settings.ServerEnvironment)
        stringList.Add("AppSettingsServerEnvironment");
      if (appSettings.LocationEnabled != settings.LocationEnabled)
        stringList.Add("AppSettingsLocationEnabled");
      if (!((IEnumerable<string>) appSettings.Languages).SequenceEqual<string>((IEnumerable<string>) settings.Languages))
        stringList.Add("AppSettingsLanguages");
      if (!string.Equals(appSettings.CodeName, settings.CodeName))
        stringList.Add("AppSettingsCodeNames");
      if (appSettings.TourEmulationEnabled != settings.TourEmulationEnabled)
        stringList.Add("AppSettingsTourEmulationEnabled");
      if (Math.Abs(appSettings.TourEmulationSpeed - settings.TourEmulationSpeed) > double.Epsilon)
        stringList.Add("AppSettingsTourEmulationSpeed");
      if (appSettings.TourStartPromptEnabled != settings.TourStartPromptEnabled)
        stringList.Add("AppSettingsTourStartPromptEnabled");
      if (appSettings.TourPauseOnVideoPromptEnabled != settings.TourPauseOnVideoPromptEnabled)
        stringList.Add("AppSettingsTourPauseOnVideoPromptEnabled");
      if (appSettings.NowPlayingString != settings.NowPlayingString)
        stringList.Add("AppSettingsNowPlayingString");
      if (appSettings.ReviewerName != settings.ReviewerName)
        stringList.Add("AppSettingsReviewerName");
      if (appSettings.FirstLaunch != settings.FirstLaunch)
        stringList.Add("AppSettingsFirstLaunch");
      if (appSettings.UserUid != settings.UserUid)
        stringList.Add("AppSettingsUserUid");
      return stringList.ToArray();
    }

    private static T GetIsolatedSettingsValue<T>(string key, T defaultValue)
    {
      return !IsolatedStorageSettings.ApplicationSettings.Contains(key) ? defaultValue : (T) IsolatedStorageSettings.ApplicationSettings[key];
    }

    private static void SetIsolatedSettingsValue(string key, object value)
    {
      if (!IsolatedStorageSettings.ApplicationSettings.Contains(key))
        IsolatedStorageSettings.ApplicationSettings.Add(key, value);
      else
        IsolatedStorageSettings.ApplicationSettings[key] = value;
    }

    private void SetIziTravelClientValues(AppSettings settings)
    {
      if (settings == null || this._iziTravelClient == null)
        return;
      this._iziTravelClient.Environment = SettingsService.GetIziTravelClientEnvironment(settings.ServerEnvironment);
      this._iziTravelClient.Password = settings.CodeName;
    }

    private static void SetUserIdValue(AppSettings settings)
    {
      if (settings == null || !string.IsNullOrWhiteSpace(settings.UserUid))
        return;
      settings.UserUid = Guid.NewGuid().ToString("D").ToUpper();
    }

    private static IziTravelEnvironment GetIziTravelClientEnvironment(ServerEnvironment environment)
    {
      if (environment == ServerEnvironment.Stage)
        return IziTravelEnvironment.Stage;
      return environment == ServerEnvironment.Development ? IziTravelEnvironment.Development : IziTravelEnvironment.Production;
    }
  }
}
