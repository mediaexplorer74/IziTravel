// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Services.Contract.IMtgObjectService
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Entities.Filters;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Services.Contract
{
  public interface IMtgObjectService
  {
    Task<MtgObject[]> GetFeaturedListAsync(string[] languages, CancellationToken ct = default (CancellationToken));

    Task<MtgObject[]> GetMtgObjectListAsync(MtgObjectListFilter filter, CancellationToken ct = default (CancellationToken));

    Task<MtgObject[]> GetMtgObjectChildrenAsync(
      MtgObjectChildrenFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<MtgChildrenListResult> GetMtgObjectChildrenExtendedAsync(
      MtgObjectChildrenExtendedFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<MtgObject[]> GetPublisherChildrenAsync(
      MtgPublisherChildrenFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<int> GetMtgObjectChildrenCountAsync(
      MtgObjectChildrenCountFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<int> GetPublisherChildrenCountAsync(
      MtgPublisherChildrenCountFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<MtgObject> GetMtgObjectAsync(MtgObjectFilter filter, CancellationToken ct = default (CancellationToken));

    Task<MtgObject> GetPublisherAsync(MtgObjectFilter filter, CancellationToken ct = default (CancellationToken));

    Task<string> GetProductIdAsync(ProductIdFilter filter, CancellationToken ct = default (CancellationToken));

    Task<MtgObject[]> GetPaidMtgObjectListAsync(
      PaidMtgObjectListFilter filter,
      CancellationToken ct = default (CancellationToken));

    Task<Review[]> GetReviewsAsync(GetReviewsFilter filter, CancellationToken ct = default (CancellationToken));

    Task PostReviewAsync(PostReviewFilter filter, CancellationToken ct = default (CancellationToken));

    Task CreatePurchaseAsync(MtgObject mtgObject);

    Task<MtgObject[]> GetPurchaseListAsync(ListFilter filter);

    Task CreateBookmarkAsync(MtgObject mtgObject, string parentUid = null);

    Task<bool> IsBookmarkExistsForMtgObjectAsync(MtgObjectFilter mtgObjectFilter);

    Task RemoveBookmarkAsync(MtgObjectFilter mtgObjectFilter);

    Task<MtgObject[]> GetBookmarkListAsync(MtgObjectListFilter filter);

    Task ClearBookmarkListAsync();

    Task CreateOrUpdateHistoryAsync(MtgObject mtgObject, string parentUid = null);

    Task<MtgObject[]> GetHistoryListAsync(HistoryListFilter filter);

    Task ClearHistoryListAsync();
  }
}
