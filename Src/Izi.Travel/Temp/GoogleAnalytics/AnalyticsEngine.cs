// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.AnalyticsEngine
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using GoogleAnalytics.Core;
using System.IO.IsolatedStorage;
using System.Windows;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class AnalyticsEngine
  {
    private const string Key_AppOptOut = "GoogleAnaltyics.AppOptOut";
    private TrackerManager manager;
    private static AnalyticsEngine current;
    private bool isAppOptOutSet;

    public static AnalyticsEngine Current
    {
      get
      {
        if (AnalyticsEngine.current == null)
          AnalyticsEngine.current = new AnalyticsEngine();
        return AnalyticsEngine.current;
      }
    }

    private AnalyticsEngine()
    {
      this.manager = new TrackerManager((IPlatformInfoProvider) new GoogleAnalytics.PlatformInfoProvider());
    }

    public bool AppOptOut
    {
      get
      {
        if (!this.isAppOptOutSet)
          this.LoadAppOptOut();
        return this.manager.AppOptOut;
      }
      set
      {
        this.manager.AppOptOut = value;
        this.isAppOptOutSet = true;
        IsolatedStorageSettings.ApplicationSettings["GoogleAnaltyics.AppOptOut"] = (object) value;
        IsolatedStorageSettings.ApplicationSettings.Save();
        if (!value)
          return;
        GAServiceManager.Current.Clear();
      }
    }

    private void LoadAppOptOut()
    {
      this.manager.AppOptOut = IsolatedStorageSettings.ApplicationSettings.Contains("GoogleAnaltyics.AppOptOut") && (bool) IsolatedStorageSettings.ApplicationSettings["GoogleAnaltyics.AppOptOut"];
      this.isAppOptOutSet = true;
    }

    public bool RequestAppOptOutAsync()
    {
      bool flag = MessageBox.Show("Allow anonomous information to be collected to help improve this application?", "Help Improve User Experience", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel;
      this.AppOptOut = flag;
      return flag;
    }

    public bool IsDebugEnabled
    {
      get => this.manager.IsDebugEnabled;
      set => this.manager.IsDebugEnabled = value;
    }

    public Tracker GetTracker(string propertyId) => this.manager.GetTracker(propertyId);

    public void CloseTracker(Tracker tracker) => this.manager.CloseTracker(tracker);

    public Tracker DefaultTracker
    {
      get => this.manager.DefaultTracker;
      set => this.manager.DefaultTracker = value;
    }

    public IPlatformInfoProvider PlatformInfoProvider => this.manager.PlatformTrackingInfo;
  }
}
