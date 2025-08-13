// ********************************************************************
// Type: Izi.Travel.Mtg.Converters.BoolToResultTextConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Resources;
using System;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Mtg.Converters
{
  public class BoolToResultTextConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return !(value is bool flag) || !flag ? (object) AppResources.LabelIncorrect.ToUpper() : (object) AppResources.LabelCorrect.ToUpper();
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

