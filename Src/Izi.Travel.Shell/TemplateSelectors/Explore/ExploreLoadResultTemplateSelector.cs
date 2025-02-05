// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.TemplateSelectors.Explore.ExploreLoadResultTemplateSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Components;
using Izi.Travel.Shell.Model.Explore;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.TemplateSelectors.Explore
{
  public class ExploreLoadResultTemplateSelector : DataTemplateSelector
  {
    public DataTemplate NoneTemplate { get; set; }

    public DataTemplate EmptyTemplate { get; set; }

    public DataTemplate NoContentTemplate { get; set; }

    public DataTemplate NoContentOfflineTemplate { get; set; }

    public DataTemplate ErrorNetworkTemplate { get; set; }

    public DataTemplate ErrorUnknownTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      if (item is ExploreLoadResult exploreLoadResult)
      {
        switch (exploreLoadResult)
        {
          case ExploreLoadResult.None:
            return this.NoneTemplate;
          case ExploreLoadResult.Empty:
            return this.EmptyTemplate;
          case ExploreLoadResult.NoContent:
            return this.NoContentTemplate;
          case ExploreLoadResult.NoContentOffline:
            return this.NoContentOfflineTemplate;
          case ExploreLoadResult.ErrorNetwork:
            return this.ErrorNetworkTemplate;
          case ExploreLoadResult.ErrorUnknown:
            return this.ErrorUnknownTemplate;
        }
      }
      return base.SelectTemplate(item, container);
    }
  }
}
