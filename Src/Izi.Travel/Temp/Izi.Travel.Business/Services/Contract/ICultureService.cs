// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.ICultureService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Culture;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface ICultureService
  {
    LanguageData GetLanguageByIsoCode(string code);

    LanguageData GetLanguageByName(string name);

    LanguageData[] GetNeutralLanguages();

    string[] GetNeutralLanguageCodes();

    RegionData GetRegionByIsoCode(string code);
  }
}
