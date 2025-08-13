// ********************************************************************
// Type: Izi.Travel.TemplateSelectors.QuickAccess.QuickAccessTemplateSelector
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Components;
using Izi.Travel.ViewModels.QuickAccess.Items;
using System;
using Windows.UI.Xaml;
#nullable disable
namespace Izi.Travel.TemplateSelectors.QuickAccess
{
  public class QuickAccessTemplateSelector : DataTemplateSelector
  {
    public DataTemplate NoneTemplate { get; set; }

    public DataTemplate PlayerTemplate { get; set; }

    public DataTemplate InfoTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (item != null)
      {
        Type type = item.GetType();
        if (type == typeof (QuickAccessInfoItemViewModel))
          return this.InfoTemplate;
        if (type == typeof (QuickAccessPlayerItemViewModel))
          return this.PlayerTemplate;
      }
      return this.NoneTemplate;
    }
  }
}

