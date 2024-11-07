// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.EventContentType
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics
{
  public sealed class EventContentType
  {
    public static readonly EventContentType Empty = new EventContentType(string.Empty);
    public static readonly EventContentType Museum = new EventContentType("museum");
    public static readonly EventContentType Exhibit = new EventContentType("exhibit");
    public static readonly EventContentType Collection = new EventContentType("collection");
    public static readonly EventContentType Tour = new EventContentType("tour");
    public static readonly EventContentType TouristAttraction = new EventContentType("tourist_attraction");
    public static readonly EventContentType NavigationStory = new EventContentType("nav_story");
    private readonly string _value;

    public string Value => this._value;

    private EventContentType(string value) => this._value = value;
  }
}
