// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Mapping.Enum.ImageExtensionMapper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

#nullable disable
namespace Izi.Travel.Business.Mapping.Enum
{
  internal class ImageExtensionMapper : MapperBase<Izi.Travel.Business.Entities.Media.ImageExtension, Izi.Travel.Client.Entities.ImageExtension>
  {
    public override Izi.Travel.Client.Entities.ImageExtension Convert(Izi.Travel.Business.Entities.Media.ImageExtension source)
    {
      return source == Izi.Travel.Business.Entities.Media.ImageExtension.Png ? Izi.Travel.Client.Entities.ImageExtension.Png : Izi.Travel.Client.Entities.ImageExtension.Jpg;
    }

    public override Izi.Travel.Business.Entities.Media.ImageExtension ConvertBack(
      Izi.Travel.Client.Entities.ImageExtension target)
    {
      return target == Izi.Travel.Client.Entities.ImageExtension.Png ? Izi.Travel.Business.Entities.Media.ImageExtension.Png : Izi.Travel.Business.Entities.Media.ImageExtension.Jpg;
    }
  }
}
