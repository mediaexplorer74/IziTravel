// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Collection.List.CollectionListItemViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using Izi.Travel.Shell.Mtg.ViewModels.Common.List;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Collection.List
{
  public class CollectionListItemViewModel : ListItemViewModel
  {
    public CollectionListItemViewModel(IListViewModel listViewModel, MtgObject mtgObject)
      : base(listViewModel, mtgObject)
    {
    }
  }
}
