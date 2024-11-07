// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.PublisherContactsMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Client.Entities;
using System;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class PublisherContactsMapper : MapperBase<Contacts, PublisherContacts>
  {
    public override PublisherContacts Convert(Contacts source)
    {
      throw new NotImplementedException();
    }

    public override Contacts ConvertBack(PublisherContacts target)
    {
      if (target == null)
        return (Contacts) null;
      return new Contacts()
      {
        WebSite = target.WebSite,
        Facebook = target.Facebook,
        Twitter = target.Twitter,
        Instagram = target.Instagram,
        GooglePlus = target.GooglePlus,
        Vk = target.Vk,
        YouTube = target.YouTube
      };
    }
  }
}
