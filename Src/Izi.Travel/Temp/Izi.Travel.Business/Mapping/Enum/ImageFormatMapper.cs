// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.ImageFormatMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class ImageFormatMapper : MapperBase<Izi.Travel.Business.Entities.Media.ImageFormat, Izi.Travel.Client.Entities.ImageFormat>
  {
    public override Izi.Travel.Client.Entities.ImageFormat Convert(Izi.Travel.Business.Entities.Media.ImageFormat source)
    {
      switch (source)
      {
        case Izi.Travel.Business.Entities.Media.ImageFormat.Low120X90:
          return Izi.Travel.Client.Entities.ImageFormat.Low120X90;
        case Izi.Travel.Business.Entities.Media.ImageFormat.Low480X360:
          return Izi.Travel.Client.Entities.ImageFormat.Low480X360;
        case Izi.Travel.Business.Entities.Media.ImageFormat.High240X180:
          return Izi.Travel.Client.Entities.ImageFormat.High240X180;
        case Izi.Travel.Business.Entities.Media.ImageFormat.High800X600:
          return Izi.Travel.Client.Entities.ImageFormat.High800X600;
        default:
          return Izi.Travel.Client.Entities.ImageFormat.Undefined;
      }
    }

    public override Izi.Travel.Business.Entities.Media.ImageFormat ConvertBack(Izi.Travel.Client.Entities.ImageFormat target)
    {
      switch (target)
      {
        case Izi.Travel.Client.Entities.ImageFormat.Low120X90:
          return Izi.Travel.Business.Entities.Media.ImageFormat.Low120X90;
        case Izi.Travel.Client.Entities.ImageFormat.Low480X360:
          return Izi.Travel.Business.Entities.Media.ImageFormat.Low480X360;
        case Izi.Travel.Client.Entities.ImageFormat.High240X180:
          return Izi.Travel.Business.Entities.Media.ImageFormat.High240X180;
        case Izi.Travel.Client.Entities.ImageFormat.High800X600:
          return Izi.Travel.Business.Entities.Media.ImageFormat.High800X600;
        default:
          return Izi.Travel.Business.Entities.Media.ImageFormat.Undefined;
      }
    }
  }
}
