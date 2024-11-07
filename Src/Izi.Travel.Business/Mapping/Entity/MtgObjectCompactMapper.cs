// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Entity.MtgObjectCompactMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Mapping.Enum;
using Izi.Travel.Client.Entities;
using System;
using System.Linq;

#nullable disable
namespace Izi.Travel.Business.Mapping.Entity
{
  internal class MtgObjectCompactMapper : MapperBase<MtgObject, MtgObjectCompact>
  {
    private readonly MtgObjectTypeMapper _mtgObjectTypeMapper;
    private readonly MtgObjectCategoryMapper _mtgObjectCategoryMapper;
    private readonly MtgObjectStatusMapper _mtgObjectStatusMapper;
    private readonly MediaMapper _mediaMapper;
    private readonly ContentProviderMapper _contentProviderMapper;
    private readonly MtgObjectPublisherCompactMapper _mtgObjectPublisherCompactMapper;
    private readonly PurchaseMapper _purchaseMapper;
    private readonly LocationMapper _locationMapper;
    private readonly TriggerZoneMapper _triggerZoneMapper;
    private readonly RatingMapper _ratingMapper;

    public MtgObjectCompactMapper(
      MtgObjectTypeMapper mtgObjectTypeMapper,
      MtgObjectCategoryMapper mtgObjectCategoryMapper,
      MtgObjectStatusMapper mtgObjectStatusMapper,
      MediaMapper mediaMapper,
      ContentProviderMapper contentProviderMapper,
      MtgObjectPublisherCompactMapper mtgObjectPublisherCompactMapper,
      PurchaseMapper purchaseMapper,
      LocationMapper locationMapper,
      TriggerZoneMapper triggerZoneMapper,
      RatingMapper ratingMapper)
    {
      this._mtgObjectTypeMapper = mtgObjectTypeMapper;
      this._mtgObjectCategoryMapper = mtgObjectCategoryMapper;
      this._mtgObjectStatusMapper = mtgObjectStatusMapper;
      this._mediaMapper = mediaMapper;
      this._contentProviderMapper = contentProviderMapper;
      this._mtgObjectPublisherCompactMapper = mtgObjectPublisherCompactMapper;
      this._purchaseMapper = purchaseMapper;
      this._locationMapper = locationMapper;
      this._triggerZoneMapper = triggerZoneMapper;
      this._ratingMapper = ratingMapper;
    }

    public override MtgObjectCompact Convert(MtgObject source)
    {
      throw new NotImplementedException();
    }

    public override MtgObject ConvertBack(MtgObjectCompact target)
    {
      if (target == null)
        return (MtgObject) null;
      Content content = new Content()
      {
        Title = target.Title,
        Language = target.Language
      };
      if (target.Images != null && target.Images.Count > 0)
        content.Images = target.Images.Select<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>((Func<Izi.Travel.Client.Entities.Media, Izi.Travel.Business.Entities.Data.Media>) (x => this._mediaMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.Media>();
      MtgObject mtgObject1 = new MtgObject();
      mtgObject1.Uid = target.Uid;
      mtgObject1.Type = this._mtgObjectTypeMapper.ConvertBack(target.Type);
      mtgObject1.Languages = target.Languages != null ? target.Languages.ToArray() : (string[]) null;
      mtgObject1.Category = this._mtgObjectCategoryMapper.ConvertBack(target.Category);
      mtgObject1.Status = this._mtgObjectStatusMapper.ConvertBack(target.Status);
      mtgObject1.Content = new Content[1]{ content };
      mtgObject1.ContentProvider = this._contentProviderMapper.ConvertBack(target.ContentProvider);
      mtgObject1.Publisher = this._mtgObjectPublisherCompactMapper.ConvertBack(target.Publisher);
      mtgObject1.Duration = target.Duration;
      mtgObject1.Distance = target.Distance;
      mtgObject1.Purchase = this._purchaseMapper.ConvertBack(target.Purchase);
      mtgObject1.Location = this._locationMapper.ConvertBack(target.Location);
      mtgObject1.TriggerZones = target.TriggerZones != null ? target.TriggerZones.Select<Izi.Travel.Client.Entities.TriggerZone, Izi.Travel.Business.Entities.Data.TriggerZone>((Func<Izi.Travel.Client.Entities.TriggerZone, Izi.Travel.Business.Entities.Data.TriggerZone>) (x => this._triggerZoneMapper.ConvertBack(x))).ToArray<Izi.Travel.Business.Entities.Data.TriggerZone>() : (Izi.Travel.Business.Entities.Data.TriggerZone[]) null;
      mtgObject1.Rating = this._ratingMapper.ConvertBack(target.Rating);
      mtgObject1.Hidden = target.Hidden;
      mtgObject1.ChildrenCount = target.ChildrenCount;
      mtgObject1.Hash = target.Hash;
      mtgObject1.AccessType = MtgObjectAccessType.Online;
      MtgObject mtgObject2 = mtgObject1;
      if (!string.IsNullOrWhiteSpace(target.Route))
        mtgObject2.Map = new MapInfo()
        {
          Route = GeoLocationHelper.Parse(target.Route)
        };
      return mtgObject2;
    }
  }
}
