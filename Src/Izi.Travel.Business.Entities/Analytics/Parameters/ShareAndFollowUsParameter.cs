// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.ShareAndFollowUsParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class ShareAndFollowUsParameter : BaseParameter
  {
    public static readonly ShareAndFollowUsParameter Empty = new ShareAndFollowUsParameter(string.Empty);
    public static readonly ShareAndFollowUsParameter ShareWithFriend = new ShareAndFollowUsParameter("Share With Friend");
    public static readonly ShareAndFollowUsParameter AppStore = new ShareAndFollowUsParameter("App Store");
    public static readonly ShareAndFollowUsParameter Feedback = new ShareAndFollowUsParameter(nameof (Feedback));
    public static readonly ShareAndFollowUsParameter Facebook = new ShareAndFollowUsParameter(nameof (Facebook));
    public static readonly ShareAndFollowUsParameter Twitter = new ShareAndFollowUsParameter(nameof (Twitter));
    public static readonly ShareAndFollowUsParameter Instagram = new ShareAndFollowUsParameter(nameof (Instagram));
    public static readonly ShareAndFollowUsParameter Foursquare = new ShareAndFollowUsParameter(nameof (Foursquare));
    public static readonly ShareAndFollowUsParameter Vkontakte = new ShareAndFollowUsParameter(nameof (Vkontakte));
    public static readonly ShareAndFollowUsParameter Website = new ShareAndFollowUsParameter(nameof (Website));

    private ShareAndFollowUsParameter(string value)
      : base(-1, value)
    {
    }
  }
}
