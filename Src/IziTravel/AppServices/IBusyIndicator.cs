// IziTravel.AppServices.IBusyIndicator

using System;

#nullable disable
namespace IziTravel.AppServices
{
  public interface IBusyIndicator
  {
    bool IsBusy { get; }

    IDisposable StartJob();

    void EndJob();
  }
}
