// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.Base.MtgObjectListQueryBase
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

#nullable disable
namespace Izi.Travel.Client.Queries.Base
{
  public abstract class MtgObjectListQueryBase : MtgObjectQueryBase
  {
    public int? Limit { get; set; }

    public int? Offset { get; set; }
  }
}
