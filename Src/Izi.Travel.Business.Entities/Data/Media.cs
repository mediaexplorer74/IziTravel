// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Media
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using Izi.Travel.Business.Entities.Media;

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Media : CoreObject
  {
    public MediaType Type { get; set; }

    public MediaFormat Format { get; set; }

    public int Order { get; set; }

    public int Duration { get; set; }

    public string Url { get; set; }

    public string Title { get; set; }

    public string Hash { get; set; }

    public int Size { get; set; }

    public object Tag { get; set; }
  }
}
