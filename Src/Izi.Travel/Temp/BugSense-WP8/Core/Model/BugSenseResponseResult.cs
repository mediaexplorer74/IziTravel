// Decompiled with JetBrains decompiler
// Type: BugSense.Core.Model.BugSenseResponseResult
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

#nullable disable
namespace BugSense.Core.Model
{
  public class BugSenseResponseResult : BugSenseResult
  {
    public long ErrorId { get; internal set; }

    public string ServerResponse { get; internal set; }

    public string Url { get; internal set; }

    public string ContentText { get; internal set; }

    public string TickerText { get; internal set; }

    public string ContentTitle { get; internal set; }

    public bool IsResolved
    {
      get
      {
        return !string.IsNullOrWhiteSpace(this.Url) && !string.IsNullOrWhiteSpace(this.ContentText) && !string.IsNullOrWhiteSpace(this.ContentTitle);
      }
    }
  }
}
