// Decompiled with JetBrains decompiler
// Type: BugSense.BugSenseHandler
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core;
using BugSense.Core.Helpers;
using BugSense.Core.Interfaces;
using BugSense.Core.Model;
using BugSense.Device.Specific;
using Microsoft.Phone.Controls;
using System;
using System.Windows;

#nullable disable
namespace BugSense
{
  public class BugSenseHandler : BugSenseSyncContextHandler
  {
    private Application ApplicationContext { get; set; }

    protected BugSenseHandler()
      : base((IRequestWorker) new BugSenseRequestWorker((IBugSenseFileClient) new FileRepository(), (IBugSenseServiceClient) new ServiceRepository(), (IRequestJsonSerializer) new PublicRequestJsonSerializer(), (IDeviceUtil) new DeviceUtil(), (IContentResolver) new PublicContentResolver()))
    {
    }

    protected BugSenseHandler(IRequestWorker requestWorker)
      : base(requestWorker)
    {
    }

    public static BugSenseHandler Instance => BugSenseHandler.Nested.instance;

    public bool InitAndStartSession(
      IExceptionManager exceptionManager,
      PhoneApplicationFrame rootFrame,
      string apiKey)
    {
      if (!BugSenseHandlerBase.IsInitialized)
      {
        if (exceptionManager == null)
          throw new ArgumentNullException(nameof (exceptionManager), "ExceptionManager cannot be null!");
        this.InitAndStartSession(apiKey);
        if (exceptionManager is ExceptionManager exceptionManager1)
        {
          this.ApplicationContext = exceptionManager1.ApplicationContext != null ? exceptionManager1.ApplicationContext : throw new ArgumentNullException(nameof (exceptionManager), "ApplicationContext cannot be null!");
          this.ApplicationContext.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.UnhandledExceptionsHandler);
        }
        else
          exceptionManager.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.UnhandledExceptionsHandler);
        BugSenseProperties.UserAgent = "WP";
        BugSenseProperties.Orientation = "PortraitUp";
        if (rootFrame != null)
          rootFrame.OrientationChanged += (EventHandler<OrientationChangedEventArgs>) ((sender, args) => BugSenseProperties.Orientation = Enum.GetName(typeof (PageOrientation), (object) args.Orientation));
        BugSenseHandlerBase.IsInitialized = true;
      }
      return BugSenseHandlerBase.IsInitialized;
    }

    private void UnhandledExceptionsHandler(object sender, ApplicationUnhandledExceptionEventArgs e)
    {
      BugSenseLogResult bugSenseLogResult = this.BugSenseWorker.HandleException(e.ExceptionObject, ExtraData.CrashExtraData);
      this.OnUnhandledExceptionHandled((object) this, new BugSenseUnhandledHandlerEventArgs()
      {
        ClientJsonRequest = bugSenseLogResult.ClientRequest,
        ExceptionObject = e.ExceptionObject,
        HandledSuccessfully = bugSenseLogResult.ResultState.Equals((object) BugSenseResultState.OK)
      });
    }

    private class Nested
    {
      internal static readonly BugSenseHandler instance = new BugSenseHandler();
    }
  }
}
