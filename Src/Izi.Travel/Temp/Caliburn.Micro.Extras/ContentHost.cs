// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.Extras.ContentHost
// Assembly: Caliburn.Micro.Extras, Version=2.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75D6380B-EA35-437B-8CE3-40FC8C25A394
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Extras.xml

using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

#nullable disable
namespace Caliburn.Micro.Extras
{
  /// <summary>
  /// Custom <see cref="T:System.Windows.Controls.ContentControl" /> that caches all views so that it can quickly switch between them.
  /// </summary>
  /// <remarks>
  /// Models have to implement <see cref="T:Caliburn.Micro.IDeactivate" /> so views can be cached.
  /// </remarks>
  public class ContentHost : Grid
  {
    /// <summary>A dependency property for attaching a model to the UI.</summary>
    public static readonly DependencyProperty CurrentModelProperty = DependencyProperty.Register(nameof (CurrentModel), typeof (object), typeof (ContentHost), new PropertyMetadata((object) null, (PropertyChangedCallback) ((s, e) => ((ContentHost) s).OnCurrentModelChanged(e))));

    static ContentHost()
    {
      ConventionManager.AddElementConvention<ContentHost>(ContentHost.CurrentModelProperty, nameof (CurrentModel), "Loaded");
    }

    /// <summary>Gets or sets the current model.</summary>
    public object CurrentModel
    {
      get => this.GetValue(ContentHost.CurrentModelProperty);
      set => this.SetValue(ContentHost.CurrentModelProperty, value);
    }

    private void OnCurrentModelChanged(DependencyPropertyChangedEventArgs e)
    {
      UIElement view = this.Children.OfType<UIElement>().FirstOrDefault<UIElement>((Func<UIElement, bool>) (v => v.Visibility == Visibility.Visible));
      this.BringToFront(this.GetView(e.NewValue), e.NewValue);
      this.SendToBack(view, e.OldValue);
    }

    private UIElement GetView(object viewModel)
    {
      if (viewModel == null)
        return (UIElement) null;
      UIElement view1 = (UIElement) this.Children.OfType<FrameworkElement>().FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>) (fe => object.ReferenceEquals(fe.DataContext, viewModel)));
      if (view1 != null)
        return view1;
      object context = View.GetContext((DependencyObject) this);
      UIElement view2 = ViewLocator.LocateForModel(viewModel, (DependencyObject) this, context);
      ViewModelBinder.Bind(viewModel, (DependencyObject) view2, context);
      return view2;
    }

    private void BringToFront(UIElement view, object viewModel)
    {
      if (view == null)
        return;
      view.Visibility = Visibility.Visible;
      if (this.Children.Contains(view))
        return;
      this.SubscribeDeactivation(viewModel);
      this.Children.Add(view);
    }

    private void SendToBack(UIElement view, object viewModel)
    {
      if (view == null)
        return;
      view.Visibility = Visibility.Collapsed;
      if (viewModel is IDeactivate)
        return;
      this.Children.Remove(view);
    }

    private void SubscribeDeactivation(object viewModel)
    {
      if (!(viewModel is IDeactivate deactivate))
        return;
      deactivate.Deactivated += new EventHandler<DeactivationEventArgs>(this.OnViewModelDeactivated);
    }

    private void OnViewModelDeactivated(object sender, DeactivationEventArgs e)
    {
      if (!e.WasClosed)
        return;
      ((IDeactivate) sender).Deactivated -= new EventHandler<DeactivationEventArgs>(this.OnViewModelDeactivated);
      FrameworkElement frameworkElement = this.Children.OfType<FrameworkElement>().FirstOrDefault<FrameworkElement>((Func<FrameworkElement, bool>) (fe => object.ReferenceEquals(fe.DataContext, sender)));
      if (frameworkElement == null)
        return;
      this.Children.Remove((UIElement) frameworkElement);
    }
  }
}
