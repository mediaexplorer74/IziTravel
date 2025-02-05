// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.TemplateSelectors.VideoPlayerTemplateSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Components;
using Izi.Travel.Shell.Media.ViewModels.Video;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Media.TemplateSelectors
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
