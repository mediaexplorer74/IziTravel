// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Converters.JsonMediaTypeEnumConverter
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Izi.Travel.Client.Entities;
using Izi.Travel.Client.Helpers;

#nullable disable
namespace Izi.Travel.Client.Converters
{
  public class JsonMediaTypeEnumConverter : JsonEnumConverterBase<MediaType>
  {
    protected override MediaType GetEnumValue(string stringValue)
    {
      return EntityEnumHelper.ParseMediaType(stringValue);
    }

    protected override string GetStringValue(MediaType enumValue)
    {
      return EntityEnumHelper.ConvertMediaType(enumValue);
    }
  }
}
