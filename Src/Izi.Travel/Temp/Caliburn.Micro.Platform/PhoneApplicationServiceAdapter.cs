// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PhoneApplicationServiceAdapter
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   An implementation of <see cref="T:Caliburn.Micro.IPhoneService" /> that adapts <see cref="T:Microsoft.Phone.Shell.PhoneApplicationService" />.
  /// </summary>
  public class PhoneApplicationServiceAdapter : IPhoneService
  {
    private readonly PhoneApplicationService service;

    /// <summary>
    ///   Creates an instance of <see cref="T:Caliburn.Micro.PhoneApplicationServiceAdapter" />.
    /// </summary>
    public PhoneApplicationServiceAdapter(
      PhoneApplicationService phoneApplicationServiceservice,
      Frame rootFrame)
    {
      PhoneApplicationServiceAdapter applicationServiceAdapter = this;
      this.service = phoneApplicationServiceservice;
      this.service.Activated += (EventHandler<ActivatedEventArgs>) ((sender, args) =>
      {
        if (!args.IsApplicationInstancePreserved)
        {
          applicationServiceAdapter.IsResurrecting = true;
          applicationServiceAdapter.Resurrecting();
          NavigatedEventHandler onNavigated = (NavigatedEventHandler) null;
          onNavigated = (NavigatedEventHandler) ((s2, e2) =>
          {
            applicationServiceAdapter.IsResurrecting = false;
            applicationServiceAdapter.Resurrected();
            rootFrame.Navigated -= onNavigated;
          });
          rootFrame.Navigated += onNavigated;
        }
        else
        {
          applicationServiceAdapter.Continuing();
          NavigatedEventHandler onNavigated = (NavigatedEventHandler) null;
          onNavigated = (NavigatedEventHandler) ((s2, e2) =>
          {
            applicationServiceAdapter.Continued();
            rootFrame.Navigated -= onNavigated;
          });
          rootFrame.Navigated += onNavigated;
        }
      });
    }

    /// <summary>Gets if the app is currently resurrecting.</summary>
    public bool IsResurrecting { get; private set; }

    /// <summary>
    ///   The state that is persisted during the tombstoning process.
    /// </summary>
    public IDictionary<string, object> State => this.service.State;

    /// <summary>Gets the mode in which the application was started.</summary>
    public StartupMode StartupMode => this.service.StartupMode;

    /// <summary>
    ///   Occurs when a fresh instance of the application is launching.
    /// </summary>
    public event EventHandler<LaunchingEventArgs> Launching
    {
      add => this.service.Launching += value;
      remove => this.service.Launching -= value;
    }

    /// <summary>
    ///   Occurs when a previously paused/tombstoned application instance is resumed/resurrected.
    /// </summary>
    public event EventHandler<ActivatedEventArgs> Activated
    {
      add => this.service.Activated += value;
      remove => this.service.Activated -= value;
    }

    /// <summary>
    ///   Occurs when the application is being paused or tombstoned.
    /// </summary>
    public event EventHandler<DeactivatedEventArgs> Deactivated
    {
      add => this.service.Deactivated += value;
      remove => this.service.Deactivated -= value;
    }

    /// <summary>Occurs when the application is closing.</summary>
    public event EventHandler<ClosingEventArgs> Closing
    {
      add => this.service.Closing += value;
      remove => this.service.Closing -= value;
    }

    /// <summary>
    ///   Occurs when the app is continuing from a temporarily paused state.
    /// </summary>
    public event System.Action Continuing = () => { };

    /// <summary>
    ///   Occurs after the app has continued from a temporarily paused state.
    /// </summary>
    public event System.Action Continued = () => { };

    /// <summary>
    ///   Occurs when the app is "resurrecting" from a tombstoned state.
    /// </summary>
    public event System.Action Resurrecting = () => { };

    /// <summary>
    ///   Occurs after the app has "resurrected" from a tombstoned state.
    /// </summary>
    public event System.Action Resurrected = () => { };

    /// <summary>Gets or sets whether user idle detection is enabled.</summary>
    public IdleDetectionMode UserIdleDetectionMode
    {
      get => this.service.UserIdleDetectionMode;
      set => this.service.UserIdleDetectionMode = value;
    }

    /// <summary>
    ///   Gets or sets whether application idle detection is enabled.
    /// </summary>
    public IdleDetectionMode ApplicationIdleDetectionMode
    {
      get => this.service.ApplicationIdleDetectionMode;
      set => this.service.ApplicationIdleDetectionMode = value;
    }
  }
}
