// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.IPlatformInfoProvider
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System;

#nullable disable
namespace GoogleAnalytics.Core
{
  public interface IPlatformInfoProvider
  {
    string AnonymousClientId { get; set; }

    void OnTracking();

    int? ScreenColorDepthBits { get; }

    Dimensions ScreenResolution { get; }

    string UserLanguage { get; }

    Dimensions ViewPortResolution { get; }

    string GetUserAgent();

    event EventHandler ViewPortResolutionChanged;

    event EventHandler ScreenResolutionChanged;
  }
}
