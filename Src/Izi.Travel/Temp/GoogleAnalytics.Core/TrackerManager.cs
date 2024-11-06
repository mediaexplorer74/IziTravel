// Decompiled with JetBrains decompiler
// Type: GoogleAnalytics.Core.TrackerManager
// Assembly: GoogleAnalytics.Core, Version=1.2.11.25892, Culture=neutral, PublicKeyToken=null
// MVID: DA6701CD-FFEA-4833-995F-5D20607A09B2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\GoogleAnalytics.Core.dll

using System.Collections.Generic;

#nullable disable
namespace GoogleAnalytics.Core
{
  public sealed class TrackerManager : IServiceManager
  {
    private readonly IPlatformInfoProvider platformTrackingInfo;
    private readonly Dictionary<string, Tracker> trackers;

    public TrackerManager(IPlatformInfoProvider platformTrackingInfo)
    {
      this.trackers = new Dictionary<string, Tracker>();
      this.platformTrackingInfo = platformTrackingInfo;
    }

    public Tracker DefaultTracker { get; set; }

    public bool IsDebugEnabled { get; set; }

    public bool AppOptOut { get; set; }

    public Tracker GetTracker(string propertyId)
    {
      propertyId = propertyId ?? string.Empty;
      if (this.trackers.ContainsKey(propertyId))
        return this.trackers[propertyId];
      Tracker tracker = new Tracker(propertyId, this.platformTrackingInfo, (IServiceManager) this);
      this.trackers.Add(propertyId, tracker);
      if (this.DefaultTracker == null)
        this.DefaultTracker = tracker;
      return tracker;
    }

    public void CloseTracker(Tracker tracker)
    {
      this.trackers.Remove(tracker.TrackingId);
      if (this.DefaultTracker != tracker)
        return;
      this.DefaultTracker = (Tracker) null;
    }

    void IServiceManager.SendPayload(Payload payload)
    {
      if (this.AppOptOut)
        return;
      ((IServiceManager) GAServiceManager.Current).SendPayload(payload);
    }

    string IServiceManager.UserAgent
    {
      get => GAServiceManager.Current.UserAgent;
      set => GAServiceManager.Current.UserAgent = value;
    }

    public IPlatformInfoProvider PlatformTrackingInfo => this.platformTrackingInfo;
  }
}
