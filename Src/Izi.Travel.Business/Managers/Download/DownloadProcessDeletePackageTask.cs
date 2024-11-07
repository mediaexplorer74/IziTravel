// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessDeletePackageTask
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Izi.Travel.Business.Entities.Download;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal class DownloadProcessDeletePackageTask : DownloadProcessTaskBase
  {
    public override int Order => 6;

    protected override DownloadProcessStep Step => DownloadProcessStep.PackageDelete;

    protected override double StepOverallProgress => 0.05;

    protected override Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken))
    {
      return Task<bool>.Factory.StartNew((Func<bool>) (() => DownloadManager.Instance.DeletePackage(process)), token);
    }
  }
}
