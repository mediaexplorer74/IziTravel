// Decompiled with JetBrains decompiler
// Type: BugSense.Core.FileRepository
// Assembly: BugSense-WP8, Version=3.6.8.0, Culture=neutral, PublicKeyToken=null
// MVID: 8A6D84D1-19B1-4B1A-84A1-50F2A1BD7889
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\BugSense-WP8.dll

using BugSense.Core.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace BugSense.Core
{
  internal class FileRepository : IBugSenseFileClient
  {
    private SemaphoreSlim SemaphoreMutex { get; set; }

    private Mutex SyncMutex { get; set; }

    public FileRepository()
    {
      this.SemaphoreMutex = new SemaphoreSlim(1);
      this.SyncMutex = new Mutex(false);
    }

    public async void CreateDirectoriesIfNotExist()
    {
      BugSenseLogResult ifNotExistsAsync = await this.CreateDirectoriesIfNotExistsAsync();
    }

    private Task<BugSenseLogResult> CreateDirectoriesIfNotExistsAsync()
    {
      return Task.Run<BugSenseLogResult>((Func<BugSenseLogResult>) (() =>
      {
        this.SyncMutex.WaitOne();
        BugSenseLogResult ifNotExistsAsync = new BugSenseLogResult();
        try
        {
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          {
            if (!storeForApplication.DirectoryExists(BugSenseProperties.ExceptionsFolderName))
              storeForApplication.CreateDirectory(BugSenseProperties.ExceptionsFolderName);
            if (!storeForApplication.DirectoryExists(BugSenseProperties.GeneralFolderName))
              storeForApplication.CreateDirectory(BugSenseProperties.GeneralFolderName);
            ifNotExistsAsync.ResultState = BugSenseResultState.OK;
          }
        }
        catch (Exception ex)
        {
          ifNotExistsAsync.ResultState = BugSenseResultState.Error;
          ifNotExistsAsync.ExceptionError = ex;
        }
        this.SyncMutex.ReleaseMutex();
        return ifNotExistsAsync;
      }));
    }

    public BugSenseLogResult Save(string filePath, string jsonRequest)
    {
      this.SyncMutex.WaitOne();
      BugSenseLogResult bugSenseLogResult1 = new BugSenseLogResult();
      bugSenseLogResult1.HandledWhileDebugging = BugSenseProperties.HandleWhileDebugging;
      BugSenseLogResult bugSenseLogResult2 = bugSenseLogResult1;
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
            filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
          if (storeForApplication.FileExists(filePath))
            storeForApplication.DeleteFile(filePath);
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) storageFileStream))
              streamWriter.Write(jsonRequest);
          }
        }
        bugSenseLogResult2.ResultState = BugSenseResultState.OK;
      }
      catch (Exception ex)
      {
        bugSenseLogResult2.ResultState = BugSenseResultState.Error;
        bugSenseLogResult2.ExceptionError = ex;
      }
      this.SyncMutex.ReleaseMutex();
      return bugSenseLogResult2;
    }

    public string Read(string filePath)
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          string str = (string) null;
          if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
            filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
          if (storeForApplication.FileExists(filePath))
          {
            using (IsolatedStorageFileStream storageFileStream = new IsolatedStorageFileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite, storeForApplication))
            {
              using (StreamReader streamReader = new StreamReader((Stream) storageFileStream))
                str = streamReader.ReadToEnd();
            }
          }
          return str;
        }
      }
      catch
      {
      }
      return (string) null;
    }

    public async Task<BugSenseLogResult> SaveAsync(string filePath, string data)
    {
      await this.SemaphoreMutex.WaitAsync().ConfigureAwait(false);
      BugSenseLogResult bugSenseLogResult = new BugSenseLogResult();
      bugSenseLogResult.HandledWhileDebugging = BugSenseProperties.HandleWhileDebugging;
      BugSenseLogResult logResult = bugSenseLogResult;
      try
      {
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
            filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
          BugSenseLogResult bugSenseLogResult1 = await this.DeleteAsync(filePath);
          using (IsolatedStorageFileStream fileStream = storage.OpenFile(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
          {
            using (StreamWriter streamWriter = new StreamWriter((Stream) fileStream))
              await streamWriter.WriteAsync(data);
          }
        }
        logResult.ResultState = BugSenseResultState.OK;
      }
      catch (Exception ex)
      {
        logResult.ResultState = BugSenseResultState.Error;
        logResult.ExceptionError = ex;
      }
      this.SemaphoreMutex.Release();
      return logResult;
    }

    public async Task<string> ReadAsync(string filePath)
    {
      try
      {
        using (IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
            filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
          if (!storage.FileExists(filePath))
            return (string) null;
          using (IsolatedStorageFileStream fileStream = storage.OpenFile(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
          {
            using (StreamReader sr = new StreamReader((Stream) fileStream))
              return await sr.ReadToEndAsync().ConfigureAwait(false);
          }
        }
      }
      catch
      {
      }
      return (string) null;
    }

    public Task<List<string>> ReadLoggedExceptions()
    {
      return Task.Run<List<string>>((Func<List<string>>) (() =>
      {
        try
        {
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            return new List<string>((IEnumerable<string>) storeForApplication.GetFileNames(BugSenseProperties.ExceptionsFolderName + "\\*.dat"));
        }
        catch
        {
        }
        return (List<string>) null;
      }));
    }

    public Task<BugSenseLogResult> DeleteAsync(string filePath)
    {
      return Task.Run<BugSenseLogResult>((Func<BugSenseLogResult>) (() =>
      {
        this.SyncMutex.WaitOne();
        BugSenseLogResult bugSenseLogResult = new BugSenseLogResult();
        try
        {
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          {
            if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
              filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
            if (storeForApplication.FileExists(filePath))
              storeForApplication.DeleteFile(filePath);
          }
          bugSenseLogResult.ResultState = BugSenseResultState.OK;
        }
        catch (Exception ex)
        {
          bugSenseLogResult.ResultState = BugSenseResultState.Error;
          bugSenseLogResult.ExceptionError = ex;
        }
        this.SyncMutex.ReleaseMutex();
        return bugSenseLogResult;
      }));
    }

    public BugSenseLogResult Delete(string filePath)
    {
      this.SyncMutex.WaitOne();
      BugSenseLogResult bugSenseLogResult = new BugSenseLogResult();
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (!filePath.Contains(BugSenseProperties.ExceptionsFolderName) && !filePath.Contains(BugSenseProperties.GeneralFolderName))
            filePath = BugSenseProperties.ExceptionsFolderName + "\\" + filePath;
          if (storeForApplication.FileExists(filePath))
            storeForApplication.DeleteFile(filePath);
        }
        bugSenseLogResult.ResultState = BugSenseResultState.OK;
      }
      catch (Exception ex)
      {
        bugSenseLogResult.ResultState = BugSenseResultState.Error;
        bugSenseLogResult.ExceptionError = ex;
      }
      this.SyncMutex.ReleaseMutex();
      return bugSenseLogResult;
    }
  }
}
