// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Extensions.MtgObjectExtensions
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Microsoft.Phone.Tasks;
using System.Device.Location;

#nullable disable
namespace Izi.Travel.Business.Extensions
{
  public static class MtgObjectExtensions
  {
    public static bool IsParentType(this MtgObject mtgObject)
    {
      if (mtgObject == null)
        return false;
      return mtgObject.Type == MtgObjectType.Museum || mtgObject.Type == MtgObjectType.Collection || mtgObject.Type == MtgObjectType.Tour;
    }

    public static bool IsMuseumOrCollection(this MtgObject mtgObject)
    {
      if (mtgObject == null)
        return false;
      return mtgObject.Type == MtgObjectType.Museum || mtgObject.Type == MtgObjectType.Collection;
    }

    public static bool IsCollectionOrExhibit(this MtgObject mtgObject)
    {
      if (mtgObject == null)
        return false;
      return mtgObject.Type == MtgObjectType.Collection || mtgObject.Type == MtgObjectType.Exhibit;
    }

    public static void ShowMapDirectionsTask(this MtgObject mtgObject)
    {
      if (mtgObject == null || mtgObject.MainContent == null || mtgObject.MainContent.Title == null || mtgObject.Location == null)
        return;
      GeoCoordinate geoCoordinate = mtgObject.Location.ToGeoCoordinate();
      if (geoCoordinate == (GeoCoordinate) null)
        return;
      if (geoCoordinate.IsUnknown)
        return;
      try
      {
        new MapsDirectionsTask()
        {
          End = new LabeledMapLocation(mtgObject.MainContent.Title, geoCoordinate)
        }.Show();
      }
      catch
      {
      }
    }

    public static string GetImageUid(this MtgObject mtgObject)
    {
      return mtgObject == null || mtgObject.MainContent == null || mtgObject.MainContent.Images == null || mtgObject.MainContent.Images.Length == 0 ? (string) null : mtgObject.MainContent.Images[0].Uid;
    }

    public static string GetContentProviderUid(this MtgObject mtgObject)
    {
      return mtgObject == null || mtgObject.ContentProvider == null ? (string) null : mtgObject.ContentProvider.Uid;
    }
  }
}
