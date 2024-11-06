// Decompiled with JetBrains decompiler
// Type: Caliburn.Micro.WindowManager
// Assembly: Caliburn.Micro.Platform, Version=2.0.1.0, Culture=neutral, PublicKeyToken=null
// MVID: 5D8D2AFD-482F-40D3-8F5B-6788C31BBFD5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.dll
// XML documentation location: C:\Users\Admin\Desktop\RE\Izi.Travel\Caliburn.Micro.Platform.xml

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

#nullable disable
namespace Caliburn.Micro
{
  /// <summary>A service that manages windows.</summary>
  public class WindowManager : IWindowManager
  {
    /// <summary>
    /// Predicate used to determine whether a page being navigated is actually a system dialog, which should
    /// cause a temporary dialog disappearance.
    /// </summary>
    /// <remarks>
    /// The default implementation just take into account DatePicker and TimePicker pages from WP7 toolkit.
    /// </remarks>
    /// 
    ///             /// <param name="uri">The destination page to check</param>
    public static Func<Uri, bool> IsSystemDialogNavigation = (Func<Uri, bool>) (uri => uri != (Uri) null && uri.ToString().StartsWith("/Microsoft.Phone.Controls.Toolkit"));

    /// <summary>Shows a modal dialog for the specified model.</summary>
    /// <param name="rootModel">The root model.</param>
    /// <param name="context">The context.</param>
    /// <param name="settings">The optional dialog settings.</param>
    public virtual void ShowDialog(
      object rootModel,
      object context = null,
      IDictionary<string, object> settings = null)
    {
      WindowManager.DialogHost target = new WindowManager.DialogHost(IoC.Get<INavigationService>());
      target.Content = ViewLocator.LocateForModel(rootModel, (DependencyObject) target, context);
      target.SetValue(View.IsGeneratedProperty, (object) true);
      ViewModelBinder.Bind(rootModel, (DependencyObject) target, (object) null);
      target.SetActionTarget(rootModel);
      WindowManager.ApplySettings((object) target, (IEnumerable<KeyValuePair<string, object>>) settings);
      if (rootModel is IActivate activate)
        activate.Activate();
      IDeactivate deactivator = rootModel as IDeactivate;
      if (deactivator != null)
        target.Closed += (EventHandler) ((param0, param1) => deactivator.Deactivate(true));
      target.Open();
    }

    /// <summary>Shows a popup at the current mouse position.</summary>
    /// <param name="rootModel">The root model.</param>
    /// <param name="context">The view context.</param>
    /// <param name="settings">The optional popup settings.</param>
    public virtual void ShowPopup(
      object rootModel,
      object context = null,
      IDictionary<string, object> settings = null)
    {
      Popup popup = this.CreatePopup(rootModel, settings);
      UIElement uiElement = ViewLocator.LocateForModel(rootModel, (DependencyObject) popup, context);
      popup.Child = uiElement;
      popup.SetValue(View.IsGeneratedProperty, (object) true);
      ViewModelBinder.Bind(rootModel, (DependencyObject) popup, (object) null);
      if (rootModel is IActivate activate)
        activate.Activate();
      IDeactivate deactivator = rootModel as IDeactivate;
      if (deactivator != null)
        popup.Closed += (EventHandler) ((param0, param1) => deactivator.Deactivate(true));
      popup.IsOpen = true;
    }

    /// <summary>Creates a popup for hosting a popup window.</summary>
    /// <param name="rootModel">The model.</param>
    /// <param name="settings">The optional popup settings.</param>
    /// <returns>The popup.</returns>
    protected virtual Popup CreatePopup(object rootModel, IDictionary<string, object> settings)
    {
      Popup target = new Popup();
      WindowManager.ApplySettings((object) target, (IEnumerable<KeyValuePair<string, object>>) settings);
      return target;
    }

    private static bool ApplySettings(
      object target,
      IEnumerable<KeyValuePair<string, object>> settings)
    {
      if (settings == null)
        return false;
      Type type = target.GetType();
      foreach (KeyValuePair<string, object> setting in settings)
        type.GetProperty(setting.Key)?.SetValue(target, setting.Value, (object[]) null);
      return true;
    }

    [ContentProperty("Content")]
    internal class DialogHost : FrameworkElement
    {
      private INavigationService navigationSvc;
      private PhoneApplicationPage currentPage;
      private Popup hostPopup;
      private bool isOpen;
      private ContentControl viewContainer;
      private Border pageFreezingLayer;
      private Border maskingLayer;
      private WindowManager.DialogHost.IElementPlacementAnimator elementPlacementAnimator;
      private Dictionary<IApplicationBarIconButton, bool> appBarButtonsStatus = new Dictionary<IApplicationBarIconButton, bool>();
      private bool appBarMenuEnabled;
      public EventHandler Closed = (EventHandler) ((param0, param1) => { });
      private Uri currentPageUri;

