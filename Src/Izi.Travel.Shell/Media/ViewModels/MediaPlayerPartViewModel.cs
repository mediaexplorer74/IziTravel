// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Media.ViewModels.MediaPlayerPartViewModel
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Media;

#nullable disable
namespace Izi.Travel.Shell.Media.ViewModels
{
  public class MediaPlayerPartViewModel : Conductor<IScreen>
  {
    public MediaFormat MediaFormat { get; set; }

    protected override void OnInitialize()
    {
      base.OnInitialize();
      this.ActiveItem = (IScreen) IoC.Get<MediaPlayerViewModel>(this.MediaFormat.ToString());
    }
  }
}
