// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Converters.ExploreItemStatusToStringConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.ViewModels.Explore;
using System;
using System.Globalization;
using System.Windows.Data;

#nullable disable
namespace Izi.Travel.Shell.Converters
{
  public class ExploreItemStatusToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ExploreItemStatus exploreItemStatus && exploreItemStatus.ExploreItemViewModel != null)
      {
        ExploreItemViewModel exploreItemViewModel = exploreItemStatus.ExploreItemViewModel;
        if (exploreItemViewModel.IsDownloaded)
          return (object) AppResources.LabelDownloaded;
        if (!string.IsNullOrWhiteSpace(exploreItemViewModel.Price))
          return !exploreItemViewModel.IsPurchased ? (object) exploreItemViewModel.Price : (object) AppResources.LabelPurchased;
      }
      return (object) AppResources.LabelFree;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