      public DialogHost(INavigationService navigationSvc)
      {
        this.navigationSvc = navigationSvc;
        this.currentPage = navigationSvc.CurrentContent as PhoneApplicationPage;
        if (this.currentPage == null)
          throw new InvalidOperationException(string.Format("In order to use ShowDialog the view currently loaded in the application frame ({0}) should inherit from PhoneApplicationPage or one of its descendents.", (object) navigationSvc.CurrentContent.GetType()));
        navigationSvc.Navigating += new NavigatingCancelEventHandler(this.OnNavigating);
        navigationSvc.Navigated += new NavigatedEventHandler(this.OnNavigated);
        this.CreateUIElements();
        this.elementPlacementAnimator = this.CreateElementsAnimator();
      }

      public void SetActionTarget(object target)
      {
        Action.SetTarget((DependencyObject) this.viewContainer, target);
      }

      public virtual UIElement Content
      {
        get => (UIElement) this.viewContainer.Content;
        set => this.viewContainer.Content = (object) value;
      }

      public void Open()
      {
        if (this.isOpen)
          return;
        this.isOpen = true;
        if (this.currentPage.ApplicationBar != null)
          this.DisableAppBar();
        this.ArrangePlacement();
        this.currentPage.BackKeyPress += new EventHandler<CancelEventArgs>(this.CurrentPageBackKeyPress);
        this.currentPage.OrientationChanged += new EventHandler<OrientationChangedEventArgs>(this.CurrentPageOrientationChanged);
        this.hostPopup.IsOpen = true;
      }

      public void Close() => this.Close(false);

      private void Close(bool reopenOnBackNavigation)
      {
        if (!this.isOpen)
          return;
        this.isOpen = false;
        this.elementPlacementAnimator.Exit((System.Action) (() => this.hostPopup.IsOpen = false));
        if (this.currentPage.ApplicationBar != null)
          this.RestoreAppBar();
        this.currentPage.BackKeyPress -= new EventHandler<CancelEventArgs>(this.CurrentPageBackKeyPress);
        this.currentPage.OrientationChanged -= new EventHandler<OrientationChangedEventArgs>(this.CurrentPageOrientationChanged);
        if (reopenOnBackNavigation)
          return;
        this.navigationSvc.Navigating -= new NavigatingCancelEventHandler(this.OnNavigating);
        this.navigationSvc.Navigated -= new NavigatedEventHandler(this.OnNavigated);
        this.Closed((object) this, EventArgs.Empty);
      }

      protected virtual WindowManager.DialogHost.IElementPlacementAnimator CreateElementsAnimator()
      {
        return (WindowManager.DialogHost.IElementPlacementAnimator) new WindowManager.DialogHost.DefaultElementPlacementAnimator((FrameworkElement) this.maskingLayer, (FrameworkElement) this.viewContainer);
      }

      protected virtual void CreateUIElements()
      {
        ContentControl contentControl = new ContentControl();
        contentControl.HorizontalContentAlignment = HorizontalAlignment.Stretch;
        contentControl.VerticalContentAlignment = VerticalAlignment.Top;
        this.viewContainer = contentControl;
        Border border1 = new Border();
        border1.Child = (UIElement) this.viewContainer;
        border1.Background = (Brush) new SolidColorBrush(Color.FromArgb((byte) 170, (byte) 0, (byte) 0, (byte) 0));
        border1.VerticalAlignment = VerticalAlignment.Top;
        border1.HorizontalAlignment = HorizontalAlignment.Left;
        this.maskingLayer = border1;
        Border border2 = new Border();
        border2.Background = (Brush) new SolidColorBrush(Colors.Transparent);
        border2.Width = Application.Current.Host.Content.ActualWidth;
        border2.Height = Application.Current.Host.Content.ActualHeight;
        this.pageFreezingLayer = border2;
        Canvas canvas = new Canvas();
        canvas.Children.Add((UIElement) this.pageFreezingLayer);
        canvas.Children.Add((UIElement) this.maskingLayer);
        this.hostPopup = new Popup()
        {
          Child = (UIElement) canvas
        };
      }

      private void DisableAppBar()
      {
        this.appBarMenuEnabled = this.currentPage.ApplicationBar.IsMenuEnabled;
        this.appBarButtonsStatus.Clear();
        this.currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>().Apply<IApplicationBarIconButton>((Action<IApplicationBarIconButton>) (b =>
        {
          this.appBarButtonsStatus.Add(b, b.IsEnabled);
          b.IsEnabled = false;
        }));
        this.currentPage.ApplicationBar.IsMenuEnabled = false;
      }

