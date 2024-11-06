// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.EasyTracker
// Assembly: GoogleAnalytics, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: ABC239A9-7B01-4013-916D-8F4A2BC96BC0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.dll

using GoogleAnalytics.Core;
using Microsoft.Phone.Shell;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

#nullable disable
namespace GoogleAnalytics
{
  public sealed class EasyTracker
  {
    private static EasyTracker current;
    private static Tracker tracker;
    private DateTime? suspended;

    public Uri ConfigPath { get; set; }

    public EasyTrackerConfig Config { get; set; }

    public static EasyTracker Current
    {
      get
      {
        if (EasyTracker.current == null)
          EasyTracker.current = new EasyTracker();
        return EasyTracker.current;
      }
    }

    public static Tracker GetTracker()
    {
      if (EasyTracker.tracker == null)
      {
        Application ctx = (Application) null;
        try
        {
          ctx = Application.Current;
        }
        catch
        {
        }
        EasyTracker.Current.SetContext(ctx);
      }
      return EasyTracker.tracker;
    }

    private void InitTracker()
    {
      AnalyticsEngine current = AnalyticsEngine.Current;
      current.IsDebugEnabled = this.Config.Debug;
      GAServiceManager.Current.DispatchPeriod = this.Config.DispatchPeriod;
      EasyTracker.tracker = current.GetTracker(this.Config.TrackingId);
      EasyTracker.tracker.SetStartSession(this.Config.SessionTimeout.HasValue);
      EasyTracker.tracker.IsUseSecure = this.Config.UseSecure;
      EasyTracker.tracker.AppName = this.Config.AppName;
      EasyTracker.tracker.AppVersion = this.Config.AppVersion;
      EasyTracker.tracker.AppId = this.Config.AppId;
      EasyTracker.tracker.AppInstallerId = this.Config.AppInstallerId;
      EasyTracker.tracker.UserId = this.Config.UserId;
      EasyTracker.tracker.IsAnonymizeIpEnabled = this.Config.AnonymizeIp;
      EasyTracker.tracker.SampleRate = this.Config.SampleFrequency;
    }

    private void InitConfig(XmlReader reader)
    {
      this.Config = EasyTrackerConfig.Load(reader);
      this.Config.Validate();
    }

    public Task Dispatch() => GAServiceManager.Current.Dispatch();

    private EasyTracker() => this.ConfigPath = new Uri("analytics.xml", UriKind.Relative);

    private void InitConfig(Uri configPath)
    {
      using (Stream stream = Application.GetResourceStream(configPath).Stream)
      {
        using (XmlReader reader = XmlReader.Create(stream))
          this.InitConfig(reader);
      }
    }

    public void SetContext(Application ctx)
    {
      if (this.Config == null)
        this.InitConfig(this.ConfigPath);
      this.PopulateMissingConfig();
      if (this.Config.ReportUncaughtExceptions && ctx != null)
      {
        ctx.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.app_UnhandledException);
        TaskScheduler.UnobservedTaskException += new EventHandler<UnobservedTaskExceptionEventArgs>(this.TaskScheduler_UnobservedTaskException);
      }
      if (this.Config.AutoTrackNetworkConnectivity)
      {
        EasyTracker.UpdateConnectionStatus();
        NetworkChange.NetworkAddressChanged += new NetworkAddressChangedEventHandler(this.NetworkChange_NetworkAddressChanged);
      }
      this.InitTracker();
      if (!this.Config.AutoAppLifetimeMonitoring || ctx == null)
        return;
      PhoneApplicationService.Current.Activated += new EventHandler<ActivatedEventArgs>(this.Current_Activated);
      PhoneApplicationService.Current.Deactivated += new EventHandler<DeactivatedEventArgs>(this.Current_Deactivated);
    }

    private void PopulateMissingConfig()
    {
      if (string.IsNullOrEmpty(this.Config.AppName))
        this.Config.AppName = Helpers.GetAppAttribute("Title");
      if (!string.IsNullOrEmpty(this.Config.AppVersion))
        return;
      this.Config.AppVersion = Helpers.GetAppAttribute("Version");
    }

    private void Current_Activated(object sender, ActivatedEventArgs e)
    {
      if (this.suspended.HasValue)
      {
        TimeSpan? sessionTimeout = this.Config.SessionTimeout;
        if (sessionTimeout.HasValue)
        {
          TimeSpan timeSpan1 = DateTime.UtcNow.Subtract(this.suspended.Value);
          sessionTimeout = this.Config.SessionTimeout;
          TimeSpan timeSpan2 = sessionTimeout.Value;
          if (timeSpan1 > timeSpan2)
            EasyTracker.tracker.SetStartSession(true);
        }
      }
      if (!this.Config.AutoAppLifetimeTracking)
        return;
      EasyTracker.tracker.SendEvent("app", "resume", !e.IsApplicationInstancePreserved ? "tombstoned" : (string) null, 0L);
    }

    private async void Current_Deactivated(object sender, DeactivatedEventArgs e)
    {
      if (this.Config.AutoAppLifetimeTracking)
        EasyTracker.tracker.SendEvent("app", "suspend", e.Reason.ToString(), 0L);
      this.suspended = new DateTime?(DateTime.UtcNow);
      await this.Dispatch();
    }

    private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
    {
      EasyTracker.UpdateConnectionStatus();
    }

    private static void UpdateConnectionStatus()
    {
      GAServiceManager.Current.IsConnected = NetworkInterface.GetIsNetworkAvailable();
    }

    private void TaskScheduler_UnobservedTaskException(
      object sender,
      UnobservedTaskExceptionEventArgs e)
    {
      Exception exception = e.Exception.InnerException ?? (Exception) e.Exception;
      EasyTracker.tracker.SendException(exception.ToString(), false);
    }

    private async void app_UnhandledException(
      object sender,
      ApplicationUnhandledExceptionEventArgs e)
    {
      if (e.Handled)
        EasyTracker.tracker.SendException(e.ExceptionObject.ToString(), false);
      else if (!(e.ExceptionObject is EasyTracker.TrackedException))
      {
        e.Handled = true;
        EasyTracker.tracker.SendException(e.ExceptionObject.ToString(), true);
        await this.Dispatch();
        throw new EasyTracker.TrackedException(e.ExceptionObject);
      }
    }

    public sealed class TrackedException : Exception
    {
      public TrackedException(Exception ex)
        : base("Exception rethrown after tracked by Google Analytics", ex)
      {
      }
    }
  }
}
