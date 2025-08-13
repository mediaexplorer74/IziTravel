// IziTravel.AppServices.IBusyIndicator

using System;

#nullable disable
namespace Izi.Travel.AppServices
{
  public interface IBusyIndicator
  {
    bool IsBusy { get; }

    IDisposable StartJob();

    void EndJob();
  }
}
