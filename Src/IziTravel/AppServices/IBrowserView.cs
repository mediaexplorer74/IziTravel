// IziTravel.AppServices.IBrowserView

using System;
using Windows.UI.Xaml.Navigation;
//using System.Windows.Navigation;

#nullable disable
namespace IziTravel.AppServices
{
  public interface IBrowserView
  {
    void LoadUri(string uri);

    void LoadContent(string htmlContent);

    event Action<NavigationEventArgs> NavigationCompleted;
  }
}
