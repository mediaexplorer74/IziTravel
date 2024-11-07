// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.ViewModels.Common.Detail.ParentObjectDetailInfoViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Shell.Mtg.ViewModels.Common.Detail
{
  public abstract class ParentObjectDetailInfoViewModel : DetailInfoViewModel
  {
    protected override string[] GetAppBarButtonKeys()
    {
      List<string> stringList = new List<string>();
      string[] appBarButtonKeys = base.GetAppBarButtonKeys();
      if (appBarButtonKeys != null)
        stringList.AddRange((IEnumerable<string>) appBarButtonKeys);
      if (this.MtgObject != null && this.MtgObject.Type != MtgObjectType.Collection)
        stringList.Add("Download");
      return stringList.ToArray();
    }
  }
}
