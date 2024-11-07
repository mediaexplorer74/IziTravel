// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.Profiler
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

using Caliburn.Micro;
using System;
using System.Diagnostics;

#nullable disable
namespace Izi.Travel.Utility
{
  public class Profiler : IDisposable
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (Profiler));
    private readonly Stopwatch _stopwatch;

    public string Text { get; set; }

    public Profiler(string text = null)
    {
      this._stopwatch = new Stopwatch();
      this._stopwatch.Start();
      this.Text = text;
    }

    public void Dispose()
    {
      this._stopwatch.Stop();
      Profiler.Logger.Info((this._stopwatch.ElapsedMilliseconds.ToString() + " " + this.Text).Trim());
    }
  }
}
