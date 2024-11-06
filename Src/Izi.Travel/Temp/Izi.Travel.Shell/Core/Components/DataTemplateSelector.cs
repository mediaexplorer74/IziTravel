// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Components.DataTemplateSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Izi.Travel.Shell.Core.Components
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
