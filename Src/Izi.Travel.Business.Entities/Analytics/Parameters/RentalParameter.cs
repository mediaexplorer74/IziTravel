// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.RentalParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class RentalParameter : BaseParameter
  {
    private const int ParamIndex = 5;
    public static readonly RentalParameter Rental = new RentalParameter("rental");
    public static readonly RentalParameter NonRental = new RentalParameter("nonrental");

    private RentalParameter(string value)
      : base(5, value)
    {
    }
  }
}
