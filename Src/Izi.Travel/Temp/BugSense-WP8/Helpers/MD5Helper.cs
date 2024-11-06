// Decompiled with JetBrains decompiler
// Type: BugSense.Helpers.MD5Helper
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

#nullable disable
namespace BugSense.Helpers
{
  internal sealed class MD5Helper
  {
    private MD5Helper()
    {
    }

    public static uint RotateLeft(uint uiNumber, ushort shift)
    {
      return uiNumber >> 32 - (int) shift | uiNumber << (int) shift;
    }

    public static uint ReverseByte(uint uiNumber)
    {
      return (uint) (((int) uiNumber & (int) byte.MaxValue) << 24 | (int) (uiNumber >> 24) | (int) ((uiNumber & 16711680U) >> 8) | ((int) uiNumber & 65280) << 8);
    }
  }
}
