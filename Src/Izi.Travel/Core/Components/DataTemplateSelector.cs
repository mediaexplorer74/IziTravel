// ********************************************************************
// Type: Izi.Travel.Core.Components.DataTemplateSelector
// Assembly: Izi.Travel, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.dll
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace Izi.Travel.Core.Components
{
  public abstract class DataTemplateSelector : ContentControl
  {
    public virtual DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      return (DataTemplate) null;
    }

    protected override void OnContentChanged(object oldContent, object newContent)
    {
      base.OnContentChanged(oldContent, newContent);
      this.ContentTemplate = this.SelectTemplate(newContent, (DependencyObject) this);
    }
  }
}

