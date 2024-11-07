// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Commands.ShareCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Data;
using Izi.Travel.Business.Helper;
using Izi.Travel.Shell.Core.Command;
using Izi.Travel.Shell.Core.Helpers;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Commands
{
  public class ShareCommand : BaseCommand
  {
    private readonly MtgObject _mtgObject;
    private readonly MtgObject _mtgObjectRoot;

    public ShareCommand(MtgObject mtgObject, MtgObject mtgObjectRoot)
    {
      this._mtgObject = mtgObject;
      this._mtgObjectRoot = mtgObjectRoot;
    }

    public override bool CanExecute(object parameter)
    {
      return this._mtgObject != null && this._mtgObject.MainContent != null && this._mtgObject.Type != MtgObjectType.StoryNavigation && this._mtgObject.Type != 0;
    }

    public override void Execute(object parameter)
    {
      if (this._mtgObject == null || this._mtgObject.MainContent == null)
        return;
      Uri uri = MtgLinkHelper.CreateUri(new MtgLinkInfo()
      {
        Uid = this._mtgObject.Uid,
        Language = this._mtgObject.MainContent.Language
      });
      if (uri == (Uri) null)
        return;
      AnalyticsHelper.SendShare(this._mtgObject);
      List<string> values = new List<string>();
      if (this._mtgObjectRoot != null)
        values.Add(this._mtgObjectRoot.Title);
      values.Add(this._mtgObject.Title);
      new ShareLinkTask()
      {
        Title = string.Join(" - ", (IEnumerable<string>) values),
        LinkUri = uri
      }.Show();
    }
  }
}
