// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Core.Services.Implementation.DialogService
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services.Contract;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Utility;
using Microsoft.Devices;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Windows.Foundation;

#nullable disable
namespace Izi.Travel.Shell.Core.Services.Implementation
{
  public class DialogService : IDialogService
  {
    public void Show(
      string title,
      string message,
      MessageBoxButtonContent button,
      Action<FlyoutDialog, MessageBoxResult> callback)
    {
      this.Show(title, message, button, (Action<FlyoutDialog>) null, callback);
    }

    public void Show(
      string title,
      string message,
      MessageBoxButtonContent button,
      Action<FlyoutDialog> prepare,
      Action<FlyoutDialog, MessageBoxResult> callback)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      DialogService.\u003C\u003Ec__DisplayClass1_0 cDisplayClass10 = new DialogService.\u003C\u003Ec__DisplayClass1_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.callback = callback;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.button = button;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.dialog = new FlyoutDialog()
      {
        Title = !string.IsNullOrWhiteSpace(title) ? title.ToUpper() : (string) null,
        Message = message,
        LeftButtonContent = (object) AppResources.StringOk.ToUpper(),
        IsLeftButtonEnabled = true
      };
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass10.button == MessageBoxButtonContent.OkCancel)
      {
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10.dialog.RightButtonContent = (object) AppResources.CommandCancel;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass10.dialog.IsRightButtonEnabled = true;
      }
      else
      {
        // ISSUE: reference to a compiler-generated field
        if (cDisplayClass10.button == MessageBoxButtonContent.YesNo)
        {
          // ISSUE: reference to a compiler-generated field
          cDisplayClass10.dialog.LeftButtonContent = (object) AppResources.StringYes;
          // ISSUE: reference to a compiler-generated field
          cDisplayClass10.dialog.RightButtonContent = (object) AppResources.StringNo;
          // ISSUE: reference to a compiler-generated field
          cDisplayClass10.dialog.IsRightButtonEnabled = true;
        }
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method pointer
      cDisplayClass10.dialog.Closed += new TypedEventHandler<FlyoutDialog, FlyoutDialogResult>((object) cDisplayClass10, __methodptr(\u003CShow\u003Eb__0));
      if (prepare != null)
      {
        // ISSUE: reference to a compiler-generated field
        prepare(cDisplayClass10.dialog);
      }
      // ISSUE: reference to a compiler-generated field
      cDisplayClass10.dialog.Show();
    }

    public void ShowToast(
      string message,
      Uri backgroundNavigationUri,
      System.Action foregroundNavigationAction,
      bool showInBackground,
      string backgroundBrushName = "IziTravelBlueBrush",
      bool vibrateAndSound = false)
    {
      ((System.Action) (() =>
      {
        if (ApplicationManager.RunningInBackground & showInBackground)
        {
          new ShellToast()
          {
            Title = ManifestResources.ApplicationTitle,
            Content = message,
            NavigationUri = backgroundNavigationUri
          }.Show();
        }
        else
        {
          Popup popup = new Popup();
          TranslateTransform target1 = new TranslateTransform();
          StackPanel target2 = new StackPanel()
          {
            Background = Application.Current.Resources[(object) backgroundBrushName] as Brush,
            Width = Application.Current.Host.Content.ActualWidth,
            CacheMode = (CacheMode) new BitmapCache(),
            RenderTransform = (Transform) target1
          };
          if (foregroundNavigationAction != null)
            target2.Tap += (EventHandler<GestureEventArgs>) ((s, e) => foregroundNavigationAction());
          TextBlock textBlock = new TextBlock()
          {
            Foreground = Application.Current.Resources[(object) "IziTravelLightBrush"] as Brush,
            FontSize = (double) Application.Current.Resources[(object) "PhoneFontSizeSmall"],
            Margin = new Thickness(24.0, 35.0, 0.0, 4.0),
            Text = message,
            TextWrapping = TextWrapping.Wrap
          };
          target2.Children.Add((UIElement) textBlock);
          popup.Child = (UIElement) target2;
          popup.IsOpen = true;
          Storyboard storyboard = new Storyboard();
          DoubleAnimationUsingKeyFrames animationUsingKeyFrames1 = new DoubleAnimationUsingKeyFrames();
          DoubleKeyFrameCollection keyFrames1 = animationUsingKeyFrames1.KeyFrames;
          keyFrames1.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0)),
            Value = -Application.Current.Host.Content.ActualWidth
          });
          DoubleKeyFrameCollection keyFrames2 = animationUsingKeyFrames1.KeyFrames;
          keyFrames2.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.25)),
            Value = 0.0,
            EasingFunction = (IEasingFunction) new ExponentialEase()
          });
          DoubleKeyFrameCollection keyFrames3 = animationUsingKeyFrames1.KeyFrames;
          keyFrames3.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.0)),
            Value = 0.0
          });
          DoubleKeyFrameCollection keyFrames4 = animationUsingKeyFrames1.KeyFrames;
          keyFrames4.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.35)),
            Value = Application.Current.Host.Content.ActualWidth,
            EasingFunction = (IEasingFunction) new ExponentialEase()
          });
          storyboard.Add((DependencyObject) target1, "X", (Timeline) animationUsingKeyFrames1);
          DoubleAnimationUsingKeyFrames animationUsingKeyFrames2 = new DoubleAnimationUsingKeyFrames();
          DoubleKeyFrameCollection keyFrames5 = animationUsingKeyFrames2.KeyFrames;
          keyFrames5.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.0)),
            Value = 0.0
          });
          DoubleKeyFrameCollection keyFrames6 = animationUsingKeyFrames2.KeyFrames;
          keyFrames6.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.15)),
            Value = 1.0,
            EasingFunction = (IEasingFunction) new ExponentialEase()
          });
          DoubleKeyFrameCollection keyFrames7 = animationUsingKeyFrames2.KeyFrames;
          keyFrames7.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.0)),
            Value = 1.0
          });
          DoubleKeyFrameCollection keyFrames8 = animationUsingKeyFrames2.KeyFrames;
          keyFrames8.Add((DoubleKeyFrame) new EasingDoubleKeyFrame()
          {
            KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(3.35)),
            Value = 0.0,
            EasingFunction = (IEasingFunction) new ExponentialEase()
          });
          storyboard.Add((DependencyObject) target2, "Opacity", (Timeline) animationUsingKeyFrames2);
          storyboard.Completed += (EventHandler) ((s, e) => popup.IsOpen = false);
          storyboard.Begin();
          if (!vibrateAndSound)
            return;
          VibrateController.Default.Start(TimeSpan.FromMilliseconds(100.0));
          using (Stream stream = TitleContainer.OpenStream("Assets/Sounds/Alert.wav"))
          {
            SoundEffect soundEffect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            soundEffect.Play();
          }
        }
      })).OnUIThread();
    }
  }
}
