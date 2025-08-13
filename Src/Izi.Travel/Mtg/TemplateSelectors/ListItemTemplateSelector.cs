// ********************************************************************
// Type: Izi.Travel.Mtg.TemplateSelectors.ListItemTemplateSelector
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Components;
using Izi.Travel.Mtg.ViewModels.Collection.List;
using Izi.Travel.Mtg.ViewModels.Exhibit.List;
using Izi.Travel.Mtg.ViewModels.TouristAttraction.List;
using System;
using Windows.UI.Xaml;
#nullable disable
namespace Izi.Travel.Mtg.TemplateSelectors
{
  public class ListItemTemplateSelector : DataTemplateSelector
  {
    public DataTemplate CollectionListItemTemplate { get; set; }

    public DataTemplate ExhibitListItemTemplate { get; set; }

    public DataTemplate TouristAttractionListItemTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (item != null)
      {
        Type type = item.GetType();
        if (type == typeof (CollectionListItemViewModel))
          return this.CollectionListItemTemplate;
        if (type == typeof (ExhibitListItemViewModel))
          return this.ExhibitListItemTemplate;
        if (type == typeof (TouristAttractionListItemViewModel))
          return this.TouristAttractionListItemTemplate;
      }
      return (DataTemplate) null;
    }
  }
}

