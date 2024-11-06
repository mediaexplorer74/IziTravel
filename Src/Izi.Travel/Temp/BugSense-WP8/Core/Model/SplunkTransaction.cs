// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.SplunkTransaction
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using System.Diagnostics;

#nullable disable
namespace BugSense.Core.Model
{
  internal class SplunkTransaction
  {
    private Stopwatch TimerStopWatch { get; set; }

    public double Elapsed
    {
      get
      {
        this.TimerStopWatch.Stop();
        return (double) this.TimerStopWatch.ElapsedMilliseconds;
      }
    }

    public string TransactionId { get; set; }

    public TrStart TransactionStart { get; set; }

    public TrStop TransactionStop { get; set; }

    public SplunkTransaction()
    {
      this.TimerStopWatch = new Stopwatch();
      this.TimerStopWatch.Start();
    }
  }
}
