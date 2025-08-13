// ********************************************************************
// Type: Izi.Travel.Mtg.ViewModels.Exhibit.List.ExhibitListItemViewModel
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Common.ViewModels.List;
using Izi.Travel.Mtg.ViewModels.Common.List;

#nullable disable
namespace Izi.Travel.Mtg.ViewModels.Exhibit.List
{
  public class ExhibitListItemViewModel : ListItemViewModel
  {
    public string FullTitle => string.Format("{0}. {1}", (object) this.Number, (object) this.Title);

    protected Location Location
    {
      get => this.MtgObject == null ? (Location) null : this.MtgObject.Location;
    }

    public string Number => this.Location == null ? string.Empty : this.Location.Number;

    public ExhibitListItemViewModel(IListViewModel listViewModel, MtgObject mtgObject)
      : base(listViewModel, mtgObject)
    {
    }
  }
}
