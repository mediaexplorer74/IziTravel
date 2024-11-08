// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.PurchaseMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class PurchaseMapper : MapperBase<Izi.Travel.Business.Entities.Data.Purchase, Izi.Travel.Client.Entities.Purchase>
  {
    public override Izi.Travel.Client.Entities.Purchase Convert(Izi.Travel.Business.Entities.Data.Purchase source)
    {
      throw new NotImplementedException();
    }

    public override Izi.Travel.Business.Entities.Data.Purchase ConvertBack(Izi.Travel.Client.Entities.Purchase target)
    {
      if (target == null)
        return (Izi.Travel.Business.Entities.Data.Purchase) null;
      return new Izi.Travel.Business.Entities.Data.Purchase()
      {
        Currency = target.Currency,
        Price = target.Price,
        ProductId = target.ProductId
      };
    }
  }
}
