// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.FrameAdapter
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  ///   A basic implementation of <see cref="T:Caliburn.Micro.INavigationService" /> designed to adapt the <see cref="T:System.Windows.Controls.Frame" /> control.
  /// </summary>
  public class FrameAdapter : INavigationService, INavigate
  {
    private readonly Frame frame;
    private readonly bool treatViewAsLoaded;

    private event NavigatingCancelEventHandler ExternalNavigatingHandler = (param0, param1) => { };

    /// <summary>
    ///   Creates an instance of <see cref="T:Caliburn.Micro.FrameAdapter" />
    /// </summary>
    /// <param name="frame"> The frame to represent as a <see cref="T:Caliburn.Micro.INavigationService" /> . </param>
    /// <param name="treatViewAsLoaded"> Tells the frame adapter to assume that the view has already been loaded by the time OnNavigated is called. This is necessary when using the TransitionFrame. </param>
    public FrameAdapter(Frame frame, bool treatViewAsLoaded = false)
    {
      this.frame = frame;
      this.treatViewAsLoaded = treatViewAsLoaded;
      this.frame.Navigating += new NavigatingCancelEventHandler(this.OnNavigating);
      this.frame.Navigated += new NavigatedEventHandler(this.OnNavigated);
    }

    /// <summary>Occurs before navigation</summary>
    /// <param name="sender"> The event sender. </param>
    /// <param name="e"> The event args. </param>
    protected virtual void OnNavigating(object sender, NavigatingCancelEventArgs e)
    {
      this.ExternalNavigatingHandler(sender, e);
      if (e.Cancel || !(this.frame.Content is FrameworkElement content))
        return;
      if (content.DataContext is IGuardClose dataContext1 && !e.Uri.IsAbsoluteUri)
      {
        bool shouldCancel = false;
        dataContext1.CanClose((Action<bool>) (result => shouldCancel = !result));
        if (shouldCancel)
        {
          e.Cancel = true;
          return;
        }
      }
      if (!(content.DataContext is IDeactivate dataContext2) || !(this.frame.CurrentSource != e.Uri))
        return;
      dataContext2.Deactivate(this.CanCloseOnNavigating(sender, e));
    }

    /// <summary>
    /// Called to check whether or not to close current instance on navigating.
    /// </summary>
    /// <param name="sender"> The event sender from OnNavigating event. </param>
    /// <param name="e"> The event args from OnNavigating event. </param>
    protected virtual bool CanCloseOnNavigating(object sender, NavigatingCancelEventArgs e)
    {
      return false;
    }

    /// <summary>Occurs after navigation</summary>
    /// <param name="sender"> The event sender. </param>
    /// <param name="e"> The event args. </param>
    protected virtual void OnNavigated(object sender, NavigationEventArgs e)
    {
      if (e.Uri.IsAbsoluteUri || e.Content == null)
        return;
      ViewLocator.InitializeComponent(e.Content);
      object viewModel = ViewModelLocator.LocateForView(e.Content);
      if (viewModel == null)
        return;
      if (!(e.Content is PhoneApplicationPage content))
        throw new ArgumentException("View '" + e.Content.GetType().FullName + "' should inherit from PhoneApplicationPage or one of its descendents.");
      if (this.treatViewAsLoaded)
        content.SetValue(View.IsLoadedProperty, (object) true);
      this.TryInjectQueryString(viewModel, (Page) content);
      ViewModelBinder.Bind(viewModel, (DependencyObject) content, (object) null);
      if (viewModel is IActivate activate)
        activate.Activate();
      GC.Collect();
    }

    /// <summary>
    ///   Attempts to inject query string parameters from the view into the view model.
    /// </summary>
    /// <param name="viewModel"> The view model. </param>
    /// <param name="page"> The page. </param>
    protected virtual void TryInjectQueryString(object viewModel, Page page)
    {
      Type type = viewModel.GetType();
      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) page.NavigationContext.QueryString)
      {
        PropertyInfo propertyCaseInsensitive = type.GetPropertyCaseInsensitive(keyValuePair.Key);
        propertyCaseInsensitive?.SetValue(viewModel, MessageBinder.CoerceValue(propertyCaseInsensitive.PropertyType, (object) keyValuePair.Value, (object) page.NavigationContext), (object[]) null);
      }
    }

    /// <summary>
    ///   The <see cref="T:System.Uri" /> source.
    /// </summary>
    public Uri Source
    {
      get => this.frame.Source;
      set => this.frame.Source = value;
    }

    /// <summary>Indicates whether the navigator can navigate back.</summary>
    public bool CanGoBack => this.frame.CanGoBack;

    /// <summary>Indicates whether the navigator can navigate forward.</summary>
    public bool CanGoForward => this.frame.CanGoForward;

    /// <summary>
    ///   The current <see cref="T:System.Uri" /> source.
    /// </summary>
    public Uri CurrentSource => this.frame.CurrentSource;

    /// <summary>The current content.</summary>
    public object CurrentContent => this.frame.Content;

    /// <summary>Stops the loading process.</summary>
    public void StopLoading() => this.frame.StopLoading();

    /// <summary>Navigates back.</summary>
    public void GoBack() => this.frame.GoBack();

    /// <summary>Navigates forward.</summary>
    public void GoForward() => this.frame.GoForward();

    /// <summary>
    ///   Navigates to the specified <see cref="T:System.Uri" /> .
    /// </summary>
    /// <param name="source"> The <see cref="T:System.Uri" /> to navigate to. </param>
    /// <returns> Whether or not navigation succeeded. </returns>
    public bool Navigate(Uri source) => this.frame.Navigate(source);

    /// <summary>Removes the most recent entry from the back stack.</summary>
    /// <returns> The entry that was removed. </returns>
    public JournalEntry RemoveBackEntry()
    {
      return ((Page) this.frame.Content).NavigationService.RemoveBackEntry();
    }

    /// <summary>
    ///   Gets an IEnumerable that you use to enumerate the entries in back navigation history.
    /// </summary>
    /// <returns>List of entries in the back stack.</returns>
    public IEnumerable<JournalEntry> BackStack
    {
      get => ((Page) this.frame.Content).NavigationService.BackStack;
    }

    /// <summary>Raised after navigation.</summary>
    public event NavigatedEventHandler Navigated
    {
      add => this.frame.Navigated += value;
      remove => this.frame.Navigated -= value;
    }

    /// <summary>Raised prior to navigation.</summary>
    public event NavigatingCancelEventHandler Navigating
    {
      add => this.ExternalNavigatingHandler += value;
      remove => this.ExternalNavigatingHandler -= value;
    }

    /// <summary>Raised when navigation fails.</summary>
    public event NavigationFailedEventHandler NavigationFailed
    {
      add => this.frame.NavigationFailed += value;
      remove => this.frame.NavigationFailed -= value;
    }

    /// <summary>Raised when navigation is stopped.</summary>
    public event NavigationStoppedEventHandler NavigationStopped
    {
      add => this.frame.NavigationStopped += value;
      remove => this.frame.NavigationStopped -= value;
    }

    /// <summary>Raised when a fragment navigation occurs.</summary>
    public event FragmentNavigationEventHandler FragmentNavigation
    {
      add => this.frame.FragmentNavigation += value;
      remove => this.frame.FragmentNavigation -= value;
    }
  }
}
