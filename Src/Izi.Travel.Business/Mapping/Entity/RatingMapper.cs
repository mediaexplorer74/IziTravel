// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.RatingMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class RatingMapper : MapperBase<Izi.Travel.Business.Entities.Data.Rating, Izi.Travel.Client.Entities.Rating>
  {
    public override Izi.Travel.Client.Entities.Rating Convert(Izi.Travel.Business.Entities.Data.Rating source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Rating ConvertBack(Izi.Travel.Client.Entities.Rating target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Rating) null;
      return new Izi.Travel.Business.Entities.Data.Rating()
      {
        Count = target.Count,
        Average = target.Average,
        ReviewsCount = target.ReviewsCount
      };
    }
  }
}
