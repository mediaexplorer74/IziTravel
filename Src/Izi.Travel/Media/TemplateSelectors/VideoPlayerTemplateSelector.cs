// ********************************************************************
// Type: Izi.Travel.Media.TemplateSelectors.VideoPlayerTemplateSelector
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll

using Izi.Travel.Core.Components;
using Izi.Travel.Media.ViewModels.Video;
using Windows.UI.Xaml;
#nullable disable
namespace Izi.Travel.Media.TemplateSelectors
{
  public class VideoPlayerTemplateSelector : DataTemplateSelector
  {
    public DataTemplate InternalVideoTemplate { get; set; }

    public DataTemplate ExternalVideoTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      return !(item is VideoMediaPlayerItemViewModel playerItemViewModel) || !playerItemViewModel.IsExternal ? this.InternalVideoTemplate : this.ExternalVideoTemplate;
    }
  }
}

