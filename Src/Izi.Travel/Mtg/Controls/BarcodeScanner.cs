// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Mtg.Controls.BarcodeScanner
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Izi.Travel.Shell.Core.Controls;
//using Microsoft.Devices;
//using Microsoft.Phone.Controls;
using System;
using System.Windows;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;



//using System.Windows.Controls;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Threading;
using ZXing;

#nullable disable
namespace Izi.Travel.Shell.Mtg.Controls
{
  [TemplatePart(Name = "PartProgress", Type = typeof (ProgressOverlay))]
  [TemplatePart(Name = "PartGrid", Type = typeof (Grid))]
  public class BarcodeScanner : Control
  {
    private const string PartProgress = "PartProgress";
    private const string PartGrid = "PartGrid";
    private readonly Uri _externalAppUri = new Uri("app://external/");
    private /*PhoneApplicationFrame*/ Frame _frame;
    private Uri _frameUri;
    private ProgressOverlay _progress;
    private Grid _grid;
    private VideoBrush _videoBrush;
    private PhotoCamera _photoCamera;
    private IBarcodeReader _barcodeReader;
    private readonly DispatcherTimer _scanTimer;
    private WriteableBitmap _previewBuffer;
    private bool _isCameraInitialized;
    private bool _isCameraFocusing;

    public event EventHandler<BarcodeScannerEventArgs> BarcodeScanned;

    public BarcodeScanner()
    {
      //this.DefaultStyleKey = (object) typeof (BarcodeScanner);
      this._scanTimer = new DispatcherTimer()
      {
        Interval = TimeSpan.FromMilliseconds(2000.0)
      };
      this._scanTimer.Tick += (s, e) => this.OnScanTimerTick(s, EventArgs.Empty);
    }

    protected /*public*/ override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      
            //RnD
            //this._progress = (ProgressOverlay)(this.GetTemplateChild("PartProgress") as ProgressOverlay);
      this._grid = this.GetTemplateChild("PartGrid") as Grid;
      if (this._grid != null)
      {
        VideoBrush videoBrush = new VideoBrush();
        videoBrush.RelativeTransform = (Transform) new CompositeTransform()
        {
          CenterX = 0.5,
          CenterY = 0.5,
          Rotation = 90.0
        };
        videoBrush.Stretch = Stretch.UniformToFill;
        this._videoBrush = videoBrush;
        this._grid.Background = default;//(Brush) this._videoBrush;
      }
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.Unloaded += new RoutedEventHandler(this.OnUnloaded);
    }

    private void Initialize()
    {
      this._isCameraInitialized = false;
      this.SetBusyState(true);
      this._photoCamera = new PhotoCamera();
      this._photoCamera.Initialized += 
                (s, e) => this.OnPhotoCameraInitialized(s, EventArgs.Empty);

      if (this._videoBrush == null)
        return;
      CameraVideoBrushExtensions.SetSource(this._videoBrush, (Camera) this._photoCamera);
    }

    private void Deinitialize()
    {
      this._scanTimer.Stop();
      if (this._photoCamera != null)
      {
        if (this._isCameraInitialized)
          this._photoCamera.Dispose();

        this._photoCamera.Initialized -= 
                    (s, e) => this.OnPhotoCameraInitialized(s, EventArgs.Empty);

        this._photoCamera.AutoFocusCompleted -= 
                    (s, e) => this.OnPhotoCameraAutoFocusCompleted(s, 
                    CameraOperationCompletedEventArgs.Empty);

        this._photoCamera = (PhotoCamera) null;

        GC.Collect(GC.MaxGeneration);
      }
      this._isCameraFocusing = false;
    }

    private void OnInitialized()
    {
      if (this._photoCamera == null)
        return;

      this._photoCamera.FlashMode = CameraFlashMode.Auto;

      this._photoCamera.AutoFocusCompleted += new EventHandler<CameraOperationCompletedEventArgs>(
          this.OnPhotoCameraAutoFocusCompleted);

      this._previewBuffer = new WriteableBitmap(
          (int) this._photoCamera.PreviewResolution.Width, 
          (int) this._photoCamera.PreviewResolution.Height);

      BarcodeReader barcodeReader = new BarcodeReader();
      barcodeReader.Options.TryHarder = true;

      this._barcodeReader = (IBarcodeReader) barcodeReader;
      this._barcodeReader.ResultFound += new Action<Result>(this.OnBarcodeReaderResultFound);
      this._scanTimer.Start();
    }

    private void OnAutoFocusCompleted()
    {
      if (this._photoCamera == null || !this._isCameraInitialized)
        return;

      //RnD
      this._photoCamera.GetPreviewBufferArgb32(/*this._previewBuffer.Pixels*/default);
      this._previewBuffer.Invalidate();
      this._barcodeReader.Decode(this._previewBuffer);
    }

    private void OnNavigatingTo() => this.Initialize();

    private void OnNavigatingFrom() => this.Deinitialize();

    private void OnBarcodeScanned(BarcodeScannerEventArgs args)
    {
      EventHandler<BarcodeScannerEventArgs> barcodeScanned = this.BarcodeScanned;
      if (barcodeScanned == null)
        return;
      barcodeScanned((object) this, args);
    }

    private void SetBusyState(bool busy)
    {
      if (this._progress == null)
        return;
      this._progress.IsBusy = busy;
    }

    private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      this._frame = default;//Application.Current.RootVisual as PhoneApplicationFrame;
      if (this._frame != null)
      {
        //RnD
        //this._frameUri = this._frame.Source;
        this._frame.Navigating += new NavigatingCancelEventHandler(this.OnFrameNavigating);
      }
      this.Initialize();
    }

    private void OnUnloaded(object sender, RoutedEventArgs routedEventArgs)
    {
      if (this._frame != null)
        this._frame.Navigating -= new NavigatingCancelEventHandler(this.OnFrameNavigating);
      this.Deinitialize();
    }

    private void OnFrameNavigating(object sender, NavigatingCancelEventArgs e)
    {
      if (e.NavigationMode == NavigationMode.New)
        this.OnNavigatingFrom();
      else if (/*e.Uri == this._frameUri &&*/ e.NavigationMode == NavigationMode.Back)
      {
        this.OnNavigatingTo();
      }
      else
      {
        if (/*!e.IsNavigationInitiator ||*/ e.NavigationMode != NavigationMode.Back)
          return;
        this.OnNavigatingFrom();
      }
    }

    private void OnPhotoCameraInitialized(object sender, /*CameraOperationCompleted*/EventArgs e)
    {
      /*if (!e.Succeeded)
        return;
      this.Dispatcher.BeginInvoke((Action) (() =>
      {
        this.OnInitialized();
        this.SetBusyState(false);
        this._isCameraInitialized = true;
      }));*/
    }

    private void OnScanTimerTick(object sender, EventArgs eventArgs)
    {
      if (this._photoCamera == null || !this._isCameraInitialized || this._isCameraFocusing)
        return;
      this._isCameraFocusing = true;
      this._photoCamera.Focus();
    }

    private void OnPhotoCameraAutoFocusCompleted(object sender, CameraOperationCompletedEventArgs e)
    {
      this._isCameraFocusing = false;
      //this.Dispatcher.BeginInvoke(new Action(this.OnAutoFocusCompleted));
    }

    private void OnBarcodeReaderResultFound(Result result)
    {
      this.OnBarcodeScanned(new BarcodeScannerEventArgs(result.Text));
    }
  }
}
