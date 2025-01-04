// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Settings.Model.LicenseTemplateSelector
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Components;
using System.Windows;

#nullable disable
namespace Izi.Travel.Shell.Settings.Model
{
  public class LicenseTemplateSelector : DataTemplateSelector
  {
    public DataTemplate HeaderTemplate { get; set; }

    public DataTemplate PackageTemplate { get; set; }

    public DataTemplate LicenseTemplate { get; set; }

    public DataTemplate LicenseContentTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      switch (item)
      {
        case Header _:
          return this.HeaderTemplate;
        case Package _:
          return this.PackageTemplate;
        case License _:
          return this.LicenseTemplate;
        case string _:
          return this.LicenseContentTemplate;
        default:
          return (DataTemplate) null;
      }
    }
  }
}
