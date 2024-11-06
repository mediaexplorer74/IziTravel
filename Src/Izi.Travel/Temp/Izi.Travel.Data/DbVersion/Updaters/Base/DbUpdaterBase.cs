// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.DbVersion.Updaters.Base.DbUpdaterBase
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.DbVersion.Profiles;
using Microsoft.Phone.Data.Linq;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;

#nullable disable
namespace Izi.Travel.Data.DbVersion.Updaters.Base
{
  public abstract class DbUpdaterBase : IDbUpdater
  {
    protected abstract IDbUpdaterProfile[] GetProfiles();

    protected abstract DataContext CreateContext();

    public virtual void Drop()
    {
      using (DataContext context = this.CreateContext())
      {
        if (!context.DatabaseExists())
          return;
        context.DeleteDatabase();
      }
    }

    public virtual void Update()
    {
      IDbUpdaterProfile[] profiles = this.GetProfiles();
      using (DataContext context = this.CreateContext())
      {
        if (!context.DatabaseExists())
        {
          context.CreateDatabase();
          context.SubmitChanges();
          DatabaseSchemaUpdater databaseSchemaUpdater = context.CreateDatabaseSchemaUpdater();
          databaseSchemaUpdater.DatabaseSchemaVersion = profiles == null || !((IEnumerable<IDbUpdaterProfile>) profiles).Any<IDbUpdaterProfile>() ? 1 : ((IEnumerable<IDbUpdaterProfile>) profiles).Max<IDbUpdaterProfile>((Func<IDbUpdaterProfile, int>) (x => x.DbVersion));
          databaseSchemaUpdater.Execute();
        }
        else
        {
          int dbVersion = context.CreateDatabaseSchemaUpdater().DatabaseSchemaVersion;
          IOrderedEnumerable<IDbUpdaterProfile> orderedEnumerable = profiles != null ? ((IEnumerable<IDbUpdaterProfile>) profiles).Where<IDbUpdaterProfile>((Func<IDbUpdaterProfile, bool>) (x => x.DbVersion > dbVersion)).OrderBy<IDbUpdaterProfile, int>((Func<IDbUpdaterProfile, int>) (x => x.DbVersion)) : (IOrderedEnumerable<IDbUpdaterProfile>) null;
          if (orderedEnumerable == null)
            return;
          foreach (IDbUpdaterProfile dbUpdaterProfile in (IEnumerable<IDbUpdaterProfile>) orderedEnumerable)
          {
            DatabaseSchemaUpdater databaseSchemaUpdater = context.CreateDatabaseSchemaUpdater();
            dbUpdaterProfile.ApplySchemaUpdate(databaseSchemaUpdater);
            databaseSchemaUpdater.DatabaseSchemaVersion = dbUpdaterProfile.DbVersion;
            databaseSchemaUpdater.Execute();
            dbUpdaterProfile.ApplyDataBaseUpdate(context);
            context.SubmitChanges();
          }
        }
      }
    }
  }
}