      private void RestoreAppBar()
      {
        this.currentPage.ApplicationBar.IsMenuEnabled = this.appBarMenuEnabled;
        this.currentPage.ApplicationBar.Buttons.Cast<IApplicationBarIconButton>().Apply<IApplicationBarIconButton>((Action<IApplicationBarIconButton>) (b =>
        {
          bool flag;
          if (!this.appBarButtonsStatus.TryGetValue(b, out flag))
            return;
          b.IsEnabled = flag;
        }));
      }

      private void ArrangePlacement()
      {
        this.maskingLayer.Dispatcher.BeginInvoke((System.Action) (() =>
        {
          WindowManager.DialogHost.ElementPlacement newPlacement = new WindowManager.DialogHost.ElementPlacement();
          WindowManager.DialogHost.ElementPlacement elementPlacement = newPlacement;
          if (!(this.currentPage.SafeTransformToVisual((UIElement) null) is Transform transform2))
            transform2 = (Transform) new TranslateTransform();
          elementPlacement.Transform = transform2;
          newPlacement.Orientation = this.currentPage.Orientation;
          newPlacement.Size = new Size(this.currentPage.ActualWidth, this.currentPage.ActualHeight);
          this.elementPlacementAnimator.AnimateTo(newPlacement);
        }));
      }

      private void OnNavigating(object sender, NavigatingCancelEventArgs e)
      {
        if (!WindowManager.IsSystemDialogNavigation(e.Uri))
          return;
        this.currentPageUri = this.navigationSvc.CurrentSource;
      }

      private void OnNavigated(object sender, NavigationEventArgs e)
      {
        if (WindowManager.IsSystemDialogNavigation(e.Uri))
          this.Close(this.currentPageUri != (Uri) null);
        else if (e.Uri.Equals((object) this.currentPageUri))
        {
          this.currentPageUri = (Uri) null;
          this.currentPage = (PhoneApplicationPage) this.navigationSvc.CurrentContent;
          this.Open();
        }
        else
          this.Close(false);
      }

      private void CurrentPageBackKeyPress(object sender, CancelEventArgs e)
      {
        e.Cancel = true;
        this.Close();
      }

      private void CurrentPageOrientationChanged(object sender, OrientationChangedEventArgs e)
      {
        this.ArrangePlacement();
      }

      public class ElementPlacement
      {
        public Transform Transform;
        public PageOrientation Orientation;
        public Size Size;

        public double AngleFromDefault
        {
          get
          {
            return (this.Orientation & PageOrientation.Landscape) == PageOrientation.None ? 0.0 : (this.Orientation == PageOrientation.LandscapeRight ? 90.0 : -90.0);
          }
        }
      }

      public interface IElementPlacementAnimator
      {
        void Enter(
          WindowManager.DialogHost.ElementPlacement initialPlacement);

        void AnimateTo(
          WindowManager.DialogHost.ElementPlacement newPlacement);

        void Exit(System.Action onCompleted);
      }

