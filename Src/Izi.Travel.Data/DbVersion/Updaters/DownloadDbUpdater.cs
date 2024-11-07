// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.DbVersion.Updaters.DownloadDbUpdater
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Context;
using Izi.Travel.Data.DbVersion.Profiles;
using Izi.Travel.Data.DbVersion.Profiles.Download;
using Izi.Travel.Data.DbVersion.Updaters.Base;
using System.Data.Linq;

#nullable disable
namespace Izi.Travel.Data.DbVersion.Updaters
{
  public class DownloadDbUpdater : DbUpdaterBase
  {
    protected override IDbUpdaterProfile[] GetProfiles()
    {
      return new IDbUpdaterProfile[1]
      {
        (IDbUpdaterProfile) new DownloadDbUpdateProfile0001()
      };
    }

    protected override DataContext CreateContext() => (DataContext) new DownloadDataContext();
  }
}
