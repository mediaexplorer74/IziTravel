// Decompiled with JetBrains decompiler
// Type: BugSense.Helpers.MD5ChangingEventArgs
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;

#nullable disable
namespace BugSense.Helpers
{
  internal class MD5ChangingEventArgs : EventArgs
  {
    public readonly byte[] NewData;

    public MD5ChangingEventArgs(byte[] data)
    {
      byte[] numArray = new byte[data.Length];
      for (int index = 0; index < data.Length; ++index)
        numArray[index] = data[index];
    }

    public MD5ChangingEventArgs(string data)
    {
      byte[] numArray = new byte[data.Length];
      for (int index = 0; index < data.Length; ++index)
        numArray[index] = (byte) data[index];
    }
  }
}
