// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Helpers.UserVoiceHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using ICSharpCode.SharpZipLib.Zip;
using Izi.Travel.Business.Managers;
using Izi.Travel.Business.Services;
using Izi.Travel.Shell.Core;
using Microsoft.Phone.Info;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UserVoice;

#nullable disable
namespace Izi.Travel.Shell.Settings.Helpers
{
  public class UserVoiceHelper
  {
    private const string SubdomainName = "izitravel";
    private const string ApiKey = "KzXaaWOvDFXZ6dnUKkeAA";
    private const string ApiSecret = "NW8WerK5q92YJMInoJv0LzLgWnRo1lGSBce1yKVhla0";
    private static readonly ILog Logger = LogManager.GetLog(typeof (UserVoiceHelper));

    public static async Task<bool> Post(string email, string subject, string message)
    {
      try
      {
        Client client = new Client("izitravel", "KzXaaWOvDFXZ6dnUKkeAA", "NW8WerK5q92YJMInoJv0LzLgWnRo1lGSBce1yKVhla0");
        string str1 = email;
        string str2 = subject;
        string str3 = message;
        var data = new
        {
          App = UserVoiceHelper.GetAppVersion(),
          OS = UserVoiceHelper.GetOsVersion(),
          Phone = UserVoiceHelper.GetPhoneModel(),
          Memory = UserVoiceHelper.GetMemoryUsage(),
          Languages = UserVoiceHelper.GetLanguages(),
          Waldo = UserVoiceHelper.GetWaldo(),
          Pass = UserVoiceHelper.GetPasscode()
        };
        var data1 = new
        {
          name = "Log.zip",
          content_type = "application/zip",
          data = await UserVoiceHelper.GetCompressedLogAsync()
        };
        \u003C\u003Ef__AnonymousType4<string, string, string>[] dataArray = new \u003C\u003Ef__AnonymousType4<string, string, string>[1]
        {
          data1
        };
        var data2 = new
        {
          subject = str2,
          message = str3,
          custom_field_values = data,
          attachments = dataArray
        };
        var parameters = new{ email = str1, ticket = data2 };
        str1 = (string) null;
        str2 = (string) null;
        str3 = (string) null;
        data = null;
        JToken jtoken = await client.Post("/api/v1/tickets.json", (object) parameters);
        return true;
      }
      catch (Exception ex)
      {
        UserVoiceHelper.Logger.Error(ex);
      }
      return false;
    }

    public static Task<string> GetCompressedLogAsync()
    {
      return Task.Run<string>((Func<string>) (() =>
      {
        try
        {
          using (MemoryStream baseOutputStream = new MemoryStream())
          {
            using (ZipOutputStream zipOutputStream = new ZipOutputStream((Stream) baseOutputStream))
            {
              zipOutputStream.PutNextEntry(new ZipEntry("Log.txt"));
              using (StreamWriter streamWriter = new StreamWriter((Stream) zipOutputStream, (Encoding) new UTF8Encoding(false, true), 1024, true))
              {
                string log = CustomLogger.GetLog();
                streamWriter.Write(log);
              }
              zipOutputStream.Finish();
              return Convert.ToBase64String(baseOutputStream.ToArray());
            }
          }
        }
        catch (Exception ex)
        {
          UserVoiceHelper.Logger.Error(ex);
          if (Debugger.IsAttached)
            Debugger.Break();
        }
        return string.Empty;
      }));
    }

    private static string GetAppVersion()
    {
      return Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    private static string GetOsVersion() => Environment.OSVersion.ToString();

    private static string GetPhoneModel()
    {
      return string.Format("{0} {1}", (object) DeviceStatus.DeviceManufacturer, (object) DeviceStatus.DeviceName);
    }

    private static string GetMemoryUsage()
    {
      return string.Format("{0}/{1}", (object) (DeviceStatus.ApplicationCurrentMemoryUsage / 1024L / 1024L), (object) (DeviceStatus.ApplicationMemoryUsageLimit / 1024L / 1024L));
    }

    private static string GetLanguages()
    {
      return string.Join(", ", ServiceFacade.SettingsService.GetAppSettings().Languages).ToUpper();
    }

    private static string GetWaldo() => Geotracker.Instance.Position?.ToString();

    private static string GetPasscode()
    {
      return ServiceFacade.SettingsService.GetAppSettings().CodeName ?? string.Empty;
    }
  }
}
