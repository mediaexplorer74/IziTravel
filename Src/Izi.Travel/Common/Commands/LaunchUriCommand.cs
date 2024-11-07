// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Commands.LaunchUriCommand
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Command;
using System;
using Windows.System;

#nullable disable
namespace Izi.Travel.Shell.Common.Commands
{
  public class LaunchUriCommand : BaseCommand
  {
    private readonly Uri _uri;

    public LaunchUriCommand(Uri uri) => this._uri = uri;

    public override bool CanExecute(object parameter) => true;

    public override async void Execute(object parameter)
    {
      int num = await Launcher.LaunchUriAsync(this._uri) ? 1 : 0;
    }
  }
}
