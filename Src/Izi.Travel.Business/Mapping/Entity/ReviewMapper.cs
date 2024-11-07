// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.ReviewMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class ReviewMapper : MapperBase<Izi.Travel.Business.Entities.Data.Review, Izi.Travel.Client.Entities.Review>
  {
    public override Izi.Travel.Client.Entities.Review Convert(Izi.Travel.Business.Entities.Data.Review source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Review ConvertBack(Izi.Travel.Client.Entities.Review target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Review) null;
      return new Izi.Travel.Business.Entities.Data.Review()
      {
        Id = target.Id,
        Language = target.Language,
        Date = target.Date,
        Rating = target.Rating,
        ReviewerName = target.ReviewerName,
        Text = target.Text
      };
    }
  }
}
