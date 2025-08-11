// ********************************************************************
// Type: Izi.Travel.Business.Services.Contract.ICultureService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Culture;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  /// <summary>
  /// Provides culture-related services for the application
  /// </summary>
  public interface ICultureService
  {
    /// <summary>
    /// Gets a language by its ISO 639-1 code (e.g., "en", "ru", "zh")
    /// </summary>
    /// <param name="code">The ISO 639-1 language code</param>
    /// <returns>The language data or null if not found</returns>
    LanguageData GetLanguageByIsoCode(string code);

    /// <summary>
    /// Gets a language by its display name
    /// </summary>
    /// <param name="name">The display name of the language</param>
    /// <returns>The language data or null if not found</returns>
    LanguageData GetLanguageByName(string name);

    /// <summary>
    /// Gets all neutral (culture-invariant) languages
    /// </summary>
    /// <returns>Array of neutral languages</returns>
    LanguageData[] GetNeutralLanguages();

    /// <summary>
    /// Gets ISO codes of all neutral (culture-invariant) languages
    /// </summary>
    /// <returns>Array of ISO language codes</returns>
    string[] GetNeutralLanguageCodes();

    /// <summary>
    /// Gets a region by its ISO 3166-1 alpha-2 code (e.g., "US", "RU", "CN")
    /// </summary>
    /// <param name="code">The ISO 3166-1 alpha-2 region code</param>
    /// <returns>The region data or null if not found</returns>
    RegionData GetRegionByIsoCode(string code);
  }
}
