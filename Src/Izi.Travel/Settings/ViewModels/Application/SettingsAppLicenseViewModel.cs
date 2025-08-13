// ********************************************************************
// Type: Izi.Travel.Settings.ViewModels.Application.SettingsAppLicenseViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Caliburn.Micro;
using Izi.Travel.Core.Extensions;
using Izi.Travel.Core.Resources;
using Izi.Travel.Settings.Model;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

#nullable disable
namespace Izi.Travel.Settings.ViewModels.Application
{
  public class SettingsAppLicenseViewModel : Screen
  {
    private readonly ILog _logger = LogManager.GetLog(typeof (SettingsAppLicenseViewModel));
    private List<object> _licenseInfo;

    public List<object> LicenseInfo
    {
      get => this._licenseInfo;
      set
      {
        this.SetProperty<List<object>>(ref this._licenseInfo, value, propertyName: nameof (LicenseInfo));
      }
    }

    protected override void OnActivate() => this.LoadData();

    private void LoadData()
    {
      try
      {
         Izi.Travel.Settings.Model.LicenseInfo data = new LicenseInfo();
        /*Izi.Travel.Settings.Model.LicenseInfo data = XmlSerializerHelper.Deserialize<Izi.Travel.Settings.Model.LicenseInfo>(
            Assembly.GetExecutingAssembly().GetManifestResourceStream("Izi.Travel.Settings.Model.licenses.xml"));
        if (data == null || data.Packages == null || data.Licenses == null)
          return;
        data.Packages.ForEach((Action<Package>) (x => x.License = data.Licenses.FirstOrDefault<License>((Func<License, bool>) (y => y.Id == x.LicenseId))));
        */
        List<object> licenseInfo = new List<object>();
        licenseInfo.Add((object) new Header(AppResources.LabelPackages.ToUpper()));
        licenseInfo.AddRange((IEnumerable<object>) data.Packages);
        licenseInfo.Add((object) new Header(AppResources.LabelLicenses.ToUpper()));
        data.Licenses.ForEach((Action<License>) (x =>
        {
          licenseInfo.Add((object) x);
          licenseInfo.AddRange((IEnumerable<object>) x.ContentParts);
        }));
        this.LicenseInfo = licenseInfo;
      }
      catch (Exception ex)
      {
        this._logger.Error(ex);
      }
    }
  }
}
