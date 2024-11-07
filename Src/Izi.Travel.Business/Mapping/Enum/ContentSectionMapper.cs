// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.ContentSectionMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class ContentSectionMapper : MapperBase<Izi.Travel.Business.Entities.Data.ContentSection, Izi.Travel.Client.Entities.ContentSection>
  {
    public override Izi.Travel.Client.Entities.ContentSection Convert(Izi.Travel.Business.Entities.Data.ContentSection source)
    {
      return (Izi.Travel.Client.Entities.ContentSection) source;
    }

    public override Izi.Travel.Business.Entities.Data.ContentSection ConvertBack(
      Izi.Travel.Client.Entities.ContentSection target)
    {
      throw new NotImplementedException();
    }
  }
}
