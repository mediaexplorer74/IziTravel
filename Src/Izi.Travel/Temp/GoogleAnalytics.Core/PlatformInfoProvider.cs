// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.PlatformInfoProvider
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class PlatformInfoProvider : IPlatformInfoProvider
  {
    private Dimensions viewPortResolution;
    private Dimensions screenResolution;

    public event EventHandler ViewPortResolutionChanged;

    public event EventHandler ScreenResolutionChanged;

    public string AnonymousClientId { get; set; }

    public void OnTracking()
    {
    }

    public int? ScreenColorDepthBits { get; set; }

    public string UserLanguage { get; set; }

    public Dimensions ScreenResolution
    {
      get => this.screenResolution;
      set
      {
        this.screenResolution = value;
        if (this.ScreenResolutionChanged == null)
          return;
        this.ScreenResolutionChanged((object) this, EventArgs.Empty);
      }
    }

    public Dimensions ViewPortResolution
    {
      get => this.viewPortResolution;
      set
      {
        this.viewPortResolution = value;
        if (this.ViewPortResolutionChanged == null)
          return;
        this.ViewPortResolutionChanged((object) this, EventArgs.Empty);
      }
    }

    string IPlatformInfoProvider.GetUserAgent() => this.UserAgent;

    public string UserAgent { get; set; }
  }
}
