// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.EventAction
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics
{
  public sealed class EventAction
  {
    public static readonly EventAction Empty = new EventAction(string.Empty);
    public static readonly EventAction Open = new EventAction(nameof (Open));
    public static readonly EventAction Play = new EventAction(nameof (Play));
    public static readonly EventAction Share = new EventAction(nameof (Share));
    public static readonly EventAction Review = new EventAction(nameof (Review));
    public static readonly EventAction ShareAndFollowUs = new EventAction("Share & Follow Us");
    public static readonly EventAction Search = new EventAction(nameof (Search));
    private readonly string _value;

    public string Value => this._value;

    private EventAction(string value) => this._value = value;
  }
}
