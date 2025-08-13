// ********************************************************************
// Type: Izi.Travel.Mtg.Converters.MtgObjectTypeToStringConverter
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.Helpers;
using System;
using Windows.UI.Xaml.Data;
#nullable disable
namespace Izi.Travel.Mtg.Converters
{
  public class MtgObjectTypeToStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      return !(value is MtgObjectType type) ? (object) null : (object) MtgObjectHelper.GetTypeName(type);
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

