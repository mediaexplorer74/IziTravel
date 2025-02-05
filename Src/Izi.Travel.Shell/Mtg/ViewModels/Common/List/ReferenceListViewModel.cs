// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.List.ReferenceListViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Common.ViewModels.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.List
{
  public class ReferenceListViewModel : ChildrenListViewModel<ReferenceListItemViewModel>
  {
    protected override Task<IEnumerable<ReferenceListItemViewModel>> GetDataAsync()
    {
      return Task<IEnumerable<ReferenceListItemViewModel>>.Factory.StartNew((Func<IEnumerable<ReferenceListItemViewModel>>) (() =>
      {
        if (this.MtgObject != null && this.MtgObject.MainContent != null && this.MtgObject.MainContent.References != null)
        {
          if (this.MtgObject.MainContent.References.Length != 0)
          {
            try
            {
              return ((IEnumerable<MtgObject>) this.MtgObject.MainContent.References).Select<MtgObject, ReferenceListItemViewModel>((Func<MtgObject, ReferenceListItemViewModel>) (x => new ReferenceListItemViewModel((IListViewModel) this, x)));
            }
            catch (Exception ex)
            {
              this.Logger.Error(ex);
              return (IEnumerable<ReferenceListItemViewModel>) null;
            }
          }
        }
        return (IEnumerable<ReferenceListItemViewModel>) null;
      }));
    }
  }
}
