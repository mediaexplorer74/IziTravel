// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.TemplateSelectors.PlayerItemTemplateSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Components;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items;
using System;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Mtg.TemplateSelectors
{
  public class PlayerItemTemplateSelector : DataTemplateSelector
  {
    public DataTemplate StartItemTemplate { get; set; }

    public DataTemplate RegularItemTemplate { get; set; }

    public DataTemplate EndItemTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (item != null)
      {
        Type type = item.GetType();
        if (type == typeof (PlayerStartItemViewModel))
          return this.StartItemTemplate;
        if (type == typeof (PlayerRegularItemViewModel))
          return this.RegularItemTemplate;
        if (type == typeof (PlayerEndItemViewModel))
          return this.EndItemTemplate;
      }
      return (DataTemplate) null;
    }
  }
}
