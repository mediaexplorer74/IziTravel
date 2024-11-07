// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectFullMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Client.Entities;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectFullMapper : MapperBase<MtgObject, MtgObjectFull>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectCategoryMapper _mtgObjectCategoryMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly ContentProviderMapper _contentProviderMapper;
    private readonly MtgObjectPublisherCompactMapper _mtgObjectPublisherCompactMapper;
    private readonly PurchaseMapper _purchaseMapper;
    private readonly LocationMapper _locationMapper;
    private readonly TriggerZoneMapper _triggerZoneMapper;
    private readonly RatingMapper _ratingMapper;
    private readonly MtgObjectContentMapper _mtgObjectContentMapper;
    private readonly MapMapper _mapMapper;
    private readonly ScheduleMapper _scheduleMapper;
    private readonly MtgObjectContactsMapper _mtgObjectContactsMapper;
    private readonly SponsorMapper _sponsorMapper;

    public MtgObjectFullMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectCategoryMapper mtgObjectCategoryMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      ContentProviderMapper contentProviderMapper,
      MtgObjectPublisherCompactMapper mtgObjectPublisherCompactMapper,
      PurchaseMapper purchaseMapper,
      LocationMapper locationMapper,
      TriggerZoneMapper triggerZoneMapper,
      RatingMapper ratingMapper,
      MtgObjectContentMapper mtgObjectContentMapper,
      MapMapper mapMapper,
      ScheduleMapper scheduleMapper,
      MtgObjectContactsMapper mtgObjectContactsMapper,
      SponsorMapper sponsorMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectCategoryMapper = mtgObjectCategoryMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._contentProviderMapper = contentProviderMapper;
      this._mtgObjectPublisherCompactMapper = mtgObjectPublisherCompactMapper;
      this._purchaseMapper = purchaseMapper;
      this._locationMapper = locationMapper;
      this._triggerZoneMapper = triggerZoneMapper;
      this._ratingMapper = ratingMapper;
      this._mtgObjectContentMapper = mtgObjectContentMapper;
      this._mapMapper = mapMapper;
      this._scheduleMapper = scheduleMapper;
      this._mtgObjectContactsMapper = mtgObjectContactsMapper;
      this._sponsorMapper = sponsorMapper;
    }

    public override MtgObjectFull Convert(MtgObject source) => throw new NotImplementedException();

    public override MtgObject ConvertBack(MtgObjectFull target)
    {
      if (target == null)
        return (MtgObject) null;
      MtgObject mtgObject = new MtgObject();
      mtgObject.Uid = target.Uid;
      mtgObject.ParentUid = target.ParentUid;
      mtgObject.Schedule = this._scheduleMapper.ConvertBack(target.Schedule);
      mtgObject.Contacts = this._mtgObjectContactsMapper.ConvertBack(target.Contacts);
      mtgObject.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject.Category = this._mtgObjectCategoryMapper.ConvertBack(target.Category);
      mtgObject.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject.Content = target.Content != null ? target.Content.Select<MtgObjectContent, Content>((Func<MtgObjectContent, Content>) (x => this._mtgObjectContentMapper.ConvertBack(x))).ToArray<Content>() : (Content[]) null;
      mtgObject.ContentProvider = this._contentProviderMapper.ConvertBack(target.ContentProvider);
      mtgObject.Publisher = this._mtgObjectPublisherCompactMapper.ConvertBack(target.Publisher);
      mtgObject.Duration = target.Duration;
      mtgObject.Distance = target.Distance;
      mtgObject.Purchase = this._purchaseMapper.ConvertBack(target.Purchase);
      mtgObject.Location = this._locationMapper.ConvertBack(target.Location);
      mtgObject.Map = this._mapMapper.ConvertBack(target.Map);
      mtgObject.TriggerZones = target.TriggerZones != null ? target.TriggerZones.Select<Izi.Travel.Client.Entities.TriggerZone, Izi.Travel.Business.Entities.Data.TriggerZone>((Func<Izi.Travel.Client.Entities.TriggerZone, Izi.Travel.Business.Entities.Data.TriggerZone>) (x => this._triggerZoneMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.TriggerZone>() : (Izi.Travel.Business.Entities.Data.TriggerZone[]) null;
      mtgObject.Rating = this._ratingMapper.ConvertBack(target.Rating);
      mtgObject.Sponsors = target.Sponsors != null ? target.Sponsors.Select<Izi.Travel.Client.Entities.Sponsor, Izi.Travel.Business.Entities.Data.Sponsor>((Func<Izi.Travel.Client.Entities.Sponsor, Izi.Travel.Business.Entities.Data.Sponsor>) (x => this._sponsorMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Sponsor>() : (Izi.Travel.Business.Entities.Data.Sponsor[]) null;
      mtgObject.Hidden = target.Hidden;
      mtgObject.Hash = target.Hash;
      mtgObject.Size = target.Size;
      mtgObject.AccessType = MtgObjectAccessType.Online;
      return mtgObject;
    }
  }
}
