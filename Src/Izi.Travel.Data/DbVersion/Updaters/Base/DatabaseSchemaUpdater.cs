// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Data.DbVersion.Updaters.Base.DbUpdaterBase
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using System;

namespace Izi.Travel.Data.DbVersion.Updaters.Base
{
    public class DatabaseSchemaUpdater
    {
        public int DatabaseSchemaVersion;

        public void Execute()
        {
            //
        }

        internal void AddColumn<T>(string v)
        {
            throw new NotImplementedException();
        }

        internal void AddTable<T>()
        {
            throw new NotImplementedException();
        }
    }
}