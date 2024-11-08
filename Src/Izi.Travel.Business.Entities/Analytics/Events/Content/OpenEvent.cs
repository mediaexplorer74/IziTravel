// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Events.Content.OpenEvent
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Events.Content
{
  public sealed class OpenEvent : BaseContentEvent
  {
    public override EventAction Action => EventAction.Open;

    public OpenEvent(EventContentType contentType)
      : base(contentType)
    {
    }
  }
}
