// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.PlatformInfoProvider
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using GoogleAnalytics.Core;
using Microsoft.Phone.Info;
using System;
using System.Globalization;
using System.IO.IsolatedStorage;
using System.Windows;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class PlatformInfoProvider : IPlatformInfoProvider
  {
    private const string Key_AnonymousClientId = "GoogleAnaltyics.AnonymousClientId";
    private string anonymousClientId;

    public event EventHandler ViewPortResolutionChanged;

    public event EventHandler ScreenResolutionChanged;

    public string AnonymousClientId
    {
      get
      {
        if (this.anonymousClientId == null)
        {
          IsolatedStorageSettings applicationSettings = IsolatedStorageSettings.ApplicationSettings;
          if (!applicationSettings.Contains("GoogleAnaltyics.AnonymousClientId"))
          {
            this.anonymousClientId = Guid.NewGuid().ToString();
            applicationSettings.Add("GoogleAnaltyics.AnonymousClientId", (object) this.anonymousClientId);
            applicationSettings.Save();
          }
          else
            this.anonymousClientId = (string) applicationSettings["GoogleAnaltyics.AnonymousClientId"];
        }
        return this.anonymousClientId;
      }
      set => this.anonymousClientId = value;
    }

    public Dimensions ScreenResolution
    {
      get
      {
        double num = (double) Application.Current.Host.Content.ScaleFactor / 100.0;
        return new Dimensions((int) Math.Ceiling(Application.Current.Host.Content.ActualHeight * num), (int) Math.Ceiling(Application.Current.Host.Content.ActualWidth * num));
      }
    }

    public Dimensions ViewPortResolution => this.ScreenResolution;

    public string UserLanguage => CultureInfo.CurrentUICulture.Name;

    public int? ScreenColorDepthBits => new int?();

    public void OnTracking()
    {
    }

    public string GetUserAgent()
    {
      CanonicalPhoneName canonicalPhoneName = PhoneNameResolver.Resolve(DeviceStatus.DeviceManufacturer, DeviceStatus.DeviceName);
      return string.Format("Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone OS {0}; Trident/6.0; IEMobile/10.0; ARM; Touch; {1}; {2})", (object) Environment.OSVersion.Version, (object) canonicalPhoneName.CanonicalManufacturer, (object) canonicalPhoneName.CanonicalModel);
    }
  }
}
