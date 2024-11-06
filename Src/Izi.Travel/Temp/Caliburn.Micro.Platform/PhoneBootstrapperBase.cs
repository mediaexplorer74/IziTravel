// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.PhoneBootstrapperBase
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Navigation;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// A custom bootstrapper designed to setup phone applications.
  /// </summary>
  public abstract class PhoneBootstrapperBase : BootstrapperBase
  {
    private bool phoneApplicationInitialized;

    /// <summary>The phone application service.</summary>
    protected PhoneApplicationService PhoneService { get; private set; }

    /// <summary>The root frame used for navigation.</summary>
    protected PhoneApplicationFrame RootFrame { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Caliburn.Micro.PhoneBootstrapperBase" /> class.
    /// </summary>
    protected PhoneBootstrapperBase()
      : base()
    {
    }

    /// <summary>
    /// Provides an opportunity to hook into the application object.
    /// </summary>
    protected override void PrepareApplication()
    {
      base.PrepareApplication();
      this.PhoneService = new PhoneApplicationService();
      this.PhoneService.Activated += new EventHandler<ActivatedEventArgs>(this.OnActivate);
      this.PhoneService.Deactivated += new EventHandler<DeactivatedEventArgs>(this.OnDeactivate);
      this.PhoneService.Launching += new EventHandler<LaunchingEventArgs>(this.OnLaunch);
      this.PhoneService.Closing += new EventHandler<ClosingEventArgs>(this.OnClose);
      this.Application.ApplicationLifetimeObjects.Add((object) this.PhoneService);
      if (this.phoneApplicationInitialized)
        return;
      this.RootFrame = this.CreatePhoneApplicationFrame();
      this.RootFrame.Navigated += new NavigatedEventHandler(this.OnNavigated);
      this.phoneApplicationInitialized = true;
    }

    private void OnNavigated(object sender, NavigationEventArgs e)
    {
      if (this.Application.RootVisual == this.RootFrame)
        return;
      this.Application.RootVisual = (UIElement) this.RootFrame;
    }

    /// <summary>Creates the root frame used by the application.</summary>
    /// <returns>The frame.</returns>
    protected virtual PhoneApplicationFrame CreatePhoneApplicationFrame()
    {
      return new PhoneApplicationFrame();
    }

    /// <summary>
    /// Occurs when a fresh instance of the application is launching.
    /// </summary>
    protected virtual void OnLaunch(object sender, LaunchingEventArgs e)
    {
    }

    /// <summary>
    /// Occurs when a previously tombstoned or paused application is resurrected/resumed.
    /// </summary>
    protected virtual void OnActivate(object sender, ActivatedEventArgs e)
    {
    }

    /// <summary>
    /// Occurs when the application is being tombstoned or paused.
    /// </summary>
    protected virtual void OnDeactivate(object sender, DeactivatedEventArgs e)
    {
    }

    /// <summary>Occurs when the application is closing.</summary>
    protected virtual void OnClose(object sender, ClosingEventArgs e)
    {
    }
  }
}
