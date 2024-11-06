// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.INavigationService
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   Implemented by services that provide <see cref="T:System.Uri" /> based navigation.
  /// </summary>
  public interface INavigationService : INavigate
  {
    /// <summary>
    ///   The <see cref="T:System.Uri" /> source.
    /// </summary>
    Uri Source { get; set; }

    /// <summary>Indicates whether the navigator can navigate back.</summary>
    bool CanGoBack { get; }

    /// <summary>Indicates whether the navigator can navigate forward.</summary>
    bool CanGoForward { get; }

    /// <summary>
    ///   The current <see cref="T:System.Uri" /> source.
    /// </summary>
    Uri CurrentSource { get; }

    /// <summary>The current content.</summary>
    object CurrentContent { get; }

    /// <summary>Stops the loading process.</summary>
    void StopLoading();

    /// <summary>Navigates back.</summary>
    void GoBack();

    /// <summary>Navigates forward.</summary>
    void GoForward();

    /// <summary>Removes the most recent entry from the back stack.</summary>
    /// <returns> The entry that was removed. </returns>
    JournalEntry RemoveBackEntry();

    /// <summary>
    ///   Gets an IEnumerable that you use to enumerate the entries in back navigation history.
    /// </summary>
    /// <returns>List of entries in the back stack.</returns>
    IEnumerable<JournalEntry> BackStack { get; }

    /// <summary>Raised after navigation.</summary>
    event NavigatedEventHandler Navigated;

    /// <summary>Raised prior to navigation.</summary>
    event NavigatingCancelEventHandler Navigating;

    /// <summary>Raised when navigation fails.</summary>
    event NavigationFailedEventHandler NavigationFailed;

    /// <summary>Raised when navigation is stopped.</summary>
    event NavigationStoppedEventHandler NavigationStopped;

    /// <summary>Raised when a fragment navigation occurs.</summary>
    event FragmentNavigationEventHandler FragmentNavigation;
  }
}
