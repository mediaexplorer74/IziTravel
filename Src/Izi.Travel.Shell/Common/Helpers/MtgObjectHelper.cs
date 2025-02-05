// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Helpers.MtgObjectHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Resources;
using System.Text;

#nullable disable
namespace Izi.Travel.Shell.Common.Helpers
{
  public static class MtgObjectHelper
  {
    public static string GetTypeName(MtgObjectType type)
    {
      switch (type)
      {
        case MtgObjectType.Museum:
          return AppResources.LabelMuseum;
        case MtgObjectType.Exhibit:
          return AppResources.LabelExhibit;
        case MtgObjectType.Tour:
          return AppResources.LabelTour;
        case MtgObjectType.TouristAttraction:
          return AppResources.LabelTouristAttraction;
        case MtgObjectType.Collection:
          return AppResources.LabelCollection;
        default:
          return (string) null;
      }
    }

    public static string GetMapImageUrl(MtgObjectType type, bool selected)
    {
      StringBuilder stringBuilder = new StringBuilder();
      switch (type)
      {
        case MtgObjectType.Museum:
        case MtgObjectType.Exhibit:
        case MtgObjectType.Collection:
          stringBuilder.Append("map.pin.museum");
          break;
        case MtgObjectType.Tour:
          stringBuilder.Append("map.pin.tour");
          break;
        case MtgObjectType.TouristAttraction:
          stringBuilder.Append("map.pin.ta");
          break;
      }
      if (stringBuilder.Length > 0)
      {
        if (selected)
          stringBuilder.Append(".selected");
        stringBuilder.Append(".png");
        stringBuilder.Insert(0, "/Assets/Icons/");
      }
      return stringBuilder.ToString();
    }
  }
}
