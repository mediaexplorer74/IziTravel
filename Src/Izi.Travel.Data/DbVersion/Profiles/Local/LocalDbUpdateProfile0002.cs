﻿// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.DbVersion.Profiles.Local.LocalDbUpdateProfile0002
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.DbVersion.Updaters.Base;
using Izi.Travel.Data.Entities.Local;
//using Microsoft.Phone.Data.Linq;
using System.Data.Linq;

#nullable disable
namespace Izi.Travel.Data.DbVersion.Profiles.Local
{
  public class LocalDbUpdateProfile0002 : IDbUpdaterProfile
  {
    public int DbVersion => 3;

    public void ApplySchemaUpdate(DatabaseSchemaUpdater dbUpdater)
    {
      dbUpdater.AddTable<Purchase>();
      dbUpdater.AddColumn<Bookmark>("ParentUid");
      dbUpdater.AddColumn<History>("ParentUid");
    }

    public void ApplyDataBaseUpdate(DataContext dataContext)
    {
    }
            
  }
}
