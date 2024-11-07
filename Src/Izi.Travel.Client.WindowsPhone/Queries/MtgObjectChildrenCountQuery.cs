// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Queries.MtgObjectChildrenCountQuery
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;

#nullable disable
namespace Izi.Travel.Client.Queries
{
  public class MtgObjectChildrenCountQuery
  {
    public string Uid { get; set; }

    public string[] Languages { get; set; }

    public MtgObjectType[] Types { get; set; }
  }
}
