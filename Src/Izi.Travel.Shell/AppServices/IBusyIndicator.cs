// IziTravel.AppServices.IBusyIndicator

using System;

#nullable disable
namespace Izi.Travel.Shell.AppServices
{
  public interface IBusyIndicator
  {
    bool IsBusy { get; }

    IDisposable StartJob();

    void EndJob();
  }
}
