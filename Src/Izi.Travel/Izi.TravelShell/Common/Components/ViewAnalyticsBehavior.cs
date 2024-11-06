// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Common.Components.ViewAnalyticsBehavior
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Helper;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

#nullable disable
namespace Izi.Travel.Shell.Common.Components
{
  public class ViewAnalyticsBehavior : Behavior<Control>
  {
    private IActivate _viewModel;

    protected override void OnAttached()
    {
      base.OnAttached();
      this.AssociatedObject.Loaded += new RoutedEventHandler(this.OnViewLoaded);
      this.AssociatedObject.Unloaded += new RoutedEventHandler(this.OnViewUnloaded);
    }

    protected override void OnDetaching()
    {
      this.AssociatedObject.Unloaded -= new RoutedEventHandler(this.OnViewUnloaded);
      this.AssociatedObject.Loaded -= new RoutedEventHandler(this.OnViewLoaded);
      base.OnDetaching();
    }

    private void SendViewAnalytics()
    {
      AnalyticsHelper.SendView(this._viewModel != null ? this._viewModel.GetType().Name : this.AssociatedObject.GetType().Name);
    }

    private void OnViewLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      this._viewModel = this.AssociatedObject.DataContext as IActivate;
      if (this._viewModel == null)
        return;
      this._viewModel.Activated += new EventHandler<ActivationEventArgs>(this.OnViewModelActivated);
      if (!this._viewModel.IsActive)
        return;
      this.SendViewAnalytics();
    }

    private void OnViewUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      if (this._viewModel == null)
        return;
      this._viewModel.Activated -= new EventHandler<ActivationEventArgs>(this.OnViewModelActivated);
    }

    private void OnViewModelActivated(object sender, ActivationEventArgs activationEventArgs)
    {
      this.SendViewAnalytics();
    }
  }
}
