// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Entities.Analytics.Parameters.ActivationTypeParameter
// Assembly: Izi.Travel.Business.Entities, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: DDED5915-8B3A-4C03-AAF5-BE6B16E9CC4A
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.Entities.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Business.Entities.Analytics.Parameters
{
  public sealed class ActivationTypeParameter : BaseParameter
  {
    private const int ParamIndex = 7;
    public static readonly ActivationTypeParameter Manual = new ActivationTypeParameter(nameof (Manual));
    public static readonly ActivationTypeParameter Numpad = new ActivationTypeParameter(nameof (Numpad));
    public static readonly ActivationTypeParameter QrCode = new ActivationTypeParameter("QRCode");
    public static readonly ActivationTypeParameter Gps = new ActivationTypeParameter("GPS");
    public static readonly ActivationTypeParameter Sequential = new ActivationTypeParameter(nameof (Sequential));

    private ActivationTypeParameter(string value)
      : base(7, value)
    {
    }

    public static bool TryParse(string str, out ActivationTypeParameter value)
    {
      value = (ActivationTypeParameter) null;
      if (string.IsNullOrWhiteSpace(str))
        return false;
      string key = str.ToLower().Trim();
      Dictionary<string, ActivationTypeParameter> dictionary = new Dictionary<string, ActivationTypeParameter>()
      {
        {
          ActivationTypeParameter.Manual.Value.ToLower(),
          ActivationTypeParameter.Manual
        },
        {
          ActivationTypeParameter.Numpad.Value.ToLower(),
          ActivationTypeParameter.Numpad
        },
        {
          ActivationTypeParameter.QrCode.Value.ToLower(),
          ActivationTypeParameter.QrCode
        },
        {
          ActivationTypeParameter.Gps.Value.ToLower(),
          ActivationTypeParameter.Gps
        },
        {
          ActivationTypeParameter.Sequential.Value.ToLower(),
          ActivationTypeParameter.Sequential
        }
      };
      if (!dictionary.ContainsKey(key))
        return false;
      value = dictionary[key];
      return true;
    }
  }
}
