// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Events.Content.BaseContentEvent
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Events.Content
{
  public abstract class BaseContentEvent : BaseEvent
  {
    public EventContentType ContentType { get; private set; }

    public TitleParameter Title { get; set; }

    public UidParameter Uid { get; set; }

    public LanguageParameter Language { get; set; }

    public AccessTypeParameter AccessType { get; set; }

    public RentalParameter Rental { get; set; }

    protected BaseContentEvent(EventContentType contentType) => this.ContentType = contentType;

    public override IEnumerable<BaseParameter> GetParameters()
    {
      return (IEnumerable<BaseParameter>) new BaseParameter[5]
      {
        (BaseParameter) this.Title,
        (BaseParameter) this.Uid,
        (BaseParameter) this.Language,
        (BaseParameter) this.AccessType,
        (BaseParameter) this.Rental
      };
    }

    public virtual string GetCustomLabel()
    {
      return string.Format("{0}|{1}|{2}|{3}", this.Uid != null ? (object) this.Uid.Value : (object) string.Empty, this.Language != null ? (object) this.Language.Value : (object) string.Empty, this.AccessType != null ? (object) this.AccessType.Value : (object) string.Empty, this.Rental != null ? (object) this.Rental.Value : (object) string.Empty);
    }
  }
}
