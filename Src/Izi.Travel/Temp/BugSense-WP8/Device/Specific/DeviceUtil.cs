// Decompiled with JetBrains decompiler
// Type: BugSense.Device.Specific.DeviceUtil
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core;
using BugSense.Core.Model;
using BugSense.Windows.Specific;
using Microsoft.Phone.Info;
using Microsoft.Phone.Net.NetworkInformation;
using System;
using System.Globalization;
using System.Net;
using System.Windows;

#nullable disable
namespace BugSense.Device.Specific
{
  internal class DeviceUtil : IDeviceUtil
  {
    public void AppendBugSenseInfo()
    {
      ManifestAppInfo manifestAppInfo = new ManifestAppInfo();
      BugSenseProperties.AppName = manifestAppInfo.Title;
      BugSenseProperties.AppVersion = manifestAppInfo.Version;
      BugSenseProperties.OSVersion = Environment.OSVersion.Version.ToString();
      object propertyValue1;
      if (DeviceExtendedProperties.TryGetValue("DeviceManufacturer", out propertyValue1))
        BugSenseProperties.PhoneBrand = propertyValue1.ToString();
      object propertyValue2;
      if (DeviceExtendedProperties.TryGetValue("DeviceName", out propertyValue2))
        BugSenseProperties.PhoneModel = propertyValue2.ToString();
      BugSenseProperties.BugSenseName = "bugsense-wp8";
      BugSenseProperties.Locale = CultureInfo.CurrentCulture.EnglishName;
      BugSenseProperties.Carrier = DeviceNetworkInformation.CellularMobileOperator;
      Size displayResolution = DeviceUtil.DisplayResolution;
      BugSenseProperties.DeviceScreenProperties.Height = displayResolution.Height;
      BugSenseProperties.DeviceScreenProperties.Width = displayResolution.Width;
    }

    public void GetDeviceConnectionInfo()
    {
      DeviceNetworkInformation.ResolveHostNameAsync(new DnsEndPoint("microsoft.com", 80), (NameResolutionCallback) (nrr =>
      {
        NetworkInterfaceInfo networkInterface = nrr.NetworkInterface;
        if (networkInterface == null)
          return;
        switch (networkInterface.InterfaceType)
        {
          case NetworkInterfaceType.Ethernet:
            BugSenseProperties.Connection = ConnectionType.NA;
            break;
          case NetworkInterfaceType.Wireless80211:
            BugSenseProperties.Connection = ConnectionType.Wifi;
            BugSenseProperties.WIFIOn = 1;
            break;
          case NetworkInterfaceType.MobileBroadbandGsm:
          case NetworkInterfaceType.MobileBroadbandCdma:
            switch (networkInterface.InterfaceSubtype)
            {
              case NetworkInterfaceSubType.Cellular_EVDO:
              case NetworkInterfaceSubType.Cellular_3G:
              case NetworkInterfaceSubType.Cellular_HSPA:
              case NetworkInterfaceSubType.Cellular_EVDV:
                BugSenseProperties.MobileNetOn = 1;
                BugSenseProperties.Connection = ConnectionType._3G;
                break;
            }
            BugSenseProperties.Connection = ConnectionType._2G;
            BugSenseProperties.MobileNetOn = 1;
            break;
        }
      }), (object) null);
    }

    public static Size DisplayResolution
    {
      get
      {
        if (Environment.OSVersion.Version.Major < 8)
          return new Size(480.0, 800.0);
        switch ((int) DeviceUtil.GetProperty((object) Application.Current.Host.Content, "ScaleFactor"))
        {
          case 100:
            return new Size(480.0, 800.0);
          case 150:
            return new Size(720.0, 1280.0);
          case 160:
            return new Size(768.0, 1280.0);
          default:
            return new Size(480.0, 800.0);
        }
      }
    }

    private static object GetProperty(object instance, string name)
    {
      return instance.GetType().GetProperty(name).GetGetMethod().Invoke(instance, (object[]) null);
    }

    public void GetScreenInfo()
    {
    }

    public AppEnvironment GetAppEnvironment()
    {
      AppEnvironment appEnvironment = new AppEnvironment()
      {
        AppName = BugSenseProperties.AppName,
        AppVersion = BugSenseProperties.AppVersion,
        OsVersion = BugSenseProperties.OSVersion,
        CpuModel = "unknown",
        CpuBitness = 64,
        Locale = CultureInfo.CurrentCulture.EnglishName,
        Uid = BugSenseProperties.UID,
        PhoneManufacturer = BugSenseProperties.PhoneBrand,
        PhoneModel = BugSenseProperties.PhoneModel,
        ScreenDpi = "unavailable",
        CellularData = DeviceNetworkInformation.IsCellularDataEnabled ? "true" : "false",
        Carrier = DeviceNetworkInformation.CellularMobileOperator,
        Rooted = false,
        GpsOn = BugSenseProperties.GPSOn.ToString()
      };
      this.GetDeviceConnectionInfo();
      this.GetScreenInfo();
      return appEnvironment;
    }

    public BugSensePerformance GetBugSensePerformance()
    {
      double megabytes1 = DeviceUtil.ConvertBytesToMegabytes(DeviceStatus.ApplicationMemoryUsageLimit);
      double megabytes2 = DeviceUtil.ConvertBytesToMegabytes(DeviceStatus.ApplicationCurrentMemoryUsage);
      double num = megabytes1 - megabytes2;
      return new BugSensePerformance()
      {
        AppMemMax = megabytes1,
        AppMemTotal = megabytes2,
        AppMemAvail = num,
        SysMemAvail = DeviceUtil.ConvertBytesToMegabytes(DeviceStatus.DeviceTotalMemory),
        SysMemLow = this.IsLowMemDevice.ToString(),
        SysMemThreshold = num
      };
    }

    private static double ConvertBytesToMegabytes(long bytes) => (double) bytes / 1024.0 / 1024.0;

    public bool IsLowMemDevice
    {
      get
      {
        try
        {
          return (long) DeviceExtendedProperties.GetValue("ApplicationWorkingSetLimit") < 94371840L;
        }
        catch (ArgumentOutOfRangeException ex)
        {
          return false;
        }
      }
    }
  }
}
