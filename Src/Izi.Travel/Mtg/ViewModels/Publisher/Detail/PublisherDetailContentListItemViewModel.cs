// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Publisher.Detail.PublisherDetailContentListItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Mtg.ViewModels.Common.List;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Publisher.Detail
{
  public class PublisherDetailContentListItemViewModel : ListItemViewModel
  {
    public PublisherDetailContentListItemViewModel(
      IListViewModel listViewModel,
      MtgObject mtgObject)
      : base(listViewModel, mtgObject)
    {
    }
  }
}
