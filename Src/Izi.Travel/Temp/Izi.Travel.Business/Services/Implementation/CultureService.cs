// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.CultureService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Culture;
using Izi.Travel.Business.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  public class CultureService : ICultureService
  {
    public LanguageData GetLanguageByIsoCode(string code)
    {
      return !string.IsNullOrWhiteSpace(code) ? CultureTables.Languages.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => x.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))) : (LanguageData) null;
    }

    public LanguageData GetLanguageByName(string name)
    {
      return !string.IsNullOrWhiteSpace(name) ? CultureTables.Languages.FirstOrDefault<LanguageData>((Func<LanguageData, bool>) (x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))) : (LanguageData) null;
    }

    public LanguageData[] GetNeutralLanguages()
    {
      return CultureTables.Languages.Where<LanguageData>((Func<LanguageData, bool>) (x => x.IsNeutral)).ToArray<LanguageData>();
    }

    public string[] GetNeutralLanguageCodes()
    {
      return ((IEnumerable<LanguageData>) this.GetNeutralLanguages()).Select<LanguageData, string>((Func<LanguageData, string>) (x => x.Code)).ToArray<string>();
    }

    public RegionData GetRegionByIsoCode(string code)
    {
      if (string.IsNullOrWhiteSpace(code))
        return (RegionData) null;
      if (code.Equals("ru", StringComparison.InvariantCultureIgnoreCase))
        return CultureTables.Regions.FirstOrDefault<RegionData>((Func<RegionData, bool>) (x => x.Name.Equals("ru-ru", StringComparison.InvariantCultureIgnoreCase)));
      return !string.IsNullOrWhiteSpace(code) ? CultureTables.Regions.FirstOrDefault<RegionData>((Func<RegionData, bool>) (x => x.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase))) : (RegionData) null;
    }
  }
}
