// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.DbVersion.Updaters.LocalDbUpdater
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.Context;
using Izi.Travel.Data.DbVersion.Profiles;
using Izi.Travel.Data.DbVersion.Profiles.Local;
using Izi.Travel.Data.DbVersion.Updaters.Base;
using System.Data.Linq;

#nullable disable
namespace Izi.Travel.Data.DbVersion.Updaters
{
  public class LocalDbUpdater : DbUpdaterBase
  {
    protected override IDbUpdaterProfile[] GetProfiles()
    {
      return new IDbUpdaterProfile[3]
      {
        (IDbUpdaterProfile) new LocalDbUpdateProfile0001(),
        (IDbUpdaterProfile) new LocalDbUpdateProfile0002(),
        (IDbUpdaterProfile) new LocalDbUpdateProfile0003()
      };
    }

    protected override DataContext CreateContext() => (DataContext) new LocalDataContext();
  }
}
