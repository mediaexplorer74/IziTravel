// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.IMtgObjectDownloadService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface IMtgObjectDownloadService
  {
    Task<int> GetMtgObjectCountAsync(MtgObjectListFilter filter);

    Task<MtgObject[]> GetMtgObjectListAsync(MtgObjectListFilter filter);

    Task<MtgObject[]> GetMtgObjectChildrenAsync(MtgObjectChildrenFilter filter);

    Task<MtgChildrenListResult> GetMtgObjectChildrenExtendedAsync(
      MtgObjectChildrenExtendedFilter filter);

    Task<MtgObject> GetMtgObjectAsync(MtgObjectFilter filter);
  }
}
