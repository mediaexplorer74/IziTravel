// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.NavigationExtensions
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>Extension methods related to navigation.</summary>
  public static class NavigationExtensions
  {
    /// <summary>Creates a Uri builder based on a view model type.</summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="navigationService">The navigation service.</param>
    /// <returns>The builder.</returns>
    public static UriBuilder<TViewModel> UriFor<TViewModel>(
      this INavigationService navigationService)
    {
      return new UriBuilder<TViewModel>().AttachTo(navigationService);
    }
  }
}
