// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.Base.MtgObjectQueryBase
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;

#nullable disable
namespace Izi.Travel.Client.Queries.Base
{
  public abstract class MtgObjectQueryBase
  {
    public string[] Languages { get; set; }

    public ContentSection Includes { get; set; }

    public ContentSection Excludes { get; set; }

    public bool IncludeChildrenCount { get; set; }

    public bool IncludeAudioDuration { get; set; }

    protected MtgObjectQueryBase() => this.Includes = ContentSection.Children;
  }
}
