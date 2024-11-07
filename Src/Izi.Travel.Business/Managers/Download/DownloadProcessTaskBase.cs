// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Business.Managers.Download.DownloadProcessTaskBase
// Assembly: Izi.Travel.Business, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: ABF4D74A-55A9-49E1-BE11-CC83659F98DD
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Business.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.Download;
using System;
using System.Threading;
using System.Threading.Tasks;

#nullable disable
namespace Izi.Travel.Business.Managers.Download
{
  internal abstract class DownloadProcessTaskBase
  {
    private readonly ILog _logger;

    public abstract int Order { get; }

    protected abstract DownloadProcessStep Step { get; }

    protected abstract double StepOverallProgress { get; }

    protected ILog Logger => this._logger;

    protected DownloadProcessTaskBase() => this._logger = LogManager.GetLog(this.GetType());

    public async Task<bool> ProcessAsync(DownloadProcess process, CancellationToken token = default (CancellationToken))
    {
      this.Logger.Info("Download process task '{0} started", (object) this.Step);
      try
      {
        double progress = process.Progress;
        if (!process.Steps.Contains(this.Step))
        {
          if (!await this.ProcessInternalAsync(process, token))
            return false;
          this.ConfirmDownloadProcessStep(process, this.Step);
        }
        DownloadManager.Instance.SetDownloadProgress(process, progress + this.StepOverallProgress);
        return true;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        return false;
      }
      finally
      {
        this.Logger.Info("Download process task '{0} completed", (object) this.Step);
      }
    }

    protected abstract Task<bool> ProcessInternalAsync(
      DownloadProcess process,
      CancellationToken token = default (CancellationToken));

    protected bool ConfirmDownloadProcessStep(
      DownloadProcess downloadProcess,
      DownloadProcessStep step)
    {
      if (downloadProcess == null)
        return false;
      if (!downloadProcess.Steps.Contains(step))
        downloadProcess.Steps.Add(step);
      try
      {
        DownloadManager.Instance.SaveDownloadProcess(downloadProcess);
        return true;
      }
      catch (Exception ex)
      {
        this.Logger.Error(ex);
        return false;
      }
    }
  }
}
