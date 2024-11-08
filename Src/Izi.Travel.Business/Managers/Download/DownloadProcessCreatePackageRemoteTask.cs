// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessCreatePackageRemoteTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Entities.Exceptions;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using Izi.Travel.Utility;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessCreatePackageRemoteTask : DownloadProcessTaskBase
  {
    public override int Order => 2;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageRemoteCreate;

    protected override double StepOverallProgress => 0.2;

    protected override async Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      try
      {
        IMtgObjectService mtgObjectService = ServiceFacade.MtgObjectService;
        MtgObjectFilter filter = new MtgObjectFilter(process.Uid, new string[1]
        {
          process.Language
        });
        filter.Includes = ContentSection.References;
        filter.IncludeAudioDuration = true;
        filter.IncludeChildrenCountInFullForm = true;
        CancellationToken ct = token;
        MtgObject mtgObjectParent = await mtgObjectService.GetMtgObjectAsync(filter, ct);
        double progressDelta = this.StepOverallProgress;
        if (mtgObjectParent != null)
        {
          int childrenCountAsync = await ServiceFacade.MtgObjectService.GetMtgObjectChildrenCountAsync(new MtgObjectChildrenCountFilter()
          {
            Uid = mtgObjectParent.Uid,
            Languages = new string[1]
            {
              mtgObjectParent.Language
            },
            Types = new MtgObjectType[3]
            {
              MtgObjectType.Collection,
              MtgObjectType.Exhibit,
              MtgObjectType.TouristAttraction
            }
          }, token);
          progressDelta /= (double) (childrenCountAsync + 1);
        }
        DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult createParentItemResult = default;
        DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult packageItemResult
                    = createParentItemResult;
        createParentItemResult = await this.CreatePackageItemRemoteAsync(
            process, (DownloadPackageItem) null, mtgObjectParent);
        if (!createParentItemResult.Success)
          return false;
        DownloadManager.Instance.SetDownloadProgress(process, process.Progress + progressDelta);
        createParentItemResult.Item.RemoteChildrenUidList.Clear();
        if (!await this.ProcessRemoteChildrenAsync(mtgObjectParent, new MtgObjectType[3]
        {
          MtgObjectType.Collection,
          MtgObjectType.Exhibit,
          MtgObjectType.TouristAttraction
        }, MtgObjectForm.Full, ContentSection.References, (Func<MtgObject, Task<bool>>) (async mtgObjectChild =>
        {
          createParentItemResult.Item.RemoteChildrenUidList.Add(mtgObjectChild.Uid);
          DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult packageItemRemoteAsync = await this.CreatePackageItemRemoteAsync(process, createParentItemResult.Item, mtgObjectChild);
          if (!packageItemRemoteAsync.Success)
            return false;
          await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(packageItemRemoteAsync.ItemPath, packageItemRemoteAsync.Item);
          DownloadManager.Instance.SetDownloadProgress(process, process.Progress + progressDelta);
          return true;
        }), token))
          return false;
        await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(createParentItemResult.ItemPath, createParentItemResult.Item);
        return true;
      }
      catch (BusinessConnectionFailedException ex)
      {
        this.Logger.Error((Exception) ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateRemoteConnectionFailed);
        return false;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCreateRemote);
        return false;
      }
    }

    private async Task<DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult> CreatePackageItemRemoteAsync(
      DownloadProcess downloadProcess,
      DownloadPackageItem parentItem,
      MtgObject mtgObject)
    {
      DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult result = new DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult()
      {
        Success = true
      };
      if (mtgObject == null)
        return result;
      result.ItemPath = DownloadManager.Instance.GetDownloadPackageItemPath(downloadProcess, mtgObject.Uid);
      try
      {
        DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult packageItemResult = result;
        DownloadPackageItem downloadPackageItem = await IsolatedStorageFileHelper.DeserializeAsync<DownloadPackageItem>(result.ItemPath);
        if (downloadPackageItem == null)
          downloadPackageItem = new DownloadPackageItem()
          {
            Action = DownloadPackageItemAction.Create
          };
        packageItemResult.Item = downloadPackageItem;
        packageItemResult = (DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult) null;
        if (result.Item.RemoteObject != null && mtgObject.Hash == result.Item.RemoteObject.Hash)
          return result;
        result.Item.RemoteObject = mtgObject;
        if (result.Item.LocalObject != null)
        {
          if (result.Item.LocalObject.Hash == mtgObject.Hash)
          {
            result.Item.Action = DownloadPackageItemAction.Ignore;
            return result;
          }
          result.Item.Action = DownloadPackageItemAction.Update;
        }
        if (mtgObject.Type == MtgObjectType.Collection)
        {
          packageItemResult = result;
          int num = await this.ProcessRemoteChildrenAsync(mtgObject, new MtgObjectType[2]
          {
            MtgObjectType.Exhibit,
            MtgObjectType.StoryNavigation
          }, MtgObjectForm.Full, ContentSection.None, (Func<MtgObject, Task<bool>>) (async x =>
          {
            if (x.Type == MtgObjectType.StoryNavigation)
            {
              DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult packageItemRemoteAsync = await this.CreatePackageItemRemoteAsync(downloadProcess, result.Item, x);
              if (!packageItemRemoteAsync.Success)
                return false;
              await IsolatedStorageFileHelper.SerializeAsync<DownloadPackageItem>(packageItemRemoteAsync.ItemPath, packageItemRemoteAsync.Item);
              parentItem?.RemoteChildrenUidList.Add(x.Uid);
            }
            result.Item.RemoteChildrenUidList.Add(x.Uid);
            return true;
          })) ? 1 : 0;
          packageItemResult.Success = num != 0;
          packageItemResult = (DownloadProcessCreatePackageRemoteTask.CreatePackageItemResult) null;
        }
        return result;
      }
      catch (BusinessConnectionFailedException ex)
      {
        this.Logger.Error((Exception) ex);
        DownloadManager.Instance.SetDownloadState(downloadProcess, DownloadProcessState.Error, DownloadProcessError.PackageCreateRemoteConnectionFailed);
        result.Success = false;
        return result;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        DownloadManager.Instance.SetDownloadState(downloadProcess, DownloadProcessState.Error, DownloadProcessError.PackageCreateRemote);
        result.Success = false;
        return result;
      }
    }

    private async Task<bool> ProcessRemoteChildrenAsync(
      MtgObject mtgObject,
      MtgObjectType[] types,
      MtgObjectForm form,
      ContentSection includes,
      Func<MtgObject, Task<bool>> process,
      CancellationToken token = default (CancellationToken))
    {
      if (token.IsCancellationRequested || mtgObject == null)
        return false;
      MtgObjectChildrenFilter objectChildrenFilter1 = new MtgObjectChildrenFilter();
      objectChildrenFilter1.Offset = new int?(0);
      objectChildrenFilter1.Limit = new int?(50);
      objectChildrenFilter1.Uid = mtgObject.Uid;
      objectChildrenFilter1.Languages = new string[1]
      {
        mtgObject.Language
      };
      objectChildrenFilter1.Types = types;
      objectChildrenFilter1.Form = form;
      objectChildrenFilter1.Includes = includes;
      objectChildrenFilter1.ShowHidden = false;
      objectChildrenFilter1.IncludeAudioDuration = true;
      objectChildrenFilter1.IncludeChildrenCountInFullForm = true;
      MtgObjectChildrenFilter mtgObjectChildrenFilter = objectChildrenFilter1;
      while (!token.IsCancellationRequested)
      {
        try
        {
          MtgObject[] mtgObjectChildren = await ServiceFacade.MtgObjectService.GetMtgObjectChildrenAsync(mtgObjectChildrenFilter, token);
          if (mtgObjectChildren != null)
          {
            if (mtgObjectChildren.Length != 0)
            {
              if (process != null)
              {
                MtgObject[] mtgObjectArray = mtgObjectChildren;
                for (int index = 0; index < mtgObjectArray.Length; ++index)
                {
                  MtgObject mtgObject1 = mtgObjectArray[index];
                  if (token.IsCancellationRequested)
                    return false;
                  if (!await process(mtgObject1))
                    return false;
                }
                mtgObjectArray = (MtgObject[]) null;
              }
              MtgObjectChildrenFilter objectChildrenFilter2 = mtgObjectChildrenFilter;
              int? offset = objectChildrenFilter2.Offset;
              int length = mtgObjectChildren.Length;
              objectChildrenFilter2.Offset = offset.HasValue ? new int?(offset.GetValueOrDefault() + length) : new int?();
              mtgObjectChildren = (MtgObject[]) null;
              continue;
            }
          }
        }
        catch (Exception ex)
        {
          this.Logger.Error(ex);
          return false;
        }
        return true;
      }
      return false;
    }

    private class CreatePackageItemResult
    {
      public bool Success { get; set; }

      public DownloadPackageItem Item { get; set; }

      public string ItemPath { get; set; }
    }
  }
}
