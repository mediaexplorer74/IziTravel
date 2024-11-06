// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.IPhoneService
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Implemented by services that provide access to the basic phone capabilities.
  /// </summary>
  public interface IPhoneService
  {
    /// <summary>
    ///   The state that is persisted during the tombstoning process.
    /// </summary>
    IDictionary<string, object> State { get; }

    /// <summary>Gets the mode in which the application was started.</summary>
    StartupMode StartupMode { get; }

    /// <summary>
    ///   Occurs when a fresh instance of the application is launching.
    /// </summary>
    event EventHandler<LaunchingEventArgs> Launching;

    /// <summary>
    ///   Occurs when a previously paused/tombstoned app is resumed/resurrected.
    /// </summary>
    event EventHandler<ActivatedEventArgs> Activated;

    /// <summary>
    ///   Occurs when the application is being paused or tombstoned.
    /// </summary>
    event EventHandler<DeactivatedEventArgs> Deactivated;

    /// <summary>Occurs when the application is closing.</summary>
    event EventHandler<ClosingEventArgs> Closing;

    /// <summary>
    ///   Occurs when the app is continuing from a temporarily paused state.
    /// </summary>
    event System.Action Continuing;

    /// <summary>
    ///   Occurs after the app has continued from a temporarily paused state.
    /// </summary>
    event System.Action Continued;

    /// <summary>
    ///   Occurs when the app is "resurrecting" from a tombstoned state.
    /// </summary>
    event System.Action Resurrecting;

    /// <summary>
    ///   Occurs after the app has "resurrected" from a tombstoned state.
    /// </summary>
    event System.Action Resurrected;

    /// <summary>Gets or sets whether user idle detection is enabled.</summary>
    IdleDetectionMode UserIdleDetectionMode { get; set; }

    /// <summary>
    ///   Gets or sets whether application idle detection is enabled.
    /// </summary>
    IdleDetectionMode ApplicationIdleDetectionMode { get; set; }

    /// <summary>Gets if the app is currently resurrecting.</summary>
    bool IsResurrecting { get; }
  }
}
