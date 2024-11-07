// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessDownloadPackageMediaTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Exceptions;
using Izi.Travel.Business.Helper;
using Izi.Travel.Business.Services;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessDownloadPackageMediaTask : DownloadProcessTaskBase
  {
    private const int MaxFileDownloadAttemptCount = 3;

    public override int Order => 3;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageMediaDownload;

    protected override double StepOverallProgress => 0.5;

    protected override async Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      try
      {
        List<string> downloadedFiles = new List<string>();
        string packageParentPath = DownloadManager.Instance.GetDownloadPackageItemPath(process, process.Uid);
        DownloadPackageItem packageParent = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(packageParentPath);
        if (packageParent == null)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDownload);
          return false;
        }
        double progressDelta = 0.5 / (double) (packageParent.RemoteChildrenUidList.Count + 1);
        if (!await this.DownloadItemMediaAsync(downloadedFiles, process, packageParent, progressDelta, token))
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDownload);
          return false;
        }
        await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(packageParentPath, packageParent);
        foreach (string remoteChildrenUid in packageParent.RemoteChildrenUidList)
        {
          string packageChildPath = DownloadManager.Instance.GetDownloadPackageItemPath(process, remoteChildrenUid);
          DownloadPackageItem packageChild = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(packageChildPath);
          if (packageChild == null)
          {
            DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDownload);
            return false;
          }
          if (!await this.DownloadItemMediaAsync(downloadedFiles, process, packageChild, progressDelta, token))
          {
            DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDownload);
            return false;
          }
          await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(packageChildPath, packageChild);
          packageChildPath = (string) null;
          packageChild = (DownloadPackageItem) null;
        }
        return true;
      }
      catch (BusinessDownloadProcessException ex)
      {
        this.Logger.Error((Exception) ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, ex.Error);
        return false;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDownload);
        return false;
      }
    }

    private async Task<bool> DownloadItemMediaAsync(
      List<string> downloadedFiles,
      DownloadProcess downloadProcess,
      DownloadPackageItem packageItem,
      double progressItemDelta,
      CancellationToken token = default (CancellationToken))
    {
      if (token.IsCancellationRequested || packageItem == null || packageItem.RemoteObject == null || packageItem.RemoteObject.ContentProvider == null)
        return false;
      if (packageItem.Action == DownloadPackageItemAction.Ignore)
        return true;
      string packageDirectory = DownloadManager.Instance.GetDownloadPackagePath(downloadProcess);
      packageItem.MediaItems.Clear();
      string[] array = ((IEnumerable<Media>) MediaHelper.GetMediaList(packageItem.RemoteObject)).SelectMany<Media, string>((Func<Media, IEnumerable<string>>) (x => (IEnumerable<string>) MediaHelper.GetMediaUrlList(x, packageItem.RemoteObject.ContentProvider.Uid))).Distinct<string>().ToArray<string>();
      if (array.Length == 0)
        return true;
      double progressDelta = progressItemDelta / (double) array.Length;
      string[] strArray = array;
label_17:
      for (int index = 0; index < strArray.Length; ++index)
      {
        string mediaUrl = strArray[index];
        if (token.IsCancellationRequested)
          return false;
        DownloadManager.Instance.SetDownloadProgress(downloadProcess, downloadProcess.Progress + progressDelta);
        int attemptCount = 0;
        while (!token.IsCancellationRequested)
        {
          Tuple<bool, DownloadProcessError> result = await this.DownloadFileMediaAsync(downloadedFiles, packageDirectory, mediaUrl, packageItem, token);
          if (!result.Item1)
          {
            if (attemptCount >= 3)
              throw new BusinessDownloadProcessException(result.Item2);
            await Task.Delay(500, token);
            ++attemptCount;
            result = (Tuple<bool, DownloadProcessError>) null;
          }
          else
          {
            mediaUrl = (string) null;
            goto label_17;
          }
        }
        return false;
      }
      strArray = (string[]) null;
      return true;
    }

    private async Task<Tuple<bool, DownloadProcessError>> DownloadFileMediaAsync(
      List<string> downloadedFiles,
      string packageDirectory,
      string url,
      DownloadPackageItem packageItem,
      CancellationToken token = default (CancellationToken))
    {
      if (token.IsCancellationRequested)
        return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.ProcessCanceled);
      string str = Path.Combine(packageDirectory, ServiceFacade.MediaService.GetLocalPath(url));
      if (string.IsNullOrWhiteSpace(str))
        return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.PackageDownload);
      if (packageItem != null)
      {
        lock (packageItem.MediaItems)
          packageItem.MediaItems.Add(new DownloadPackageMediaItem()
          {
            Url = url,
            File = str
          });
      }
      lock (downloadedFiles)
      {
        if (downloadedFiles.Contains(url))
          return new Tuple<bool, DownloadProcessError>(true, DownloadProcessError.None);
      }
      string filePath = str + ".media";
      string etagPath = str + ".etag";
      try
      {
        string etagLocal = await IsolatedStorageFileHelper.DeserializeAsync<string>(etagPath);
        using (HttpClient httpClient = new HttpClient())
        {
          httpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue()
          {
            NoCache = true
          };
          if (token.IsCancellationRequested)
            return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.ProcessCanceled);
          HttpResponseMessage async = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, token);
          if (!async.IsSuccessStatusCode)
            return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.PackageDownloadConnectionFailed);
          EntityTagHeaderValue etagRemote = async.Headers.ETag;
          if (etagRemote != null && etagRemote.Tag == etagLocal)
            return new Tuple<bool, DownloadProcessError>(true, DownloadProcessError.None);
          if (async.Content == null)
            return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.PackageDownload);
          long? contentLength = async.Content.Headers.ContentLength;
          if (contentLength.HasValue)
          {
            using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
            {
              if (storeForApplication.AvailableFreeSpace < contentLength.Value + 10485760L)
                return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.PackageDownloadNotEnoughSpace);
            }
          }
          if (token.IsCancellationRequested)
            return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.ProcessCanceled);
          using (IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication())
          {
            using (Stream contentStream = await async.Content.ReadAsStreamAsync())
            {
              string directoryName = Path.GetDirectoryName(filePath);
              if (directoryName != null && !isolatedStorageFile.DirectoryExists(directoryName))
                isolatedStorageFile.CreateDirectory(directoryName);
              using (IsolatedStorageFileStream fileStream = isolatedStorageFile.CreateFile(filePath))
                await contentStream.CopyToAsync((Stream) fileStream, 1048576, token);
            }
          }
          if (etagRemote != null)
            await IsolatedStorageFileHelper.SerializeAsync<string>(etagPath, etagRemote.Tag);
          lock (downloadedFiles)
          {
            if (!downloadedFiles.Contains(url))
              downloadedFiles.Add(url);
          }
          return new Tuple<bool, DownloadProcessError>(true, DownloadProcessError.None);
        }
      }
      catch (Exception ex)
      {
        try
        {
          IsolatedStorageFileHelper.DeleteFile(etagPath);
          IsolatedStorageFileHelper.DeleteFile(filePath);
        }
        catch
        {
        }
        this.Logger.Error(ex);
        return new Tuple<bool, DownloadProcessError>(false, DownloadProcessError.PackageDownload);
      }
    }
  }
}
