// ********************************************************************
// Type: Izi.Travel.Toolkit.Controls.Maps.MapOverlayItem
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


#nullable disable
namespace Izi.Travel.Toolkit.Controls.Maps
{
  internal class MapOverlayItem : ContentPresenter
  {
    public MapOverlayItem(object content, DataTemplate contentTemplate, MapOverlay mapOverlay)
    {
      this.ContentTemplate = contentTemplate;
      this.Content = content;
      this.MapOverlay = mapOverlay;
    }

    private MapOverlay MapOverlay { get; set; }

   /* public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      MapChild.BindMapOverlayProperties(this.MapOverlay);
    }
   */
  }
}

