// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Client.Converters.JsonEnumConverterBase`1
// Assembly: Izi.Travel.Client.WindowsPhone, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: C73077B8-157D-49B0-834E-CAFCC993728A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Client.WindowsPhone.dll

using Newtonsoft.Json;
using System;

#nullable disable
namespace Izi.Travel.Client.Converters
{
  public abstract class JsonEnumConverterBase<T> : JsonConverter where T : struct
  {
    public override sealed void WriteJson(
      JsonWriter writer,
      object value,
      JsonSerializer serializer)
    {
      writer.WriteValue(this.GetStringValue((T) value));
    }

    public override sealed object ReadJson(
      JsonReader reader,
      Type objectType,
      object existingValue,
      JsonSerializer serializer)
    {
      return (object) this.GetEnumValue((string) reader.Value);
    }

    public override sealed bool CanConvert(Type objectType) => objectType == typeof (string);

    protected virtual T GetEnumValue(string stringValue)
    {
      T result;
      return !Enum.TryParse<T>(stringValue, true, out result) ? default (T) : result;
    }

    protected virtual string GetStringValue(T enumValue) => enumValue.ToString().ToLower();
  }
}
