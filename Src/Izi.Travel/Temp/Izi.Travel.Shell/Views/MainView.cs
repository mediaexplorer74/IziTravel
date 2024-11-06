// Decompiled with JetBrains decompiler
// Type: Izi.Travel.Shell.Views.MainView
// Assembly: Izi.Travel.Shell, Version=2.3.4.18, Culture=neutral, PublicKeyToken=null
// MVID: A80CFBDE-81BF-4633-8B4B-CE4786A327B5
// Assembly location: C:\Users\Admin\Desktop\RE\Izi.Travel\Izi.Travel.Shell.dll

using Caliburn.Micro;
using Izi.Travel.Business.Entities.TourPlayback;
using Izi.Travel.Business.Managers;
using Izi.Travel.Shell.Core.Controls.Flyout;
using Izi.Travel.Shell.Core.Resources;
using Izi.Travel.Shell.Core.Services;
using Izi.Travel.Shell.Core.Services.Entities;
using Izi.Travel.Shell.ViewModels;
using Izi.Travel.Utility;
using Microsoft.Phone.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

#nullable disable
namespace Izi.Travel.Shell.Views
{
  public class MainView : PhoneApplicationPage
  {
    internal Pivot PartPivot;
    private bool _contentLoaded;

    public MainView()
    {
      Counter.Construct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Views\\MainView.xaml.cs");
      this.InitializeComponent();
    }

    ~MainView()
    {
      Counter.Destruct("D:\\TeamCity\\buildAgent\\work\\961976af89b2d6d9\\src\\Izi.Travel.Shell\\Views\\MainView.xaml.cs");
    }

    private void Fill()
    {
      string path = Path.Combine("Content", "data");
      using (IsolatedStorageFile storeForApplication = IsolatedStorageFile.GetUserStoreForApplication())
      {
        string directoryName = Path.GetDirectoryName(path);
        if (directoryName != null && !storeForApplication.DirectoryExists(directoryName))
          storeForApplication.CreateDirectory(directoryName);
        if (storeForApplication.FileExists(path))
          return;
        using (IsolatedStorageFileStream file = storeForApplication.CreateFile(path))
          file.SetLength(storeForApplication.AvailableFreeSpace - 62914560L);
      }
    }

    protected override void OnBackKeyPress(CancelEventArgs e)
    {
      if (VisualTreeHelper.GetOpenPopups().Any<Popup>() || !(this.DataContext is MainViewModel dataContext))
        return;
      if (dataContext.ActiveItem != dataContext.ExploreViewModel)
      {
        MainViewModel mainViewModel = dataContext;
        mainViewModel.ActiveItem = (IScreen) mainViewModel.ExploreViewModel;
        e.Cancel = true;
      }
      else
      {
        if (TourPlaybackManager.Instance.TourPlaybackState != TourPlaybackState.Started)
          return;
        ShellServiceFacade.DialogService.Show(ManifestResources.ApplicationTitle, AppResources.PromptAppExitWhenTourStartedInfo, MessageBoxButtonContent.OkCancel, (Action<FlyoutDialog>) null, (Action<FlyoutDialog, MessageBoxResult>) ((d, x) =>
        {
          if (x != MessageBoxResult.OK)
            return;
          Application.Current.Terminate();
        }));
        e.Cancel = true;
      }
    }

    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("/Izi.Travel.Shell;component/Views/MainView.xaml", UriKind.Relative));
      this.PartPivot = (Pivot) this.FindName("PartPivot");
    }
  }
}
