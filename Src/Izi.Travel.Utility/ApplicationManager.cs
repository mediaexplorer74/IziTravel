// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Utility.ApplicationManager
// Assembly: Izi.Travel.Utility, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 6E74EF73-7EB1-46AA-A84C-A1A7E0B11FE0
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Utility.dll

//using Caliburn.Micro;
using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

#nullable disable
namespace Izi.Travel.Utility
{
  public class ApplicationManager
  {
    private static Lazy<string> _isolatedStoragePath = new Lazy<string>((Func<string>) (() =>
    {
      try
      {
        return string.Format("C:/Data/Users/DefApps/AppData/{0}/Local/", 
            (object) new Guid(XDocument.Load("WMAppManifest.xml").Root.Element((XName) "App")
            .Attributes().FirstOrDefault<XAttribute>((Func<XAttribute, bool>)
            (x => x.Name.LocalName.ToLower() == "productid")).Value).ToString("B"));
      }
      catch (Exception ex)
      {
        //LogManager.GetLog(typeof (ApplicationManager)).Error(ex);
        Debug.WriteLine("[ex] Load WMAppManifest.xml error: " + ex.Message);
      }
      return (string) null;
    }));

    public static bool RunningInBackground { get; set; }

    public static string IsolatedStoragePath => ApplicationManager._isolatedStoragePath.Value;
  }
}
