// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Implementation.CultureTables
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Culture;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Services.Implementation
{
  public static class CultureTables
  {
    public static readonly List<LanguageData> Languages = new List<LanguageData>();
    public static readonly List<RegionData> Regions = new List<RegionData>();

    static CultureTables()
    {
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "54",
        Name = "af",
        Code = "af",
        EnglishName = "Afrikaans",
        NativeName = "Afrikaans",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "AF",
        Code = "AF",
        EnglishName = "Afghanistan",
        NativeName = "افغانستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1078",
        Name = "af-ZA",
        Code = "af",
        EnglishName = "Afrikaans (South Africa)",
        NativeName = "Afrikaans (Suid-Afrika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "af-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "Suid-Afrika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "94",
        Name = "am",
        Code = "am",
        EnglishName = "Amharic",
        NativeName = "አማርኛ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "AM",
        Code = "AM",
        EnglishName = "Armenia",
        NativeName = "Հայաստան"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1118",
        Name = "am-ET",
        Code = "am",
        EnglishName = "Amharic (Ethiopia)",
        NativeName = "አማርኛ (ኢትዮጵያ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "am-ET",
        Code = "ET",
        EnglishName = "Ethiopia",
        NativeName = "ኢትዮጵያ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "1",
        Name = "ar",
        Code = "ar",
        EnglishName = "Arabic",
        NativeName = "العربية",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "AR",
        Code = "AR",
        EnglishName = "Argentina",
        NativeName = "Argentina"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "14337",
        Name = "ar-AE",
        Code = "ar",
        EnglishName = "Arabic (U.A.E.)",
        NativeName = "العربية (الإمارات العربية المتحدة)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-AE",
        Code = "AE",
        EnglishName = "U.A.E.",
        NativeName = "الإمارات العربية المتحدة"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "15361",
        Name = "ar-BH",
        Code = "ar",
        EnglishName = "Arabic (Bahrain)",
        NativeName = "العربية (البحرين)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-BH",
        Code = "BH",
        EnglishName = "Bahrain",
        NativeName = "البحرين"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5121",
        Name = "ar-DZ",
        Code = "ar",
        EnglishName = "Arabic (Algeria)",
        NativeName = "العربية (الجزائر)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-DZ",
        Code = "DZ",
        EnglishName = "Algeria",
        NativeName = "الجزائر"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3073",
        Name = "ar-EG",
        Code = "ar",
        EnglishName = "Arabic (Egypt)",
        NativeName = "العربية (مصر)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-EG",
        Code = "EG",
        EnglishName = "Egypt",
        NativeName = "مصر"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2049",
        Name = "ar-IQ",
        Code = "ar",
        EnglishName = "Arabic (Iraq)",
        NativeName = "العربية (العراق)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-IQ",
        Code = "IQ",
        EnglishName = "Iraq",
        NativeName = "العراق"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "11265",
        Name = "ar-JO",
        Code = "ar",
        EnglishName = "Arabic (Jordan)",
        NativeName = "العربية (الأردن)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-JO",
        Code = "JO",
        EnglishName = "Jordan",
        NativeName = "الأردن"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "13313",
        Name = "ar-KW",
        Code = "ar",
        EnglishName = "Arabic (Kuwait)",
        NativeName = "العربية (الكويت)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-KW",
        Code = "KW",
        EnglishName = "Kuwait",
        NativeName = "الكويت"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "12289",
        Name = "ar-LB",
        Code = "ar",
        EnglishName = "Arabic (Lebanon)",
        NativeName = "العربية (لبنان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-LB",
        Code = "LB",
        EnglishName = "Lebanon",
        NativeName = "لبنان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4097",
        Name = "ar-LY",
        Code = "ar",
        EnglishName = "Arabic (Libya)",
        NativeName = "العربية (ليبيا)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-LY",
        Code = "LY",
        EnglishName = "Libya",
        NativeName = "ليبيا"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6145",
        Name = "ar-MA",
        Code = "ar",
        EnglishName = "Arabic (Morocco)",
        NativeName = "العربية (المملكة المغربية)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-MA",
        Code = "MA",
        EnglishName = "Morocco",
        NativeName = "المملكة المغربية"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8193",
        Name = "ar-OM",
        Code = "ar",
        EnglishName = "Arabic (Oman)",
        NativeName = "العربية (عمان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-OM",
        Code = "OM",
        EnglishName = "Oman",
        NativeName = "عمان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "16385",
        Name = "ar-QA",
        Code = "ar",
        EnglishName = "Arabic (Qatar)",
        NativeName = "العربية (قطر)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-QA",
        Code = "QA",
        EnglishName = "Qatar",
        NativeName = "قطر"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1025",
        Name = "ar-SA",
        Code = "ar",
        EnglishName = "Arabic (Saudi Arabia)",
        NativeName = "العربية (المملكة العربية السعودية)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-SA",
        Code = "SA",
        EnglishName = "Saudi Arabia",
        NativeName = "المملكة العربية السعودية"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "10241",
        Name = "ar-SY",
        Code = "ar",
        EnglishName = "Arabic (Syria)",
        NativeName = "العربية (سوريا)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-SY",
        Code = "SY",
        EnglishName = "Syria",
        NativeName = "سوريا"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "7169",
        Name = "ar-TN",
        Code = "ar",
        EnglishName = "Arabic (Tunisia)",
        NativeName = "العربية (تونس)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-TN",
        Code = "TN",
        EnglishName = "Tunisia",
        NativeName = "تونس"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9217",
        Name = "ar-YE",
        Code = "ar",
        EnglishName = "Arabic (Yemen)",
        NativeName = "العربية (اليمن)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ar-YE",
        Code = "YE",
        EnglishName = "Yemen",
        NativeName = "اليمن"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "122",
        Name = "arn",
        Code = "arn",
        EnglishName = "Mapudungun",
        NativeName = "Mapudungun",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1146",
        Name = "arn-CL",
        Code = "arn",
        EnglishName = "Mapudungun (Chile)",
        NativeName = "Mapudungun (Chile)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "arn-CL",
        Code = "CL",
        EnglishName = "Chile",
        NativeName = "Chile"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "77",
        Name = "as",
        Code = "as",
        EnglishName = "Assamese",
        NativeName = "অসমীয়া",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1101",
        Name = "as-IN",
        Code = "as",
        EnglishName = "Assamese (India)",
        NativeName = "অসমীয়া (ভাৰত)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "as-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ভাৰত"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "44",
        Name = "az",
        Code = "az",
        EnglishName = "Azerbaijani",
        NativeName = "Azərbaycan\u00ADılı",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "AZ",
        Code = "AZ",
        EnglishName = "Azerbaijan",
        NativeName = "Азәрбајҹан"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "29740",
        Name = "az-Cyrl",
        Code = "az",
        EnglishName = "Azerbaijani (Cyrillic)",
        NativeName = "Азәрбајҹан дили",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2092",
        Name = "az-Cyrl-AZ",
        Code = "az",
        EnglishName = "Azerbaijani (Cyrillic, Azerbaijan)",
        NativeName = "Азәрбајҹан (Азәрбајҹан)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "az-Cyrl-AZ",
        Code = "AZ",
        EnglishName = "Azerbaijan",
        NativeName = "Азәрбајҹан"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30764",
        Name = "az-Latn",
        Code = "az",
        EnglishName = "Azerbaijani (Latin)",
        NativeName = "Azərbaycan dili (Azərbaycan)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1068",
        Name = "az-Latn-AZ",
        Code = "az",
        EnglishName = "Azerbaijani (Latin, Azerbaijan)",
        NativeName = "Azərbaycan dili (Azərbaycan)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "az-Latn-AZ",
        Code = "AZ",
        EnglishName = "Azerbaijan",
        NativeName = "Azərbaycan"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "109",
        Name = "ba",
        Code = "ba",
        EnglishName = "Bashkir",
        NativeName = "Башҡорт",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Босна и Херцеговина"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1133",
        Name = "ba-RU",
        Code = "ba",
        EnglishName = "Bashkir (Russia)",
        NativeName = "Башҡорт (Рәсәй)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ba-RU",
        Code = "RU",
        EnglishName = "Russia",
        NativeName = "Рәсәй"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "35",
        Name = "be",
        Code = "be",
        EnglishName = "Belarusian",
        NativeName = "Беларуская",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BE",
        Code = "BE",
        EnglishName = "Belgium",
        NativeName = "Belgique"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1059",
        Name = "be-BY",
        Code = "be",
        EnglishName = "Belarusian (Belarus)",
        NativeName = "Беларуская (Беларусь)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "be-BY",
        Code = "BY",
        EnglishName = "Belarus",
        NativeName = "Беларусь"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "2",
        Name = "bg",
        Code = "bg",
        EnglishName = "Bulgarian",
        NativeName = "български",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BG",
        Code = "BG",
        EnglishName = "Bulgaria",
        NativeName = "България"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1026",
        Name = "bg-BG",
        Code = "bg",
        EnglishName = "Bulgarian (Bulgaria)",
        NativeName = "български (България)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bg-BG",
        Code = "BG",
        EnglishName = "Bulgaria",
        NativeName = "България"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "69",
        Name = "bn",
        Code = "bn",
        EnglishName = "Bangla",
        NativeName = "বাংলা",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BN",
        Code = "BN",
        EnglishName = "Brunei Darussalam",
        NativeName = "Brunei Darussalam"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2117",
        Name = "bn-BD",
        Code = "bn",
        EnglishName = "Bangla (Bangladesh)",
        NativeName = "বাংলা (বাংলাদেশ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bn-BD",
        Code = "BD",
        EnglishName = "Bangladesh",
        NativeName = "বাংলাদেশ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1093",
        Name = "bn-IN",
        Code = "bn",
        EnglishName = "Bangla (India)",
        NativeName = "বাংলা (ভারত)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bn-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ভারত"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "81",
        Name = "bo",
        Code = "bo",
        EnglishName = "Tibetan",
        NativeName = "བོད་ཡིག",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BO",
        Code = "BO",
        EnglishName = "Bolivia",
        NativeName = "Bolivia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1105",
        Name = "bo-CN",
        Code = "bo",
        EnglishName = "Tibetan (China)",
        NativeName = "བོད་ཡིག (ཀྲུང་ཧྭ་མི་དམངས་སྤྱི་མཐུན་རྒྱལ་ཁབ།)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bo-CN",
        Code = "CN",
        EnglishName = "China",
        NativeName = "ཀྲུང་ཧྭ་མི་དམངས་སྤྱི་མཐུན་རྒྱལ་ཁབ།"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "126",
        Name = "br",
        Code = "br",
        EnglishName = "Breton",
        NativeName = "brezhoneg",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "BR",
        Code = "BR",
        EnglishName = "Brazil",
        NativeName = "Brasil"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1150",
        Name = "br-FR",
        Code = "br",
        EnglishName = "Breton (France)",
        NativeName = "brezhoneg (Frañs)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "br-FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "Frañs"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30746",
        Name = "bs",
        Code = "bs",
        EnglishName = "Bosnian",
        NativeName = "bosanski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "25626",
        Name = "bs-Cyrl",
        Code = "bs",
        EnglishName = "Bosnian (Cyrillic)",
        NativeName = "босански",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8218",
        Name = "bs-Cyrl-BA",
        Code = "bs",
        EnglishName = "Bosnian (Cyrillic, Bosnia and Herzegovina)",
        NativeName = "босански (Босна и Херцеговина)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bs-Cyrl-BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Босна и Херцеговина"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "26650",
        Name = "bs-Latn",
        Code = "bs",
        EnglishName = "Bosnian (Latin)",
        NativeName = "bosanski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5146",
        Name = "bs-Latn-BA",
        Code = "bs",
        EnglishName = "Bosnian (Latin, Bosnia and Herzegovina)",
        NativeName = "bosanski (Bosna i Hercegovina)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "bs-Latn-BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Bosna i Hercegovina"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "3",
        Name = "ca",
        Code = "ca",
        EnglishName = "Catalan",
        NativeName = "Català",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "Canada"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1027",
        Name = "ca-ES",
        Code = "ca",
        EnglishName = "Catalan (Catalan)",
        NativeName = "Català (Català)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ca-ES",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "Espanya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2051",
        Name = "ca-ES-valencia",
        Code = "ca",
        EnglishName = "Valencian (Spain)",
        NativeName = "Valencià (Espanya)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ca-ES-valencia",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "Espanya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "92",
        Name = "chr",
        Code = "chr",
        EnglishName = "Cherokee",
        NativeName = "ᏣᎳᎩ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31836",
        Name = "chr-Cher",
        Code = "chr",
        EnglishName = "Cherokee",
        NativeName = "ᏣᎳᎩ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1116",
        Name = "chr-Cher-US",
        Code = "chr",
        EnglishName = "Cherokee (Cherokee)",
        NativeName = "ᏣᎳᎩ (ᏣᎳᎩ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "chr-Cher-US",
        Code = "US",
        EnglishName = "United States",
        NativeName = "United States"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "131",
        Name = "co",
        Code = "co",
        EnglishName = "Corsican",
        NativeName = "Corsu",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "CO",
        Code = "CO",
        EnglishName = "Colombia",
        NativeName = "Colombia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1155",
        Name = "co-FR",
        Code = "co",
        EnglishName = "Corsican (France)",
        NativeName = "Corsu (Francia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "co-FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "Francia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "5",
        Name = "cs",
        Code = "cs",
        EnglishName = "Czech",
        NativeName = "čeština",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "CS",
        Code = "CS",
        EnglishName = "Serbia and Montenegro (Former)",
        NativeName = "Србија и Црна Гора (Бивша)"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1029",
        Name = "cs-CZ",
        Code = "cs",
        EnglishName = "Czech (Czech Republic)",
        NativeName = "čeština (Česká republika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "cs-CZ",
        Code = "CZ",
        EnglishName = "Czech Republic",
        NativeName = "Česká republika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "82",
        Name = "cy",
        Code = "cy",
        EnglishName = "Welsh",
        NativeName = "Cymraeg",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1106",
        Name = "cy-GB",
        Code = "cy",
        EnglishName = "Welsh (United Kingdom)",
        NativeName = "Cymraeg (Y Deyrnas Unedig)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "cy-GB",
        Code = "GB",
        EnglishName = "United Kingdom",
        NativeName = "Y Deyrnas Unedig"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "6",
        Name = "da",
        Code = "da",
        EnglishName = "Danish",
        NativeName = "dansk",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1030",
        Name = "da-DK",
        Code = "da",
        EnglishName = "Danish (Denmark)",
        NativeName = "dansk (Danmark)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "da-DK",
        Code = "DK",
        EnglishName = "Denmark",
        NativeName = "Danmark"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "7",
        Name = "de",
        Code = "de",
        EnglishName = "German",
        NativeName = "Deutsch",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "DE",
        Code = "DE",
        EnglishName = "Germany",
        NativeName = "Deutschland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3079",
        Name = "de-AT",
        Code = "de",
        EnglishName = "German (Austria)",
        NativeName = "Deutsch (Österreich)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "de-AT",
        Code = "AT",
        EnglishName = "Austria",
        NativeName = "Österreich"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2055",
        Name = "de-CH",
        Code = "de",
        EnglishName = "German (Switzerland)",
        NativeName = "Deutsch (Schweiz)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "de-CH",
        Code = "CH",
        EnglishName = "Switzerland",
        NativeName = "Schweiz"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1031",
        Name = "de-DE",
        Code = "de",
        EnglishName = "German (Germany)",
        NativeName = "Deutsch (Deutschland)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "de-DE",
        Code = "DE",
        EnglishName = "Germany",
        NativeName = "Deutschland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5127",
        Name = "de-LI",
        Code = "de",
        EnglishName = "German (Liechtenstein)",
        NativeName = "Deutsch (Liechtenstein)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "de-LI",
        Code = "LI",
        EnglishName = "Liechtenstein",
        NativeName = "Liechtenstein"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4103",
        Name = "de-LU",
        Code = "de",
        EnglishName = "German (Luxembourg)",
        NativeName = "Deutsch (Luxemburg)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "de-LU",
        Code = "LU",
        EnglishName = "Luxembourg",
        NativeName = "Luxemburg"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31790",
        Name = "dsb",
        Code = "dsb",
        EnglishName = "Lower Sorbian",
        NativeName = "dolnoserbšćina",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2094",
        Name = "dsb-DE",
        Code = "dsb",
        EnglishName = "Lower Sorbian (Germany)",
        NativeName = "dolnoserbšćina (Nimska)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "dsb-DE",
        Code = "DE",
        EnglishName = "Germany",
        NativeName = "Nimska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "101",
        Name = "dv",
        Code = "dv",
        EnglishName = "Divehi",
        NativeName = "ދިވެހިބަސް",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1125",
        Name = "dv-MV",
        Code = "dv",
        EnglishName = "Divehi (Maldives)",
        NativeName = "ދިވެހިބަސް (ދިވެހި ރާއްޖެ)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "dv-MV",
        Code = "MV",
        EnglishName = "Maldives",
        NativeName = "ދިވެހި ރާއްޖެ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "8",
        Name = "el",
        Code = "el",
        EnglishName = "Greek",
        NativeName = "Ελληνικά",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1032",
        Name = "el-GR",
        Code = "el",
        EnglishName = "Greek (Greece)",
        NativeName = "Ελληνικά (Ελλάδα)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "el-GR",
        Code = "GR",
        EnglishName = "Greece",
        NativeName = "Ελλάδα"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "9",
        Name = "en",
        Code = "en",
        EnglishName = "English",
        NativeName = "English",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9225",
        Name = "en-029",
        Code = "en",
        EnglishName = "English (Caribbean)",
        NativeName = "English (Caribbean)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-029",
        Code = "029",
        EnglishName = "Caribbean",
        NativeName = "Caribbean"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3081",
        Name = "en-AU",
        Code = "en",
        EnglishName = "English (Australia)",
        NativeName = "English (Australia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-AU",
        Code = "AU",
        EnglishName = "Australia",
        NativeName = "Australia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "10249",
        Name = "en-BZ",
        Code = "en",
        EnglishName = "English (Belize)",
        NativeName = "English (Belize)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-BZ",
        Code = "BZ",
        EnglishName = "Belize",
        NativeName = "Belize"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4105",
        Name = "en-CA",
        Code = "en",
        EnglishName = "English (Canada)",
        NativeName = "English (Canada)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "Canada"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2057",
        Name = "en-GB",
        Code = "en",
        EnglishName = "English (United Kingdom)",
        NativeName = "English (United Kingdom)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-GB",
        Code = "GB",
        EnglishName = "United Kingdom",
        NativeName = "United Kingdom"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "15369",
        Name = "en-HK",
        Code = "en",
        EnglishName = "English (Hong Kong)",
        NativeName = "English (Hong Kong)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-HK",
        Code = "HK",
        EnglishName = "Hong Kong",
        NativeName = "Hong Kong"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6153",
        Name = "en-IE",
        Code = "en",
        EnglishName = "English (Ireland)",
        NativeName = "English (Ireland)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-IE",
        Code = "IE",
        EnglishName = "Ireland",
        NativeName = "Ireland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "16393",
        Name = "en-IN",
        Code = "en",
        EnglishName = "English (India)",
        NativeName = "English (India)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "India"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8201",
        Name = "en-JM",
        Code = "en",
        EnglishName = "English (Jamaica)",
        NativeName = "English (Jamaica)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-JM",
        Code = "JM",
        EnglishName = "Jamaica",
        NativeName = "Jamaica"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "17417",
        Name = "en-MY",
        Code = "en",
        EnglishName = "English (Malaysia)",
        NativeName = "English (Malaysia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-MY",
        Code = "MY",
        EnglishName = "Malaysia",
        NativeName = "Malaysia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5129",
        Name = "en-NZ",
        Code = "en",
        EnglishName = "English (New Zealand)",
        NativeName = "English (New Zealand)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-NZ",
        Code = "NZ",
        EnglishName = "New Zealand",
        NativeName = "New Zealand"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "13321",
        Name = "en-PH",
        Code = "en",
        EnglishName = "English (Philippines)",
        NativeName = "English (Philippines)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-PH",
        Code = "PH",
        EnglishName = "Philippines",
        NativeName = "Philippines"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "18441",
        Name = "en-SG",
        Code = "en",
        EnglishName = "English (Singapore)",
        NativeName = "English (Singapore)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-SG",
        Code = "SG",
        EnglishName = "Singapore",
        NativeName = "Singapore"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "11273",
        Name = "en-TT",
        Code = "en",
        EnglishName = "English (Trinidad and Tobago)",
        NativeName = "English (Trinidad and Tobago)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-TT",
        Code = "TT",
        EnglishName = "Trinidad and Tobago",
        NativeName = "Trinidad and Tobago"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1033",
        Name = "en-US",
        Code = "en",
        EnglishName = "English (United States)",
        NativeName = "English (United States)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-US",
        Code = "US",
        EnglishName = "United States",
        NativeName = "United States"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "7177",
        Name = "en-ZA",
        Code = "en",
        EnglishName = "English (South Africa)",
        NativeName = "English (South Africa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "South Africa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "12297",
        Name = "en-ZW",
        Code = "en",
        EnglishName = "English (Zimbabwe)",
        NativeName = "English (Zimbabwe)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "en-ZW",
        Code = "ZW",
        EnglishName = "Zimbabwe",
        NativeName = "Zimbabwe"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "10",
        Name = "es",
        Code = "es",
        EnglishName = "Spanish",
        NativeName = "español",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "ES",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "Espanya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "22538",
        Name = "es-419",
        Code = "es",
        EnglishName = "Spanish (Latin America)",
        NativeName = "español (Latinoamérica)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-419",
        Code = "419",
        EnglishName = "Latin America",
        NativeName = "Latinoamérica"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "11274",
        Name = "es-AR",
        Code = "es",
        EnglishName = "Spanish (Argentina)",
        NativeName = "español (Argentina)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-AR",
        Code = "AR",
        EnglishName = "Argentina",
        NativeName = "Argentina"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "16394",
        Name = "es-BO",
        Code = "es",
        EnglishName = "Spanish (Bolivia)",
        NativeName = "español (Bolivia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-BO",
        Code = "BO",
        EnglishName = "Bolivia",
        NativeName = "Bolivia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "13322",
        Name = "es-CL",
        Code = "es",
        EnglishName = "Spanish (Chile)",
        NativeName = "español (Chile)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-CL",
        Code = "CL",
        EnglishName = "Chile",
        NativeName = "Chile"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9226",
        Name = "es-CO",
        Code = "es",
        EnglishName = "Spanish (Colombia)",
        NativeName = "español (Colombia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-CO",
        Code = "CO",
        EnglishName = "Colombia",
        NativeName = "Colombia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5130",
        Name = "es-CR",
        Code = "es",
        EnglishName = "Spanish (Costa Rica)",
        NativeName = "español (Costa Rica)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-CR",
        Code = "CR",
        EnglishName = "Costa Rica",
        NativeName = "Costa Rica"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "7178",
        Name = "es-DO",
        Code = "es",
        EnglishName = "Spanish (Dominican Republic)",
        NativeName = "español (República Dominicana)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-DO",
        Code = "DO",
        EnglishName = "Dominican Republic",
        NativeName = "República Dominicana"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "12298",
        Name = "es-EC",
        Code = "es",
        EnglishName = "Spanish (Ecuador)",
        NativeName = "español (Ecuador)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-EC",
        Code = "EC",
        EnglishName = "Ecuador",
        NativeName = "Ecuador"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3082",
        Name = "es-ES",
        Code = "es",
        EnglishName = "Spanish (Spain, International Sort)",
        NativeName = "español (España, alfabetización internacional)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-ES",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "España"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4106",
        Name = "es-GT",
        Code = "es",
        EnglishName = "Spanish (Guatemala)",
        NativeName = "español (Guatemala)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-GT",
        Code = "GT",
        EnglishName = "Guatemala",
        NativeName = "Guatemala"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "18442",
        Name = "es-HN",
        Code = "es",
        EnglishName = "Spanish (Honduras)",
        NativeName = "español (Honduras)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-HN",
        Code = "HN",
        EnglishName = "Honduras",
        NativeName = "Honduras"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2058",
        Name = "es-MX",
        Code = "es",
        EnglishName = "Spanish (Mexico)",
        NativeName = "español (México)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-MX",
        Code = "MX",
        EnglishName = "Mexico",
        NativeName = "México"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "19466",
        Name = "es-NI",
        Code = "es",
        EnglishName = "Spanish (Nicaragua)",
        NativeName = "español (Nicaragua)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-NI",
        Code = "NI",
        EnglishName = "Nicaragua",
        NativeName = "Nicaragua"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6154",
        Name = "es-PA",
        Code = "es",
        EnglishName = "Spanish (Panama)",
        NativeName = "español (Panamá)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-PA",
        Code = "PA",
        EnglishName = "Panama",
        NativeName = "Panamá"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "10250",
        Name = "es-PE",
        Code = "es",
        EnglishName = "Spanish (Peru)",
        NativeName = "español (Perú)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-PE",
        Code = "PE",
        EnglishName = "Peru",
        NativeName = "Perú"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "20490",
        Name = "es-PR",
        Code = "es",
        EnglishName = "Spanish (Puerto Rico)",
        NativeName = "español (Puerto Rico)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-PR",
        Code = "PR",
        EnglishName = "Puerto Rico",
        NativeName = "Puerto Rico"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "15370",
        Name = "es-PY",
        Code = "es",
        EnglishName = "Spanish (Paraguay)",
        NativeName = "español (Paraguay)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-PY",
        Code = "PY",
        EnglishName = "Paraguay",
        NativeName = "Paraguay"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "17418",
        Name = "es-SV",
        Code = "es",
        EnglishName = "Spanish (El Salvador)",
        NativeName = "español (El Salvador)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-SV",
        Code = "SV",
        EnglishName = "El Salvador",
        NativeName = "El Salvador"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "21514",
        Name = "es-US",
        Code = "es",
        EnglishName = "Spanish (United States)",
        NativeName = "español (Estados Unidos)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-US",
        Code = "US",
        EnglishName = "United States",
        NativeName = "Estados Unidos"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "14346",
        Name = "es-UY",
        Code = "es",
        EnglishName = "Spanish (Uruguay)",
        NativeName = "español (Uruguay)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-UY",
        Code = "UY",
        EnglishName = "Uruguay",
        NativeName = "Uruguay"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8202",
        Name = "es-VE",
        Code = "es",
        EnglishName = "Spanish (Bolivarian Republic of Venezuela)",
        NativeName = "español (Republica Bolivariana de Venezuela)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "es-VE",
        Code = "VE",
        EnglishName = "Bolivarian Republic of Venezuela",
        NativeName = "Republica Bolivariana de Venezuela"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "37",
        Name = "et",
        Code = "et",
        EnglishName = "Estonian",
        NativeName = "eesti",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "ET",
        Code = "ET",
        EnglishName = "Ethiopia",
        NativeName = "ኢትዮጵያ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1061",
        Name = "et-EE",
        Code = "et",
        EnglishName = "Estonian (Estonia)",
        NativeName = "eesti (Eesti)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "et-EE",
        Code = "EE",
        EnglishName = "Estonia",
        NativeName = "Eesti"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "45",
        Name = "eu",
        Code = "eu",
        EnglishName = "Basque",
        NativeName = "euskara",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1069",
        Name = "eu-ES",
        Code = "eu",
        EnglishName = "Basque (Basque)",
        NativeName = "euskara (euskara)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "eu-ES",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "Espainia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "41",
        Name = "fa",
        Code = "fa",
        EnglishName = "Persian",
        NativeName = "فارسى",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1065",
        Name = "fa-IR",
        Code = "fa",
        EnglishName = "Persian",
        NativeName = "فارسى (ایران)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fa-IR",
        Code = "IR",
        EnglishName = "Iran",
        NativeName = "ایران"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "103",
        Name = "ff",
        Code = "ff",
        EnglishName = "Fulah",
        NativeName = "Fulah",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31847",
        Name = "ff-Latn",
        Code = "ff",
        EnglishName = "Fulah",
        NativeName = "Fulah",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2151",
        Name = "ff-Latn-SN",
        Code = "ff",
        EnglishName = "Fulah (Latin, Senegal)",
        NativeName = "Fulah (Sénégal)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ff-Latn-SN",
        Code = "SN",
        EnglishName = "Senegal",
        NativeName = "Sénégal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "11",
        Name = "fi",
        Code = "fi",
        EnglishName = "Finnish",
        NativeName = "suomi",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Suomi"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1035",
        Name = "fi-FI",
        Code = "fi",
        EnglishName = "Finnish (Finland)",
        NativeName = "suomi (Suomi)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fi-FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Suomi"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "100",
        Name = "fil",
        Code = "fil",
        EnglishName = "Filipino",
        NativeName = "Filipino",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1124",
        Name = "fil-PH",
        Code = "fil",
        EnglishName = "Filipino (Philippines)",
        NativeName = "Filipino (Pilipinas)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fil-PH",
        Code = "PH",
        EnglishName = "Philippines",
        NativeName = "Pilipinas"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "56",
        Name = "fo",
        Code = "fo",
        EnglishName = "Faroese",
        NativeName = "føroyskt",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "FO",
        Code = "FO",
        EnglishName = "Faroe Islands",
        NativeName = "Føroyar"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1080",
        Name = "fo-FO",
        Code = "fo",
        EnglishName = "Faroese (Faroe Islands)",
        NativeName = "føroyskt (Føroyar)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fo-FO",
        Code = "FO",
        EnglishName = "Faroe Islands",
        NativeName = "Føroyar"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "12",
        Name = "fr",
        Code = "fr",
        EnglishName = "French",
        NativeName = "français",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "Frañs"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2060",
        Name = "fr-BE",
        Code = "fr",
        EnglishName = "French (Belgium)",
        NativeName = "français (Belgique)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-BE",
        Code = "BE",
        EnglishName = "Belgium",
        NativeName = "Belgique"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3084",
        Name = "fr-CA",
        Code = "fr",
        EnglishName = "French (Canada)",
        NativeName = "français (Canada)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "Canada"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9228",
        Name = "fr-CD",
        Code = "fr",
        EnglishName = "French (Congo [DRC])",
        NativeName = "français (Congo [RDC])",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-CD",
        Code = "CD",
        EnglishName = "Congo [DRC]",
        NativeName = "Congo [RDC]"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4108",
        Name = "fr-CH",
        Code = "fr",
        EnglishName = "French (Switzerland)",
        NativeName = "français (Suisse)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-CH",
        Code = "CH",
        EnglishName = "Switzerland",
        NativeName = "Suisse"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "12300",
        Name = "fr-CI",
        Code = "fr",
        EnglishName = "French (Ivory Coast)",
        NativeName = "français (Côte d’Ivoire)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-CI",
        Code = "CI",
        EnglishName = "Ivory Coast",
        NativeName = "Côte d’Ivoire"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "11276",
        Name = "fr-CM",
        Code = "fr",
        EnglishName = "French (Cameroon)",
        NativeName = "français (Cameroun)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-CM",
        Code = "CM",
        EnglishName = "Cameroon",
        NativeName = "Cameroun"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1036",
        Name = "fr-FR",
        Code = "fr",
        EnglishName = "French (France)",
        NativeName = "français (France)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "France"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "15372",
        Name = "fr-HT",
        Code = "fr",
        EnglishName = "French (Haiti)",
        NativeName = "français (Haïti)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-HT",
        Code = "HT",
        EnglishName = "Haiti",
        NativeName = "Haïti"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5132",
        Name = "fr-LU",
        Code = "fr",
        EnglishName = "French (Luxembourg)",
        NativeName = "français (Luxembourg)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-LU",
        Code = "LU",
        EnglishName = "Luxembourg",
        NativeName = "Luxembourg"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "14348",
        Name = "fr-MA",
        Code = "fr",
        EnglishName = "French (Morocco)",
        NativeName = "français (Maroc)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-MA",
        Code = "MA",
        EnglishName = "Morocco",
        NativeName = "Maroc"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6156",
        Name = "fr-MC",
        Code = "fr",
        EnglishName = "French (Monaco)",
        NativeName = "français (Principauté de Monaco)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-MC",
        Code = "MC",
        EnglishName = "Principality of Monaco",
        NativeName = "Principauté de Monaco"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "13324",
        Name = "fr-ML",
        Code = "fr",
        EnglishName = "French (Mali)",
        NativeName = "français (Mali)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-ML",
        Code = "ML",
        EnglishName = "Mali",
        NativeName = "Mali"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8204",
        Name = "fr-RE",
        Code = "fr",
        EnglishName = "French (Réunion)",
        NativeName = "français (Réunion)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-RE",
        Code = "RE",
        EnglishName = "Réunion",
        NativeName = "Réunion"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "10252",
        Name = "fr-SN",
        Code = "fr",
        EnglishName = "French (Senegal)",
        NativeName = "français (Sénégal)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fr-SN",
        Code = "SN",
        EnglishName = "Senegal",
        NativeName = "Sénégal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "98",
        Name = "fy",
        Code = "fy",
        EnglishName = "Frisian",
        NativeName = "Frysk",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1122",
        Name = "fy-NL",
        Code = "fy",
        EnglishName = "Frisian (Netherlands)",
        NativeName = "Frysk (Nederlân)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "fy-NL",
        Code = "NL",
        EnglishName = "Netherlands",
        NativeName = "Nederlân"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "60",
        Name = "ga",
        Code = "ga",
        EnglishName = "Irish",
        NativeName = "Gaeilge",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2108",
        Name = "ga-IE",
        Code = "ga",
        EnglishName = "Irish (Ireland)",
        NativeName = "Gaeilge (Éire)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ga-IE",
        Code = "IE",
        EnglishName = "Ireland",
        NativeName = "Éire"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "145",
        Name = "gd",
        Code = "gd",
        EnglishName = "Scottish Gaelic",
        NativeName = "Gàidhlig",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1169",
        Name = "gd-GB",
        Code = "gd",
        EnglishName = "Scottish Gaelic (United Kingdom)",
        NativeName = "Gàidhlig (An Rìoghachd Aonaichte)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "gd-GB",
        Code = "GB",
        EnglishName = "United Kingdom",
        NativeName = "An Rìoghachd Aonaichte"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "86",
        Name = "gl",
        Code = "gl",
        EnglishName = "Galician",
        NativeName = "galego",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "GL",
        Code = "GL",
        EnglishName = "Greenland",
        NativeName = "Kalaallit Nunaat"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1110",
        Name = "gl-ES",
        Code = "gl",
        EnglishName = "Galician (Galician)",
        NativeName = "galego (galego)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "gl-ES",
        Code = "ES",
        EnglishName = "Spain",
        NativeName = "España"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "116",
        Name = "gn",
        Code = "gn",
        EnglishName = "Guarani",
        NativeName = "Guarani",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "GN",
        Code = "GN",
        EnglishName = "Guinea",
        NativeName = "ߖߌ߬ߣߍ߬ ߞߊ߲ߓߍ߲"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1140",
        Name = "gn-PY",
        Code = "gn",
        EnglishName = "Guarani (Paraguay)",
        NativeName = "Guarani (Paraguái)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "gn-PY",
        Code = "PY",
        EnglishName = "Paraguay",
        NativeName = "Paraguái"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "132",
        Name = "gsw",
        Code = "gsw",
        EnglishName = "Alsatian",
        NativeName = "Elsässisch",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1156",
        Name = "gsw-FR",
        Code = "gsw",
        EnglishName = "Alsatian (France)",
        NativeName = "Elsässisch (Frànkrisch)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "gsw-FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "Frànkrisch"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "71",
        Name = "gu",
        Code = "gu",
        EnglishName = "Gujarati",
        NativeName = "ગુજરાતી",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1095",
        Name = "gu-IN",
        Code = "gu",
        EnglishName = "Gujarati (India)",
        NativeName = "ગુજરાતી (ભારત)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "gu-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ભારત"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "104",
        Name = "ha",
        Code = "ha",
        EnglishName = "Hausa",
        NativeName = "Hausa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31848",
        Name = "ha-Latn",
        Code = "ha",
        EnglishName = "Hausa (Latin)",
        NativeName = "Hausa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1128",
        Name = "ha-Latn-NG",
        Code = "ha",
        EnglishName = "Hausa (Latin, Nigeria)",
        NativeName = "Hausa (Nijeriya)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ha-Latn-NG",
        Code = "NG",
        EnglishName = "Nigeria",
        NativeName = "Nijeriya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "117",
        Name = "haw",
        Code = "haw",
        EnglishName = "Hawaiian",
        NativeName = "Hawaiʻi",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1141",
        Name = "haw-US",
        Code = "haw",
        EnglishName = "Hawaiian (United States)",
        NativeName = "Hawaiʻi (ʻAmelika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "haw-US",
        Code = "US",
        EnglishName = "United States",
        NativeName = "ʻAmelika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "13",
        Name = "he",
        Code = "he",
        EnglishName = "Hebrew",
        NativeName = "עברית",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1037",
        Name = "he-IL",
        Code = "he",
        EnglishName = "Hebrew (Israel)",
        NativeName = "עברית (ישראל)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "he-IL",
        Code = "IL",
        EnglishName = "Israel",
        NativeName = "ישראל"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "57",
        Name = "hi",
        Code = "hi",
        EnglishName = "Hindi",
        NativeName = "हिंदी",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1081",
        Name = "hi-IN",
        Code = "hi",
        EnglishName = "Hindi (India)",
        NativeName = "हिंदी (भारत)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hi-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "भारत"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "26",
        Name = "hr",
        Code = "hr",
        EnglishName = "Croatian",
        NativeName = "hrvatski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "HR",
        Code = "HR",
        EnglishName = "Croatia",
        NativeName = "Hrvatska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4122",
        Name = "hr-BA",
        Code = "hr",
        EnglishName = "Croatian (Latin, Bosnia and Herzegovina)",
        NativeName = "hrvatski (Bosna i Hercegovina)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hr-BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Bosna i Hercegovina"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1050",
        Name = "hr-HR",
        Code = "hr",
        EnglishName = "Croatian (Croatia)",
        NativeName = "hrvatski (Hrvatska)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hr-HR",
        Code = "HR",
        EnglishName = "Croatia",
        NativeName = "Hrvatska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "46",
        Name = "hsb",
        Code = "hsb",
        EnglishName = "Upper Sorbian",
        NativeName = "hornjoserbšćina",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1070",
        Name = "hsb-DE",
        Code = "hsb",
        EnglishName = "Upper Sorbian (Germany)",
        NativeName = "hornjoserbšćina (Němska)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hsb-DE",
        Code = "DE",
        EnglishName = "Germany",
        NativeName = "Němska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "14",
        Name = "hu",
        Code = "hu",
        EnglishName = "Hungarian",
        NativeName = "magyar",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "HU",
        Code = "HU",
        EnglishName = "Hungary",
        NativeName = "Magyarország"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1038",
        Name = "hu-HU",
        Code = "hu",
        EnglishName = "Hungarian (Hungary)",
        NativeName = "magyar (Magyarország)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hu-HU",
        Code = "HU",
        EnglishName = "Hungary",
        NativeName = "Magyarország"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "43",
        Name = "hy",
        Code = "hy",
        EnglishName = "Armenian",
        NativeName = "Հայերեն",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1067",
        Name = "hy-AM",
        Code = "hy",
        EnglishName = "Armenian (Armenia)",
        NativeName = "Հայերեն (Հայաստան)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "hy-AM",
        Code = "AM",
        EnglishName = "Armenia",
        NativeName = "Հայաստան"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "33",
        Name = "id",
        Code = "id",
        EnglishName = "Indonesian",
        NativeName = "Bahasa Indonesia",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "ID",
        Code = "ID",
        EnglishName = "Indonesia",
        NativeName = "Indonesia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1057",
        Name = "id-ID",
        Code = "id",
        EnglishName = "Indonesian (Indonesia)",
        NativeName = "Bahasa Indonesia (Indonesia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "id-ID",
        Code = "ID",
        EnglishName = "Indonesia",
        NativeName = "Indonesia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "112",
        Name = "ig",
        Code = "ig",
        EnglishName = "Igbo",
        NativeName = "Igbo",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1136",
        Name = "ig-NG",
        Code = "ig",
        EnglishName = "Igbo (Nigeria)",
        NativeName = "Igbo (Nigeria)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ig-NG",
        Code = "NG",
        EnglishName = "Nigeria",
        NativeName = "Nigeria"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "120",
        Name = "ii",
        Code = "ii",
        EnglishName = "Yi",
        NativeName = "ꆈꌠꁱꂷ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1144",
        Name = "ii-CN",
        Code = "ii",
        EnglishName = "Yi (China)",
        NativeName = "ꆈꌠꁱꂷ (ꍏꉸꏓꂱꇭꉼꇩ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ii-CN",
        Code = "CN",
        EnglishName = "China",
        NativeName = "ꍏꉸꏓꂱꇭꉼꇩ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "15",
        Name = "is",
        Code = "is",
        EnglishName = "Icelandic",
        NativeName = "íslenska",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "IS",
        Code = "IS",
        EnglishName = "Iceland",
        NativeName = "Ísland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1039",
        Name = "is-IS",
        Code = "is",
        EnglishName = "Icelandic (Iceland)",
        NativeName = "íslenska (Ísland)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "is-IS",
        Code = "IS",
        EnglishName = "Iceland",
        NativeName = "Ísland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "16",
        Name = "it",
        Code = "it",
        EnglishName = "Italian",
        NativeName = "italiano",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "IT",
        Code = "IT",
        EnglishName = "Italy",
        NativeName = "Italia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2064",
        Name = "it-CH",
        Code = "it",
        EnglishName = "Italian (Switzerland)",
        NativeName = "italiano (Svizzera)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "it-CH",
        Code = "CH",
        EnglishName = "Switzerland",
        NativeName = "Svizzera"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1040",
        Name = "it-IT",
        Code = "it",
        EnglishName = "Italian (Italy)",
        NativeName = "italiano (Italia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "it-IT",
        Code = "IT",
        EnglishName = "Italy",
        NativeName = "Italia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "93",
        Name = "iu",
        Code = "iu",
        EnglishName = "Inuktitut",
        NativeName = "Inuktitut",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30813",
        Name = "iu-Cans",
        Code = "iu",
        EnglishName = "Inuktitut (Syllabics)",
        NativeName = "ᐃᓄᒃᑎᑐᑦ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1117",
        Name = "iu-Cans-CA",
        Code = "iu",
        EnglishName = "Inuktitut (Syllabics, Canada)",
        NativeName = "ᐃᓄᒃᑎᑐᑦ (ᑲᓇᑕᒥ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "iu-Cans-CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "ᑲᓇᑕ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31837",
        Name = "iu-Latn",
        Code = "iu",
        EnglishName = "Inuktitut (Latin)",
        NativeName = "Inuktitut",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2141",
        Name = "iu-Latn-CA",
        Code = "iu",
        EnglishName = "Inuktitut (Latin, Canada)",
        NativeName = "Inuktitut (Kanatami)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "iu-Latn-CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "Kanata"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "17",
        Name = "ja",
        Code = "ja",
        EnglishName = "Japanese",
        NativeName = "日本語",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1041",
        Name = "ja-JP",
        Code = "ja",
        EnglishName = "Japanese (Japan)",
        NativeName = "日本語 (日本)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ja-JP",
        Code = "JP",
        EnglishName = "Japan",
        NativeName = "日本"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "jv",
        Code = "jv",
        EnglishName = "Javanese",
        NativeName = "Basa Jawa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "jv-Latn",
        Code = "jv",
        EnglishName = "Javanese",
        NativeName = "Basa Jawa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "jv-Latn-ID",
        Code = "jv",
        EnglishName = "Javanese (Indonesia)",
        NativeName = "Basa Jawa (Indonesia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "jv-Latn-ID",
        Code = "ID",
        EnglishName = "Indonesia",
        NativeName = "Indonesia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "55",
        Name = "ka",
        Code = "ka",
        EnglishName = "Georgian",
        NativeName = "ქართული",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1079",
        Name = "ka-GE",
        Code = "ka",
        EnglishName = "Georgian (Georgia)",
        NativeName = "ქართული (საქართველო)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ka-GE",
        Code = "GE",
        EnglishName = "Georgia",
        NativeName = "საქართველო"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "63",
        Name = "kk",
        Code = "kk",
        EnglishName = "Kazakh",
        NativeName = "Қазақ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1087",
        Name = "kk-KZ",
        Code = "kk",
        EnglishName = "Kazakh (Kazakhstan)",
        NativeName = "Қазақ (Қазақстан)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "kk-KZ",
        Code = "KZ",
        EnglishName = "Kazakhstan",
        NativeName = "Қазақстан"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "111",
        Name = "kl",
        Code = "kl",
        EnglishName = "Greenlandic",
        NativeName = "kalaallisut",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1135",
        Name = "kl-GL",
        Code = "kl",
        EnglishName = "Greenlandic (Greenland)",
        NativeName = "kalaallisut (Kalaallit Nunaat)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "kl-GL",
        Code = "GL",
        EnglishName = "Greenland",
        NativeName = "Kalaallit Nunaat"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "83",
        Name = "km",
        Code = "km",
        EnglishName = "Khmer",
        NativeName = "ភាសាខ្មែរ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1107",
        Name = "km-KH",
        Code = "km",
        EnglishName = "Khmer (Cambodia)",
        NativeName = "ភាសាខ្មែរ (កម្ពុជា)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "km-KH",
        Code = "KH",
        EnglishName = "Cambodia",
        NativeName = "កម្ពុជា"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "75",
        Name = "kn",
        Code = "kn",
        EnglishName = "Kannada",
        NativeName = "ಕನ್ನಡ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1099",
        Name = "kn-IN",
        Code = "kn",
        EnglishName = "Kannada (India)",
        NativeName = "ಕನ್ನಡ (ಭಾರತ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "kn-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ಭಾರತ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "18",
        Name = "ko",
        Code = "ko",
        EnglishName = "Korean",
        NativeName = "한국어",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1042",
        Name = "ko-KR",
        Code = "ko",
        EnglishName = "Korean (Korea)",
        NativeName = "한국어(대한민국)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ko-KR",
        Code = "KR",
        EnglishName = "Korea",
        NativeName = "대한민국"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "87",
        Name = "kok",
        Code = "kok",
        EnglishName = "Konkani",
        NativeName = "कोंकणी",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1111",
        Name = "kok-IN",
        Code = "kok",
        EnglishName = "Konkani (India)",
        NativeName = "कोंकणी (भारत)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "kok-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "भारत"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "146",
        Name = "ku",
        Code = "ku",
        EnglishName = "Central Kurdish",
        NativeName = "کوردیی ناوەڕاست",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31890",
        Name = "ku-Arab",
        Code = "ku",
        EnglishName = "Central Kurdish",
        NativeName = "کوردیی ناوەڕاست",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1170",
        Name = "ku-Arab-IQ",
        Code = "ku",
        EnglishName = "Central Kurdish (Iraq)",
        NativeName = "کوردیی ناوەڕاست (کوردستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ku-Arab-IQ",
        Code = "IQ",
        EnglishName = "Iraq",
        NativeName = "کوردستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "64",
        Name = "ky",
        Code = "ky",
        EnglishName = "Kyrgyz",
        NativeName = "Кыргыз",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1088",
        Name = "ky-KG",
        Code = "ky",
        EnglishName = "Kyrgyz (Kyrgyzstan)",
        NativeName = "Кыргыз (Кыргызстан)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ky-KG",
        Code = "KG",
        EnglishName = "Kyrgyzstan",
        NativeName = "Кыргызстан"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "110",
        Name = "lb",
        Code = "lb",
        EnglishName = "Luxembourgish",
        NativeName = "Lëtzebuergesch",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "LB",
        Code = "LB",
        EnglishName = "Lebanon",
        NativeName = "لبنان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1134",
        Name = "lb-LU",
        Code = "lb",
        EnglishName = "Luxembourgish (Luxembourg)",
        NativeName = "Lëtzebuergesch (Lëtzebuerg)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "lb-LU",
        Code = "LU",
        EnglishName = "Luxembourg",
        NativeName = "Lëtzebuerg"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "84",
        Name = "lo",
        Code = "lo",
        EnglishName = "Lao",
        NativeName = "ພາສາລາວ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1108",
        Name = "lo-LA",
        Code = "lo",
        EnglishName = "Lao (Lao PDR)",
        NativeName = "ພາສາລາວ (ສປປ ລາວ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "lo-LA",
        Code = "LA",
        EnglishName = "Lao PDR",
        NativeName = "ສປປ ລາວ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "39",
        Name = "lt",
        Code = "lt",
        EnglishName = "Lithuanian",
        NativeName = "lietuvių",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "LT",
        Code = "LT",
        EnglishName = "Lithuania",
        NativeName = "Lietuva"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1063",
        Name = "lt-LT",
        Code = "lt",
        EnglishName = "Lithuanian (Lithuania)",
        NativeName = "lietuvių (Lietuva)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "lt-LT",
        Code = "LT",
        EnglishName = "Lithuania",
        NativeName = "Lietuva"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "38",
        Name = "lv",
        Code = "lv",
        EnglishName = "Latvian",
        NativeName = "latviešu",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "LV",
        Code = "LV",
        EnglishName = "Latvia",
        NativeName = "Latvija"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1062",
        Name = "lv-LV",
        Code = "lv",
        EnglishName = "Latvian (Latvia)",
        NativeName = "latviešu (Latvija)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "lv-LV",
        Code = "LV",
        EnglishName = "Latvia",
        NativeName = "Latvija"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "mg",
        Code = "mg",
        EnglishName = "Malagasy",
        NativeName = "Malagasy",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "MG",
        Code = "MG",
        EnglishName = "Madagascar",
        NativeName = "Madagasikara"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "mg-MG",
        Code = "mg",
        EnglishName = "Malagasy (Madagascar)",
        NativeName = "Malagasy (Madagasikara)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mg-MG",
        Code = "MG",
        EnglishName = "Madagascar",
        NativeName = "Madagasikara"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "129",
        Name = "mi",
        Code = "mi",
        EnglishName = "Maori",
        NativeName = "Reo Māori",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1153",
        Name = "mi-NZ",
        Code = "mi",
        EnglishName = "Maori (New Zealand)",
        NativeName = "Reo Māori (Aotearoa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mi-NZ",
        Code = "NZ",
        EnglishName = "New Zealand",
        NativeName = "Aotearoa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "47",
        Name = "mk",
        Code = "mk",
        EnglishName = "Macedonian (Former Yugoslav Republic of Macedonia)",
        NativeName = "македонски јазик",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "MK",
        Code = "MK",
        EnglishName = "Macedonia (Former Yugoslav Republic of Macedonia)",
        NativeName = "Македонија"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1071",
        Name = "mk-MK",
        Code = "mk",
        EnglishName = "Macedonian (Former Yugoslav Republic of Macedonia)",
        NativeName = "македонски јазик (Македонија)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mk-MK",
        Code = "MK",
        EnglishName = "Macedonia (Former Yugoslav Republic of Macedonia)",
        NativeName = "Македонија"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "76",
        Name = "ml",
        Code = "ml",
        EnglishName = "Malayalam",
        NativeName = "മലയാളം",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "ML",
        Code = "ML",
        EnglishName = "Mali",
        NativeName = "Mali"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1100",
        Name = "ml-IN",
        Code = "ml",
        EnglishName = "Malayalam (India)",
        NativeName = "മലയാളം (ഭാരതം)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ml-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ഭാരതം"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "80",
        Name = "mn",
        Code = "mn",
        EnglishName = "Mongolian",
        NativeName = "Монгол хэл",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "MN",
        Code = "MN",
        EnglishName = "Mongolia",
        NativeName = "Монгол улс"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30800",
        Name = "mn-Cyrl",
        Code = "mn",
        EnglishName = "Mongolian (Cyrillic)",
        NativeName = "Монгол хэл",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1104",
        Name = "mn-MN",
        Code = "mn",
        EnglishName = "Mongolian (Cyrillic, Mongolia)",
        NativeName = "Монгол хэл (Монгол улс)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mn-MN",
        Code = "MN",
        EnglishName = "Mongolia",
        NativeName = "Монгол улс"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31824",
        Name = "mn-Mong",
        Code = "mn",
        EnglishName = "Mongolian (Traditional Mongolian)",
        NativeName = "ᠮᠤᠨᠭᠭᠤᠯ ᠬᠡᠯᠡ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2128",
        Name = "mn-Mong-CN",
        Code = "mn",
        EnglishName = "Mongolian (Traditional Mongolian, China)",
        NativeName = "ᠮᠤᠨᠭᠭᠤᠯ ᠬᠡᠯᠡ (ᠪᠦᠭᠦᠳᠡ ᠨᠠᠢᠷᠠᠮᠳᠠᠬᠤ ᠳᠤᠮᠳᠠᠳᠤ ᠠᠷᠠᠳ ᠣᠯᠣᠰ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mn-Mong-CN",
        Code = "CN",
        EnglishName = "China",
        NativeName = "ᠪᠦᠭᠦᠳᠡ ᠨᠠᠢᠷᠠᠮᠳᠠᠬᠤ ᠳᠤᠮᠳᠠᠳᠤ ᠠᠷᠠᠳ ᠣᠯᠣᠰ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3152",
        Name = "mn-Mong-MN",
        Code = "mn",
        EnglishName = "Mongolian (Traditional Mongolian, Mongolia)",
        NativeName = "ᠮᠤᠨᠭᠭᠤᠯ ᠬᠡᠯᠡ (ᠮᠤᠨᠭᠭᠤᠯ ᠣᠯᠣᠰ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mn-Mong-MN",
        Code = "MN",
        EnglishName = "Mongolia",
        NativeName = "ᠮᠤᠨᠭᠭᠤᠯ ᠣᠯᠣᠰ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "124",
        Name = "moh",
        Code = "moh",
        EnglishName = "Mohawk",
        NativeName = "Kanien'kéha",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1148",
        Name = "moh-CA",
        Code = "moh",
        EnglishName = "Mohawk (Mohawk)",
        NativeName = "Kanien'kéha",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "moh-CA",
        Code = "CA",
        EnglishName = "Canada",
        NativeName = "Canada"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "78",
        Name = "mr",
        Code = "mr",
        EnglishName = "Marathi",
        NativeName = "मराठी",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1102",
        Name = "mr-IN",
        Code = "mr",
        EnglishName = "Marathi (India)",
        NativeName = "मराठी (भारत)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mr-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "भारत"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "62",
        Name = "ms",
        Code = "ms",
        EnglishName = "Malay",
        NativeName = "Bahasa Melayu",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2110",
        Name = "ms-BN",
        Code = "ms",
        EnglishName = "Malay (Brunei Darussalam)",
        NativeName = "Bahasa Melayu (Brunei Darussalam)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ms-BN",
        Code = "BN",
        EnglishName = "Brunei Darussalam",
        NativeName = "Brunei Darussalam"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1086",
        Name = "ms-MY",
        Code = "ms",
        EnglishName = "Malay (Malaysia)",
        NativeName = "Bahasa Melayu (Malaysia)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ms-MY",
        Code = "MY",
        EnglishName = "Malaysia",
        NativeName = "Malaysia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "58",
        Name = "mt",
        Code = "mt",
        EnglishName = "Maltese",
        NativeName = "Malti",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "MT",
        Code = "MT",
        EnglishName = "Malta",
        NativeName = "Malta"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1082",
        Name = "mt-MT",
        Code = "mt",
        EnglishName = "Maltese (Malta)",
        NativeName = "Malti (Malta)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "mt-MT",
        Code = "MT",
        EnglishName = "Malta",
        NativeName = "Malta"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "85",
        Name = "my",
        Code = "my",
        EnglishName = "Burmese",
        NativeName = "ဗမာ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "MY",
        Code = "MY",
        EnglishName = "Malaysia",
        NativeName = "Malaysia"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1109",
        Name = "my-MM",
        Code = "my",
        EnglishName = "Burmese (Myanmar)",
        NativeName = "ဗမာ (မြန်မာ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "my-MM",
        Code = "MM",
        EnglishName = "Myanmar",
        NativeName = "မြန်မာ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31764",
        Name = "nb",
        Code = "nb",
        EnglishName = "Norwegian (Bokmål)",
        NativeName = "norsk (bokmål)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1044",
        Name = "nb-NO",
        Code = "nb",
        EnglishName = "Norwegian, Bokmål (Norway)",
        NativeName = "norsk, bokmål (Norge)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nb-NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Norge"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "97",
        Name = "ne",
        Code = "ne",
        EnglishName = "Nepali",
        NativeName = "नेपाली",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2145",
        Name = "ne-IN",
        Code = "ne",
        EnglishName = "Nepali (India)",
        NativeName = "नेपाली (भारत)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ne-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "भारत"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1121",
        Name = "ne-NP",
        Code = "ne",
        EnglishName = "Nepali (Nepal)",
        NativeName = "नेपाली (नेपाल)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ne-NP",
        Code = "NP",
        EnglishName = "Nepal",
        NativeName = "नेपाल"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "19",
        Name = "nl",
        Code = "nl",
        EnglishName = "Dutch",
        NativeName = "Nederlands",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "NL",
        Code = "NL",
        EnglishName = "Netherlands",
        NativeName = "Nederlân"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2067",
        Name = "nl-BE",
        Code = "nl",
        EnglishName = "Dutch (Belgium)",
        NativeName = "Nederlands (België)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nl-BE",
        Code = "BE",
        EnglishName = "Belgium",
        NativeName = "België"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1043",
        Name = "nl-NL",
        Code = "nl",
        EnglishName = "Dutch (Netherlands)",
        NativeName = "Nederlands (Nederland)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nl-NL",
        Code = "NL",
        EnglishName = "Netherlands",
        NativeName = "Nederland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30740",
        Name = "nn",
        Code = "nn",
        EnglishName = "Norwegian (Nynorsk)",
        NativeName = "norsk (nynorsk)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2068",
        Name = "nn-NO",
        Code = "nn",
        EnglishName = "Norwegian, Nynorsk (Norway)",
        NativeName = "norsk, nynorsk (Noreg)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nn-NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Noreg"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "20",
        Name = "no",
        Code = "nb",
        EnglishName = "Norwegian",
        NativeName = "norsk",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Norge"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "nqo",
        Code = "nqo",
        EnglishName = "N'ko",
        NativeName = "ߒߞߏ",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "nqo-GN",
        Code = "nqo",
        EnglishName = "N'ko (Guinea)",
        NativeName = "ߒߞߏ (ߖߌ߬ߣߍ߬ ߞߊ߲ߓߍ߲)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nqo-GN",
        Code = "GN",
        EnglishName = "Guinea",
        NativeName = "ߖߌ߬ߣߍ߬ ߞߊ߲ߓߍ߲"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "108",
        Name = "nso",
        Code = "nso",
        EnglishName = "Sesotho sa Leboa",
        NativeName = "Sesotho sa Leboa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1132",
        Name = "nso-ZA",
        Code = "nso",
        EnglishName = "Sesotho sa Leboa (South Africa)",
        NativeName = "Sesotho sa Leboa (Afrika Borwa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "nso-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "Afrika Borwa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "130",
        Name = "oc",
        Code = "oc",
        EnglishName = "Occitan",
        NativeName = "Occitan",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1154",
        Name = "oc-FR",
        Code = "oc",
        EnglishName = "Occitan (France)",
        NativeName = "Occitan (França)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "oc-FR",
        Code = "FR",
        EnglishName = "France",
        NativeName = "França"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "114",
        Name = "om",
        Code = "om",
        EnglishName = "Oromo",
        NativeName = "Oromoo",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "OM",
        Code = "OM",
        EnglishName = "Oman",
        NativeName = "عمان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1138",
        Name = "om-ET",
        Code = "om",
        EnglishName = "Oromo (Ethiopia)",
        NativeName = "Oromoo (Itoophiyaa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "om-ET",
        Code = "ET",
        EnglishName = "Ethiopia",
        NativeName = "Itoophiyaa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "72",
        Name = "or",
        Code = "or",
        EnglishName = "Odia",
        NativeName = "ଓଡ଼ିଆ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1096",
        Name = "or-IN",
        Code = "or",
        EnglishName = "Odia (India)",
        NativeName = "ଓଡ଼ିଆ (ଭାରତ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "or-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ଭାରତ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "70",
        Name = "pa",
        Code = "pa",
        EnglishName = "Punjabi",
        NativeName = "ਪੰਜਾਬੀ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "PA",
        Code = "PA",
        EnglishName = "Panama",
        NativeName = "Panamá"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31814",
        Name = "pa-Arab",
        Code = "pa",
        EnglishName = "Punjabi",
        NativeName = "پنجابی",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2118",
        Name = "pa-Arab-PK",
        Code = "pa",
        EnglishName = "Punjabi (Pakistan)",
        NativeName = "پنجابی (پاکستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pa-Arab-PK",
        Code = "PK",
        EnglishName = "Pakistan",
        NativeName = "پاکستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1094",
        Name = "pa-IN",
        Code = "pa",
        EnglishName = "Punjabi (India)",
        NativeName = "ਪੰਜਾਬੀ (ਭਾਰਤ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pa-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "ਭਾਰਤ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "21",
        Name = "pl",
        Code = "pl",
        EnglishName = "Polish",
        NativeName = "polski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "PL",
        Code = "PL",
        EnglishName = "Poland",
        NativeName = "Polska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1045",
        Name = "pl-PL",
        Code = "pl",
        EnglishName = "Polish (Poland)",
        NativeName = "polski (Polska)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pl-PL",
        Code = "PL",
        EnglishName = "Poland",
        NativeName = "Polska"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "140",
        Name = "prs",
        Code = "prs",
        EnglishName = "Dari",
        NativeName = "درى",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1164",
        Name = "prs-AF",
        Code = "prs",
        EnglishName = "Dari (Afghanistan)",
        NativeName = "درى (افغانستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "prs-AF",
        Code = "AF",
        EnglishName = "Afghanistan",
        NativeName = "افغانستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "99",
        Name = "ps",
        Code = "ps",
        EnglishName = "Pashto",
        NativeName = "پښتو",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1123",
        Name = "ps-AF",
        Code = "ps",
        EnglishName = "Pashto (Afghanistan)",
        NativeName = "پښتو (افغانستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ps-AF",
        Code = "AF",
        EnglishName = "Afghanistan",
        NativeName = "افغانستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "22",
        Name = "pt",
        Code = "pt",
        EnglishName = "Portuguese",
        NativeName = "português",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "PT",
        Code = "PT",
        EnglishName = "Portugal",
        NativeName = "Portugal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "pt-AO",
        Code = "pt",
        EnglishName = "Portuguese (Angola)",
        NativeName = "português (Angola)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pt-AO",
        Code = "AO",
        EnglishName = "Angola",
        NativeName = "Angola"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1046",
        Name = "pt-BR",
        Code = "pt",
        EnglishName = "Portuguese (Brazil)",
        NativeName = "português (Brasil)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pt-BR",
        Code = "BR",
        EnglishName = "Brazil",
        NativeName = "Brasil"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2070",
        Name = "pt-PT",
        Code = "pt",
        EnglishName = "Portuguese (Portugal)",
        NativeName = "português (Portugal)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "pt-PT",
        Code = "PT",
        EnglishName = "Portugal",
        NativeName = "Portugal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "134",
        Name = "qut",
        Code = "qut",
        EnglishName = "K'iche'",
        NativeName = "K'iche'",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1158",
        Name = "qut-GT",
        Code = "qut",
        EnglishName = "K'iche' (Guatemala)",
        NativeName = "K'iche' (Guatemala)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "qut-GT",
        Code = "GT",
        EnglishName = "Guatemala",
        NativeName = "Guatemala"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "107",
        Name = "quz",
        Code = "quz",
        EnglishName = "Quechua",
        NativeName = "runasimi",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1131",
        Name = "quz-BO",
        Code = "quz",
        EnglishName = "Quechua (Bolivia)",
        NativeName = "runasimi (Qullasuyu)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "quz-BO",
        Code = "BO",
        EnglishName = "Bolivia",
        NativeName = "Bolivia Suyu"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2155",
        Name = "quz-EC",
        Code = "quz",
        EnglishName = "Quichua (Ecuador)",
        NativeName = "runa shimi (Ecuador Suyu)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "quz-EC",
        Code = "EC",
        EnglishName = "Ecuador",
        NativeName = "Ecuador Suyu"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3179",
        Name = "quz-PE",
        Code = "quz",
        EnglishName = "Quechua (Peru)",
        NativeName = "runasimi (Peru)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "quz-PE",
        Code = "PE",
        EnglishName = "Peru",
        NativeName = "Peru"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "23",
        Name = "rm",
        Code = "rm",
        EnglishName = "Romansh",
        NativeName = "Rumantsch",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1047",
        Name = "rm-CH",
        Code = "rm",
        EnglishName = "Romansh (Switzerland)",
        NativeName = "Rumantsch (Svizra)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "rm-CH",
        Code = "CH",
        EnglishName = "Switzerland",
        NativeName = "Svizra"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "24",
        Name = "ro",
        Code = "ro",
        EnglishName = "Romanian",
        NativeName = "română",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "RO",
        Code = "RO",
        EnglishName = "Romania",
        NativeName = "România"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2072",
        Name = "ro-MD",
        Code = "ro",
        EnglishName = "Romanian (Moldova)",
        NativeName = "română (Republica Moldova)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ro-MD",
        Code = "MD",
        EnglishName = "Moldova",
        NativeName = "Republica Moldova"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1048",
        Name = "ro-RO",
        Code = "ro",
        EnglishName = "Romanian (Romania)",
        NativeName = "română (România)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ro-RO",
        Code = "RO",
        EnglishName = "Romania",
        NativeName = "România"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "25",
        Name = "ru",
        Code = "ru",
        EnglishName = "Russian",
        NativeName = "русский",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "RU",
        Code = "RU",
        EnglishName = "Russia",
        NativeName = "Рәсәй"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1049",
        Name = "ru-RU",
        Code = "ru",
        EnglishName = "Russian (Russia)",
        NativeName = "русский (Россия)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ru-RU",
        Code = "RU",
        EnglishName = "Russia",
        NativeName = "Россия"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "135",
        Name = "rw",
        Code = "rw",
        EnglishName = "Kinyarwanda",
        NativeName = "Kinyarwanda",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "RW",
        Code = "RW",
        EnglishName = "Rwanda",
        NativeName = "Rwanda"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1159",
        Name = "rw-RW",
        Code = "rw",
        EnglishName = "Kinyarwanda (Rwanda)",
        NativeName = "Kinyarwanda (Rwanda)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "rw-RW",
        Code = "RW",
        EnglishName = "Rwanda",
        NativeName = "Rwanda"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "79",
        Name = "sa",
        Code = "sa",
        EnglishName = "Sanskrit",
        NativeName = "संस्कृत",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SA",
        Code = "SA",
        EnglishName = "Saudi Arabia",
        NativeName = "المملكة العربية السعودية"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1103",
        Name = "sa-IN",
        Code = "sa",
        EnglishName = "Sanskrit (India)",
        NativeName = "संस्कृत (भारतम्)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sa-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "भारतम्"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "133",
        Name = "sah",
        Code = "sah",
        EnglishName = "Sakha",
        NativeName = "Саха",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1157",
        Name = "sah-RU",
        Code = "sah",
        EnglishName = "Sakha (Russia)",
        NativeName = "Саха (Россия)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sah-RU",
        Code = "RU",
        EnglishName = "Russia",
        NativeName = "Россия"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "89",
        Name = "sd",
        Code = "sd",
        EnglishName = "Sindhi",
        NativeName = "سنڌي",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31833",
        Name = "sd-Arab",
        Code = "sd",
        EnglishName = "Sindhi",
        NativeName = "سنڌي",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2137",
        Name = "sd-Arab-PK",
        Code = "sd",
        EnglishName = "Sindhi (Pakistan)",
        NativeName = "سنڌي (پاکستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sd-Arab-PK",
        Code = "PK",
        EnglishName = "Pakistan",
        NativeName = "پاکستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "59",
        Name = "se",
        Code = "se",
        EnglishName = "Sami (Northern)",
        NativeName = "davvisámegiella",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SE",
        Code = "SE",
        EnglishName = "Sweden",
        NativeName = "Ruoŧŧa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3131",
        Name = "se-FI",
        Code = "se",
        EnglishName = "Sami, Northern (Finland)",
        NativeName = "davvisámegiella (Suopma)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "se-FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Suopma"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1083",
        Name = "se-NO",
        Code = "se",
        EnglishName = "Sami, Northern (Norway)",
        NativeName = "davvisámegiella (Norga)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "se-NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Norga"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2107",
        Name = "se-SE",
        Code = "se",
        EnglishName = "Sami, Northern (Sweden)",
        NativeName = "davvisámegiella (Ruoŧŧa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "se-SE",
        Code = "SE",
        EnglishName = "Sweden",
        NativeName = "Ruoŧŧa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "91",
        Name = "si",
        Code = "si",
        EnglishName = "Sinhala",
        NativeName = "සිංහල",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SI",
        Code = "SI",
        EnglishName = "Slovenia",
        NativeName = "Slovenija"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1115",
        Name = "si-LK",
        Code = "si",
        EnglishName = "Sinhala (Sri Lanka)",
        NativeName = "සිංහල (ශ්\u200Dරී ලංකා)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "si-LK",
        Code = "LK",
        EnglishName = "Sri Lanka",
        NativeName = "ශ්\u200Dරී ලංකා"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "27",
        Name = "sk",
        Code = "sk",
        EnglishName = "Slovak",
        NativeName = "slovenčina",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SK",
        Code = "SK",
        EnglishName = "Slovakia",
        NativeName = "Slovenská republika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1051",
        Name = "sk-SK",
        Code = "sk",
        EnglishName = "Slovak (Slovakia)",
        NativeName = "slovenčina (Slovenská republika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sk-SK",
        Code = "SK",
        EnglishName = "Slovakia",
        NativeName = "Slovenská republika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "36",
        Name = "sl",
        Code = "sl",
        EnglishName = "Slovenian",
        NativeName = "slovenščina",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1060",
        Name = "sl-SI",
        Code = "sl",
        EnglishName = "Slovenian (Slovenia)",
        NativeName = "slovenščina (Slovenija)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sl-SI",
        Code = "SI",
        EnglishName = "Slovenia",
        NativeName = "Slovenija"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30779",
        Name = "sma",
        Code = "sma",
        EnglishName = "Sami (Southern)",
        NativeName = "åarjelsaemiengïele",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6203",
        Name = "sma-NO",
        Code = "sma",
        EnglishName = "Sami, Southern (Norway)",
        NativeName = "åarjelsaemiengïele (Nöörje)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sma-NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Nöörje"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "7227",
        Name = "sma-SE",
        Code = "sma",
        EnglishName = "Sami, Southern (Sweden)",
        NativeName = "åarjelsaemiengïele (Sveerje)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sma-SE",
        Code = "SE",
        EnglishName = "Sweden",
        NativeName = "Sveerje"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31803",
        Name = "smj",
        Code = "smj",
        EnglishName = "Sami (Lule)",
        NativeName = "julevusámegiella",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4155",
        Name = "smj-NO",
        Code = "smj",
        EnglishName = "Sami, Lule (Norway)",
        NativeName = "julevusámegiella (Vuodna)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "smj-NO",
        Code = "NO",
        EnglishName = "Norway",
        NativeName = "Vuodna"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5179",
        Name = "smj-SE",
        Code = "smj",
        EnglishName = "Sami, Lule (Sweden)",
        NativeName = "julevusámegiella (Svierik)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "smj-SE",
        Code = "SE",
        EnglishName = "Sweden",
        NativeName = "Svierik"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "28731",
        Name = "smn",
        Code = "smn",
        EnglishName = "Sami (Inari)",
        NativeName = "sämikielâ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9275",
        Name = "smn-FI",
        Code = "smn",
        EnglishName = "Sami, Inari (Finland)",
        NativeName = "sämikielâ (Suomâ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "smn-FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Suomâ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "29755",
        Name = "sms",
        Code = "sms",
        EnglishName = "Sami (Skolt)",
        NativeName = "sää´mǩiõll",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "8251",
        Name = "sms-FI",
        Code = "sms",
        EnglishName = "Sami, Skolt (Finland)",
        NativeName = "sää´mǩiõll (Lää´ddjânnam)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sms-FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Lää´ddjânnam"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "sn",
        Code = "sn",
        EnglishName = "Shona",
        NativeName = "chiShona",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SN",
        Code = "SN",
        EnglishName = "Senegal",
        NativeName = "Sénégal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "sn-Latn",
        Code = "sn",
        EnglishName = "Shona (Latin)",
        NativeName = "chiShona (Latin)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "sn-Latn-ZW",
        Code = "sn",
        EnglishName = "Shona (Latin, Zimbabwe)",
        NativeName = "chiShona (Latin, Zimbabwe)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sn-Latn-ZW",
        Code = "ZW",
        EnglishName = "Zimbabwe",
        NativeName = "Zimbabwe"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "119",
        Name = "so",
        Code = "so",
        EnglishName = "Somali",
        NativeName = "Soomaali",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SO",
        Code = "SO",
        EnglishName = "Somalia",
        NativeName = "Soomaaliya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1143",
        Name = "so-SO",
        Code = "so",
        EnglishName = "Somali (Somalia)",
        NativeName = "Soomaali (Soomaaliya)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "so-SO",
        Code = "SO",
        EnglishName = "Somalia",
        NativeName = "Soomaaliya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "28",
        Name = "sq",
        Code = "sq",
        EnglishName = "Albanian",
        NativeName = "Shqip",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1052",
        Name = "sq-AL",
        Code = "sq",
        EnglishName = "Albanian (Albania)",
        NativeName = "Shqip (Shqipëria)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sq-AL",
        Code = "AL",
        EnglishName = "Albania",
        NativeName = "Shqipëria"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31770",
        Name = "sr",
        Code = "sr",
        EnglishName = "Serbian",
        NativeName = "srpski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "27674",
        Name = "sr-Cyrl",
        Code = "sr",
        EnglishName = "Serbian (Cyrillic)",
        NativeName = "српски",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "7194",
        Name = "sr-Cyrl-BA",
        Code = "sr",
        EnglishName = "Serbian (Cyrillic, Bosnia and Herzegovina)",
        NativeName = "српски (Босна и Херцеговина)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Cyrl-BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Босна и Херцеговина"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3098",
        Name = "sr-Cyrl-CS",
        Code = "sr",
        EnglishName = "Serbian (Cyrillic, Serbia and Montenegro (Former))",
        NativeName = "српски (Србија и Црна Гора (Бивша))",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Cyrl-CS",
        Code = "CS",
        EnglishName = "Serbia and Montenegro (Former)",
        NativeName = "Србија и Црна Гора (Бивша)"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "12314",
        Name = "sr-Cyrl-ME",
        Code = "sr",
        EnglishName = "Serbian (Cyrillic, Montenegro)",
        NativeName = "српски (Црна Гора)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Cyrl-ME",
        Code = "ME",
        EnglishName = "Montenegro",
        NativeName = "Црна Гора"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "10266",
        Name = "sr-Cyrl-RS",
        Code = "sr",
        EnglishName = "Serbian (Cyrillic, Serbia)",
        NativeName = "српски (Србија)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Cyrl-RS",
        Code = "RS",
        EnglishName = "Serbia",
        NativeName = "Србија"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "28698",
        Name = "sr-Latn",
        Code = "sr",
        EnglishName = "Serbian (Latin)",
        NativeName = "srpski",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "6170",
        Name = "sr-Latn-BA",
        Code = "sr",
        EnglishName = "Serbian (Latin, Bosnia and Herzegovina)",
        NativeName = "srpski (Bosna i Hercegovina)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Latn-BA",
        Code = "BA",
        EnglishName = "Bosnia and Herzegovina",
        NativeName = "Bosna i Hercegovina"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2074",
        Name = "sr-Latn-CS",
        Code = "sr",
        EnglishName = "Serbian (Latin, Serbia and Montenegro (Former))",
        NativeName = "srpski (Srbija i Crna Gora (Bivša))",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Latn-CS",
        Code = "CS",
        EnglishName = "Serbia and Montenegro (Former)",
        NativeName = "Srbija i Crna Gora (Bivša)"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "11290",
        Name = "sr-Latn-ME",
        Code = "sr",
        EnglishName = "Serbian (Latin, Montenegro)",
        NativeName = "srpski (Crna Gora)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Latn-ME",
        Code = "ME",
        EnglishName = "Montenegro",
        NativeName = "Crna Gora"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "9242",
        Name = "sr-Latn-RS",
        Code = "sr",
        EnglishName = "Serbian (Latin, Serbia)",
        NativeName = "srpski (Srbija)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sr-Latn-RS",
        Code = "RS",
        EnglishName = "Serbia",
        NativeName = "Srbija"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "48",
        Name = "st",
        Code = "st",
        EnglishName = "Southern Sotho",
        NativeName = "Sesotho",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1072",
        Name = "st-ZA",
        Code = "st",
        EnglishName = "Southern Sotho (South Africa)",
        NativeName = "Sesotho (South Africa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "st-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "South Africa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "29",
        Name = "sv",
        Code = "sv",
        EnglishName = "Swedish",
        NativeName = "svenska",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "SV",
        Code = "SV",
        EnglishName = "El Salvador",
        NativeName = "El Salvador"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2077",
        Name = "sv-FI",
        Code = "sv",
        EnglishName = "Swedish (Finland)",
        NativeName = "svenska (Finland)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sv-FI",
        Code = "FI",
        EnglishName = "Finland",
        NativeName = "Finland"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1053",
        Name = "sv-SE",
        Code = "sv",
        EnglishName = "Swedish (Sweden)",
        NativeName = "svenska (Sverige)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sv-SE",
        Code = "SE",
        EnglishName = "Sweden",
        NativeName = "Sverige"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "65",
        Name = "sw",
        Code = "sw",
        EnglishName = "Kiswahili",
        NativeName = "Kiswahili",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1089",
        Name = "sw-KE",
        Code = "sw",
        EnglishName = "Kiswahili (Kenya)",
        NativeName = "Kiswahili (Kenya)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "sw-KE",
        Code = "KE",
        EnglishName = "Kenya",
        NativeName = "Kenya"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "90",
        Name = "syr",
        Code = "syr",
        EnglishName = "Syriac",
        NativeName = "ܣܘܪܝܝܐ",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1114",
        Name = "syr-SY",
        Code = "syr",
        EnglishName = "Syriac (Syria)",
        NativeName = "ܣܘܪܝܝܐ (ܣܘܪܝܐ)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "syr-SY",
        Code = "SY",
        EnglishName = "Syria",
        NativeName = "ܣܘܪܝܐ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "73",
        Name = "ta",
        Code = "ta",
        EnglishName = "Tamil",
        NativeName = "தமிழ்",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1097",
        Name = "ta-IN",
        Code = "ta",
        EnglishName = "Tamil (India)",
        NativeName = "தமிழ் (இந்தியா)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ta-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "இந்தியா"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2121",
        Name = "ta-LK",
        Code = "ta",
        EnglishName = "Tamil (Sri Lanka)",
        NativeName = "தமிழ் (இலங்கை)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ta-LK",
        Code = "LK",
        EnglishName = "Sri Lanka",
        NativeName = "இலங்கை"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "74",
        Name = "te",
        Code = "te",
        EnglishName = "Telugu",
        NativeName = "తెలుగు",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1098",
        Name = "te-IN",
        Code = "te",
        EnglishName = "Telugu (India)",
        NativeName = "తెలుగు (భారత దేశం)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "te-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "భారత దేశం"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "40",
        Name = "tg",
        Code = "tg",
        EnglishName = "Tajik",
        NativeName = "Тоҷикӣ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31784",
        Name = "tg-Cyrl",
        Code = "tg",
        EnglishName = "Tajik (Cyrillic)",
        NativeName = "Тоҷикӣ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1064",
        Name = "tg-Cyrl-TJ",
        Code = "tg",
        EnglishName = "Tajik (Cyrillic, Tajikistan)",
        NativeName = "Тоҷикӣ (Тоҷикистон)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tg-Cyrl-TJ",
        Code = "TJ",
        EnglishName = "Tajikistan",
        NativeName = "Тоҷикистон"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30",
        Name = "th",
        Code = "th",
        EnglishName = "Thai",
        NativeName = "ไทย",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "TH",
        Code = "TH",
        EnglishName = "Thailand",
        NativeName = "ไทย"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1054",
        Name = "th-TH",
        Code = "th",
        EnglishName = "Thai (Thailand)",
        NativeName = "ไทย (ไทย)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "th-TH",
        Code = "TH",
        EnglishName = "Thailand",
        NativeName = "ไทย"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "115",
        Name = "ti",
        Code = "ti",
        EnglishName = "Tigrinya",
        NativeName = "ትግርኛ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2163",
        Name = "ti-ER",
        Code = "ti",
        EnglishName = "Tigrinya (Eritrea)",
        NativeName = "ትግርኛ (ኤርትራ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ti-ER",
        Code = "ER",
        EnglishName = "Eritrea",
        NativeName = "ኤርትራ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1139",
        Name = "ti-ET",
        Code = "ti",
        EnglishName = "Tigrinya (Ethiopia)",
        NativeName = "ትግርኛ (ኢትዮጵያ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ti-ET",
        Code = "ET",
        EnglishName = "Ethiopia",
        NativeName = "ኢትዮጵያ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "66",
        Name = "tk",
        Code = "tk",
        EnglishName = "Turkmen",
        NativeName = "Türkmen dili",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1090",
        Name = "tk-TM",
        Code = "tk",
        EnglishName = "Turkmen (Turkmenistan)",
        NativeName = "Türkmen dili (Türkmenistan)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tk-TM",
        Code = "TM",
        EnglishName = "Turkmenistan",
        NativeName = "Türkmenistan"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "50",
        Name = "tn",
        Code = "tn",
        EnglishName = "Setswana",
        NativeName = "Setswana",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "TN",
        Code = "TN",
        EnglishName = "Tunisia",
        NativeName = "تونس"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2098",
        Name = "tn-BW",
        Code = "tn",
        EnglishName = "Setswana (Botswana)",
        NativeName = "Setswana (Botswana)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tn-BW",
        Code = "BW",
        EnglishName = "Botswana",
        NativeName = "Botswana"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1074",
        Name = "tn-ZA",
        Code = "tn",
        EnglishName = "Setswana (South Africa)",
        NativeName = "Setswana (Aforika Borwa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tn-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "Aforika Borwa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31",
        Name = "tr",
        Code = "tr",
        EnglishName = "Turkish",
        NativeName = "Türkçe",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "TR",
        Code = "TR",
        EnglishName = "Turkey",
        NativeName = "Türkiye"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1055",
        Name = "tr-TR",
        Code = "tr",
        EnglishName = "Turkish (Turkey)",
        NativeName = "Türkçe (Türkiye)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tr-TR",
        Code = "TR",
        EnglishName = "Turkey",
        NativeName = "Türkiye"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "49",
        Name = "ts",
        Code = "ts",
        EnglishName = "Tsonga",
        NativeName = "Xitsonga",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1073",
        Name = "ts-ZA",
        Code = "ts",
        EnglishName = "Tsonga (South Africa)",
        NativeName = "Xitsonga (South Africa)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ts-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "South Africa"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "68",
        Name = "tt",
        Code = "tt",
        EnglishName = "Tatar",
        NativeName = "Татар",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "TT",
        Code = "TT",
        EnglishName = "Trinidad and Tobago",
        NativeName = "Trinidad and Tobago"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1092",
        Name = "tt-RU",
        Code = "tt",
        EnglishName = "Tatar (Russia)",
        NativeName = "Татар (Россия)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tt-RU",
        Code = "RU",
        EnglishName = "Russia",
        NativeName = "Россия"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "95",
        Name = "tzm",
        Code = "tzm",
        EnglishName = "Tamazight",
        NativeName = "Tamazight",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31839",
        Name = "tzm-Latn",
        Code = "tzm",
        EnglishName = "Central Atlas Tamazight (Latin)",
        NativeName = "Tamazight",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2143",
        Name = "tzm-Latn-DZ",
        Code = "tzm",
        EnglishName = "Central Atlas Tamazight (Latin, Algeria)",
        NativeName = "Tamazight (Djazaïr)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tzm-Latn-DZ",
        Code = "DZ",
        EnglishName = "Algeria",
        NativeName = "Djazaïr"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30815",
        Name = "tzm-Tfng",
        Code = "tzm",
        EnglishName = "Central Atlas Tamazight (Tifinagh)",
        NativeName = "ⵜⴰⵎⴰⵣⵉⵖⵜ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4191",
        Name = "tzm-Tfng-MA",
        Code = "tzm",
        EnglishName = "Central Atlas Tamazight (Tifinagh, Morocco)",
        NativeName = "ⵜⴰⵎⴰⵣⵉⵖⵜ (ⵍⵎⵖⵔⵉⴱ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "tzm-Tfng-MA",
        Code = "MA",
        EnglishName = "Morocco",
        NativeName = "ⵍⵎⵖⵔⵉⴱ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "128",
        Name = "ug",
        Code = "ug",
        EnglishName = "Uyghur",
        NativeName = "ئۇيغۇرچە",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1152",
        Name = "ug-CN",
        Code = "ug",
        EnglishName = "Uyghur (China)",
        NativeName = "ئۇيغۇرچە (جۇڭخۇا خەلق جۇمھۇرىيىتى)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ug-CN",
        Code = "CN",
        EnglishName = "China",
        NativeName = "جۇڭخۇا خەلق جۇمھۇرىيىتى"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "34",
        Name = "uk",
        Code = "uk",
        EnglishName = "Ukrainian",
        NativeName = "українська",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1058",
        Name = "uk-UA",
        Code = "uk",
        EnglishName = "Ukrainian (Ukraine)",
        NativeName = "українська (Україна)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "uk-UA",
        Code = "UA",
        EnglishName = "Ukraine",
        NativeName = "Україна"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "32",
        Name = "ur",
        Code = "ur",
        EnglishName = "Urdu",
        NativeName = "اُردو",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2080",
        Name = "ur-IN",
        Code = "ur",
        EnglishName = "Urdu (India)",
        NativeName = "اردو (بھارت)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ur-IN",
        Code = "IN",
        EnglishName = "India",
        NativeName = "بھارت"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1056",
        Name = "ur-PK",
        Code = "ur",
        EnglishName = "Urdu (Pakistan)",
        NativeName = "اُردو (پاکستان)",
        IsRightToLeft = bool.Parse("True")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "ur-PK",
        Code = "PK",
        EnglishName = "Pakistan",
        NativeName = "پاکستان"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "67",
        Name = "uz",
        Code = "uz",
        EnglishName = "Uzbek",
        NativeName = "O'zbekcha",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("True"),
        Name = "UZ",
        Code = "UZ",
        EnglishName = "Uzbekistan",
        NativeName = "Ўзбекистон Республикаси"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30787",
        Name = "uz-Cyrl",
        Code = "uz",
        EnglishName = "Uzbek (Cyrillic)",
        NativeName = "Ўзбекча",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2115",
        Name = "uz-Cyrl-UZ",
        Code = "uz",
        EnglishName = "Uzbek (Cyrillic, Uzbekistan)",
        NativeName = "Ўзбекча (Ўзбекистон Республикаси)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "uz-Cyrl-UZ",
        Code = "UZ",
        EnglishName = "Uzbekistan",
        NativeName = "Ўзбекистон Республикаси"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31811",
        Name = "uz-Latn",
        Code = "uz",
        EnglishName = "Uzbek (Latin)",
        NativeName = "O'zbekcha",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1091",
        Name = "uz-Latn-UZ",
        Code = "uz",
        EnglishName = "Uzbek (Latin, Uzbekistan)",
        NativeName = "O'zbekcha (O'zbekiston Respublikasi)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "uz-Latn-UZ",
        Code = "UZ",
        EnglishName = "Uzbekistan",
        NativeName = "O'zbekiston Respublikasi"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "42",
        Name = "vi",
        Code = "vi",
        EnglishName = "Vietnamese",
        NativeName = "Tiếng Việt",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1066",
        Name = "vi-VN",
        Code = "vi",
        EnglishName = "Vietnamese (Vietnam)",
        NativeName = "Tiếng Việt (Việt Nam)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "vi-VN",
        Code = "VN",
        EnglishName = "Vietnam",
        NativeName = "Việt Nam"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "136",
        Name = "wo",
        Code = "wo",
        EnglishName = "Wolof",
        NativeName = "Wolof",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1160",
        Name = "wo-SN",
        Code = "wo",
        EnglishName = "Wolof (Senegal)",
        NativeName = "Wolof (Senegaal)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "wo-SN",
        Code = "SN",
        EnglishName = "Senegal",
        NativeName = "Senegaal"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "52",
        Name = "xh",
        Code = "xh",
        EnglishName = "isiXhosa",
        NativeName = "isiXhosa",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1076",
        Name = "xh-ZA",
        Code = "xh",
        EnglishName = "isiXhosa (South Africa)",
        NativeName = "isiXhosa (uMzantsi Afrika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "xh-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "uMzantsi Afrika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "106",
        Name = "yo",
        Code = "yo",
        EnglishName = "Yoruba",
        NativeName = "Yoruba",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1130",
        Name = "yo-NG",
        Code = "yo",
        EnglishName = "Yoruba (Nigeria)",
        NativeName = "Yoruba (Nigeria)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "yo-NG",
        Code = "NG",
        EnglishName = "Nigeria",
        NativeName = "Nigeria"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "zgh",
        Code = "zgh",
        EnglishName = "Standard Morrocan Tamazight",
        NativeName = "Standard Morrocan Tamazight",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4096",
        Name = "zgh-Tfng",
        Code = "zgh",
        EnglishName = "Standard Morrocan Tamazight (Tifinagh)",
        NativeName = "ⵜⴰⵎⴰⵣⵉⵖⵜ",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4096",
        Name = "zgh-Tfng-MA",
        Code = "zgh",
        EnglishName = "Standard Morrocan Tamazight (Tifinagh, Morocco)",
        NativeName = "ⵜⴰⵎⴰⵣⵉⵖⵜ (ⵍⵎⵖⵔⵉⴱ)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zgh-Tfng-MA",
        Code = "MA",
        EnglishName = "Morocco",
        NativeName = "ⵍⵎⵖⵔⵉⴱ"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "30724",
        Name = "zh",
        Code = "zh",
        EnglishName = "Chinese",
        NativeName = "中文",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "2052",
        Name = "zh-CN",
        Code = "zh",
        EnglishName = "Chinese (Simplified, China)",
        NativeName = "中文(中华人民共和国)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zh-CN",
        Code = "CN",
        EnglishName = "China",
        NativeName = "中华人民共和国"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4",
        Name = "zh-Hans",
        Code = "zh",
        EnglishName = "Chinese (Simplified)",
        NativeName = "中文(简体)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31748",
        Name = "zh-Hant",
        Code = "zh",
        EnglishName = "Chinese (Traditional)",
        NativeName = "中文(繁體)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "3076",
        Name = "zh-HK",
        Code = "zh",
        EnglishName = "Chinese (Traditional, Hong Kong SAR)",
        NativeName = "中文(香港特別行政區)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zh-HK",
        Code = "HK",
        EnglishName = "Hong Kong SAR",
        NativeName = "香港特別行政區"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "5124",
        Name = "zh-MO",
        Code = "zh",
        EnglishName = "Chinese (Traditional, Macao SAR)",
        NativeName = "中文(澳門特別行政區)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zh-MO",
        Code = "MO",
        EnglishName = "Macao SAR",
        NativeName = "澳門特別行政區"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "4100",
        Name = "zh-SG",
        Code = "zh",
        EnglishName = "Chinese (Simplified, Singapore)",
        NativeName = "中文(新加坡)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zh-SG",
        Code = "SG",
        EnglishName = "Singapore",
        NativeName = "新加坡"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1028",
        Name = "zh-TW",
        Code = "zh",
        EnglishName = "Chinese (Traditional, Taiwan)",
        NativeName = "中文(台灣)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zh-TW",
        Code = "TW",
        EnglishName = "Taiwan",
        NativeName = "台灣"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "53",
        Name = "zu",
        Code = "zu",
        EnglishName = "isiZulu",
        NativeName = "isiZulu",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("False"),
        LCID = "1077",
        Name = "zu-ZA",
        Code = "zu",
        EnglishName = "isiZulu (South Africa)",
        NativeName = "isiZulu (iNingizimu Afrika)",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Regions.Add(new RegionData()
      {
        IsNeutral = bool.Parse("False"),
        Name = "zu-ZA",
        Code = "ZA",
        EnglishName = "South Africa",
        NativeName = "iNingizimu Afrika"
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "4",
        Name = "zh-CHS",
        Code = "zh",
        EnglishName = "Chinese (Simplified) Legacy",
        NativeName = "中文(简体) 旧版",
        IsRightToLeft = bool.Parse("False")
      });
      CultureTables.Languages.Add(new LanguageData()
      {
        IsNeutral = bool.Parse("True"),
        LCID = "31748",
        Name = "zh-CHT",
        Code = "zh",
        EnglishName = "Chinese (Traditional) Legacy",
        NativeName = "中文(繁體) 舊版",
        IsRightToLeft = bool.Parse("False")
      });
    }
  }
}
