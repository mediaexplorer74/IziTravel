// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Settings.AppSettings
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Media;

#nullable disable
namespace Izi.Travel.Business.Entities.Settings
{
  public class AppSettings
  {
    public const string KeyServerEnvironment = "AppSettingsServerEnvironment";
    public const string KeyLocationEnabled = "AppSettingsLocationEnabled";
    public const string KeyLanguages = "AppSettingsLanguages";
    public const string KeyCodeNames = "AppSettingsCodeNames";
    public const string KeyTourEmulationEnabled = "AppSettingsTourEmulationEnabled";
    public const string KeyTourEmulationSpeed = "AppSettingsTourEmulationSpeed";
    public const string KeyTourStartPromptEnabled = "AppSettingsTourStartPromptEnabled";
    public const string KeyTourPauseOnVideoPromptEnabled = "AppSettingsTourPauseOnVideoPromptEnabled";
    public const string KeyNowPlayingString = "AppSettingsNowPlayingString";
    public const string KeyReviewerName = "AppSettingsReviewerName";
    public const string KeyFirstLaunch = "AppSettingsFirstLaunch";
    public const string KeyUserUid = "AppSettingsUserUid";

    public ServerEnvironment ServerEnvironment { get; set; }

    public bool LocationEnabled { get; set; }

    public string[] Languages { get; set; }

    public string[] CodeNames { get; set; }

    public string CodeName
    {
      get
      {
        return this.CodeNames == null || this.CodeNames.Length == 0 ? (string) null : this.CodeNames[0];
      }
      set
      {
        string[] strArray;
        if (string.IsNullOrWhiteSpace(value))
          strArray = (string[]) null;
        else
          strArray = new string[1]{ value };
        this.CodeNames = strArray;
      }
    }

    public bool TourEmulationEnabled { get; set; }

    public double TourEmulationSpeed { get; set; }

    public bool TourStartPromptEnabled { get; set; }

    public bool TourPauseOnVideoPromptEnabled { get; set; }

    public string NowPlayingString { get; set; }

    public AudioTrackInfo NowPlaying
    {
      get => AudioTrackInfo.FromTag(this.NowPlayingString);
      set => this.NowPlayingString = value?.ToTag();
    }

    public string ReviewerName { get; set; }

    public bool FirstLaunch { get; set; }

    public string UserUid { get; set; }
  }
}
