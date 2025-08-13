// ********************************************************************
// Type: Izi.Travel.Mtg.Controls.BarcodeScannerEventArgs
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using System;

#nullable disable
namespace Izi.Travel.Mtg.Controls
{
  public class BarcodeScannerEventArgs : EventArgs
  {
    public string Data { get; private set; }

    public BarcodeScannerEventArgs(string data) => this.Data = data;
  }
}
