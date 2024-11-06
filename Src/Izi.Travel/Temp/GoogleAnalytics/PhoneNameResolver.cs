// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.PhoneNameResolver
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using System.Collections.Generic;
using System.Text.RegularExpressions;

#nullable disable
namespace GoogleAnalytics
{
  internal static class PhoneNameResolver
  {
    private static Dictionary<string, CanonicalPhoneName> huaweiLookupTable = new Dictionary<string, CanonicalPhoneName>()
    {
      {
        "HUAWEI H883G",
        new CanonicalPhoneName() { CanonicalModel = "Ascend W1" }
      },
      {
        "HUAWEI W1",
        new CanonicalPhoneName() { CanonicalModel = "Ascend W1" }
      },
      {
        "HUAWEI W2",
        new CanonicalPhoneName() { CanonicalModel = "Ascend W2" }
      }
    };
    private static Dictionary<string, CanonicalPhoneName> lgLookupTable = new Dictionary<string, CanonicalPhoneName>()
    {
      {
        "LG-C900",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Optimus 7Q/Quantum"
        }
      },
      {
        "LG-E900",
        new CanonicalPhoneName() { CanonicalModel = "Optimus 7" }
      },
      {
        "LG-E906",
        new CanonicalPhoneName() { CanonicalModel = "Jil Sander" }
      }
    };
    private static Dictionary<string, CanonicalPhoneName> samsungLookupTable = new Dictionary<string, CanonicalPhoneName>()
    {
      {
        "GT-I8350",
        new CanonicalPhoneName() { CanonicalModel = "Omnia W" }
      },
      {
        "GT-I8350T",
        new CanonicalPhoneName() { CanonicalModel = "Omnia W" }
      },
      {
        "OMNIA W",
        new CanonicalPhoneName() { CanonicalModel = "Omnia W" }
      },
      {
        "GT-I8700",
        new CanonicalPhoneName() { CanonicalModel = "Omnia 7" }
      },
      {
        "OMNIA7",
        new CanonicalPhoneName() { CanonicalModel = "Omnia 7" }
      },
      {
        "GT-S7530",
        new CanonicalPhoneName() { CanonicalModel = "Omnia 7" }
      },
      {
        "I917",
        new CanonicalPhoneName() { CanonicalModel = "Focus" }
      },
      {
        "SGH-I917",
        new CanonicalPhoneName() { CanonicalModel = "Focus" }
      },
      {
        "SGH-I667",
        new CanonicalPhoneName() { CanonicalModel = "Focus 2" }
      },
      {
        "SGH-I677",
        new CanonicalPhoneName() { CanonicalModel = "Focus Flash" }
      },
      {
        "HADEN",
        new CanonicalPhoneName() { CanonicalModel = "Focus S" }
      },
      {
        "SGH-I937",
        new CanonicalPhoneName() { CanonicalModel = "Focus S" }
      },
      {
        "GT-I8750",
        new CanonicalPhoneName() { CanonicalModel = "ATIV S" }
      },
      {
        "SGH-T899M",
        new CanonicalPhoneName() { CanonicalModel = "ATIV S" }
      },
      {
        "SCH-I930",
        new CanonicalPhoneName() { CanonicalModel = "ATIV Odyssey" }
      },
      {
        "SCH-R860U",
        new CanonicalPhoneName()
        {
          CanonicalModel = "ATIV Odyssey",
          Comments = "US Cellular"
        }
      },
      {
        "SPH-I800",
        new CanonicalPhoneName()
        {
          CanonicalModel = "ATIV S Neo",
          Comments = "Sprint"
        }
      },
      {
        "SGH-I187",
        new CanonicalPhoneName()
        {
          CanonicalModel = "ATIV S Neo",
          Comments = "AT&T"
        }
      },
      {
        "GT-I8675",
        new CanonicalPhoneName() { CanonicalModel = "ATIV S Neo" }
      },
      {
        "SM-W750V",
        new CanonicalPhoneName()
        {
          CanonicalModel = "ATIV SE",
          Comments = "Verizon"
        }
      }
    };
    private static Dictionary<string, CanonicalPhoneName> htcLookupTable = new Dictionary<string, CanonicalPhoneName>()
    {
      {
        "7 MONDRIAN T8788",
        new CanonicalPhoneName() { CanonicalModel = "Surround" }
      },
      {
        "T8788",
        new CanonicalPhoneName() { CanonicalModel = "Surround" }
      },
      {
        "SURROUND",
        new CanonicalPhoneName() { CanonicalModel = "Surround" }
      },
      {
        "SURROUND T8788",
        new CanonicalPhoneName() { CanonicalModel = "Surround" }
      },
      {
        "7 MOZART",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "7 MOZART T8698",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "HTC MOZART",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "MERSAD 7 MOZART T8698",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "MOZART",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "MOZART T8698",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "PD67100",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "T8697",
        new CanonicalPhoneName() { CanonicalModel = "Mozart" }
      },
      {
        "7 PRO T7576",
        new CanonicalPhoneName() { CanonicalModel = "7 Pro" }
      },
      {
        "MWP6885",
        new CanonicalPhoneName() { CanonicalModel = "7 Pro" }
      },
      {
        "USCCHTC-PC93100",
        new CanonicalPhoneName() { CanonicalModel = "7 Pro" }
      },
      {
        "PC93100",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Arrive",
          Comments = "Sprint"
        }
      },
      {
        "T7575",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Arrive",
          Comments = "Sprint"
        }
      },
      {
        "HD2",
        new CanonicalPhoneName() { CanonicalModel = "HD2" }
      },
      {
        "HD2 LEO",
        new CanonicalPhoneName() { CanonicalModel = "HD2" }
      },
      {
        "LEO",
        new CanonicalPhoneName() { CanonicalModel = "HD2" }
      },
      {
        "7 SCHUBERT T9292",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "GOLD",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "HD7",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "HD7 T9292",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "MONDRIAN",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "SCHUBERT",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "Schubert T9292",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "T9296",
        new CanonicalPhoneName()
        {
          CanonicalModel = "HD7",
          Comments = "Telstra, AU"
        }
      },
      {
        "TOUCH-IT HD7",
        new CanonicalPhoneName() { CanonicalModel = "HD7" }
      },
      {
        "T9295",
        new CanonicalPhoneName() { CanonicalModel = "HD7S" }
      },
      {
        "7 TROPHY",
        new CanonicalPhoneName() { CanonicalModel = "Trophy" }
      },
      {
        "7 TROPHY T8686",
        new CanonicalPhoneName() { CanonicalModel = "Trophy" }
      },
      {
        "PC40100",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Trophy",
          Comments = "Verizon"
        }
      },
      {
        "SPARK",
        new CanonicalPhoneName() { CanonicalModel = "Trophy" }
      },
      {
        "TOUCH-IT TROPHY",
        new CanonicalPhoneName() { CanonicalModel = "Trophy" }
      },
      {
        "MWP6985",
        new CanonicalPhoneName() { CanonicalModel = "Trophy" }
      },
      {
        "A620",
        new CanonicalPhoneName() { CanonicalModel = "8S" }
      },
      {
        "WINDOWS PHONE 8S BY HTC",
        new CanonicalPhoneName() { CanonicalModel = "8S" }
      },
      {
        "C620",
        new CanonicalPhoneName() { CanonicalModel = "8X" }
      },
      {
        "C625",
        new CanonicalPhoneName() { CanonicalModel = "8X" }
      },
      {
        "HTC6990LVW",
        new CanonicalPhoneName()
        {
          CanonicalModel = "8X",
          Comments = "Verizon"
        }
      },
      {
        "PM23300",
        new CanonicalPhoneName()
        {
          CanonicalModel = "8X",
          Comments = "AT&T"
        }
      },
      {
        "WINDOWS PHONE 8X BY HTC",
        new CanonicalPhoneName() { CanonicalModel = "8X" }
      },
      {
        "HTCPO881 SPRINT",
        new CanonicalPhoneName()
        {
          CanonicalModel = "8XT",
          Comments = "Sprint"
        }
      },
      {
        "ETERNITY",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Titan",
          Comments = "China"
        }
      },
      {
        "PI39100",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Titan",
          Comments = "AT&T"
        }
      },
      {
        "TITAN X310E",
        new CanonicalPhoneName() { CanonicalModel = "Titan" }
      },
      {
        "ULTIMATE",
        new CanonicalPhoneName() { CanonicalModel = "Titan" }
      },
      {
        "X310E",
        new CanonicalPhoneName() { CanonicalModel = "Titan" }
      },
      {
        "X310E TITAN",
        new CanonicalPhoneName() { CanonicalModel = "Titan" }
      },
      {
        "PI86100",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Titan II",
          Comments = "AT&T"
        }
      },
      {
        "RADIANT",
        new CanonicalPhoneName() { CanonicalModel = "Titan II" }
      },
      {
        "RADAR",
        new CanonicalPhoneName() { CanonicalModel = "Radar" }
      },
      {
        "RADAR 4G",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Radar",
          Comments = "T-Mobile USA"
        }
      },
      {
        "RADAR C110E",
        new CanonicalPhoneName() { CanonicalModel = "Radar" }
      },
      {
        "HTC6995LVW",
        new CanonicalPhoneName()
        {
          CanonicalModel = "One M8",
          Comments = "Verizon"
        }
      }
    };
    private static Dictionary<string, CanonicalPhoneName> nokiaLookupTable = new Dictionary<string, CanonicalPhoneName>()
    {
      {
        "LUMIA 505",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 505" }
      },
      {
        "LUMIA 510",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 510" }
      },
      {
        "NOKIA 510",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 510" }
      },
      {
        "LUMIA 610",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 610" }
      },
      {
        "LUMIA 610 NFC",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 610",
          Comments = "NFC"
        }
      },
      {
        "NOKIA 610",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 610" }
      },
      {
        "NOKIA 610C",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 610" }
      },
      {
        "LUMIA 620",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 620" }
      },
      {
        "RM-846",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 620" }
      },
      {
        "LUMIA 710",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 710" }
      },
      {
        "NOKIA 710",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 710" }
      },
      {
        "LUMIA 800",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 800" }
      },
      {
        "LUMIA 800C",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 800" }
      },
      {
        "NOKIA 800",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 800" }
      },
      {
        "NOKIA 800C",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 800",
          Comments = "China"
        }
      },
      {
        "RM-878",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 810" }
      },
      {
        "RM-824",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 820" }
      },
      {
        "RM-825",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 820" }
      },
      {
        "RM-826",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 820" }
      },
      {
        "RM-845",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 822",
          Comments = "Verizon"
        }
      },
      {
        "LUMIA 900",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 900" }
      },
      {
        "NOKIA 900",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 900" }
      },
      {
        "RM-820",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 920" }
      },
      {
        "RM-821",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 920" }
      },
      {
        "RM-822",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 920" }
      },
      {
        "RM-867",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 920",
          Comments = "920T"
        }
      },
      {
        "NOKIA 920",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 920" }
      },
      {
        "LUMIA 920",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 920" }
      },
      {
        "RM-914",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 520" }
      },
      {
        "RM-915",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 520" }
      },
      {
        "RM-913",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 520",
          Comments = "520T"
        }
      },
      {
        "RM-917",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 521",
          Comments = "T-Mobile 520"
        }
      },
      {
        "RM-885",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 720" }
      },
      {
        "RM-887",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 720",
          Comments = "China 720T"
        }
      },
      {
        "RM-860",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 928" }
      },
      {
        "RM-892",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 925" }
      },
      {
        "RM-893",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 925" }
      },
      {
        "RM-910",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 925" }
      },
      {
        "RM-955",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 925",
          Comments = "China 925T"
        }
      },
      {
        "RM-875",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" }
      },
      {
        "RM-876",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" }
      },
      {
        "RM-877",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1020" }
      },
      {
        "RM-941",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 625" }
      },
      {
        "RM-942",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 625" }
      },
      {
        "RM-943",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 625" }
      },
      {
        "RM-937",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1520" }
      },
      {
        "RM-938",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 1520",
          Comments = "AT&T"
        }
      },
      {
        "RM-939",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1520" }
      },
      {
        "RM-940",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 1520",
          Comments = "AT&T"
        }
      },
      {
        "RM-998",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 525" }
      },
      {
        "RM-994",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1320" }
      },
      {
        "RM-995",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1320" }
      },
      {
        "RM-996",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 1320" }
      },
      {
        "RM-927",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia Icon",
          Comments = "Verizon"
        }
      },
      {
        "RM-976",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 630" }
      },
      {
        "RM-977",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 630" }
      },
      {
        "RM-978",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 630" }
      },
      {
        "RM-979",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 630" }
      },
      {
        "RM-974",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 635" }
      },
      {
        "RM-975",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 635" }
      },
      {
        "RM-997",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 526",
          Comments = "China Mobile"
        }
      },
      {
        "RM-1045",
        new CanonicalPhoneName() { CanonicalModel = "Lumia 930" }
      },
      {
        "RM-1027",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 636",
          Comments = "China"
        }
      },
      {
        "RM-1010",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 638",
          Comments = "China"
        }
      },
      {
        "RM-1017",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 530",
          Comments = "Single SIM"
        }
      },
      {
        "RM-1018",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 530",
          Comments = "Single SIM"
        }
      },
      {
        "RM-1019",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 530",
          Comments = "Dual SIM"
        }
      },
      {
        "RM-1020",
        new CanonicalPhoneName()
        {
          CanonicalModel = "Lumia 530",
          Comments = "Dual SIM"
        }
      }
    };

    public static CanonicalPhoneName Resolve(string manufacturer, string model)
    {
      switch (manufacturer.Trim().ToUpper())
      {
        case "NOKIA":
          return PhoneNameResolver.ResolveNokia(manufacturer, model);
        case "HTC":
          return PhoneNameResolver.ResolveHtc(manufacturer, model);
        case "SAMSUNG":
          return PhoneNameResolver.ResolveSamsung(manufacturer, model);
        case "LG":
          return PhoneNameResolver.ResolveLg(manufacturer, model);
        case "HUAWEI":
          return PhoneNameResolver.ResolveHuawei(manufacturer, model);
        default:
          return new CanonicalPhoneName()
          {
            ReportedManufacturer = manufacturer,
            ReportedModel = model,
            CanonicalManufacturer = manufacturer,
            CanonicalModel = model,
            IsResolved = false
          };
      }
    }

    private static CanonicalPhoneName ResolveHuawei(string manufacturer, string model)
    {
      string upper = model.Trim().ToUpper();
      CanonicalPhoneName canonicalPhoneName1 = new CanonicalPhoneName()
      {
        ReportedManufacturer = manufacturer,
        ReportedModel = model,
        CanonicalManufacturer = "HUAWEI",
        CanonicalModel = model,
        IsResolved = false
      };
      string key = upper;
      if (key.StartsWith("HUAWEI H883G"))
        key = "HUAWEI H883G";
      if (key.StartsWith("HUAWEI W1"))
        key = "HUAWEI W1";
      if (upper.StartsWith("HUAWEI W2"))
        key = "HUAWEI W2";
      if (PhoneNameResolver.huaweiLookupTable.ContainsKey(key))
      {
        CanonicalPhoneName canonicalPhoneName2 = PhoneNameResolver.huaweiLookupTable[key];
        canonicalPhoneName1.CanonicalModel = canonicalPhoneName2.CanonicalModel;
        canonicalPhoneName1.Comments = canonicalPhoneName2.Comments;
        canonicalPhoneName1.IsResolved = true;
      }
      return canonicalPhoneName1;
    }

    private static CanonicalPhoneName ResolveLg(string manufacturer, string model)
    {
      string upper = model.Trim().ToUpper();
      CanonicalPhoneName canonicalPhoneName1 = new CanonicalPhoneName()
      {
        ReportedManufacturer = manufacturer,
        ReportedModel = model,
        CanonicalManufacturer = "LG",
        CanonicalModel = model,
        IsResolved = false
      };
      string key = upper;
      if (key.StartsWith("LG-C900"))
        key = "LG-C900";
      if (key.StartsWith("LG-E900"))
        key = "LG-E900";
      if (PhoneNameResolver.lgLookupTable.ContainsKey(key))
      {
        CanonicalPhoneName canonicalPhoneName2 = PhoneNameResolver.lgLookupTable[key];
        canonicalPhoneName1.CanonicalModel = canonicalPhoneName2.CanonicalModel;
        canonicalPhoneName1.Comments = canonicalPhoneName2.Comments;
        canonicalPhoneName1.IsResolved = true;
      }
      return canonicalPhoneName1;
    }

    private static CanonicalPhoneName ResolveSamsung(string manufacturer, string model)
    {
      string upper = model.Trim().ToUpper();
      CanonicalPhoneName canonicalPhoneName1 = new CanonicalPhoneName()
      {
        ReportedManufacturer = manufacturer,
        ReportedModel = model,
        CanonicalManufacturer = "SAMSUNG",
        CanonicalModel = model,
        IsResolved = false
      };
      string key = upper;
      if (key.StartsWith("GT-S7530"))
        key = "GT-S7530";
      if (key.StartsWith("SGH-I917"))
        key = "SGH-I917";
      if (PhoneNameResolver.samsungLookupTable.ContainsKey(key))
      {
        CanonicalPhoneName canonicalPhoneName2 = PhoneNameResolver.samsungLookupTable[key];
        canonicalPhoneName1.CanonicalModel = canonicalPhoneName2.CanonicalModel;
        canonicalPhoneName1.Comments = canonicalPhoneName2.Comments;
        canonicalPhoneName1.IsResolved = true;
      }
      return canonicalPhoneName1;
    }

    private static CanonicalPhoneName ResolveHtc(string manufacturer, string model)
    {
      string upper = model.Trim().ToUpper();
      CanonicalPhoneName canonicalPhoneName1 = new CanonicalPhoneName()
      {
        ReportedManufacturer = manufacturer,
        ReportedModel = model,
        CanonicalManufacturer = "HTC",
        CanonicalModel = model,
        IsResolved = false
      };
      string key = upper;
      if (key.StartsWith("A620"))
        key = "A620";
      if (key.StartsWith("C625"))
        key = "C625";
      if (key.StartsWith("C620"))
        key = "C620";
      if (PhoneNameResolver.htcLookupTable.ContainsKey(key))
      {
        CanonicalPhoneName canonicalPhoneName2 = PhoneNameResolver.htcLookupTable[key];
        canonicalPhoneName1.CanonicalModel = canonicalPhoneName2.CanonicalModel;
        canonicalPhoneName1.Comments = canonicalPhoneName2.Comments;
        canonicalPhoneName1.IsResolved = true;
      }
      return canonicalPhoneName1;
    }

    private static CanonicalPhoneName ResolveNokia(string manufacturer, string model)
    {
      string upper = model.Trim().ToUpper();
      CanonicalPhoneName canonicalPhoneName1 = new CanonicalPhoneName()
      {
        ReportedManufacturer = manufacturer,
        ReportedModel = model,
        CanonicalManufacturer = "NOKIA",
        CanonicalModel = model,
        IsResolved = false
      };
      string key = upper;
      if (upper.StartsWith("RM-"))
        key = Regex.Match(upper, "(RM-)([0-9]+)").Value;
      if (PhoneNameResolver.nokiaLookupTable.ContainsKey(key))
      {
        CanonicalPhoneName canonicalPhoneName2 = PhoneNameResolver.nokiaLookupTable[key];
        canonicalPhoneName1.CanonicalModel = canonicalPhoneName2.CanonicalModel;
        canonicalPhoneName1.Comments = canonicalPhoneName2.Comments;
        canonicalPhoneName1.IsResolved = true;
      }
      return canonicalPhoneName1;
    }
  }
}
