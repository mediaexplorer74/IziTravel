// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.ContentSection
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  [Flags]
  public enum ContentSection
  {
    None = 1,
    Children = 2,
    Collections = 4,
    References = 8,
    Download = 16, // 0x00000010
    Translations = 32, // 0x00000020
    News = 64, // 0x00000040
    Sponsors = 128, // 0x00000080
    All = Sponsors | News | Translations | Download | References | Collections | Children, // 0x000000FE
  }
}
