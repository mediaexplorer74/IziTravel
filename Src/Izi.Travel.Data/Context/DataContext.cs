// ********************************************************************
// Type: Izi.Travel.Data.Context.DownloadDataContext
// Assembly: Izi.Travel.Data, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: 9765AC3B-732C-4703-A0F8-C0EBF29D8E89
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Data.dll

using Izi.Travel.Data.DbVersion.Updaters.Base;
using System;

namespace Izi.Travel.Data.Context
{
    public class DataContext : IDisposable
    {
        private string connectionString;

        public DataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        internal void CreateDatabase()
        {
            throw new NotImplementedException();
        }

        internal DatabaseSchemaUpdater CreateDatabaseSchemaUpdater()
        {
            throw new NotImplementedException();
        }

        internal bool DatabaseExists()
        {
            throw new NotImplementedException();
        }

        internal void DeleteDatabase()
        {
            throw new NotImplementedException();
        }

        internal void SubmitChanges(Services.Implementation.ConflictMode failOnFirstConflict)
        {
            throw new NotImplementedException();
        }

        internal void SubmitChanges()
        {
            throw new NotImplementedException();
        }
    }
}