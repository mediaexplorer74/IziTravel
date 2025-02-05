// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Helpers.DownloadHelper
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Shell.Core.Resources;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Helpers
{
  public static class DownloadHelper
  {
    public static string GetDownloadStateString(DownloadProcessState state)
    {
      switch (state)
      {
        case DownloadProcessState.Downloading:
          return AppResources.ActionDownloading;
        case DownloadProcessState.Downloaded:
          return AppResources.LabelDownloaded;
        case DownloadProcessState.Updating:
          return AppResources.ActionUpdating;
        case DownloadProcessState.Updated:
          return AppResources.LabelUpdated;
        case DownloadProcessState.Error:
          return AppResources.LabelError;
        default:
          return (string) null;
      }
    }
  }
}
