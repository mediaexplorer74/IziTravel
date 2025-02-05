// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Converters.ExploreItemStatusToForegroundConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.ViewModels.Explore;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Converters
{
  public class ExploreItemStatusToForegroundConverter : IValueConverter
  {
    public Brush NormalBrush { get; set; }

    public Brush HighlightBrush { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      if (value is ExploreItemStatus exploreItemStatus && exploreItemStatus.ExploreItemViewModel != null)
      {
        ExploreItemViewModel exploreItemViewModel = exploreItemStatus.ExploreItemViewModel;
        if (exploreItemViewModel.IsDownloaded || exploreItemViewModel.Price != null)
          return (object) this.HighlightBrush;
      }
      return (object) this.NormalBrush;
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
