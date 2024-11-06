// Decompiled with JetBrains decompiler
// Type: AdjustSdk.UtilWP80
// Assembly: AdjustWP80, Version=3.5.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 184DAB71-F439-4FAD-8AD3-F5ADA24036D2
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\AdjustWP80.dll

using AdjustSdk.Pcl;
using Microsoft.Devices;
using Microsoft.Phone.Info;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Xml.Linq;
using Windows.System;

#nullable disable
namespace AdjustSdk
{
  public class UtilWP80 : DeviceUtil
  {
    public string ClientSdk => "wphone80-3.5.1";

    public string GetMd5Hash(string input) => MD5Core.GetHashString(input);

    public string GetDeviceUniqueId()
    {
      ILogger logger = AdjustFactory.Logger;
      object propertyValue;
      if (!DeviceExtendedProperties.TryGetValue("DeviceUniqueId", out propertyValue))
      {
        logger.Error("This SDK requires the capability ID_CAP_IDENTITY_DEVICE. You might need to adjust your manifest file. See the README for details.");
        return (string) null;
      }
      string base64String = Convert.ToBase64String(propertyValue as byte[]);
      logger.Debug("Device unique Id ({0})", (object) base64String);
      return base64String;
    }

    public string GetHardwareId() => (string) null;

    public string GetNetworkAdapterId() => (string) null;

    public string GetUserAgent()
    {
      return string.Join(" ", new string[11]
      {
        UtilWP80.getAppName(),
        UtilWP80.getAppVersion(),
        UtilWP80.getAppAuthor(),
        UtilWP80.getAppPublisher(),
        UtilWP80.getDeviceType(),
        UtilWP80.getDeviceName(),
        UtilWP80.getDeviceManufacturer(),
        UtilWP80.getOsName(),
        UtilWP80.getOsVersion(),
        UtilWP80.getLanguage(),
        UtilWP80.getCountry()
      });
    }

    public void RunResponseDelegate(
      Action<ResponseData> responseDelegate,
      ResponseData responseData)
    {
      Deployment.Current.Dispatcher.BeginInvoke((Action) (() => responseDelegate(responseData)));
    }

    public void Sleep(int milliseconds) => Thread.Sleep(milliseconds);

    private static string getAppName()
    {
      return Util.SanitizeUserAgent(UtilWP80.getManifest().Root.Element((XName) "App").Attribute((XName) "Title").Value);
    }

    private static string getAppVersion()
    {
      string str = UtilWP80.getManifest().Root.Element((XName) "App").Attribute((XName) "Version").Value;
      string[] strArray = str.Split('.');
      if (strArray.Length >= 2)
        str = Util.f("{0}.{1}", (object) strArray[0], (object) strArray[1]);
      return Util.SanitizeUserAgent(str);
    }

    private static string getAppAuthor()
    {
      return Util.SanitizeUserAgent(UtilWP80.getManifest().Root.Element((XName) "App").Attribute((XName) "Author").Value);
    }

    private static string getAppPublisher()
    {
      return Util.SanitizeUserAgent(UtilWP80.getManifest().Root.Element((XName) "App").Attribute((XName) "Publisher").Value);
    }

    private static string getDeviceType()
    {
      switch (Microsoft.Devices.Environment.DeviceType)
      {
        case DeviceType.Device:
          return "phone";
        case DeviceType.Emulator:
          return "emulator";
        default:
          return "unknown";
      }
    }

    private static string getDeviceName() => Util.SanitizeUserAgent(DeviceStatus.DeviceName);

    private static string getDeviceManufacturer()
    {
      return Util.SanitizeUserAgent(DeviceStatus.DeviceManufacturer);
    }

    private static string getOsName() => "windows-phone";

    private static string getOsVersion()
    {
      Version version = System.Environment.OSVersion.Version;
      return Util.SanitizeUserAgent(Util.f("{0}.{1}", (object) version.Major, (object) version.Minor));
    }

    private static string getLanguage()
    {
      string name = CultureInfo.CurrentUICulture.Name;
      return name.Length < 2 ? "zz" : Util.SanitizeUserAgent(name.Substring(0, 2), "zz");
    }

    private static string getCountry()
    {
      string name = CultureInfo.CurrentCulture.Name;
      int length = name.Length;
      return length < 2 ? "zz" : Util.SanitizeUserAgent(name.Substring(length - 2, 2).ToLower(), "zz");
    }

    private static XDocument getManifest() => XDocument.Load("WMAppManifest.xml");

    public void LauchDeepLink(Uri deepLinkUri) => Launcher.LaunchUriAsync(deepLinkUri);
  }
}
