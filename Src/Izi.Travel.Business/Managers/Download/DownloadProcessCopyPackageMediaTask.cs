// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessCopyPackageMediaTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Download;
using Izi.Travel.Business.Services;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessCopyPackageMediaTask : DownloadProcessTaskBase
  {
    public override int Order => 4;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageMediaCopy;

    protected override double StepOverallProgress => 0.15;

    protected override Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      return Task<bool>.Factory.StartNew((Func<bool>) (() =>
      {
        try
        {
          string localDirectory = ServiceFacade.MediaService.GetLocalDirectory();
          string str1 = localDirectory;
          string path1 = Path.Combine(DownloadManager.Instance.GetDownloadPackagePath(process), localDirectory);
          using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
          {
            if (!storeForApplication.DirectoryExists(str1))
              storeForApplication.CreateDirectory(str1);
            string[] fileNames = storeForApplication.GetFileNames(Path.Combine(path1, "*.media"));
            if (fileNames.Length == 0)
              return true;
            double num = 0.15 / (double) fileNames.Length;
            foreach (string str2 in fileNames)
            {
              string sourceFileName = Path.Combine(path1, str2);
              string withoutExtension = Path.GetFileNameWithoutExtension(str2);
              if (string.IsNullOrWhiteSpace(withoutExtension))
              {
                DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCopyMedia);
                return false;
              }
              string destinationFileName = Path.Combine(str1, withoutExtension);
              storeForApplication.CopyFile(sourceFileName, destinationFileName, true);
              DownloadManager.Instance.SetDownloadProgress(process, process.Progress + num);
            }
          }
          return true;
        }
        catch (Exception ex)
        {
          this.Logger.Error(ex);
          DownloadManager.Instance.SetDownloadState(process, DownloadProcessState.Error, DownloadProcessError.PackageCopyMedia);
          return false;
        }
      }), token);
    }
  }
}
