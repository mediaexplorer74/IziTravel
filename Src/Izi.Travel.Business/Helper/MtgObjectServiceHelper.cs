// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Helper.MtgObjectServiceHelper
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using Izi.Travel.Business.Services;
using Izi.Travel.Business.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Helper
{
  public static class MtgObjectServiceHelper
  {
    private static readonly ILog Logger = LogManager.GetLog(typeof (MtgObjectServiceHelper));

    public static async Task<MtgObject> GetMtgObjectAsync(MtgObjectFilter filter)
    {
      MtgObject mtgObject = (MtgObject) null;
      try
      {
        mtgObject = await ServiceFacade.MtgObjectDownloadService.GetMtgObjectAsync(filter);
      }
      catch (Exception ex)
      {
        MtgObjectServiceHelper.Logger.Error(ex);
      }
      if (mtgObject == null)
      {
        try
        {
          mtgObject = await ServiceFacade.MtgObjectService.GetMtgObjectAsync(filter);
        }
        catch (Exception ex)
        {
          MtgObjectServiceHelper.Logger.Error(ex);
        }
      }
      return mtgObject;
    }

    public static async Task<MtgListResult> GetMtgObjectListAsync(MtgObjectListFilter filter)
    {
      MtgListResult result = new MtgListResult();
      try
      {
        MtgObject[] remoteData = await ServiceFacade.MtgObjectService.GetMtgObjectListAsync(filter);
        if (remoteData != null)
        {
          MtgObject[] localData = (MtgObject[]) null;
          try
          {
            IMtgObjectDownloadService objectDownloadService = ServiceFacade.MtgObjectDownloadService;
            MtgObjectListFilter filter1 = new MtgObjectListFilter();
            filter1.UidList = ((IEnumerable<MtgObject>) remoteData).Select<MtgObject, string>((Func<MtgObject, string>) (x => x.Uid)).ToArray<string>();
            filter1.Languages = filter.Languages;
            localData = await objectDownloadService.GetMtgObjectListAsync(filter1);
            result.LocalSuccess = true;
          }
          catch (Exception ex)
          {
            MtgObjectServiceHelper.Logger.Error(ex);
            result.LocalSuccess = false;
          }
          if (result.LocalSuccess && localData != null && localData.Length != 0)
          {
            foreach (MtgObject mtgObject in remoteData)
            {
              if (mtgObject.MainContent == null)
              {
                result.Data.Add(mtgObject);
              }
              else
              {
                string remoteUid = mtgObject.Uid;
                string remoteLanguage = mtgObject.MainContent.Language;
                result.Data.Add(((IEnumerable<MtgObject>) localData).FirstOrDefault<MtgObject>((Func<MtgObject, bool>) (x => x.MainContent != null && x.Uid.Equals(remoteUid, StringComparison.CurrentCultureIgnoreCase) && x.MainContent.Language.Equals(remoteLanguage, StringComparison.CurrentCultureIgnoreCase))) ?? mtgObject);
              }
            }
          }
          else
            result.Data.AddRange((IEnumerable<MtgObject>) remoteData);
          result.RemoteSuccess = true;
          localData = (MtgObject[]) null;
        }
        remoteData = (MtgObject[]) null;
      }
      catch (Exception ex)
      {
        MtgObjectServiceHelper.Logger.Error(ex);
      }
      if (!result.RemoteSuccess)
      {
        try
        {
          MtgObject[] mtgObjectListAsync = await ServiceFacade.MtgObjectDownloadService.GetMtgObjectListAsync(filter);
          if (mtgObjectListAsync != null)
          {
            if (mtgObjectListAsync.Length != 0)
            {
              foreach (MtgObject mtgObject in ((IEnumerable<MtgObject>) mtgObjectListAsync).Where<MtgObject>((Func<MtgObject, bool>) (localObject => result.Data.All<MtgObject>((Func<MtgObject, bool>) (x => x.Uid != localObject.Uid)))))
                result.Data.Add(mtgObject);
            }
          }
        }
        catch (Exception ex)
        {
          MtgObjectServiceHelper.Logger.Error(ex);
          result.LocalSuccess = false;
        }
      }
      return result;
    }

    public static async Task<MtgObject[]> GetMtgObjectChildrenAsync(MtgObjectChildrenFilter filter)
    {
      MtgObject[] result = (MtgObject[]) null;
      try
      {
        result = await ServiceFacade.MtgObjectDownloadService.GetMtgObjectChildrenAsync(filter);
      }
      catch (Exception ex)
      {
        MtgObjectServiceHelper.Logger.Error(ex);
      }
      if (result == null)
      {
        try
        {
          result = await ServiceFacade.MtgObjectService.GetMtgObjectChildrenAsync(filter);
        }
        catch (Exception ex)
        {
          MtgObjectServiceHelper.Logger.Error(ex);
        }
      }
      return result;
    }

    public static async Task<MtgChildrenListResult> GetMtgObjectChildrenExtendedAsync(
      MtgObjectChildrenExtendedFilter filter)
    {
      MtgChildrenListResult result = (MtgChildrenListResult) null;
      try
      {
        result = await ServiceFacade.MtgObjectDownloadService.GetMtgObjectChildrenExtendedAsync(filter);
      }
      catch (Exception ex)
      {
        MtgObjectServiceHelper.Logger.Error(ex);
      }
      if (result == null)
      {
        try
        {
          result = await ServiceFacade.MtgObjectService.GetMtgObjectChildrenExtendedAsync(filter);
        }
        catch (Exception ex)
        {
          MtgObjectServiceHelper.Logger.Error(ex);
        }
      }
      return result;
    }
  }
}
