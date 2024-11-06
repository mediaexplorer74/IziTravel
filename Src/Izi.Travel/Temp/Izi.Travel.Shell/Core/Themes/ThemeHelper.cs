// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Themes.ThemeHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Core.Themes
{
  public static class ThemeHelper
  {
    private static readonly ResourceDictionary Resources = Application.Current.Resources;

    public static void OverrideSystemColors()
    {
      ThemeHelper.OverrideColor("PhoneBackgroundColor", "IziTravelLightGrayColor");
      ThemeHelper.OverrideSolidColorBrush("PhoneChromeBrush", "IziTravelBlueBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneAccentBrush", "IziTravelBlueBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneForegroundBrush", "IziTravelDarkBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneBackgroundBrush", "IziTravelLightGrayBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneContrastBackgroundBrush", "IziTravelDarkBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneContrastForegroundBrush", "IziTravelLightGrayBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneDisabledBrush", "IziTravelGrayBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneTextBoxBrush", "IziTravelLightBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneTextBoxEditBorderBrush", "IziTravelDarkBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneTextBoxEditBackgroundBrush", "IziTravelLightBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneRadioCheckBoxBrush", "IziTraveDarkBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneRadioCheckBoxCheckBrush", "IziTravelDarkBrush");
      ThemeHelper.OverrideSolidColorBrush("PhoneRadioCheckBoxPressedBrush", "IziTravelBlueBrush");
    }

    public static Color GetThemeColor(string name)
    {
      return !ThemeHelper.Resources.Contains((object) name) || !(ThemeHelper.Resources[(object) name] is Color) ? Colors.Transparent : (Color) ThemeHelper.Resources[(object) name];
    }

    public static Color GetThemeColor(string name, byte alpha)
    {
      return ThemeHelper.GetThemeColor(name) with
      {
        A = alpha
      };
    }

    private static void OverrideColor(string sourceColorName, string targetColorName)
    {
      if (!ThemeHelper.Resources.Contains((object) sourceColorName) || !ThemeHelper.Resources.Contains((object) targetColorName))
        return;
      Color resource = (Color) ThemeHelper.Resources[(object) targetColorName];
      ThemeHelper.Resources.Remove(sourceColorName);
      ThemeHelper.Resources.Add(sourceColorName, (object) resource);
    }

    private static void OverrideSolidColorBrush(string sourceBrushName, string targetBrushName)
    {
      if (!ThemeHelper.Resources.Contains((object) sourceBrushName) || !ThemeHelper.Resources.Contains((object) targetBrushName))
        return;
      SolidColorBrush resource1 = ThemeHelper.Resources[(object) sourceBrushName] as SolidColorBrush;
      SolidColorBrush resource2 = ThemeHelper.Resources[(object) targetBrushName] as SolidColorBrush;
      if (resource1 == null || resource2 == null)
        return;
      resource1.Color = resource2.Color;
    }
  }
}
