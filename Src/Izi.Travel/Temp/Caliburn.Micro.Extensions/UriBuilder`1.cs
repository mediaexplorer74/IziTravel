// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.UriBuilder`1
// Assembly: Caliburn.Micro.Extensions, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: F2ADA3C9-2FAD-4D48-AC26-D2E113F06E6E
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extensions.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>
  /// Builds a Uri in a strongly typed fashion, based on a ViewModel.
  /// </summary>
  /// <typeparam name="TViewModel"></typeparam>
  public class UriBuilder<TViewModel>
  {
    private readonly Dictionary<string, string> queryString = new Dictionary<string, string>();
    private INavigationService navigationService;

    /// <summary>Adds a query string parameter to the Uri.</summary>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="property">The property.</param>
    /// <param name="value">The property value.</param>
    /// <returns>Itself</returns>
    public UriBuilder<TViewModel> WithParam<TValue>(
      Expression<Func<TViewModel, TValue>> property,
      TValue value)
    {
      if ((object) value is ValueType || !object.ReferenceEquals((object) null, (object) value))
        this.queryString[property.GetMemberInfo().Name] = value.ToString();
      return this;
    }

    /// <summary>Attaches a navigation servies to this builder.</summary>
    /// <param name="navigationService">The navigation service.</param>
    /// <returns>Itself</returns>
    public UriBuilder<TViewModel> AttachTo(INavigationService navigationService)
    {
      this.navigationService = navigationService;
      return this;
    }

    /// <summary>Navigates to the Uri represented by this builder.</summary>
    public void Navigate()
    {
      Uri source = this.BuildUri();
      if (this.navigationService == null)
        throw new InvalidOperationException("Cannot navigate without attaching an INavigationService. Call AttachTo first.");
      this.navigationService.Navigate(source);
    }

    /// <summary>Builds the URI.</summary>
    /// <returns>A uri constructed with the current configuration information.</returns>
    public Uri BuildUri()
    {
      return new Uri(ViewLocator.DeterminePackUriFromType(typeof (TViewModel), ViewLocator.LocateTypeForModelType(typeof (TViewModel), (DependencyObject) null, (object) null) ?? throw new InvalidOperationException(string.Format("No view was found for {0}. See the log for searched views.", (object) typeof (TViewModel).FullName))) + this.BuildQueryString(), UriKind.Relative);
    }

    private string BuildQueryString()
    {
      if (this.queryString.Count < 1)
        return string.Empty;
      string str = this.queryString.Aggregate<KeyValuePair<string, string>, string>("?", (Func<string, KeyValuePair<string, string>, string>) ((current, pair) => current + pair.Key + "=" + Uri.EscapeDataString(pair.Value) + "&"));
      return str.Remove(str.Length - 1);
    }
  }
}
