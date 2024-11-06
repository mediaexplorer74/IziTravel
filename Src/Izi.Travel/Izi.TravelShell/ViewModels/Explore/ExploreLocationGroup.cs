// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.ViewModels.Explore.ExploreLocationGroup
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Shell.ViewModels.Explore
{
  public class ExploreLocationGroup : List<ExploreLocationItem>
  {
    public string Key { get; private set; }

    public ExploreLocationGroup(string key) => this.Key = key;
  }
}
