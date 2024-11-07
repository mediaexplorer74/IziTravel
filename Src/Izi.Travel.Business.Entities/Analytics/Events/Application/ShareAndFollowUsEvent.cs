// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Events.Application.ShareAndFollowUsEvent
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Analytics.Parameters;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Events.Application
{
  public sealed class ShareAndFollowUsEvent : BaseApplicationEvent
  {
    public override EventAction Action => EventAction.ShareAndFollowUs;

    private ShareAndFollowUsEvent(ShareAndFollowUsParameter parameter)
    {
      if (parameter == null)
        return;
      this.Label = parameter.Value;
    }

    public static ShareAndFollowUsEvent Create(ShareAndFollowUsParameter parameter)
    {
      return new ShareAndFollowUsEvent(parameter);
    }
  }
}
