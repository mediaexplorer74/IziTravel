// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.TemplateSelectors.PlayerActionSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Shell.Core.Components;
using Izi.Travel.Shell.Mtg.ViewModels.Common.Player.Items;
using Izi.Travel.Shell.ViewModels.QuickAccess.Items;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.TemplateSelectors
{
  public class PlayerActionSelector : DataTemplateSelector
  {
    public DataTemplate Audio { get; set; }

    public DataTemplate Video { get; set; }

    public DataTemplate Info { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      MtgObject mtgObject = (MtgObject) null;
      if (item is PlayerItemViewModel playerItemViewModel1)
        mtgObject = playerItemViewModel1.MtgObject;
      if (item is QuickAccessPlayerItemViewModel playerItemViewModel2)
        mtgObject = playerItemViewModel2.MtgObject;
      if (mtgObject == null)
        return (DataTemplate) null;
      if (mtgObject.MainContent != null && mtgObject.MainContent.Audio != null)
        return this.Audio;
      return mtgObject.MainContent != null && mtgObject.MainContent.Video != null ? this.Video : this.Info;
    }
  }
}
