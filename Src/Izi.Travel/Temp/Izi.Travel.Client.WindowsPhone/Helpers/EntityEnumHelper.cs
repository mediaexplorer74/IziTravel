// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Helpers.EntityEnumHelper
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Izi.Travel.Client.Helpers
{
  internal static class EntityEnumHelper
  {
    internal static MtgObjectType ParseMtgObjectType(string value)
    {
      switch (value)
      {
        case "city":
          return MtgObjectType.City;
        case "collection":
          return MtgObjectType.Collection;
        case "country":
          return MtgObjectType.Country;
        case "exhibit":
          return MtgObjectType.Exhibit;
        case "museum":
          return MtgObjectType.Museum;
        case "publisher":
          return MtgObjectType.Publisher;
        case "story_navigation":
          return MtgObjectType.NavigationStory;
        case "tour":
          return MtgObjectType.Tour;
        case "tourist_attraction":
          return MtgObjectType.TouristAttraction;
        default:
          return MtgObjectType.Unknown;
      }
    }

    internal static string ConvertMtgObjectType(MtgObjectType value)
    {
      switch (value)
      {
        case MtgObjectType.Museum:
          return "museum";
        case MtgObjectType.Collection:
          return "collection";
        case MtgObjectType.Exhibit:
          return "exhibit";
        case MtgObjectType.Tour:
          return "tour";
        case MtgObjectType.TouristAttraction:
          return "tourist_attraction";
        case MtgObjectType.NavigationStory:
          return "story_navigation";
        case MtgObjectType.City:
          return "city";
        case MtgObjectType.Country:
          return "country";
        case MtgObjectType.Publisher:
          return "publisher";
        default:
          return (string) null;
      }
    }

    internal static MediaType ParseMediaType(string value)
    {
      switch (value)
      {
        case "brand_cover":
          return MediaType.BrandCover;
        case "brand_logo":
          return MediaType.BrandLogo;
        case "featured":
          return MediaType.Featured;
        case "map":
          return MediaType.Map;
        case "sponsor_logo":
          return MediaType.SponsorLogo;
        case "story":
          return MediaType.Story;
        case "youtube":
          return MediaType.YouTube;
        default:
          return MediaType.Unknown;
      }
    }

    internal static string ConvertMediaType(MediaType value)
    {
      switch (value)
      {
        case MediaType.Story:
          return "story";
        case MediaType.Map:
          return "map";
        case MediaType.BrandLogo:
          return "brand_logo";
        case MediaType.BrandCover:
          return "brand_cover";
        case MediaType.SponsorLogo:
          return "sponsor_logo";
        case MediaType.Featured:
          return "featured";
        case MediaType.YouTube:
          return "youtube";
        default:
          return (string) null;
      }
    }

    internal static string[] ConvertContentSection(ContentSection value)
    {
      if (value == ContentSection.None)
        return new string[1]{ "none" };
      if (value != ContentSection.All)
        return value.GetFlags().Select<Enum, string>((Func<Enum, string>) (flag => flag.ToString().ToLower())).ToArray<string>();
      return new string[1]{ "all" };
    }

    public static IEnumerable<Enum> GetFlags(this Enum input)
    {
      return Enum.GetValues(input.GetType()).Cast<Enum>().Where<Enum>(new Func<Enum, bool>(input.HasFlag));
    }
  }
}
