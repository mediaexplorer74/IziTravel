// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.DownloadManager
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Exceptions;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Managers.Download;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Business.Managers
{
  public sealed class DownloadManager
  {
    private const string DirectoryNameDownload = "Download";
    private static volatile DownloadManager _instance;
    private static readonly object SyncRoot = new object();
    private static readonly ILog Logger = LogManager.GetLog(typeof (DownloadManager));
    private static readonly IDownloadDataService DataService = IoC.Get<IDownloadDataService>();
    private static readonly object ClearLock = new object();
    private static readonly MtgObjectType[] UpdateMtgObjectTypes = new MtgObjectType[2]
    {
      MtgObjectType.Museum,
      MtgObjectType.Tour
    };
    private Task _taskRestore;
    private ImmutableList<DownloadProcess> _processes;

    public static DownloadManager Instance
    {
      get
      {
        if (DownloadManager._instance == null)
        {
          lock (DownloadManager.SyncRoot)
          {
            if (DownloadManager._instance == null)
              DownloadManager._instance = new DownloadManager();
          }
        }
        return DownloadManager._instance;
      }
    }

    public event TypedEventHandler<DownloadManager, DownloadProcess> DownloadProcessStateChanged
    {
      add
      {
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler1 = this.DownloadProcessStateChanged;
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<DownloadManager, DownloadProcess>>(ref this.DownloadProcessStateChanged, (TypedEventHandler<DownloadManager, DownloadProcess>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler1 = this.DownloadProcessStateChanged;
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<DownloadManager, DownloadProcess>>(ref this.DownloadProcessStateChanged, (TypedEventHandler<DownloadManager, DownloadProcess>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    public event TypedEventHandler<DownloadManager, DownloadProcess> DownloadProcessProgressChanged
    {
      add
      {
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler1 = this.DownloadProcessProgressChanged;
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<DownloadManager, DownloadProcess>>(ref this.DownloadProcessProgressChanged, (TypedEventHandler<DownloadManager, DownloadProcess>) Delegate.Combine((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
      remove
      {
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler1 = this.DownloadProcessProgressChanged;
        TypedEventHandler<DownloadManager, DownloadProcess> typedEventHandler2;
        do
        {
          typedEventHandler2 = typedEventHandler1;
          typedEventHandler1 = Interlocked.CompareExchange<TypedEventHandler<DownloadManager, DownloadProcess>>(ref this.DownloadProcessProgressChanged, (TypedEventHandler<DownloadManager, DownloadProcess>) Delegate.Remove((Delegate) typedEventHandler2, (Delegate) value), typedEventHandler2);
        }
        while (typedEventHandler1 != typedEventHandler2);
      }
    }

    private DownloadManager() => this._processes = ImmutableList<DownloadProcess>.Empty;

    public void Restore()
    {
      this._taskRestore = this._taskRestore == null ? this.RestoreInternalAsync() : throw new BusinessException("Restore have already started");
    }

    public async void DownloadAsync(MtgObject mtgObject, bool isRestored = false)
    {
      try
      {
        if (this._taskRestore != null)
          await this._taskRestore;
        if (mtgObject == null || string.IsNullOrWhiteSpace(mtgObject.Uid) || string.IsNullOrWhiteSpace(mtgObject.Language))
          return;
        DownloadProcess process = this.GetDownloadProcess(mtgObject);
        if (process != null)
        {
          if (process.State != DownloadProcessState.Error)
            return;
          this._processes = this._processes.Remove(process);
        }
        bool isUpdate = mtgObject.AccessType == MtgObjectAccessType.Offline;
        process = this.LoadDownloadProcess(mtgObject.Uid, mtgObject.Language);
        if (process == null)
        {
          process = new DownloadProcess()
          {
            MtgObject = mtgObject
          };
          this.SaveDownloadProcess(process);
        }
        else
          process.IsRestored = isRestored;
        this._processes = this._processes.Add(process);
        this.SetDownloadState(process, isUpdate ? DownloadProcessState.Updating : DownloadProcessState.Downloading);
        this.SetDownloadProgress(process, 0.0);
        process.CancellationTokenSource = new CancellationTokenSource();
        process.Task = DownloadManager.DownloadInternalAsync(process, process.CancellationTokenSource.Token);
        bool flag;
        try
        {
          flag = await process.Task;
        }
        catch (Exception ex)
        {
          DownloadManager.Logger.Error(ex);
          flag = false;
        }
        if (flag)
        {
          if (this._processes.Contains(process))
            this._processes = this._processes.Remove(process);
          IsolatedStorageFileHelper.DeleteFile(this.GetDownloadProcessPath(mtgObject.Uid, mtgObject.Language));
          this.SetDownloadState(process, isUpdate ? DownloadProcessState.Updated : DownloadProcessState.Downloaded);
        }
        else
          this.SetDownloadState(process, DownloadProcessState.Error);
        process.CancellationTokenSource = (CancellationTokenSource) null;
        process.Task = (Task<bool>) null;
        process = (DownloadProcess) null;
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
      }
    }

    public async void RemoveAsync(MtgObject mtgObject)
    {
      try
      {
        if (this._taskRestore != null)
          await this._taskRestore;
        if (mtgObject == null)
          return;
        DownloadProcess process = this.GetDownloadProcess(mtgObject);
        if (process != null)
        {
          if (process.State == DownloadProcessState.Removing || process.State == DownloadProcessState.Removed)
            return;
        }
        else
        {
          process = new DownloadProcess()
          {
            MtgObject = mtgObject,
            Progress = 0.0
          };
          this._processes = this._processes.Add(process);
        }
        this.SetDownloadProgress(process, 0.0);
        this.SetDownloadState(process, DownloadProcessState.Removing);
        if (process.CancellationTokenSource != null)
          process.CancellationTokenSource.Cancel();
        if (process.Task != null)
        {
          int num = await process.Task ? 1 : 0;
          process.Task = (Task<bool>) null;
          process.CancellationTokenSource = (CancellationTokenSource) null;
        }
        this.DeletePackage(process);
        IsolatedStorageFileHelper.DeleteFile(this.GetDownloadProcessPath(process.Uid, process.Language));
        if (await DownloadManager.RemoveInternalAsync(process))
        {
          if (this._processes.Contains(process))
            this._processes = this._processes.Remove(process);
          this.SetDownloadState(process, DownloadProcessState.Removed);
        }
        process = (DownloadProcess) null;
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
      }
    }

    public async Task<bool> CheckUpdateAsync(MtgObject localMtgObject)
    {
      if (localMtgObject == null || localMtgObject.MainContent == null || localMtgObject.AccessType != MtgObjectAccessType.Offline || !((IEnumerable<MtgObjectType>) DownloadManager.UpdateMtgObjectTypes).Contains<MtgObjectType>(localMtgObject.Type))
        return false;
      if (this.GetDownloadProcess(localMtgObject) != null)
        return false;
      MtgObject remoteMtgObject;
      try
      {
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        MtgObjectFilter mtgObjectFilter = new MtgObjectFilter();
        mtgObjectFilter.Uid = localMtgObject.Uid;
        mtgObjectFilter.Languages = new string[1]
        {
          localMtgObject.MainContent.Language
        };
        mtgObjectFilter.Includes = ContentSection.None;
        mtgObjectFilter.Excludes = ContentSection.All;
        mtgObjectFilter.Form = MtgObjectForm.Compact;
        MtgObjectFilter filter = mtgObjectFilter;
        CancellationToken ct = new CancellationToken();
        remoteMtgObject = await mtgObjectService.GetMtgObjectAsync(filter, ct);
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
        return false;
      }
      return remoteMtgObject != null && !string.IsNullOrWhiteSpace(remoteMtgObject.Hash) && !remoteMtgObject.Hash.Equals(localMtgObject.Hash, StringComparison.CurrentCultureIgnoreCase);
    }

    public DownloadProcess GetDownloadProcess(MtgObject mtgObject)
    {
      return mtgObject == null ? (DownloadProcess) null : this._processes.Where<DownloadProcess>((Func<DownloadProcess, bool>) (x => x.MtgObject != null)).FirstOrDefault<DownloadProcess>((Func<DownloadProcess, bool>) (x => x.Key == mtgObject.Key));
    }

    public DownloadProcess[] GetDownloadProcessList()
    {
      return this._processes.Where<DownloadProcess>((Func<DownloadProcess, bool>) (x => x.MtgObject != null)).ToArray<DownloadProcess>();
    }

    public bool CheckMtgObjectDownloadProcess(MtgObject mtgObject, DownloadProcess downloadProcess)
    {
      return mtgObject != null && downloadProcess != null && mtgObject.Key == downloadProcess.Key;
    }

    public Tuple<DownloadProcessState, double> GetMtgObjectDownloadInfo(MtgObject mtgObject)
    {
      if (mtgObject == null || string.IsNullOrWhiteSpace(mtgObject.Uid) || string.IsNullOrWhiteSpace(mtgObject.Language))
        return new Tuple<DownloadProcessState, double>(DownloadProcessState.Unknown, 0.0);
      DownloadProcess downloadProcess = this.GetDownloadProcess(mtgObject);
      if (downloadProcess != null)
        return new Tuple<DownloadProcessState, double>(downloadProcess.State, downloadProcess.Progress);
      DownloadObject downloadObject = DownloadManager.DataService.FindDownloadObject(mtgObject.Uid, mtgObject.Language, new DownloadStatus?());
      return downloadObject == null ? new Tuple<DownloadProcessState, double>(DownloadProcessState.Unknown, 0.0) : new Tuple<DownloadProcessState, double>(downloadObject.Status == DownloadStatus.Completed ? DownloadProcessState.Downloaded : DownloadProcessState.Downloading, 0.0);
    }

    private Task RestoreInternalAsync()
    {
      return Task.Factory.StartNew((Action) (() =>
      {
        string[] fileNames = IsolatedStorageFileHelper.GetFileNames(Path.Combine("Download", "*.download"));
        if (fileNames == null || fileNames.Length == 0)
          return;
        foreach (string path2 in fileNames)
        {
          DownloadProcess downloadProcess = this.LoadDownloadProcess(Path.Combine("Download", path2));
          if (downloadProcess != null && downloadProcess.MtgObject != null)
            this.DownloadAsync(downloadProcess.MtgObject, true);
        }
      }));
    }

    private static async Task<bool> DownloadInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      foreach (DownloadProcessTaskBase downloadProcessTaskBase in (IEnumerable<DownloadProcessTaskBase>) ((IEnumerable<DownloadProcessTaskBase>) new DownloadProcessTaskBase[6]
      {
        (DownloadProcessTaskBase) new DownloadProcessCreatePackageLocalTask(),
        (DownloadProcessTaskBase) new DownloadProcessCreatePackageRemoteTask(),
        (DownloadProcessTaskBase) new DownloadProcessDownloadPackageMediaTask(),
        (DownloadProcessTaskBase) new DownloadProcessCopyPackageMediaTask(),
        (DownloadProcessTaskBase) new DownloadProcessUpdateDataBaseTask(),
        (DownloadProcessTaskBase) new DownloadProcessDeletePackageTask()
      }).OrderBy<DownloadProcessTaskBase, int>((Func<DownloadProcessTaskBase, int>) (x => x.Order)))
      {
        if (!await downloadProcessTaskBase.ProcessAsync(process, token))
          return false;
      }
      return true;
    }

    private static Task<bool> RemoveInternalAsync(DownloadProcess process)
    {
      return Task<bool>.Factory.StartNew((Func<bool>) (() =>
      {
        try
        {
          string[] exclusiveMediaPathList = DownloadManager.DataService.GetDownloadExclusiveMediaPathList(process.Uid, process.Language);
          if (exclusiveMediaPathList != null)
          {
            using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
              foreach (string url in exclusiveMediaPathList)
              {
                string localPath = ServiceFacade.MediaService.GetLocalPath(url);
                if (storeForApplication.FileExists(localPath))
                  storeForApplication.DeleteFile(localPath);
              }
            }
          }
          lock (DownloadManager.ClearLock)
            DownloadManager.DataService.Clear(process.Uid, process.Language);
        }
        catch (Exception ex)
        {
          DownloadManager.Logger.Error(ex);
          return false;
        }
        return true;
      }));
    }

    private void OnDownloadStateChanged(DownloadProcess downloadProcess)
    {
      // ISSUE: reference to a compiler-generated field
      this.DownloadProcessStateChanged?.Invoke(this, downloadProcess);
    }

    private void OnDownloadProgressChanged(DownloadProcess downloadProcess)
    {
      // ISSUE: reference to a compiler-generated field
      this.DownloadProcessProgressChanged?.Invoke(this, downloadProcess);
    }

    internal DownloadProcess LoadDownloadProcess(string uid, string language)
    {
      string downloadProcessPath = this.GetDownloadProcessPath(uid, language);
      return string.IsNullOrWhiteSpace(downloadProcessPath) ? (DownloadProcess) null : this.LoadDownloadProcess(downloadProcessPath);
    }

    internal DownloadProcess LoadDownloadProcess(string path)
    {
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (!storeForApplication.FileExists(path))
            return (DownloadProcess) null;
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(path, FileMode.Open, FileAccess.Read))
            return new DataContractJsonSerializer(typeof (DownloadProcess)).ReadObject((Stream) storageFileStream) as DownloadProcess;
        }
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
        return (DownloadProcess) null;
      }
    }

    internal void SaveDownloadProcess(DownloadProcess downloadProcess)
    {
      if (downloadProcess == null)
        return;
      string downloadProcessPath = this.GetDownloadProcessPath(downloadProcess.Uid, downloadProcess.Language);
      if (string.IsNullOrWhiteSpace(downloadProcessPath))
        return;
      try
      {
        using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
        {
          if (storeForApplication.FileExists(downloadProcessPath))
            storeForApplication.DeleteFile(downloadProcessPath);
          string directoryName = Path.GetDirectoryName(downloadProcessPath);
          if (directoryName != null && !storeForApplication.DirectoryExists(directoryName))
            storeForApplication.CreateDirectory(directoryName);
          using (IsolatedStorageFileStream storageFileStream = storeForApplication.OpenFile(downloadProcessPath, FileMode.CreateNew, FileAccess.ReadWrite))
            new DataContractJsonSerializer(typeof (DownloadProcess)).WriteObject((Stream) storageFileStream, (object) downloadProcess);
        }
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
        throw;
      }
    }

    internal string GetDownloadProcessPath(string uid, string language)
    {
      return string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(language) ? (string) null : Path.Combine("Download", uid + language + ".download");
    }

    internal void SetDownloadState(
      DownloadProcess downloadProcess,
      DownloadProcessState state,
      DownloadProcessError error = DownloadProcessError.None)
    {
      if (downloadProcess == null || downloadProcess.State == state)
        return;
      if (state == DownloadProcessState.Error)
        downloadProcess.Error = downloadProcess.CancellationTokenSource == null || !downloadProcess.CancellationTokenSource.IsCancellationRequested ? error : DownloadProcessError.ProcessCanceled;
      downloadProcess.State = state;
      this.OnDownloadStateChanged(downloadProcess);
    }

    internal void SetDownloadProgress(DownloadProcess downloadProcess, double progress)
    {
      if (downloadProcess == null)
        return;
      downloadProcess.Progress = progress;
      this.OnDownloadProgressChanged(downloadProcess);
    }

    internal string GetDownloadPackagePath(DownloadProcess downloadProcess)
    {
      return downloadProcess == null ? (string) null : this.GetDownloadPackagePath(downloadProcess.Uid, downloadProcess.Language);
    }

    internal string GetDownloadPackagePath(string uid, string language)
    {
      if (string.IsNullOrWhiteSpace(uid) || string.IsNullOrWhiteSpace(language))
        return (string) null;
      return Path.Combine("Download", uid, language);
    }

    internal string GetDownloadPackageItemPath(DownloadProcess downloadProcess, string uid)
    {
      string downloadPackagePath = this.GetDownloadPackagePath(downloadProcess);
      return !string.IsNullOrWhiteSpace(downloadPackagePath) ? Path.Combine(downloadPackagePath, uid + ".json") : (string) null;
    }

    internal bool DeletePackage(DownloadProcess downloadProcess)
    {
      try
      {
        string downloadPackagePath = this.GetDownloadPackagePath(downloadProcess);
        if (!string.IsNullOrWhiteSpace(downloadPackagePath))
          IsolatedStorageFileHelper.DeleteDirectory(downloadPackagePath);
        return true;
      }
      catch (Exception ex)
      {
        DownloadManager.Logger.Error(ex);
        return false;
      }
    }
  }
}
