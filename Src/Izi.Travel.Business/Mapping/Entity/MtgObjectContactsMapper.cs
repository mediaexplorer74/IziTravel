// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectContactsMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectContactsMapper : MapperBase<Contacts, MtgObjectContacts>
  {
    public override MtgObjectContacts Convert(Contacts source)
    {
      throw new NotImplementedException();
    }

    public override Contacts ConvertBack(MtgObjectContacts target)
    {
      if (target == null)
        return (Contacts) null;
      return new Contacts()
      {
        PhoneNumber = target.PhoneNumber,
        WebSite = target.WebSite,
        Country = target.Country,
        City = target.City,
        Address = target.Address,
        PostCode = target.PostCode,
        State = target.State
      };
    }
  }
}
