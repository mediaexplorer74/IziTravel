// Decompiled with JetBrains decompiler
// Type: BugSense.Helpers.MD5ChangedEventArgs
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System;

#nullable disable
namespace BugSense.Helpers
{
  internal class MD5ChangedEventArgs : EventArgs
  {
    public readonly byte[] NewData;
    public readonly string FingerPrint;

    public MD5ChangedEventArgs(byte[] data, string HashedValue)
    {
      byte[] numArray = new byte[data.Length];
      for (int index = 0; index < data.Length; ++index)
        numArray[index] = data[index];
      this.FingerPrint = HashedValue;
    }

    public MD5ChangedEventArgs(string data, string HashedValue)
    {
      byte[] numArray = new byte[data.Length];
      for (int index = 0; index < data.Length; ++index)
        numArray[index] = (byte) data[index];
      this.FingerPrint = HashedValue;
    }
  }
}
