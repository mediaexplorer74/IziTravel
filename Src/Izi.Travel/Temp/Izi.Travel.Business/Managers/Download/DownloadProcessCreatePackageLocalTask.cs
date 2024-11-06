// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessCreatePackageLocalTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Data.Entities.Download;
using Izi.Travel.Data.Entities.Download.Query;
using Izi.Travel.Data.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessCreatePackageLocalTask : DownloadProcessTaskBase
  {
    private static readonly IDownloadDataService DataService = IoC.Get<IDownloadDataService>();

    public override int Order => 1;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageLocalCreate;

    protected override double StepOverallProgress => 0.05;

    protected override async Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      try
      {
        DownloadObject downloadObject = DownloadProcessCreatePackageLocalTask.DataService.FindDownloadObject(process.Uid, process.Language, new DownloadStatus?());
        double progressDelta = this.StepOverallProgress;
        if (downloadObject != null)
        {
          IDownloadDataService dataService = DownloadProcessCreatePackageLocalTask.DataService;
          DownloadObjectChildrenQuery query = new DownloadObjectChildrenQuery()
          {
            ParentId = new int?(downloadObject.Id),
            Languages = new string[1]
            {
              downloadObject.Language
            }
          };
          progressDelta /= (double) (dataService.GetDownloadObjectChildrenCount(query) + 1);
        }
        DownloadProcessCreatePackageLocalTask.CreatePackageItemResult createParentItemResult;
        DownloadProcessCreatePackageLocalTask.CreatePackageItemResult packageItemResult = createParentItemResult;
        createParentItemResult = await this.CreatePackageItemLocalAsync(process, DownloadObjectInfo.FromDownloadObject(downloadObject), token);
        if (!createParentItemResult.Success)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateLocal);
          return false;
        }
        DownloadManager.Instance.SetDownloadProgress(process, process.Progress + progressDelta);
        if (createParentItemResult.Item == null)
          return true;
        createParentItemResult.Item.LocalChidlrenUidList.Clear();
        bool hasChildrenError = false;
        await DownloadProcessCreatePackageLocalTask.ProcessLocalChildrenAsync(createParentItemResult.Item.LocalObject, new DownloadObjectType[3]
        {
          DownloadObjectType.Collection,
          DownloadObjectType.Exhibit,
          DownloadObjectType.TouristAttraction
        }, (Func<DownloadObject, Task>) (async x =>
        {
          if (token.IsCancellationRequested | hasChildrenError)
            return;
          try
          {
            createParentItemResult.Item.LocalChidlrenUidList.Add(x.Uid, x.Id);
            DownloadProcessCreatePackageLocalTask.CreatePackageItemResult packageItemLocalAsync = await this.CreatePackageItemLocalAsync(process, DownloadObjectInfo.FromDownloadObject(x), token);
            if (!packageItemLocalAsync.Success)
            {
              hasChildrenError = true;
            }
            else
            {
              await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(packageItemLocalAsync.ItemPath, packageItemLocalAsync.Item);
              DownloadManager.Instance.SetDownloadProgress(process, process.Progress + progressDelta);
            }
          }
          catch (Exception ex)
          {
            this.Logger.Error(ex);
            DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateLocal);
            hasChildrenError = true;
          }
        }), token);
        if (token.IsCancellationRequested)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.ProcessCanceled);
          return false;
        }
        if (hasChildrenError)
        {
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateLocal);
          return false;
        }
        await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(createParentItemResult.ItemPath, createParentItemResult.Item);
        return true;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateLocal);
        return false;
      }
    }

    private async Task<DownloadProcessCreatePackageLocalTask.CreatePackageItemResult> CreatePackageItemLocalAsync(
      DownloadProcess downloadProcess,
      DownloadObjectInfo downloadObject,
      CancellationToken token = default (CancellationToken))
    {
      DownloadProcessCreatePackageLocalTask.CreatePackageItemResult result = new DownloadProcessCreatePackageLocalTask.CreatePackageItemResult();
      if (downloadObject == null)
      {
        result.Success = true;
        return result;
      }
      try
      {
        result.ItemPath = DownloadManager.Instance.GetDownloadPackageItemPath(downloadProcess, downloadObject.Uid);
        DownloadPackageItem packageItem;
        DownloadPackageItem downloadPackageItem = packageItem;
        packageItem = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(result.ItemPath);
        if (packageItem != null && packageItem.LocalObject != null && downloadObject.Hash == packageItem.LocalObject.Hash)
        {
          result.Item = packageItem;
          result.Success = true;
          return result;
        }
        packageItem = new DownloadPackageItem()
        {
          LocalObject = downloadObject,
          Action = DownloadPackageItemAction.Delete
        };
        result.Item = packageItem;
        if (downloadObject.Type == DownloadObjectType.Collection)
          await DownloadProcessCreatePackageLocalTask.ProcessLocalChildrenAsync(downloadObject, new DownloadObjectType[2]
          {
            DownloadObjectType.Exhibit,
            DownloadObjectType.StoryNavigation
          }, (Func<DownloadObject, Task>) (x => Task.Factory.StartNew((Action) (() => packageItem.LocalChidlrenUidList.Add(x.Uid, x.Id)), token)), token);
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

    private static async Task ProcessLocalChildrenAsync(
      DownloadObjectInfo downloadObject,
      DownloadObjectType[] types,
      Func<DownloadObject, Task> action,
      CancellationToken token = default (CancellationToken))
    {
      if (token.IsCancellationRequested || downloadObject == null)
        return;
      DownloadObjectChildrenQuery downloadObjectChildrenQuery = new DownloadObjectChildrenQuery()
      {
        Offset = new int?(0),
        Limit = new int?(50),
        ParentId = new int?(downloadObject.Id),
        Types = types,
        Languages = new string[1]{ downloadObject.Language }
      };
      while (!token.IsCancellationRequested)
      {
        DownloadObject[] downloadObjectChildren = DownloadProcessCreatePackageLocalTask.DataService.GetDownloadObjectChildren(downloadObjectChildrenQuery);
        if (downloadObjectChildren == null || downloadObjectChildren.Length == 0)
          break;
        if (action != null)
        {
          DownloadObject[] downloadObjectArray = downloadObjectChildren;
          for (int index = 0; index < downloadObjectArray.Length; ++index)
          {
            DownloadObject downloadObject1 = downloadObjectArray[index];
            if (token.IsCancellationRequested)
              return;
            await action(downloadObject1);
          }
          downloadObjectArray = (DownloadObject[]) null;
        }
        DownloadObjectChildrenQuery objectChildrenQuery = downloadObjectChildrenQuery;
        int? offset = objectChildrenQuery.Offset;
        int length = downloadObjectChildren.Length;
        objectChildrenQuery.Offset = offset.HasValue ? new int?(offset.GetValueOrDefault() + length) : new int?();
        downloadObjectChildren = (DownloadObject[]) null;
      }
    }

    private class CreatePackageItemResult
    {
      public bool Success { get; set; }

      public DownloadPackageItem Item { get; set; }

      public string ItemPath { get; set; }
    }
  }
}