      public class DefaultElementPlacementAnimator : 
        WindowManager.DialogHost.IElementPlacementAnimator
      {
        private FrameworkElement maskingLayer;
        private FrameworkElement viewContainer;
        private Storyboard storyboard = new Storyboard();
        private WindowManager.DialogHost.ElementPlacement currentPlacement;

        public DefaultElementPlacementAnimator(
          FrameworkElement maskingLayer,
          FrameworkElement viewContainer)
        {
          this.maskingLayer = maskingLayer;
          this.viewContainer = viewContainer;
        }

        public void Enter(
          WindowManager.DialogHost.ElementPlacement initialPlacement)
        {
          this.currentPlacement = initialPlacement;
          this.maskingLayer.Width = this.currentPlacement.Size.Width;
          this.maskingLayer.Height = this.currentPlacement.Size.Height;
          this.maskingLayer.RenderTransform = this.currentPlacement.Transform;
          PlaneProjection target = new PlaneProjection()
          {
            CenterOfRotationY = 0.1
          };
          this.viewContainer.Projection = (Projection) target;
          this.AddDoubleAnimation((DependencyObject) target, "RotationX", -90.0, 0.0, 400);
          this.AddDoubleAnimation((DependencyObject) this.maskingLayer, "Opacity", 0.0, 1.0, 400);
          this.storyboard.Begin();
        }

        public void AnimateTo(
          WindowManager.DialogHost.ElementPlacement newPlacement)
        {
          this.storyboard.Stop();
          this.storyboard.Children.Clear();
          if (this.currentPlacement == null)
          {
            this.Enter(newPlacement);
          }
          else
          {
            this.AddDoubleAnimation((DependencyObject) this.maskingLayer, "Width", this.currentPlacement.Size.Width, newPlacement.Size.Width, 200);
            this.AddDoubleAnimation((DependencyObject) this.maskingLayer, "Height", this.currentPlacement.Size.Height, newPlacement.Size.Height, 200);
            TransformGroup transformGroup = new TransformGroup();
            RotateTransform target = new RotateTransform()
            {
              CenterX = Application.Current.Host.Content.ActualWidth / 2.0,
              CenterY = Application.Current.Host.Content.ActualHeight / 2.0
            };
            transformGroup.Children.Add(newPlacement.Transform);
            transformGroup.Children.Add((Transform) target);
            this.maskingLayer.RenderTransform = (Transform) transformGroup;
            this.AddDoubleAnimation((DependencyObject) target, "Angle", newPlacement.AngleFromDefault - this.currentPlacement.AngleFromDefault, 0.0);
            this.AddFading(this.maskingLayer);
            this.currentPlacement = newPlacement;
            this.storyboard.Begin();
          }
        }

        public void Exit(System.Action onCompleted)
        {
          this.storyboard.Stop();
          this.storyboard.Children.Clear();
          PlaneProjection target = new PlaneProjection()
          {
            CenterOfRotationY = 0.1
          };
          this.viewContainer.Projection = (Projection) target;
          this.AddDoubleAnimation((DependencyObject) target, "RotationX", 0.0, 90.0, 250);
          this.AddDoubleAnimation((DependencyObject) this.maskingLayer, "Opacity", 1.0, 0.0, 350);
          EventHandler handler = (EventHandler) null;
          handler = (EventHandler) ((o, e) =>
          {
            this.storyboard.Completed -= handler;
            onCompleted();
            this.currentPlacement = (WindowManager.DialogHost.ElementPlacement) null;
          });
          this.storyboard.Completed += handler;
          this.storyboard.Begin();
        }

        private void AddDoubleAnimation(
          DependencyObject target,
          string property,
          double from,
          double to,
          int ms = 500)
        {
          DoubleAnimation doubleAnimation1 = new DoubleAnimation();
          doubleAnimation1.From = new double?(from);
          doubleAnimation1.To = new double?(to);
          DoubleAnimation doubleAnimation2 = doubleAnimation1;
          ExponentialEase exponentialEase1 = new ExponentialEase();
          exponentialEase1.EasingMode = EasingMode.EaseOut;
          exponentialEase1.Exponent = 4.0;
          ExponentialEase exponentialEase2 = exponentialEase1;
          doubleAnimation2.EasingFunction = (IEasingFunction) exponentialEase2;
          doubleAnimation1.Duration = new Duration(TimeSpan.FromMilliseconds((double) ms));
          DoubleAnimation element = doubleAnimation1;
          Storyboard.SetTarget((Timeline) element, target);
          Storyboard.SetTargetProperty((Timeline) element, new PropertyPath(property, new object[0]));
          this.storyboard.Children.Add((Timeline) element);
        }

        private void AddFading(FrameworkElement target)
        {
          DoubleAnimationUsingKeyFrames animationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
          animationUsingKeyFrames.Duration = new Duration(TimeSpan.FromMilliseconds(500.0));
          DoubleAnimationUsingKeyFrames element = animationUsingKeyFrames;
          DoubleKeyFrameCollection keyFrames1 = element.KeyFrames;
          LinearDoubleKeyFrame linearDoubleKeyFrame1 = new LinearDoubleKeyFrame();
          linearDoubleKeyFrame1.Value = 1.0;
          linearDoubleKeyFrame1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0.0));
          LinearDoubleKeyFrame linearDoubleKeyFrame2 = linearDoubleKeyFrame1;
          keyFrames1.Add((DoubleKeyFrame) linearDoubleKeyFrame2);
          DoubleKeyFrameCollection keyFrames2 = element.KeyFrames;
          LinearDoubleKeyFrame linearDoubleKeyFrame3 = new LinearDoubleKeyFrame();
          linearDoubleKeyFrame3.Value = 0.5;
          linearDoubleKeyFrame3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(150.0));
          LinearDoubleKeyFrame linearDoubleKeyFrame4 = linearDoubleKeyFrame3;
          keyFrames2.Add((DoubleKeyFrame) linearDoubleKeyFrame4);
          DoubleKeyFrameCollection keyFrames3 = element.KeyFrames;
          LinearDoubleKeyFrame linearDoubleKeyFrame5 = new LinearDoubleKeyFrame();
          linearDoubleKeyFrame5.Value = 1.0;
          linearDoubleKeyFrame5.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300.0));
          LinearDoubleKeyFrame linearDoubleKeyFrame6 = linearDoubleKeyFrame5;
          keyFrames3.Add((DoubleKeyFrame) linearDoubleKeyFrame6);
          Storyboard.SetTarget((Timeline) element, (DependencyObject) target);
          Storyboard.SetTargetProperty((Timeline) element, new PropertyPath("Opacity", new object[0]));
          this.storyboard.Children.Add((Timeline) element);
        }
      }
    }
  }
}
