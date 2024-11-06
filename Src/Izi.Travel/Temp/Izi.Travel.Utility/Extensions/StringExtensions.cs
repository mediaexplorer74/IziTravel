// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Extensions.StringExtensions
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

#nullable disable
namespace Izi.Travel.Utility.Extensions
{
  public static class StringExtensions
  {
    public static string Truncate(this string str, int length)
    {
      return string.IsNullOrEmpty(str) || str.Length <= length ? str : str.Substring(0, length) + "...";
    }
  }
}
