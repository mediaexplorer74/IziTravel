// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.ContentTypeParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class ContentTypeParameter : BaseParameter
  {
    private const int ParamIndex = 6;
    public static readonly ContentTypeParameter Audio = new ContentTypeParameter(nameof (Audio));
    public static readonly ContentTypeParameter Video = new ContentTypeParameter(nameof (Video));

    private ContentTypeParameter(string value)
      : base(6, value)
    {
    }
  }
}
