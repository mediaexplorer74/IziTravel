// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Data.Content
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Data
{
  public class Content
  {
    public string Language { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public Playback Playback { get; set; }

    public Media[] Images { get; set; }

    public Media[] Audio { get; set; }

    public Media[] Video { get; set; }

    public MtgObject[] Children { get; set; }

    public MtgObject[] Collections { get; set; }

    public MtgObject[] References { get; set; }

    public Quiz Quiz { get; set; }

    public string News { get; set; }

    public int ChildrenCount { get; set; }

    public int AudioDuration { get; set; }
  }
}
