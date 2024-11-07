// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Events.Content.PlayEvent
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Events.Content
{
  public sealed class PlayEvent : BaseContentEvent
  {
    public override EventAction Action => EventAction.Play;

    public ContentTypeParameter MediaContentType { get; set; }

    public ActivationTypeParameter MediaActivationType { get; set; }

    public CompletionReasonParameter MediaCompletionReason { get; set; }

    public PlayEvent(EventContentType contentType)
      : base(contentType)
    {
    }

    public override IEnumerable<BaseParameter> GetParameters()
    {
      return (IEnumerable<BaseParameter>) new BaseParameter[8]
      {
        (BaseParameter) this.Title,
        (BaseParameter) this.Uid,
        (BaseParameter) this.Language,
        (BaseParameter) this.AccessType,
        (BaseParameter) this.Rental,
        (BaseParameter) this.MediaContentType,
        (BaseParameter) this.MediaActivationType,
        (BaseParameter) this.MediaCompletionReason
      };
    }

    public override string GetCustomLabel()
    {
      return string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", this.Uid != null ? (object) this.Uid.Value : (object) string.Empty, this.Language != null ? (object) this.Language.Value : (object) string.Empty, this.AccessType != null ? (object) this.AccessType.Value : (object) string.Empty, this.Rental != null ? (object) this.Rental.Value : (object) string.Empty, this.MediaContentType != null ? (object) this.MediaContentType.Value : (object) string.Empty, this.MediaActivationType != null ? (object) this.MediaActivationType.Value : (object) string.Empty, this.MediaCompletionReason != null ? (object) this.MediaCompletionReason.Value : (object) string.Empty);
    }
  }
}
