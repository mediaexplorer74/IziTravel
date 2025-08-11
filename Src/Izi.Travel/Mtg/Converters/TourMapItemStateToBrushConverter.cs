// ********************************************************************
// Type: Izi.Travel.Shell.Mtg.Converters.TourMapItemStateToBrushConverter
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Mtg.ViewModels.Tour.Map;
using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Converters
{
  public class TourMapItemStateToBrushConverter : IValueConverter
  {
    public Brush NormalBrush { get; set; }

    public Brush SelectedBrush { get; set; }

    public Brush VisitedBrush { get; set; }

    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value is TourMapItemState tourMapItemState)
      {
        if (tourMapItemState == TourMapItemState.Selected)
          return (object) this.SelectedBrush;
        if (tourMapItemState == TourMapItemState.Visited)
          return (object) this.VisitedBrush;
      }
      return (object) this.NormalBrush;
    }

    public object ConvertBack(
      object value,
      Type targetType,
      object parameter,
      string language)
    {
      throw new NotImplementedException();
    }
  }
}

