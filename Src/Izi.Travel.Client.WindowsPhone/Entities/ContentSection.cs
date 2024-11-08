// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Entities.ContentSection
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using System;

#nullable disable
namespace Izi.Travel.Client.Entities
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
