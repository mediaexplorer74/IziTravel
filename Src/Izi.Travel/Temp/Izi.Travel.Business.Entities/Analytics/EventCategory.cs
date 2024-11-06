// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.EventCategory
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics
{
  public sealed class EventCategory
  {
    public static readonly EventCategory Directory = new EventCategory("IZIDirectory");
    public static readonly EventCategory Cms = new EventCategory("IZICMS");
    public static readonly EventCategory Application = new EventCategory("IZIApp");
    private readonly string _value;

    public string Value => this._value;

    private EventCategory(string value) => this._value = value;
  }
}
