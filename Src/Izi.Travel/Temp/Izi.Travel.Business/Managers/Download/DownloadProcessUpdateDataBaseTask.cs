// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessUpdateDataBaseTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Extensions;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessUpdateDataBaseTask : DownloadProcessTaskBase
  {
    private static readonly IDownloadDataService DataService = IoC.Get<IDownloadDataService>();

    public override int Order => 5;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageDataBaseUpdate;

    protected override double StepOverallProgress => 0.05;

    protected override async Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      try
      {
        List<DownloadBatchItem> batchItems = new List<DownloadBatchItem>();
        DownloadProcessUpdateDataBaseTask.CreateUpdateItemResult updateItemAsync1 = await this.CreateUpdateItemAsync(process, process.Uid);
        if (!updateItemAsync1.Success)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDataBaseUpdate);
          return false;
        }
        process.MtgObject = updateItemAsync1.PackageItem.RemoteObject;
        DownloadManager.Instance.SaveDownloadProcess(process);
        if (updateItemAsync1.Item == null)
          return true;
        batchItems.Add(updateItemAsync1.Item);
        if (updateItemAsync1.PackageItem == null)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDataBaseUpdate);
          return false;
        }
        foreach (string uid in updateItemAsync1.PackageItem.LocalChidlrenUidList.Keys.Union<string>((IEnumerable<string>) updateItemAsync1.PackageItem.RemoteChildrenUidList).Distinct<string>())
        {
          DownloadProcessUpdateDataBaseTask.CreateUpdateItemResult updateItemAsync2 = await this.CreateUpdateItemAsync(process, uid);
          if (!updateItemAsync2.Success)
          {
            DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDataBaseUpdate);
            return false;
          }
          if (updateItemAsync2.Item != null)
            batchItems.Add(updateItemAsync2.Item);
        }
        if (token.IsCancellationRequested)
          return false;
        DownloadProcessUpdateDataBaseTask.DataService.DownloadBatchUpdate(batchItems.ToArray());
        return true;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageDataBaseUpdate);
        return false;
      }
    }

    private async Task<DownloadProcessUpdateDataBaseTask.CreateUpdateItemResult> CreateUpdateItemAsync(
      DownloadProcess downloadProcess,
      string uid)
    {
      DownloadProcessUpdateDataBaseTask.CreateUpdateItemResult result = new DownloadProcessUpdateDataBaseTask.CreateUpdateItemResult();
      try
      {
        DownloadPackageItem packageItem = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(DownloadManager.Instance.GetDownloadPackageItemPath(downloadProcess, uid));
        if (packageItem == null)
        {
          result.Success = false;
          return result;
        }
        result.PackageItem = packageItem;
        if (packageItem.Action == DownloadPackageItemAction.Ignore)
        {
          result.Success = true;
          return result;
        }
        DownloadBatchItem batchItem = new DownloadBatchItem()
        {
          Action = DownloadProcessUpdateDataBaseTask.GetDownloadBatchItemAction(packageItem.Action)
        };
        if (batchItem.Action == DownloadBatchItemAction.Create)
          batchItem.DownloadObject = packageItem.RemoteObject.ToDownloadObject();
        else if (batchItem.Action == DownloadBatchItemAction.Update)
        {
          batchItem.DownloadObject = packageItem.RemoteObject.ToDownloadObject();
          batchItem.DownloadObject.Id = packageItem.LocalObject.Id;
        }
        else if (batchItem.Action == DownloadBatchItemAction.Delete)
          batchItem.DownloadObject = new DownloadObject()
          {
            Id = packageItem.LocalObject.Id
          };
        batchItem.DownloadObject.Status = DownloadStatus.Completed;
        foreach (string childUid in packageItem.RemoteChildrenUidList)
        {
          int localId = 0;
          DownloadPackageItem downloadPackageItem = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(DownloadManager.Instance.GetDownloadPackageItemPath(downloadProcess, childUid));
          if (downloadPackageItem != null && downloadPackageItem.LocalObject != null && downloadPackageItem.LocalObject.Id > 0)
            localId = downloadPackageItem.LocalObject.Id;
          batchItem.Children.Add(childUid, localId);
        }
        batchItem.DownloadMediaList.AddRange(packageItem.MediaItems.Where<DownloadPackageMediaItem>((Func<DownloadPackageMediaItem, bool>) (x => x != null)).Select<DownloadPackageMediaItem, DownloadMedia>((Func<DownloadPackageMediaItem, DownloadMedia>) (x => new DownloadMedia()
        {
          Object = batchItem.DownloadObject,
          Path = x.Url,
          Status = DownloadStatus.Completed
        })));
        result.Item = batchItem;
        result.Success = true;
        return result;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        result.Success = false;
        return result;
      }
    }

    private static DownloadBatchItemAction GetDownloadBatchItemAction(
      DownloadPackageItemAction action)
    {
      if (action == DownloadPackageItemAction.Create)
        return DownloadBatchItemAction.Create;
      return action == DownloadPackageItemAction.Delete ? DownloadBatchItemAction.Delete : DownloadBatchItemAction.Update;
    }

    private class CreateUpdateItemResult
    {
      public bool Success { get; set; }

      public DownloadBatchItem Item { get; set; }

      public DownloadPackageItem PackageItem { get; set; }
    }
  }
}
