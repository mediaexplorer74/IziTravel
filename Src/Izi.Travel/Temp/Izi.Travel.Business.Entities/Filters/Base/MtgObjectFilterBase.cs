// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Filters.Base.MtgObjectFilterBase
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Data;

#nullable disable
namespace Izi.Travel.Business.Entities.Filters.Base
{
  public abstract class MtgObjectFilterBase
  {
    public string[] Languages { get; set; }

    public ContentSection Includes { get; set; }

    public ContentSection Excludes { get; set; }

    public MtgObjectForm Form { get; set; }

    public bool IncludeChildrenCountInFullForm { get; set; }

    public bool IncludeAudioDuration { get; set; }

    protected MtgObjectFilterBase() => this.Includes = ContentSection.Children;

    public ContentSection GetContentIntersection()
    {
      ContentSection contentSection = this.Includes ^ this.Includes & this.Excludes;
      return contentSection <= (ContentSection) 0 ? ContentSection.None : contentSection;
    }
  }
}
